using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace BattleShips
{
    public abstract class Player
    {
        public TileStatus[,] Board { get; protected set; } = new TileStatus[10, 10];
        public List<Ship> Ships { get; set; } = new List<Ship>();
        public Player Enemy { get; set; }
        public string Name { get; }

        abstract public void SetUpBoard();
        abstract public Boolean Shoot();
        abstract public void PrintBothBoards();
       
        public Player(string name)
        {
            this.Name = name;
        }

        public Boolean PlaceShip(int x1, int y1, int x2, int y2, int size)
        {
            if (x1 > 10 || y1 > 10 || x2 > 10 || y2 > 10 || x1 < 0 || y1 < 0 || x2 < 0 || y2 < 0 || Abs(x1 - x2) + Abs(y1 - y2) + 1 != size || !IsFreeSpace(x1, y1, x2, y2))
                return false;
            List<ShipPart> parts = new List<ShipPart>();
            for (int i = 0; i <= Abs(x1 - x2); i++)
                for (int j = 0; j <= Abs(y1 - y2); j++)
                {
                    int x = Min(x1, x2) + i;
                    int y = Min(y1, y2) + j;
                    parts.Add(new ShipPart(x, y));
                    Board[x, y] = TileStatus.Ship;
                }
            Ships.Add(new Ship(parts, this));
            return true;
        }

        protected Boolean IsFreeSpace(int x1, int y1, int x2, int y2)
        {
            if (x1 == x2)
            {
                int minY = Min(y1, y2);
                minY = minY == 0 ? minY : minY - 1;
                int maxY = Max(y1, y2);
                maxY = maxY == 9 ? maxY : maxY + 1;
                for (int i = minY; i < maxY; i++)
                    if (Board[x1, i] == TileStatus.Ship || (x1 - 1 >= 0 && Board[x1 - 1, i] == TileStatus.Ship) || (x1 + 1 < 10 && Board[x1 + 1, i] == TileStatus.Ship))
                    {
                        return false;
                    }
                return true;
            }
            else if (y1 == y2)
            {
                int minX = Min(x1, x2);
                minX = minX == 0 ? minX : minX - 1;
                int maxX = Max(x1, x2);
                maxX = maxX == 9 ? maxX : maxX + 1;
                for (int i = minX; i < maxX; i++)
                    if (Board[i, y1] == TileStatus.Ship || (y1 - 1 >= 0 && Board[i, y1 - 1] == TileStatus.Ship) || (y1 + 1 < 10 && Board[i, y1 + 1] == TileStatus.Ship))
                    {
                        return false;
                    }
                return true;
            }
            throw new ArgumentException();
        }

        protected void MarkDiagonally(int x, int y)
        {
            Enemy.Board[x, y] = TileStatus.Hit;
            if (x - 1 >= 0 && y - 1 >= 0)
                Enemy.Board[x - 1, y - 1] = TileStatus.Miss;
            if (x + 1 <= 9 && y - 1 >= 0)
                Enemy.Board[x + 1, y - 1] = TileStatus.Miss;
            if (x + 1 <= 9 && y + 1 <= 9)
                Enemy.Board[x + 1, y + 1] = TileStatus.Miss;
            if (x - 1 >= 0 && y + 1 <= 9)
                Enemy.Board[x - 1, y + 1] = TileStatus.Miss;
        }

        public bool IsDefeated()
        {
            if (Ships.Exists(ship => ship.Destroyed == false))
                return false;
            return true;
        }

        protected void PrintBoard(TileStatus[,] Board, Func<TileStatus, string> printer)
        {
            Console.WriteLine("   A|B|C|D|E|F|G|H|I|J|");
            for (int i = 0; i < 10; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + "|");
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(printer(Board[j, i]) + "|");
                }
                Console.WriteLine();
            }
        }

        protected void PrintBoard(Func<TileStatus, string> printer)
        {
            PrintBoard(this.Board, printer);
        }

        protected void PrintFriendlyBoard()
        {
            Func<TileStatus, string> printer = tile =>
            {
                switch (tile)
                {
                    case TileStatus.Empty:
                        return " ";
                    case TileStatus.Hit:
                        return "X";
                    case TileStatus.Miss:
                        return "0";
                    case TileStatus.Ship:
                        return "S";
                    default:
                        return " ";
                }
            };
            PrintBoard(printer);
        }

        protected void PrintEnemysBoard()
        {
            Func<TileStatus, string> printer = tile =>
            {
                switch (tile)
                {
                    case TileStatus.Empty:
                        return " ";
                    case TileStatus.Hit:
                        return "X";
                    case TileStatus.Miss:
                        return "0";
                    default:
                        return " ";
                }
            };
            PrintBoard(Enemy.Board, printer);
        }
    }
}
