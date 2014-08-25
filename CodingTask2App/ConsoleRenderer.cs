using System;
using CodingTask2;

namespace CodingTask2App
{
   public class ConsoleRenderer
   {
      private const char PlayerChar = 'P';
      private const char TargetChar = 'T';
      private const char ObstacleChar = 'O';

      private Coord _startPosition;

      public void RenderGameData(GameData data, Coord startPosition)
      {
         _startPosition = startPosition;
         Console.Clear();
         RenderOstacles(data.Obstacles);
         RenderTarget(data.Target);
         RenderPlayer(data.Player);
         Console.SetCursorPosition(0,data.BoardSize.Y);
      }

      private void RenderPlayer(Coord player)
      {
         WriteCharInPosition(player, PlayerChar);
      }

      private void RenderTarget(Coord target)
      {
         WriteCharInPosition(target, TargetChar);
      }

      private void RenderOstacles(Coord[] obstacles)
      {
         foreach(var o in obstacles)
         {
            WriteCharInPosition(o, ObstacleChar);
         }
      }

      private void WriteCharInPosition(Coord o, char value)
      {
         Console.SetCursorPosition(_startPosition.X + o.X, _startPosition.Y + o.Y);
         Console.Write(value);
      }
   }
}