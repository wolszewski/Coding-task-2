using System.Collections.Generic;
using CodingTask2;

namespace CodingTask2App
{
   public class GameData
   {
      public Coord Player { get; set; } 
      public Coord Target { get; set; }
      public Coord[] Obstacles { get; set; }
      public Coord BoardSize { get; set; }
   }
}