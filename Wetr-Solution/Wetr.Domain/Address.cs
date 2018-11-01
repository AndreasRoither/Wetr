namespace Wetr.Domain
{
    public class Address
    {
        public int AddressId { get; set; }
        public int CommunityId { get; set; }
        public string Location { get; set; }
        public string Zip { get; set; }

        public override string ToString() =>
            $"[{AddressId}] {CommunityId} {Zip} {Location}";
    }
}