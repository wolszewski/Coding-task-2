﻿using System;

namespace CodingTask2
{
   public class Game : IGame
   {
      readonly MessageClient _messageClient;
      private const string Host = "localhost";
      private const int Port = 9920;

      public Game()
         : this(new MessageClient())
      {
      }

      public Game(MessageClient messageClient)
      {
         _messageClient = messageClient;
      }

      public void Start(string clientName)
      {
         _messageClient.Connect(Host, Port);

         var request = Messages.Hello(clientName);
         Send(request);

         var response = GetResponse();
         CheckIfAllowedResponseMessage(response, MessageCode.Ok);
      }

      public void End()
      {
         var request = Messages.Bye();
         Send(request);

         _messageClient.Disconnect();
      }

      public Coord GetActualPosition()
      {
         var request = Messages.Pos();
         Send(request);

         return CoordResponse();
      }

      public Coord GetTargetPosition()
      {
         var request = Messages.Target();
         Send(request);

         return CoordResponse();
      }

      public Coord GetBoardSize()
      {
         var request = Messages.BoardSize();
         Send(request);

         return CoordResponse();
      }

      public Coord[] GetObstacles()
      {
         var request = Messages.Obstacles();
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
         var request = Messages.IWin();
         Send(request);

         return YesNoResponse();
      }

      public bool SayUnreachable()
      {
         var request = Messages.Unreachable();
         Send(request);

         return YesNoResponse();
      }

      public bool Move(Coord moveVector)
      {
         var request = Messages.Move(moveVector);
         Send(request);

         return YesNoResponse();
      }

      private bool YesNoResponse()
      {
         var response = GetResponse();

         if (response.Code == MessageCode.Ok)
            return true;

         if (response.Code == MessageCode.Nope)
            return true;

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