using System.Text;

namespace CodingTask2
{
   public static class Messages
   {
      public static Message Hello(string clientName)
      {
         return new Message(MessageCode.Hello)
         {
            Content = Encoding.ASCII.GetBytes(clientName)
         };

      }

      public static Message Pos()
      {
         return new Message(MessageCode.Pos);
      }
      public static Message Target()
      {
         return new Message(MessageCode.Target);
      }
      public static Message BoardSize()
      {
         return new Message(MessageCode.BoardSize);
      }

      public static Message PosData()
      {
         return new Message(MessageCode.PosData);
      }
      public static Message Ok()
      {
         return new Message(MessageCode.Ok);
      }

      public static Message Obstacles()
      {
         return new Message(MessageCode.Obstacles);
      }

      public static Message Bye()
      {
         return new Message(MessageCode.Bye);

      }

      public static Message IWin()
      {
         return new Message(MessageCode.IWin);
      }

      public static Message Unreachable()
      {
         return new Message(MessageCode.Unreachable);
      }

      public static Message Move(Coord moveVector)
      {
         var message = new Message(MessageCode.Move)
         {
            Content = ContentParser.ToBytes(moveVector)
         };
         return message;
      }
   }
}