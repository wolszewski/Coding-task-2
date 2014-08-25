namespace CodingTask2
{
   public interface IGame
   {
      void Start();
      void MovePlayer(Coord direction);
      void CheckWin();
      void CheckUnreachable();
      void Stop();
      bool IsActive { get; }
      GameState State { get; }
      GameData Data { get; }
      void UpdateState();
      void AutoFindSolution();
   }
}