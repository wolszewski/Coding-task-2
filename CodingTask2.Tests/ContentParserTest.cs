using System;
using FluentAssertions;
using NUnit.Framework;

namespace CodingTask2.Tests
{
   [TestFixture]
   public class ContentParserTest
   {
      [Test]
      public void ToCoordArrayTest()
      {
         var expected = new Coord[] { new Coord(1, 2), new Coord(3, 4), new Coord(5, 6) };
         var input = new byte[] { 3, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 4, 0, 0, 0, 5, 0, 0, 0, 6, 0, 0, 0 };
         var result = ContentParser.ToCoordArray(input);

         result.ShouldAllBeEquivalentTo(expected);
      }

      [Test]
      public void ToCoordTest()
      {
         var expected =new Coord(1, 2);
         var input = new byte[] { 0, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0 };
         var result = ContentParser.ToCoord(input,4);

         result.ShouldBeEquivalentTo(expected);
      }
   }
}