using System.Net.Sockets;

namespace CodingTask2
{
   public class MessageClient
   {
      private const int MaxMessageLength = 4096;

      private readonly MessageSerializer _messageSerializer;
      private readonly byte[] _buffer = new byte[MaxMessageLength];

      private TcpClient _tcpClient;
      private string _host;
      private int _port;
      private NetworkStream _networkStream;

      public MessageClient(): this(new MessageSerializer())
      {
      }

      public MessageClient(MessageSerializer messageSerializer)
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

      public void Send(Message message)
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


      public void Disconnect()
      {
         _tcpClient.Close();
      }

      public void Connect(ConnectionInfo connectionInfo)
      {
         Connect(connectionInfo.Host, connectionInfo.Port);
      }
   }
}