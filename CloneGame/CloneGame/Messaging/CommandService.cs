using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace CloneGame.Messaging
{
	class CommandService
	{
		private static CommandService _instance;

		private readonly IDictionary<string, Action<Message>> _commands;

		private CommandService()
		{
			_commands = new Dictionary<string, Action<Message>>();
			var commandStream = MessageService.GetInstance().Messages.Where(m => m.MessageType == MessageType.Command);
			IDisposable disposable = commandStream.Subscribe(ExecuteCommand);
		}

		private static void ExecuteCommand(Message command)
		{
			if (GetInstance()._commands.ContainsKey(command.Text))
			{
				GetInstance()._commands[command.Text].Invoke(command);
			}
		}

		public static CommandService GetInstance()
		{
			return _instance ?? (_instance = new CommandService());
		}

		public static void RegisterCommand(string commandName, Action<Message> action)
		{
			GetInstance()._commands.Add(commandName, action);
		}
		/*
		private static void ExecuteCommand(string commandName)
		{
			if (GetInstance()._commands.ContainsKey(commandName))
			{
				GetInstance()._commands[commandName].Invoke();
			}
		}
		 * */
	}
}
