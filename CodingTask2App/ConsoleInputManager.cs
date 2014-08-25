using System;
using System.Collections.Generic;

namespace CodingTask2App
{
   public class ConsoleInputManager
   {
      private readonly Dictionary<ConsoleKey, Action> InputActions = new Dictionary<ConsoleKey, Action>();

      public void Bind(ConsoleKey key, Action action)
      {
         InputActions.Add(key, action);
      }

      public Action GetActionForKey(ConsoleKey key)
      {
         Action action;
         InputActions.TryGetValue(key, out action);
         return action;
      }

      public void ProcessKey(ConsoleKeyInfo consoleKey)
      {
         var action = GetActionForKey(consoleKey.Key);
         if (action != null)
            action();
      }
   }
}