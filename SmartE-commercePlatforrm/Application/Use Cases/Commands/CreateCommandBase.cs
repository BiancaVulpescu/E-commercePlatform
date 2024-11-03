﻿using MediatR;

namespace Application.Use_Cases.Commands
{
    public class CreateCommandBase
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
    }
}
