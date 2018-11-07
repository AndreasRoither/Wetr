using System;

namespace Wetr.Domain
{
    public class Unit : IEquatable<Unit>
    {
        public int UnitId { get; set; }
        public string Name { get; set; }

        public bool Equals(Unit other)
        {
            return UnitId == other.UnitId && Name == other.Name;
        }

        public override string ToString() =>
            $"[{UnitId}] {Name}";
    }
}