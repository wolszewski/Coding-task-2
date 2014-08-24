using System.Net.Sockets;
using System.Text;

namespace CodingTask2
{
   public class GameClient
   {
      private readonly MessageSerializer _messageSerializer;
      private TcpClient _tcpClient;
      private string _host;
      private int _port;
      private NetworkStream _networkStream;

      public GameClient(): this(new MessageSerializer())
      {

      }

      public GameClient(MessageSerializer messageSerializer)
      {
         _messageSerializer = messageSerializer;
      }

      public void Connect(string host, int port)
      {
         _host = host;
         _port = port;
         _tcpClient = new TcpClient(_host, _port);
         _networkStream = _tcpClient.GetStream();

      }

      private void SendMessage(Message message)
      {
         var messageBytes = _messageSerializer.Serialize(message);
         _networkStream.Write(messageBytes, 0, messageBytes.Length);
         _networkStream.Flush();
      }

      public int GetResponse()
      {
         return _networkStream.ReadByte();
      }

      public void SendHello(string clientName)
      {
         var helloMessage = new Message
         {
            Code = MessageCode.Hello,
            Content = Encoding.ASCII.GetBytes(clientName)
         };

         SendMessage(helloMessage);
      }

   }
}