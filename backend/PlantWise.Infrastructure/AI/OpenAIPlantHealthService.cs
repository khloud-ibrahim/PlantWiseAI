using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PlantWise.Application.Features.Plants.DTOs;
using PlantWise.Application.Features.Plants.Interfaces;
using PlantWise.Shared.Settings;
using System.Net.Http;
namespace PlantWise.Infrastructure.AI;

public class OpenAIPlantHealthService : IPlantHealthService
{
    private readonly OpenAISettings _settings;
private readonly HttpClient _httpClient;
public OpenAIPlantHealthService(
    IOptions<OpenAISettings> options)
{
    _settings = options.Value;
    _httpClient = new HttpClient();
}

    public async Task<PlantHealthAnalysisResponse> AnalyzeAsync(
        Stream imageStream,
        string fileName,
        string contentType,
        string? plantName = null)
    {
        if (string.IsNullOrWhiteSpace(_settings.ApiKey))
            throw new InvalidOperationException(
                "OpenAI API key is not configured.");

        using var memoryStream = new MemoryStream();

        await imageStream.CopyToAsync(memoryStream);

        var imageBytes = memoryStream.ToArray();
        if (imageBytes.Length == 0)
{
    throw new InvalidOperationException("Uploaded image is empty.");
}
Console.WriteLine($"File: {fileName}");
Console.WriteLine($"Content-Type: {contentType}");
Console.WriteLine($"Size: {imageBytes.Length} bytes");
Console.WriteLine(
    $"Header: {BitConverter.ToString(imageBytes.Take(12).ToArray())}");
        var base64Image = Convert.ToBase64String(imageBytes);
var prompt = $"""
Analyze this plant image carefully.

{(string.IsNullOrWhiteSpace(plantName)
    ? ""
    : $"Plant name: {plantName}")}

Return ONLY valid JSON.

The JSON must contain these fields:
plantName
healthStatus
diseaseOrProblem
confidence
symptoms
treatment
careTips

Rules:
- confidence must be between 0 and 1.
- If you cannot confidently identify a disease, say "Unknown or unclear".
- Do not invent symptoms that are not visible.
- Provide practical treatment and care recommendations.
- Return JSON only.
""";

        var requestBody = new
        {
            model = _settings.Model,

            input = new object[]
            {
                new
                {
                    role = "user",

                    content = new object[]
                    {
                        new
                        {
                            type = "input_text",
                            text = prompt
                        },
                        new
                        {
                            type = "input_image",
                            image_url =
                                $"data:{contentType};base64,{base64Image}"
                        }
                    }
                }
            }
        };

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.openai.com/v1/responses");

        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                _settings.ApiKey);

        request.Content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json");

        using var response =
            await _httpClient.SendAsync(request);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"OpenAI API error: {responseContent}");
        }

        var json = JsonDocument.Parse(responseContent);

        var outputText = ExtractOutputText(json);

        if (string.IsNullOrWhiteSpace(outputText))
        {
            throw new InvalidOperationException(
                "OpenAI returned an empty response.");
        }

        outputText = CleanJson(outputText);

        var result =
            JsonSerializer.Deserialize<PlantHealthAnalysisResponse>(
                outputText,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

        if (result is null)
        {
            throw new InvalidOperationException(
                "Could not parse OpenAI response.");
        }

        return result;
    }

    private static string ExtractOutputText(JsonDocument json)
    {
        if (!json.RootElement.TryGetProperty(
                "output",
                out var output))
        {
            return string.Empty;
        }

        foreach (var item in output.EnumerateArray())
        {
            if (!item.TryGetProperty(
                    "content",
                    out var content))
            {
                continue;
            }

            foreach (var contentItem in content.EnumerateArray())
            {
                if (contentItem.TryGetProperty(
                        "text",
                        out var text))
                {
                    return text.GetString() ?? string.Empty;
                }
            }
        }

        return string.Empty;
    }

    private static string CleanJson(string text)
    {
        text = text.Trim();

        if (text.StartsWith("```json"))
            text = text[7..];

        if (text.StartsWith("```"))
            text = text[3..];

        if (text.EndsWith("```"))
            text = text[..^3];

        return text.Trim();
    }
}