namespace CodingTask2
{
   public interface IPathFinder
   {
      PathInfo FindPath(Coord startLocation, Coord finalLocation);
   }
}