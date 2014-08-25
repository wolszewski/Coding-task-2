using CodingTask2;

namespace CodingTask2App
{
   public class Game : IGame
   {
      private readonly GameClient _client;
      private readonly string _clientName;
      public bool IsActive { get; private set; }

      public Game(string clientName, ConnectionInfo connectionInfo)
      {
         _clientName = clientName;
         _client = new GameClient(connectionInfo);
      }

      public GameData Data{get;private set;}

      public void Start()
      {
         _client.ConnectAndSayHello(_clientName);
         IsActive = true;
         Data = LoadState();
      }

      private  GameData LoadState()
      {
         return new GameData
         {
            Player = _client.GetPlayerPosition(),
            Target = _client.GetTargetPosition(),
            BoardSize = _client.GetBoardSize(),
            Obstacles = _client.GetObstacles()
         };
      }

      public void UpdateState()
      {
         Data.Player = _client.GetPlayerPosition();
      }

      public void MovePlayer(Coord direction)
      {
         _client.Move(direction);
      }

      public void CheckWin()
      {
         _client.SayWin();
      }
      public void CheckUnreachable()
      {
         _client.SayUnreachable();
      }

      public void Stop()
      {
         _client.SayByeAndDisconnect();
         IsActive = false;
      }
   }
}