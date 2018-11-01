namespace Wetr.Domain
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{CountryId}] {Name}";
    }
}