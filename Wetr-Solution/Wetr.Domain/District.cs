using System.Collections.Generic;

namespace Wetr.Domain
{
    public class District
    {
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{DistrictId}] {ProvinceId} {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                District temp = (District)obj;
                return ((this.DistrictId == temp.DistrictId) && (this.ProvinceId == temp.ProvinceId) && (this.Name == temp.Name));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1213410472;
            hashCode = hashCode * -1521134295 + DistrictId.GetHashCode();
            hashCode = hashCode * -1521134295 + ProvinceId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}