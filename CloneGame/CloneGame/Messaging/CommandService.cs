using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloneGame.Messaging
{
	class CommandService
	{
		private static CommandService _instance;

		private System.Collections.Generic.IDictionary<string, Action> commands;

		private CommandService	()
		{
			commands = new Dictionary<string, Action>();

		}

		public static CommandService GetInstance()
		{
			return _instance ?? (_instance = new CommandService());
		}

		public static void RegisterCommand(string commandName, Action action)
		{
			GetInstance().commands.Add(commandName, action);
		}

		public static void ExecuteCommand(string commandName)
		{
			if (GetInstance().commands.ContainsKey(commandName))
			{
				GetInstance().commands[commandName].Invoke();
			}
		}
	}
}
