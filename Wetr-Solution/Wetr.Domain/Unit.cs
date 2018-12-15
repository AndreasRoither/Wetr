using System;
using System.Collections.Generic;

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

        public override int GetHashCode()
        {
            var hashCode = 1404724320;
            hashCode = hashCode * -1521134295 + UnitId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            return hashCode;
        }
    }
}