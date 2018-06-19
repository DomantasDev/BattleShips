using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;
using static BattleShips.CoordinatesReader;

namespace BattleShips
{
    class HumanPlayer : Player
    {
        public HumanPlayer(string name) : base(name) { }

        public override void SetUpBoard()
        {
            for (int i = 4; i > 0; i--)        
                for (int j = i; j <= 4; j++) 
                {
                    int x1, y1, x2, y2;
                    string input;
                    Console.Clear();
                    PrintFriendlyBoard();
                    if (i == 1)
                    {
                        do
                        {
                            while (!IsValidCoordinate(input = Console.ReadLine())) ;
                            (x1, y1) = ConvertToCoordinates(input);
                        } while (!PlaceShip(x1, y1, x1, y1, i));
                    }
                    else
                    {                        
                        do
                        {
                            while (!IsValidCoordinatesPair(input = Console.ReadLine())) ;
                            (x1, y1, x2, y2) = ConvertToPairOfCoordinates(input);
                        } while (!PlaceShip(x1, y1, x2, y2, i));
                    }
                }
        }

        public override bool Shoot()
        {
            string input;
            while (!IsValidCoordinate(input = Console.ReadLine()));
            (int x, int y) = ConvertToCoordinates(input);
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

        public override void PrintBothBoards()
        {
            Console.Clear();
            Console.WriteLine("Friendly Ships");
            PrintFriendlyBoard();
            Console.WriteLine("Hostile Ships");
            PrintEnemysBoard();           
        }

    }
}
