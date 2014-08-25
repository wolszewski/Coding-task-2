using System;

namespace CodingTask2
{
   public class GameClient : IGameClient
   {
      private readonly MessageClient _messageClient;
      private readonly ConnectionInfo _connectionInfo;
      private readonly MessageFactory _messageFactory;

      public GameClient(ConnectionInfo connectionInfo)
         : this(new MessageClient(), new MessageFactory(), connectionInfo )
      {
      }

      internal GameClient(MessageClient messageClient, MessageFactory messageFactory, ConnectionInfo connectionInfo)
      {
         _messageClient = messageClient;
         _connectionInfo = connectionInfo;
         _messageFactory = messageFactory;
      }

      public void ConnectAndSayHello(string clientName)
      {
         _messageClient.Connect(_connectionInfo);

         var request = _messageFactory.Hello(clientName);
         Send(request);

         var response = GetResponse();
         CheckIfAllowedResponseMessage(response, MessageCode.Ok);
      }

      public void SayByeAndDisconnect()
      {
         var request = _messageFactory.Bye();
         Send(request);

         _messageClient.Disconnect();
      }

      public Coord GetPlayerPosition()
      {
         var request = _messageFactory.Pos();
         Send(request);

         return CoordResponse();
      }

      public Coord GetTargetPosition()
      {
         var request = _messageFactory.Target();
         Send(request);

         return CoordResponse();
      }

      public Coord GetBoardSize()
      {
         var request = _messageFactory.BoardSize();
         Send(request);

         return CoordResponse();
      }

      public Coord[] GetObstacles()
      {
         var request = _messageFactory.Obstacles();
         Send(request);

         return CoordArrayResponse();
      }

      private Coord[] CoordArrayResponse()
      {
         var response = GetResponse();
         CheckIfAllowedResponseMessage(response, MessageCode.PosList);

         return ContentParser.ToCoordArray(response.Content);
      }

      public bool SayWin()
      {
         var request = _messageFactory.IWin();
         Send(request);

         return YesNoResponse();
      }

      public bool SayUnreachable()
      {
         var request = _messageFactory.Unreachable();
         Send(request);

         return YesNoResponse();
      }

      public bool Move(Coord moveVector)
      {
         var request = _messageFactory.Move(moveVector);
         Send(request);

         return YesNoResponse();
      }

      private bool YesNoResponse()
      {
         var response = GetResponse();

         if (response.Code == MessageCode.Ok)
            return true;

         if (response.Code == MessageCode.Nope)
            return false;

         throw new Exception("Nieoczekiwana wiadomość: " + response.Code);
      }


      private Coord CoordResponse()
      {
         var response = GetResponse();
         CheckIfAllowedResponseMessage(response, MessageCode.PosData);

         return ContentParser.ToCoord(response.Content);
      }

      private void CheckIfAllowedResponseMessage(Message response, MessageCode expectedMessageCode)
      {
         if (response.Code != expectedMessageCode)
            throw new Exception("Nieoczekiwana wiadomość: " + response.Code);
      }


      private Message GetResponse()
      {
         return _messageClient.Receive();
      }

      private void Send(Message helloMessage)
      {
         _messageClient.Send(helloMessage);
      }
   }
}