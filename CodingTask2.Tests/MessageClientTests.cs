using FluentAssertions;
using NUnit.Framework;

namespace CodingTask2.Tests
{
   [TestFixture, Explicit("Testy sprawdzające odpowiedzi serwera")]
   public class MessageClientTests
   {
      private MessageClient _client;
      private const string host = "localhost";
      private const int port = 9920;

      [SetUp]
      public void TestSetup()
      {
         _client = new MessageClient();
         _client.Connect(host, port);
      }

      [TearDown]
      public void TestTearDown()
      {
         _client.Close();
      }

      [Test]
      public void HelloTest()
      {
         _client.Send(Messages.Hello("client1"));
         var response = _client.Receive();

         var expected = Messages.Ok();
         response.ShouldBeEquivalentTo(expected);
      }

      public void SayHello()
      {
         _client.Send(Messages.Hello("client1"));
         _client.Receive();
      }

      [Test]
      public void PosDataTest()
      {
         SayHello();
         _client.Send(Messages.Pos());
         var response = _client.Receive();

         response.Code.ShouldBeEquivalentTo(MessageCode.PosData);
      }
   }
}
