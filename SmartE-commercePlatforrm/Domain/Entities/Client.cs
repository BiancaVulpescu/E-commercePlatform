using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } //trebuie sa schimbam apoi cu un hash cand implementam asta
        public string Location { get; set; }
    }
}
