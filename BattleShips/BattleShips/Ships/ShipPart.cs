using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    public class ShipPart : IEquatable<ShipPart>
    {
        public int X { get;}
        public int Y { get;}
        public bool Destryed { get; set; } = false;

        public ShipPart(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(ShipPart other)
        {                   
            if (this.X == other.X && this.Y == other.Y)
                return true;
            return false;
      
        }
    }
}
