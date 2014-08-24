namespace CodingTask2
{
   public enum MessageCode
   {
      Hello = 0x01,
      Bye = 0x02,
      IWin = 0x03,
      Unreachable = 0x04,
      Pos = 0x05,
      Target = 0x06,
      Obstacles = 0x07,
      Move = 0x08,
      BoardSize = 0x09,

      Ok = 0xE1,
      Nope = 0xE2,

      PosData = 0xF0,
      PosList = 0xF1,

      BadMsg = 0xEE
   }
}