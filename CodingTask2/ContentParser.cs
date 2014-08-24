using System;

namespace CodingTask2
{
   public class ContentParser
   {
      const int Int32Size = sizeof(Int32);


      public static Coord ToCoord(byte[] content, int startIndex=0)
      {
         var x = BitConverter.ToInt32(content, startIndex);
         var y = BitConverter.ToInt32(content, startIndex + Int32Size);
         return new Coord(x, y);
      }

      public static Coord[] ToCoordArray(byte[] content)
      {
         var length = BitConverter.ToInt32(content, 0);
         var result = new Coord[length];

         int index = 4;

         for (int i = 0; i < length; i++)
         {
            result[i] = ToCoord(content, index);
            index += 2 * Int32Size;
         }

         return result;
      }
   }
}