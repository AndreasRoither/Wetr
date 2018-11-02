using System;
using System.Collections.Generic;

namespace Wetr.Domain
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public override string ToString() =>
            $"[{UserId}] {FirstName} {LastName} {Password} {Email}";
    }
}