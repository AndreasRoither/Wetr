using System.Collections.Generic;

namespace Wetr.Domain
{
    public class Community
    {
        public int CommunityId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{CommunityId}] {DistrictId} {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Community temp = (Community)obj;
                return ((this.CommunityId == temp.CommunityId) && (this.DistrictId == temp.DistrictId) && (this.Name == temp.Name));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 944434587;
            hashCode = hashCode * -1521134295 + CommunityId.GetHashCode();
            hashCode = hashCode * -1521134295 + DistrictId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}