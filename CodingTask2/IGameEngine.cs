namespace CodingTask2
{
   public interface IGameEngine
   {
      void Start(string clientName);
      void End();
      Coord GetActualPosition();
      Coord GetTargetPosition();
      Coord GetBoardSize();
      Coord[] GetObstacles();
      bool SayWin();
      bool SayUnreachable();
      bool Move(Coord moveVector);
   }
}