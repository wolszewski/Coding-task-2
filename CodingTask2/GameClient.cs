using System;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CodingTask2
{
   public class GameClient
   {
      private const int IntLength = 4;
      private TcpClient _tcpClient;
      private readonly string _host;
      private readonly int _port;
      private NetworkStream _networkStream;

      public GameClient(string host, int port)
      {
         _host = host;
         _port = port;
      }

      public void Connect()
      {
         _tcpClient = new TcpClient(_host, _port);
         _networkStream = _tcpClient.GetStream();
      }

      public void SendHello()
      {
         var helloMessage = new Message
         {
            Code = 0x01,
            Content = "client1"
         };


         var contentBytes = Encoding.ASCII.GetBytes(helloMessage.Content);
         var lengthWithoutCrc = 1 + IntLength + contentBytes.Length;
         var wholeMessageLength = lengthWithoutCrc + IntLength;

         var lengthBytes = BitConverter.GetBytes(wholeMessageLength);

         var buffer = new byte[wholeMessageLength];
         buffer[0] = helloMessage.Code;
         lengthBytes.CopyTo(buffer,1);
         contentBytes.CopyTo(buffer,5);


         int crc = buffer.Sum(b => b);
         var crcBytes = BitConverter.GetBytes(crc);
         crcBytes.CopyTo(buffer, lengthWithoutCrc);

         _networkStream.Write(buffer,0,wholeMessageLength);
         _networkStream.Flush();
      }

      public int GetResponse()
      {
         return _networkStream.ReadByte();
      }
   }
}