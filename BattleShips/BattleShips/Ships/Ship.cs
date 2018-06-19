using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    public class Ship
    {
        public List<ShipPart> Parts { get; }
        public bool Destroyed { get; private set; } = false;
        private Player player;

        public Ship(List<ShipPart> parts, Player player)
        {
            this.player = player;
            Parts = parts;
        }

        public void TakeAHit(ShipPart part)
        {
            ShipPart shipPart;
            if ((shipPart = Parts.FirstOrDefault(p => part.Equals(p))) != null)
            {
                if (shipPart.Destryed == false)
                {
                    shipPart.Destryed = true;
                    CheckIfDestroyed();
                    if (Destroyed)
                        ShipDestroyed();
                }
            }
        }

        private void CheckIfDestroyed()
        {
            if (Parts.Exists(part => part.Destryed == false))
                Destroyed = false;
            else
                Destroyed = true;
        }

        private void ShipDestroyed()
        {
            foreach(var part in Parts)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        int x = part.X - 1 + i;
                        int y = part.Y - 1 + j;
                        if (x >= 0 && x <= 9 && y >= 0 && y <= 9 && player.Board[x, y] != TileStatus.Hit)
                            player.Board[x, y] = TileStatus.Miss;
                    }
            }
        }
    }
}
