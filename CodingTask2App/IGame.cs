using CodingTask2;

namespace CodingTask2App
{
   public interface IGame
   {
      void Start();
      void MovePlayer(Coord direction);
      void CheckWin();
      void CheckUnreachable();
      void Stop();
      bool IsActive { get; }
      GameData Data { get; }
      void UpdateState();
   }
}