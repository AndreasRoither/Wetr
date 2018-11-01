namespace Wetr.Domain
{
    public class District
    {
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{DistrictId}] {ProvinceId} {Name}";
    }
}