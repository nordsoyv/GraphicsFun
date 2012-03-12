using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using CloneGame.Messaging;

namespace CloneGame.Input
{
	class ConsoleOutLogObserver
	{
		public ConsoleOutLogObserver()
		{
			var logMessages = MessageService.GetInstance().Messages.Where(mes => mes.MessageType == MessageType.Log);

			logMessages.Subscribe(WriteLogMessage);
		}


		private static void WriteLogMessage(Message value)
		{
			Console.Out.WriteLine("LOG: " + value.Text);
		}



	}

	class ConsoleOutCommandObserver
	{
		public ConsoleOutCommandObserver()
		{

			var commandMessages = MessageService.GetInstance().Messages.Where(mes => mes.MessageType == MessageType.Command);
			commandMessages.Subscribe(WriteCommandMessages);
		}



		private static void WriteCommandMessages(Message value)
		{
			Console.Out.WriteLine("COMMAND: " + value.Text);
		}

	}
}
