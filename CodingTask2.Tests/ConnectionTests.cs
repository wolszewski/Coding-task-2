using FluentAssertions;
using NUnit.Framework;

namespace CodingTask2.Tests
{
    public class ConnectionTests
    {
       [Test]
       public void HelloTest()
       {
          string host = "localhost";
          int port=9920;
          var client = new GameClient();
          client.Connect(host, port);
          client.SendHello("client1");
          var response = client.GetResponse();

          response.Should().Be((int)MessageCode.Ok);
       }
    }
}
