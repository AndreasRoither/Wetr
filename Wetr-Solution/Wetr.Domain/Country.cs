using System.Collections.Generic;

namespace Wetr.Domain
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{CountryId}] {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Country temp = (Country)obj;
                return ((this.CountryId == temp.CountryId) && (this.Name == temp.Name));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -1769064886;
            hashCode = hashCode * -1521134295 + CountryId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}