using System;
using System.Linq;

namespace CodingTask2
{
   public class MessageSerializer
   {
      public byte[] Serialize(Message message)
      {
         var lengthWithoutCrc = GetLengthWithoutCrc(message);
         var messageLength = lengthWithoutCrc + sizeof(Int32);

         var buffer = new byte[messageLength];

         WriteMessageCode(buffer, message);
         WriteMessageLength(buffer, messageLength);
         WriteContent(buffer, message);
         WriteCrc(buffer, lengthWithoutCrc);

         return buffer;
      }

      private static void WriteCrc(byte[] buffer, int lengthWithoutCrc)
      {
         int crc = buffer.Sum(b => b);
         var crcBytes = BitConverter.GetBytes(crc);
         crcBytes.CopyTo(buffer, lengthWithoutCrc);
      }

      private static void WriteContent(byte[] buffer, Message message)
      {
         if (message.GetContentLength() > 0)
            message.Content.CopyTo(buffer, 5);
      }

      private static void WriteMessageLength(byte[] buffer, int wholeMessageLength)
      {
         var lengthBytes = BitConverter.GetBytes(wholeMessageLength);
         lengthBytes.CopyTo(buffer, 1);
      }

      private static void WriteMessageCode(byte[] buffer, Message message)
      {
         buffer[0] = (byte)message.Code;
      }

      private static int GetLengthWithoutCrc(Message message)
      {
         return 1 + sizeof(Int32) + message.GetContentLength();
      }
   }
}