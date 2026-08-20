namespace PlantWise.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public string Token { get; set; } = string.Empty;

    public DateTime ExpiryDate { get; set; }
     public bool IsRevoked { get; set; }
}