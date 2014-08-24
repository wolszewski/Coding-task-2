namespace CodingTask2
{
   public class Message
   {
      public MessageCode Code { get; set; }
      public byte[] Content { get; set; }

      public int GetContentLength()
      {
         return Content != null ? Content.Length : 0;
      }

      public Message(MessageCode code)
      {
         Code = code;
      }
   }
}