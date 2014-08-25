using System.Collections.Generic;

namespace CodingTask2
{
   public class Game : IGame
   {
      private readonly GameClient _client;
      private readonly string _clientName;
      private readonly IPathFinder _pathFinder;
      public bool IsActive { get; private set; }
      public GameState State { get; private set; }

      public Game(string clientName, ConnectionInfo connectionInfo, IPathFinder pathFinder)
      {
         _clientName = clientName;
         _pathFinder = pathFinder;
         _client = new GameClient(connectionInfo);
      }

      public GameData Data{get;private set;}

      public void Start()
      {
         _client.ConnectAndSayHello(_clientName);
         IsActive = true;
         State = GameState.Unknown;
         Data = LoadData();
      }

      private  GameData LoadData()
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

      public void AutoFindSolution()
      {
         var path = _pathFinder.FindPath(Data.Player, Data.Target);
         
         if (path.Exists)
         {
            MovePlayerToTargetAndCheckWin(path.Moves);
         }
         else
         {
            CheckUnreachable();
         }
      }

      private void MovePlayerToTargetAndCheckWin(IEnumerable<Coord> moves)
      {
         foreach (var direction in moves)
         {
            MovePlayer(direction);
         }
         CheckWin();
      }

      public void MovePlayer(Coord direction)
      {
         _client.Move(direction);
      }

      public void CheckWin()
      {
        var isCorrectGuess =  _client.SayWin();
        SetStateDependingOnGuessResult(isCorrectGuess);
         IsActive = false;
      }

      private void SetStateDependingOnGuessResult(bool isCorrectGuess)
      {
         State = isCorrectGuess ? GameState.Win : GameState.Loss;
      }

      public void CheckUnreachable()
      {
         var isCorrectGuess = _client.SayUnreachable();
         SetStateDependingOnGuessResult(isCorrectGuess);
         IsActive = false;
      }

      public void Stop()
      {
         _client.SayByeAndDisconnect();
         IsActive = false;
      }
   }
}