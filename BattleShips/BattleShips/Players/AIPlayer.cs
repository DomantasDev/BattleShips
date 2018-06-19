using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BattleShips
{
    class AIPlayer : Player
    {
        public AIPlayer(string name) : base(name) { }
     
        public override void SetUpBoard()
        {
            int x1, y1, x2, y2;
            for (int i = 4; i > 0; i--)
            {
                for (int j = i; j <= 4; j++)
                {
                    do
                    {                       
                        x1 = rand.Next(10);
                        y1 = rand.Next(10);
                        if (RandomBool())
                        {
                            x2 = x1;
                            y2 = SetAsRandom(y1, i);
                        }
                        else
                        {
                            y2 = y1;
                            x2 = SetAsRandom(x1, i);
                        }
                    } while (!PlaceShip(x1, y1, x2, y2, i));
                }              
            }
        }

        private int SetAsRandom(int x, int size)
        {
            size--;
            if (x - size < 0)
                return x + size;
            else if (x + size > 9)
                return x - size;
            else if (x + size <= 9 && RandomBool())
                return x + size;
            else
                return x - size;
        }

        private Random rand = new Random();
        private Boolean RandomBool()
        {
            return rand.Next(2) == 0;
        }

        public override bool Shoot()
        {
            int x = rand.Next(9) + 1;
            int y = rand.Next(9) + 1;
            switch (Enemy.Board[x, y])
            {
                case TileStatus.Ship:
                    MarkDiagonally(x, y);
                    Enemy.Ships.ForEach(ship => ship.TakeAHit(new ShipPart(x, y)));
                    return true;
                case TileStatus.Empty:
                    Enemy.Board[x, y] = TileStatus.Miss;
                    return false;
                default:
                    return true;
            }
        }

        public override void PrintBothBoards() { }
    }
}
