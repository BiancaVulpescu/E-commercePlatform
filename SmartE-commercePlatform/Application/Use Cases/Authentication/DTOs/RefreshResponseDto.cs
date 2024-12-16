namespace Application.Use_Cases.Authentication.DTOs
{
    public class RefreshResponseDto
    {
        public required string AccessToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
