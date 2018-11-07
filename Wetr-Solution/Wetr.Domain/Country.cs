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
    }
}