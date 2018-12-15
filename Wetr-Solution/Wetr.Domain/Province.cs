using System.Collections.Generic;

namespace Wetr.Domain
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{ProvinceId}] {CountryId} {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Province temp = (Province)obj;
                return ((this.ProvinceId == temp.ProvinceId) && (this.Name == temp.Name) && (this.CountryId == temp.CountryId));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -1615181142;
            hashCode = hashCode * -1521134295 + ProvinceId.GetHashCode();
            hashCode = hashCode * -1521134295 + CountryId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}