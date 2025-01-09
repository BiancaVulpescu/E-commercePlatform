namespace Domain.Entities
{
    public class LoginResponse
    {
        public Guid RefreshTokenId { get; set; }
        public required string RefreshToken { get; set; }
        public required string AccessToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
