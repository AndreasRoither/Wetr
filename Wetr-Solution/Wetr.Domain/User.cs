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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                User temp = (User)obj;
                return ((this.UserId == temp.UserId) && (this.FirstName == temp.FirstName)
                    && (this.LastName == temp.LastName) && (this.Password == temp.Password)
                    && (this.Email == temp.Email));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -1698601670;
            hashCode = hashCode * -1521134295 + UserId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FirstName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            return hashCode;
        }
    }
}