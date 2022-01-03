using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;

namespace DomainLayer
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime CreationDate { get; set; }
        public ICollection<UserStocks> UserStocks { get; set; }
    }
}