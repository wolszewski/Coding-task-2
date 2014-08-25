using System.Text;

namespace CodingTask2
{
   public  class MessageFactory
   {
      public  Message Hello(string clientName)
      {
         return new Message(MessageCode.Hello)
         {
            Content = Encoding.ASCII.GetBytes(clientName)
         };

      }

      public Message Pos()
      {
         return new Message(MessageCode.Pos);
      }
      public Message Target()
      {
         return new Message(MessageCode.Target);
      }
      public Message BoardSize()
      {
         return new Message(MessageCode.BoardSize);
      }

      public Message PosData()
      {
         return new Message(MessageCode.PosData);
      }
      public Message Ok()
      {
         return new Message(MessageCode.Ok);
      }

      public Message Obstacles()
      {
         return new Message(MessageCode.Obstacles);
      }

      public Message Bye()
      {
         return new Message(MessageCode.Bye);

      }

      public Message IWin()
      {
         return new Message(MessageCode.IWin);
      }

      public Message Unreachable()
      {
         return new Message(MessageCode.Unreachable);
      }

      public Message Move(Coord moveVector)
      {
         var message = new Message(MessageCode.Move)
         {
            Content = ContentParser.ToBytes(moveVector)
         };
         return message;
      }
   }
}