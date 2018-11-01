namespace Wetr.Domain
{
    public class Community
    {
        public int CommunityId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{CommunityId}] {DistrictId} {Name}";
    }
}