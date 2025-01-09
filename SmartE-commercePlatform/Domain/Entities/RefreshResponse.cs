namespace Domain.Entities
{
    public class RefreshResponse
    {
        public required string AccessToken { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
