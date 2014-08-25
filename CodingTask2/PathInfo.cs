namespace CodingTask2
{
   public class PathInfo
   {
      public bool Exists {get; private set;}
      public Coord[] Moves{get;private set;}

      public static PathInfo NotExistingPath()
      {
         return new PathInfo() { Exists = false };
      }

   }
}