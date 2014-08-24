using System;
using System.Linq;

namespace CodingTask2
{
   public class MessageSerializer
   {
      private const int CodeIndex = 0;
      private const int LengthIndex = 1;
      private const int ContentIndex = 5;
      private const int MessageInfoSize = (1 + 2 * sizeof(int));

      public byte[] ToBytes(Message message)
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
            message.Content.CopyTo(buffer, ContentIndex);
      }

      private static void WriteMessageLength(byte[] buffer, int wholeMessageLength)
      {
         var lengthBytes = BitConverter.GetBytes(wholeMessageLength);
         lengthBytes.CopyTo(buffer, LengthIndex);
      }

      private static void WriteMessageCode(byte[] buffer, Message message)
      {
         buffer[CodeIndex] = (byte)message.Code;
      }

      private static int GetLengthWithoutCrc(Message message)
      {
         return 1 + sizeof(Int32) + message.GetContentLength();
      }

      public Message FromBytes(byte[] data)
      {
         var messageCode = (MessageCode)data[CodeIndex];
         var message = new Message(messageCode);
         
         var messageLength = BitConverter.ToInt32(data,LengthIndex);
         var contentLength = messageLength - MessageInfoSize;

         if (contentLength > 0)
            message.Content = data.SubArray(ContentIndex, contentLength);
         return message;
      }
   }
}