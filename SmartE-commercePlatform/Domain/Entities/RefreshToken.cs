namespace Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string SecretHash { get; set; }
        public DateTimeOffset ExpiresAt { get; set; }

    }
}
