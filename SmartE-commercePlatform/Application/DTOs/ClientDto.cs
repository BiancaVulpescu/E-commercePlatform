namespace Application.DTOs
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; } //trebuie sa schimbam apoi cu un hash cand implementam asta
    }
}
