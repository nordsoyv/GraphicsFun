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

		private readonly IDictionary<string, Action> _commands;

		private CommandService	()
		{
			_commands = new Dictionary<string, Action>();

		}

		public static CommandService GetInstance()
		{
			return _instance ?? (_instance = new CommandService());
		}

		public static void RegisterCommand(string commandName, Action action)
		{
			GetInstance()._commands.Add(commandName, action);
		}

		public static void ExecuteCommand(string commandName)
		{
			if (GetInstance()._commands.ContainsKey(commandName))
			{
				GetInstance()._commands[commandName].Invoke();
			}
		}
	}
}
