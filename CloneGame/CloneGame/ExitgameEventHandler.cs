using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using CloneGame.Input;
using CloneGame.Messaging;

namespace CloneGame
{
    class ExitgameEventHandler 
    {
        private readonly Game1 _game;

		private void QuitGame(Message m)
		{
			_game.Exit();
		}

        public ExitgameEventHandler( Game1 game)
        {
            _game = game;
/*
        	var commands = from mes in MessageService.GetInstance().Messages
        	               where mes.MessageType == MessageType.Command
        	               where mes.Text == Commands.QUIT
        	               select mes;

        	commands.Subscribe(m => QuitGame());
			*/
        	Action<Message> commandTarget = QuitGame;

			CommandService.RegisterCommand(Commands.QUIT, commandTarget);
        }
    }
}
