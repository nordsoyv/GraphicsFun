using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloneGame.Messaging
{
	public class ConsoleVarHandler
	{
		private static ConsoleVarHandler _instance;

		private readonly List<ConsoleVar> _cvars;

		private ConsoleVarHandler()
		{
			_cvars = new List<ConsoleVar>();
			CommandService.RegisterCommand(Commands.SET,  SetVar);
			CommandService.RegisterCommand(Commands.ECHO, EchoVar);
			CommandService.RegisterCommand(Commands.ECHO_ALL, EchoAllVars);
		}

		private void EchoAllVars(Message m)
		{
			foreach (var consoleVar in GetAllVars())
			{
				LogCvar(consoleVar);
			} 
		}

		private static void EchoVar(Message message)
		{
			var items = message.Text.Split(' ');
			var cvar = GetVar(items[1]);
			if(cvar != null)
			{
				LogCvar(cvar);
			}
		}

		private static void LogCvar(ConsoleVar cvar )
		{
			MessageService.LogMessage(cvar.Name + " : " + cvar.Value);
		}

		static public ConsoleVarHandler GetInstance()
		{
			return _instance ?? (_instance = new ConsoleVarHandler());
		}

		private static void SetVar(Message m)
		{
			var text = m.Text;
			var items = text.Split(' ');
			var cvarName = items[1];
			var cvarValue = items[2];

			SetVar(cvarName,cvarValue);

		}


		public static void SetVar(string name, string value)
		{
			if (GetInstance()._cvars.Where(c => c.Name == name).Any())
			{
				var cvar = GetInstance()._cvars.Where(c => c.Name == name).First();
				cvar.Value = value;
			}
			else
			{
				ConsoleVar v = new ConsoleVar();
				v.Name = name;
				v.Value = value;
				GetInstance()._cvars.Add(v);
			}
		}

		public static IEnumerable<ConsoleVar> GetAllVars()
		{
			return GetInstance()._cvars.ToList();
		}

		public static ConsoleVar GetVar(string name)
		{
			if (GetInstance()._cvars.Where(c => c.Name == name).Any())
			{
				return GetInstance()._cvars.Where(c => c.Name == name).Select(c => c).First();
			}
			return null;
		}

		public static void ResetVars()
		{
			GetInstance()._cvars.Clear();
		}
	}
}
