using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace CloneGame.Messaging
{
	public class CommandService
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
			var items = command.Text.Split(' ');
			if (GetInstance()._commands.ContainsKey(items[0]))
			{
				GetInstance()._commands[items[0]].Invoke(command);
			}
		}

		public static CommandService GetInstance()
		{
			return _instance ?? (_instance = new CommandService());
		}

		public static void RegisterCommand(string commandName, Action<Message> action)
		{
			if (GetInstance()._commands.ContainsKey(commandName))
				throw new ArgumentException("Command already registered");
			GetInstance()._commands.Add(commandName, action);
		}
		
	}
}
