using System;
using System.Collections.Generic;
using System.Linq;

namespace CloneGame.Messaging
{
	public enum MessageType
	{
		Log,
		Debug,
		Command
	}

	public class  Message
	{
		public string Text { get; set; }
		public MessageType MessageType { get; set; }
		public DateTime Time { get; set; }

	}

	public class MessageService
	{
		static private MessageService instance;
		private Channel channel;
		public IObservable<Message> Messages { get { return channel; }   }

		private MessageService()
		{
			channel = new Channel();
		}

		public static MessageService GetInstance()
		{
			return instance ?? (instance = new MessageService());
		}


		public static void SendMessage(Message m)
		{
			instance.channel.SendMessage(m);
		}


		public static void LogMessage(string text)
		{
			var m = new Message();
			m.MessageType = MessageType.Log;
			m.Text = text;
			m.Time = DateTime.Now;
			instance.channel.SendMessage(m);
		}

		public static void DebugMessage(string text)
		{
			var m = new Message();
			m.MessageType = MessageType.Debug;
			m.Text = text;
			m.Time = DateTime.Now;
			instance.channel.SendMessage(m);
		}

		public static void Commandmessage(string text)
		{
			var m = new Message();
			if (text.First() == '/')
			{
				m.MessageType = MessageType.Command;
				m.Text = text;
				m.Time = DateTime.Now;
				GetInstance().channel.SendMessage(m);
			}
			else
			{
				throw new ArgumentException("Commands need to start with /");
			}
		}


	}

	
	class Channel : IObservable<Message>
	{
		

		private List<IObserver<Message>> observers;

		public Channel()
		{
			observers = new List<IObserver<Message>>();
		}

		public IDisposable Subscribe(IObserver<Message> observer)
		{
			if (!observers.Contains(observer))
				observers.Add(observer);
			return new Unsubscriber(observers, observer);
		}

		private class Unsubscriber : IDisposable
		{
			private List<IObserver<Message>> _observers;
			private IObserver<Message> _observer;

			public Unsubscriber(List<IObserver<Message>> observers, IObserver<Message> observer)
			{
				this._observers = observers;
				this._observer = observer;
			}

			public void Dispose()
			{
				if (_observer != null && _observers.Contains(_observer))
					_observers.Remove(_observer);
			}
		}



		public void SendMessage(Message m)
		{
			if (observers != null)
			{
				foreach (var observer in observers)
				{
					observer.OnNext(m);
				}
			}
		}
		
	}


}
