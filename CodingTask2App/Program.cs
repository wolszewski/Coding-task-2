using System;
using System.Text;
using CodingTask2;

namespace CodingTask2App
{

   class Program
   {
      private static GameEngine _game;
      private static Board _board;
      private static string _message;

      static void Main(string[] args)
      {
         var connectionInfo = new ConnectionInfo("localhost",9920);
         _game = new GameEngine(connectionInfo);
         _board = new Board();
         _game.Start("client1");

         ConsoleKeyInfo pressedKey;
         do
         {
            RenderFrame(_game, _board);
            pressedKey = Console.ReadKey();
            ProcessInput(pressedKey);
         } while (pressedKey.Key != ConsoleKey.X);
       
      }

      private static void ProcessInput(ConsoleKeyInfo key)
      {
         _message = "";
         switch (key.Key)
         {
            case ConsoleKey.X:
               _game.End();
               break;

            case ConsoleKey.W:
               Move(Directions.Up);
               break;

            case ConsoleKey.S:
               Move(Directions.Down);
               break;


            case ConsoleKey.A:
               Move(Directions.Left);
               break;


            case ConsoleKey.D:
               Move(Directions.Right);
               break;

            default:
               return;               
         }
      }

      private static void Move(Coord direction)
      {
        var succes = _game.Move(direction);
         if(!succes)
            _message = "Nie można wykonać ruchu";
      }

      private static void RenderFrame(GameEngine game, Board board)
      {
         Console.Clear();
         var state = GetGameState(game);
         var boardSize = game.GetBoardSize();
         Console.WriteLine("Board size: " + boardSize);
         Console.WriteLine("Actual: " + state.ActualPosition);
         Console.WriteLine("Target: " + state.TargetPosition);

         board.Size = boardSize;
         var obstacles = game.GetObstacles();
         board.Clear();
         board.SetObstacles(obstacles);
         board.SetState(state);
         RenderBoard(board);

         if(!string.IsNullOrEmpty(_message))
            Console.WriteLine(_message);
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
               throw new Exception("Nieobsługiwany typ obiektu");

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
