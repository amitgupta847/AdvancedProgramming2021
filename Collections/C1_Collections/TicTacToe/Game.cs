using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections.C1_Collections.TicTacToe
{
  public class Game
  {
    private Square[,] _board = new Square[3, 3];

    public Game()
    {
      FillBoard();
      // DisplayBoard();
    }

    private void FillBoard()
    {
      for (int i = 0; i < _board.GetLength(0); i++)
      {
        for (int j = 0; j < _board.GetLength(1); j++)
        {
          _board[i, j] = new Square(Player.None);
        }
      }
    }

    public void PlayGame()
    {
      Player player = Player.Crosses;
      bool @continue = true;
      while (@continue)
      {
        DisplayBoard();
        @continue = PlayMove(player);
        if (!@continue)
          return;

        player = 3 - player;    // (a trick to get 1 or 2 alternatively)
      }
    }

    public void DisplayBoard()
    {
      for (int i = 0; i < _board.GetLength(0); i++)
      {
        for (int j = 0; j < _board.GetLength(1); j++)
          Console.Write(" " + _board[i, j]);

        Console.WriteLine();
      }
    }

    private bool PlayMove(Player player)
    {
      Console.WriteLine("Enter row column, eg: 3,3. (Game will quit if you enter invalid input)");
      string input = Console.ReadLine();
      string[] parts = input.Split(',');

      if (parts.Length != 2)
        return false;

      int.TryParse(parts[0], out int row);
      int.TryParse(parts[1], out int column);

      if (row < 1 || row > 3 || column < 1 || column > 3)
        return false;

      if (_board[row - 1, column - 1].Owner != Player.None)
      {
        Console.WriteLine("Square is already ");
        return false;
      }

      _board[row - 1, column - 1] = new Square(player);

      //Amit - we can extend this to see if any row/column or diagnol has all same character.
      //if yes then the current player won and game is finished.

      return true;
    }
  }
}



// using a jagged array
//private Square[][] _board =
//{
//      new Square[3],
//      new Square[3],
//      new Square[3]
//    };

// replace with this definition of board to see how to delare
// with a multi-dimensional array. 
// You'll then need to replace all [][] operators with [,] for the code to compile
//		private Square[,] _board = new Square[3, 3];


// replace with this definition of board to see how to
// work with lists instead of arrays
// (but this demo is really best suited to a multi-dimensional array)
//private List<List<Square>> _board = new List<List<Square>>()
//{
//	new List<Square>{ new Square(), new Square(), new Square()},
//	new List<Square>{ new Square(), new Square(), new Square()},
//	new List<Square>{ new Square(), new Square(), new Square()}
//};
