namespace Application.Use_Cases.Authentication.DTOs
{
    public class LoginResponseDto
    {
        public Guid RefreshTokenId { get; set; }
        public required string RefreshToken { get; set; }
        public required string AccessToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
