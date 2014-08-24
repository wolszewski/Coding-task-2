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
      private readonly byte[] _buffer = new byte[4096];

      public GameClient()
         : this(new MessageSerializer())
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

      private void Send(Message message)
      {
         var messageBytes = _messageSerializer.ToBytes(message);
         _networkStream.Write(messageBytes, 0, messageBytes.Length);
         _networkStream.Flush();
      }

      public Message Receive()
      {
         _networkStream.Read(_buffer, 0, _buffer.Length);
         return _messageSerializer.FromBytes(_buffer);
      }

      public void SendHello(string clientName)
      {
         var helloMessage = new Message(MessageCode.Hello)
         {
            Content = Encoding.ASCII.GetBytes(clientName)
         };

         Send(helloMessage);
      }

   }
}