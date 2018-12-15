using System.Collections.Generic;

namespace Wetr.Domain
{
    public class Address
    {
        public int AddressId { get; set; }
        public int CommunityId { get; set; }
        public string Location { get; set; }
        public string Zip { get; set; }

        public override string ToString() =>
            $"[{AddressId}] {CommunityId} {Zip} {Location}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Address temp = (Address)obj;
                return ((this.AddressId == temp.AddressId) && (this.CommunityId == temp.CommunityId) && (this.Location == temp.Location) && (this.Zip == temp.Zip));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -699461029;
            hashCode = hashCode * -1521134295 + AddressId.GetHashCode();
            hashCode = hashCode * -1521134295 + CommunityId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Location);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Zip);
            return hashCode;
        }
    }
}