using System.Collections.Generic;

namespace Wetr.Domain
{
    public class StationType
    {
        public int StationTypeId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{StationTypeId}] {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                StationType temp = (StationType)obj;
                return ((this.StationTypeId == temp.StationTypeId) && (this.Name == temp.Name));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = -189422660;
            hashCode = hashCode * -1521134295 + StationTypeId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}