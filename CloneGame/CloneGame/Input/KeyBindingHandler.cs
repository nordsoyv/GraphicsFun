using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloneGame.Messaging;

namespace CloneGame.Input
{
	class KeyBindingHandler : IKeyboardEventReciver
	{

		private IDictionary<string, string> keysToCommands;

		public KeyBindingHandler()
		{
			keysToCommands = new Dictionary<string, string>();
			keysToCommands.Add("n",  Commands.NEW_LANDSCAPE);
			keysToCommands.Add("N", Commands.NEW_LANDSCAPE);
		}


		public void HandleEvent(IEnumerable<KeyboardEvent> events)
		{
			foreach (var keyboardEvent in events)
			{
				if(typeof(KeybuttonEvent) == keyboardEvent.GetType())
				{
					var ev = (KeybuttonEvent)keyboardEvent;
					var key = ev.Key.ToString();
					if (keysToCommands.ContainsKey(key))
					{
						CommandService.ExecuteCommand(keysToCommands[key]);
						ev.Handled = true;
					}
				}
			}
		}
	}
}
