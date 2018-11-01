namespace Wetr.Domain
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{UnitId}] {Name}";
    }
}