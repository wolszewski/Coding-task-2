namespace CodingTask2
{
   public interface IGameClient
   {
      void Start(string clientName);
      void End();
      Coord GetPlayerPosition();
      Coord GetTargetPosition();
      Coord GetBoardSize();
      Coord[] GetObstacles();
      bool SayWin();
      bool SayUnreachable();
      bool Move(Coord moveVector);
   }
}