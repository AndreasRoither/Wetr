using System.Collections.Generic;

namespace Wetr.Domain
{
    public class MeasurementType
    {
        public int MeasurementTypeId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{MeasurementTypeId}] {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                MeasurementType temp = (MeasurementType)obj;
                return ((this.MeasurementTypeId == temp.MeasurementTypeId) && (this.Name == temp.Name));
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 2069641964;
            hashCode = hashCode * -1521134295 + MeasurementTypeId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}