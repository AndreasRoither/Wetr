namespace Wetr.Domain
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString() =>
            $"[{PermissionId}] {Name} {Description}";
    }
}