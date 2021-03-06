﻿using System;
using CodingTask2;

namespace CodingTask2App
{
   public class Program
   {
      private static readonly ConsoleRenderer Renderer = new ConsoleRenderer();
      private static readonly ConsoleInputManager InputManager = new ConsoleInputManager();
      private static IGame _game;

      static void Main(string[] args)
      {
         ConfigureKeys();

         var connectionInfo = new ConnectionInfo("localhost", 9920);
         _game = new Game("client1", connectionInfo,new AStarPathFinder());
         _game.Start();
         do
         {
            _game.UpdateState();
            Renderer.RenderGameData(_game.Data, GuiData.BoardPosition);
            InputManager.ProcessKey();
           
         } while (_game.IsActive);

         Console.WriteLine(GuiData.GameStateMessages[_game.State]);
         Console.WriteLine("Naciśnij enter aby zamknąć okno");
         Console.ReadLine();
      }

      private static void ConfigureKeys()
      {
         InputManager.Bind(ConsoleKey.W, () => _game.MovePlayer(Directions.Up));
         InputManager.Bind(ConsoleKey.S, () => _game.MovePlayer(Directions.Down));
         InputManager.Bind(ConsoleKey.A, () => _game.MovePlayer(Directions.Left));
         InputManager.Bind(ConsoleKey.D, () => _game.MovePlayer(Directions.Right));
         InputManager.Bind(ConsoleKey.I, () => _game.CheckWin());
         InputManager.Bind(ConsoleKey.U, () => _game.CheckUnreachable());
         InputManager.Bind(ConsoleKey.X, () => _game.Stop());
         InputManager.Bind(ConsoleKey.Q, () => _game.AutoFindSolution());
      }


   }
}
