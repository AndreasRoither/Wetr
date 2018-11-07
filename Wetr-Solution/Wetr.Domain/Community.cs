namespace Wetr.Domain
{
    public class Community
    {
        public int CommunityId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{CommunityId}] {DistrictId} {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Community temp = (Community)obj;
                return ((this.CommunityId == temp.CommunityId) && (this.DistrictId == temp.DistrictId) && (this.Name == temp.Name));
            }
        }
    }
}