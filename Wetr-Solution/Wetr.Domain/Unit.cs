using System;

namespace Wetr.Domain
{
    public class Unit
    {
        public int UnitId { get; set; }
        public string Name { get; set; }

        public override string ToString() =>
            $"[{UnitId}] {Name}";

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Unit temp = (Unit)obj;
                return ((this.UnitId == temp.UnitId) && (this.Name == temp.Name));
            }
        }
    }
}