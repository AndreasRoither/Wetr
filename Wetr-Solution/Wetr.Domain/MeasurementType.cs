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
    }
}