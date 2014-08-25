using System.Collections.Generic;
using CodingTask2;

namespace CodingTask2App
{
   public class GuiData
   {
      public static readonly Coord BoardPosition = new Coord(10, 0);
      public static readonly IReadOnlyDictionary<GameState,string> GameStateMessages = new Dictionary<GameState, string>
      {
         {GameState.Unknown, "Gra zakończona przedwcześnie"},
         {GameState.Loss, "Ha, ha loser !"},
         {GameState.Win, "You're awsome !"},
      }; 
   }
}