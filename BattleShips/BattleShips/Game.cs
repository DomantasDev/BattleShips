using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips
{
    class Game
    {
        Player player1, player2;

        public Game(Player player1, Player player2)
        {
            this.player1 = player1;
            this.player1.Enemy = player2;
            this.player2 = player2;
            this.player2.Enemy = player1;
        }

        public void StartGame()
        {
            player1.SetUpBoard();
            player2.SetUpBoard();
            while(true)
            {
                if (MakeAMove(player1, player2))
                    break;
                if (MakeAMove(player2, player1))
                    break;
            }
        }

        private bool MakeAMove(Player turnPlayer, Player waitingPlayer)
        {
            turnPlayer.PrintBothBoards();
            while (turnPlayer.Shoot())
            {
                turnPlayer.PrintBothBoards();
                if (waitingPlayer.IsDefeated())
                {
                    Console.WriteLine(turnPlayer.Name + " WON!");
                    return true;
                }
            }
            turnPlayer.PrintBothBoards();
            return false;
        }
    }
}
