using System;
using System.Collections.Generic;
using CodingTask2;

namespace CodingTask2App
{

   class Program
   {
      private static GameData _gameData;
      private static GameClient _client;
      private static readonly Coord BoardPosition = new Coord(10,0);
      private static readonly Dictionary<ConsoleKey,Action>  InputActions = new Dictionary<ConsoleKey, Action>();

      static void Main(string[] args)
      {
         ConfigureKeys();

         var connectionInfo = new ConnectionInfo("localhost", 9920);
         _client = new GameClient(connectionInfo);
         _client.Start("client1");

         _gameData = LoadData();
         var renderer = new ConsoleRenderer();

         do
         {
            renderer.RenderGameData(_gameData,BoardPosition);
            ProcessKey(Console.ReadKey());
            UpdateGameData();
         } while (true);

      }

      private static void ConfigureKeys()
      {
         InputActions.Add(ConsoleKey.W, ()=> _client.Move(Directions.Up));
         InputActions.Add(ConsoleKey.S, ()=> _client.Move(Directions.Down));
         InputActions.Add(ConsoleKey.A, ()=> _client.Move(Directions.Left));
         InputActions.Add(ConsoleKey.D, ()=> _client.Move(Directions.Right));
         InputActions.Add(ConsoleKey.I, ()=> _client.SayWin());
         InputActions.Add(ConsoleKey.U, ()=> _client.SayUnreachable());
      }

      private static GameData LoadData()
      {
         return new GameData
         {
            Player = _client.GetPlayerPosition(),
            Target = _client.GetTargetPosition(),
            BoardSize = _client.GetBoardSize(),
            Obstacles = _client.GetObstacles()
         };
      }

      private static void UpdateGameData()
      {
         _gameData.Player = _client.GetPlayerPosition();
      }

      private static void ProcessKey(ConsoleKeyInfo keyInfo)
      {
         Action action;
         InputActions.TryGetValue(keyInfo.Key, out action);
         if (action != null)
            action();
      }
   }
}
