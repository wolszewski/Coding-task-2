namespace CodingTask2
{
   public struct Coord
   {
      public int X { get; private set; }
      public int Y { get; private set; }

      public Coord(int x, int y) : this()
      {
         X = x;
         Y = y;
      }
   }
}