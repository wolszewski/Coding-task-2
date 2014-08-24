namespace CodingTask2
{
   public class ConnectionInfo
   {
      public string Host { get; set; }
      public int Port { get; set; }

      public ConnectionInfo(string host, int port)
      {
         Host = host;
         Port = port;
      }
   }
}