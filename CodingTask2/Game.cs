using System;

namespace CodingTask2
{
   public class Game
   {
      readonly MessageClient _messageClient;

      public Game(): this(new MessageClient())
      {
      }

      public Game(MessageClient messageClient)
      {
         _messageClient = messageClient;
      }

      public void SayHello(string clientName)
      {
         var request = Messages.Hello(clientName);
         Send(request);

         var response = GetResponse();
         CheckIfAllowedMessage(response, MessageCode.Ok);
      }

      public Coord GetActualPosition()
      {
         var request = Messages.Pos();
         Send(request);

         return GetCoordsDataResponse();
      }

      public Coord GetTargetPosition()
      {
         var request = Messages.Target();
         Send(request);

         return GetCoordsDataResponse();
      }

      public Coord GetBoardSize()
      {
         var request = Messages.BoardSize();
         Send(request);

         return GetCoordsDataResponse();
      }

      public Coord[] GetObstacles()
      {
         var request = Messages.Obstacles();
         Send(request);

         var response = GetResponse();
         CheckIfAllowedMessage(response, MessageCode.PosList);

         return ContentParser.ToCoordArray(response.Content);
      }


      private Coord GetCoordsDataResponse()
      {
         var response = GetResponse();
         CheckIfAllowedMessage(response, MessageCode.PosData);

         return ContentParser.ToCoord(response.Content);
      }

      private void CheckIfAllowedMessage(Message response, MessageCode expectedMessageCode)
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