namespace Wetr.Domain
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{ProvinceId}] {CountryId} {Name}";
    }
}