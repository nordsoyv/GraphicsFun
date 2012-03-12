using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using CloneGame.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CloneGameTest
{
	[TestClass]
	public class ConsoleVarHandlerTest
	{
		private ConsoleVarHandler _handler;
		private MessageLogReader log;

		[TestInitialize]
		public void Init()
		{

			_handler = ConsoleVarHandler.GetInstance();
			ConsoleVarHandler.ResetVars();
			log = new MessageLogReader();
		}

		[TestMethod]
		public void TestSinglelton()
		{
			
			Assert.IsNotNull(_handler);
		}

		[TestMethod]
		public void TestAddConsoleVar()
		{

			ConsoleVarHandler.SetVar("TEST", "VALUE");
		}

		[TestMethod]
		public void TestListConsoleVarsNotNull()
		{
			IEnumerable<ConsoleVar> vars = ConsoleVarHandler.GetAllVars();
			Assert.IsNotNull(vars);
		}


		[TestMethod]
		public void TestListConsoleVarsSameAsInput()
		{
			ConsoleVarHandler.SetVar("TEST", "VALUE");
			ConsoleVarHandler.SetVar("TEST2", "VALUE2");

			IEnumerable<ConsoleVar> vars = ConsoleVarHandler.GetAllVars();

			ConsoleVar v = vars.ElementAt(0);
			Assert.AreEqual(v.Name, "TEST");
			Assert.AreEqual(v.Value, "VALUE");
			v = vars.ElementAt(1);
			Assert.AreEqual(v.Name, "TEST2");
			Assert.AreEqual(v.Value, "VALUE2");
		}

		[TestMethod]
		public void TestGetConsoleVar()
		{
			ConsoleVarHandler.SetVar("TEST", "VALUE");

			Assert.AreEqual("VALUE", ConsoleVarHandler.GetVar("TEST").Value);
		}

		[TestMethod]
		public void TestUpdateConsoleVar()
		{
			ConsoleVarHandler.SetVar("TEST", "VALUE");

			Assert.AreEqual("VALUE", ConsoleVarHandler.GetVar("TEST").Value);

			ConsoleVarHandler.SetVar("TEST", "VALUE2");
			Assert.AreEqual("VALUE2", ConsoleVarHandler.GetVar("TEST").Value);
		}

		[TestMethod]
		public void TestSetCommand()
		{

			MessageService.Commandmessage("/set test value");
			Assert.AreEqual("value", ConsoleVarHandler.GetVar("test").Value);
		}



		[TestMethod]
		public void TestEchoCommand()
		{
			MessageService.Commandmessage("/set test value");
			Assert.AreEqual("value", ConsoleVarHandler.GetVar("test").Value);

			MessageService.Commandmessage("/echo test");
			Assert.AreEqual("test : value", log.log.First() );
		}

	}
}
