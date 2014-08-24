using CodingTask2;

namespace CodingTask2App
{
   public class Board
   {
      public Coord Size { get; set; }
      private BoardElement[][] Fields;
      private const int MaxBoardSize = 50;
      public int ElementCount { get { return Size.X * Size.Y; } }

      public Board()
      {
         Initialize();
      }

      public void SetField(Coord coord, BoardElement element)
      {
         Fields[coord.X][coord.Y] = element;
      }

      public BoardElement GetField(int x, int y)
      {
         return Fields[x][y];
      }

      private void Initialize()
      {
         Fields = new BoardElement[MaxBoardSize][];
         for (int i = 0; i < MaxBoardSize; i++)
         {
            Fields[i] = new BoardElement[MaxBoardSize];
         }
      }

      public void SetObstacles(Coord[] obstacleList)
      {
         foreach (var obstacle in obstacleList)
         {
            SetField(obstacle, BoardElement.Ostacle);
         }
      }

      public void SetState(GameState state)
      {
         SetField(state.ActualPosition,BoardElement.Player);
         SetField(state.TargetPosition,BoardElement.Target);
      }



      public void Clear()
      {
         for (int i = 0; i < MaxBoardSize; i++)
         {
            for (int j = 0; j < MaxBoardSize; j++)
            {
               Fields[i][j] = BoardElement.None;
            }
         }

      }
   }
}