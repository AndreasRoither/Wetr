using System;

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
    }
}