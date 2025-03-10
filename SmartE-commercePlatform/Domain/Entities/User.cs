﻿namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public Guid CartsId { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = [];
    }
}
