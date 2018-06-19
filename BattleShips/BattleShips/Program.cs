using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new HumanPlayer("Human"), new AIPlayer("Bot"));
            game.StartGame();
            Console.WriteLine("Press Enter to exit game");
            Console.ReadLine();
        }
    }
}
