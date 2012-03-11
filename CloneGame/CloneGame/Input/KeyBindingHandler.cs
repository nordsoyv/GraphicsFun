using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloneGame.Messaging;

namespace CloneGame.Input
{
	class KeyBindingHandler : IKeyboardEventReciver
	{

		private readonly IDictionary<string, string> _keysToCommands;

		public KeyBindingHandler()
		{
			_keysToCommands = new Dictionary<string, string>();
			AddKeyBinding("n", Commands.NEW_LANDSCAPE);
			AddKeyBinding("N", Commands.NEW_LANDSCAPE);
			AddKeyBinding("Escape", Commands.QUIT);
		}

		public void AddKeyBinding(string key, string command)
		{
			_keysToCommands.Add(key, command);
		}

		public void HandleEvent(IEnumerable<KeyboardEvent> events)
		{
			foreach (var keyboardEvent in events)
			{
				if(typeof(KeybuttonEvent) == keyboardEvent.GetType())
				{
					var ev = (KeybuttonEvent)keyboardEvent;
					var key = ev.Key.ToString();
					if (_keysToCommands.ContainsKey(key))
					{
						MessageService.Commandmessage(_keysToCommands[key]);

					//CommandService.ExecuteCommand(_keysToCommands[key]);
						ev.Handled = true;
					}
				}
			}
		}
	}
}
