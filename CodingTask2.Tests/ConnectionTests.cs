using System;
using FluentAssertions;
using NUnit.Framework;

namespace CodingTask2.Tests
{
    public class ConnectionTests
    {
       [Test]
       public void HelloTest()
       {
          const byte BadMsg = 0xFF;
          const byte Ok = 0xE1;

          string host = "localhost";
          int port=9920;
          var client = new GameClient(host, port);
          client.Connect();
          client.SendHello();
          var response = client.GetResponse();

          response.Should().Be(Ok);
       }
    }
}
