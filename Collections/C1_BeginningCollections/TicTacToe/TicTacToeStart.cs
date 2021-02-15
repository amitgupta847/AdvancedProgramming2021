using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_BeginningCollections.TicTacToe
{
  public class TicTacToeStart
  {
    public static void StartGame()
    {
      Game game = new Game();
      game.PlayGame();
      Console.WriteLine("Game over");
    }
  }
}
