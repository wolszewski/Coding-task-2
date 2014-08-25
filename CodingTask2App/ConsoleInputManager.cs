using System;
using System.Collections.Generic;

namespace CodingTask2App
{
   public class ConsoleInputManager
   {
      private readonly Dictionary<ConsoleKey, Action> _inputActions = new Dictionary<ConsoleKey, Action>();

      public void Bind(ConsoleKey key, Action action)
      {
         _inputActions.Add(key, action);
      }

      public Action GetActionForKey(ConsoleKey key)
      {
         Action action;
         _inputActions.TryGetValue(key, out action);
         return action;
      }

      public void ProcessKey()
      {
         var pressed = Console.ReadKey(true);
         var action = GetActionForKey(pressed.Key);
         if (action != null)
            action();
      }
   }
}