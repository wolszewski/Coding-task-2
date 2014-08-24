using System;
using System.Text;
using CodingTask2;

namespace CodingTask2App
{
   class Program
   {
      static void Main(string[] args)
      {
         var connectionInfo = new ConnectionInfo("localhost",9920);
         var game = new GameEngine(connectionInfo);
         var board = new Board();
         game.Start("client1");

         var state = GetGameState(game);


         var boardSize = game.GetBoardSize();
         Console.WriteLine("Board size: "+boardSize);
         Console.WriteLine("Actual: "+state.ActualPosition);
         Console.WriteLine("Target: "+state.TargetPosition);

         board.Size = boardSize;
         var obstacles = game.GetObstacles();

         board.SetObstacles(obstacles);
         board.SetState(state);
         
         RenderBoard(board);
         Console.WriteLine("Obstacles:");
         foreach (var obstacle in obstacles)
         {
            Console.WriteLine(obstacle);
         }
         Console.ReadLine();
         game.End();
      }

      private static void RenderBoard(Board board)
      {
         var sb = new StringBuilder(2504);

         for (int y = 0; y < board.Size.Y; y++)
         {
            for (int x = 0; x < board.Size.X; x++)
            {
               sb.Append(GetCharForElement(board.GetField(x, y)));
            }
            sb.AppendLine();
         }
         Console.WriteLine(sb.ToString());
      }

      private static char GetCharForElement(BoardElement element)
      {
         switch (element)
         {
            case BoardElement.None:
               return ' ';
            case BoardElement.Ostacle:
               return 'O';
            case BoardElement.Player:
               return 'P';
            case BoardElement.Target:
               return 'X';
            default:
               throw new Exception("Nieobsługiwany typ planszy");

         }
      }

      private static GameState GetGameState(GameEngine game)
      {
         return new GameState
         {
            ActualPosition = game.GetActualPosition(), 
            TargetPosition = game.GetTargetPosition()
         };
      }
   }
}
