namespace CodingTask2
{
   public interface IGameClient
   {
      void ConnectAndSayHello(string clientName);
      void SayByeAndDisconnect();
      Coord GetPlayerPosition();
      Coord GetTargetPosition();
      Coord GetBoardSize();
      Coord[] GetObstacles();
      bool SayWin();
      bool SayUnreachable();
      bool Move(Coord moveVector);
   }
}