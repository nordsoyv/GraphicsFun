using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;

namespace CloneGame.Input
{
	class ConsoleOutMessageObserver 
	{
		public ConsoleOutMessageObserver()
		{
			var logMessages = MessageService.GetInstance().Messages.Where(mes => mes.MessageType == MessageType.Log);

			logMessages.Subscribe(WriteLogMessage);

			var commandMessages = MessageService.GetInstance().Messages.Where(mes => mes.MessageType == MessageType.Command);
			commandMessages.Subscribe(WriteCommandMessages);
		}


		private static void WriteLogMessage(Message value)
		{
			Console.Out.WriteLine("LOG: " + value.Text);
		}

		private static void WriteCommandMessages(Message value)
		{
			Console.Out.WriteLine("COMMAND: " + value.Text);
		}

	}
}
