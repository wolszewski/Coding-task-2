using CodingTask2;

namespace CodingTask2App
{
   public static class Directions
   {
      public static Coord Up { get { return new Coord(0,-1);} }
      public static Coord Down { get { return new Coord(0,1);} }
      public static Coord Right { get { return new Coord(1,0);} }
      public static Coord Left { get { return new Coord(-1,0);} }
   }
}