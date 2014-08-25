namespace CodingTask2
{
   public class GameData
   {
      public Coord Player { get; set; } 
      public Coord Target { get; set; }
      public Coord[] Obstacles { get; set; }
      public Coord BoardSize { get; set; }
   }
}