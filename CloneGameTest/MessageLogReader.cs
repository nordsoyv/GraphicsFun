using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using CloneGame.Messaging;

namespace CloneGameTest
{
	class MessageLogReader
	{

		public List<string> log;


		public MessageLogReader()
		{

			var commandMessages = MessageService.GetInstance().Messages.Where(mes => mes.MessageType == MessageType.Log);
			commandMessages.Subscribe(WriteLogMessages);
			log = new List<string>();
		}



		private  void WriteLogMessages(Message value)
		{
			log.Add(value.Text);
		}




	}
}
