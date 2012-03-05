using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloneGame.Input;

namespace CloneGame
{
    class ExitgameEventHandler : IKeyboardEventReciver
    {
        private Game1 game;

		public void HandleEvent(IEnumerable<KeyboardEvent> events)
        {

			foreach (var keyboardEvent in events)
			{
				if (typeof(KeybuttonEvent) == keyboardEvent.GetType())
				{
					KeybuttonEvent e = (KeybuttonEvent) keyboardEvent;
					if(e.Key == Microsoft.Xna.Framework.Input.Keys.Escape)
					{
						e.Handled = true;
						game.Exit();
					}
				}
			}

        }

        public ExitgameEventHandler( Game1 game)
        {
            this.game = game;
        }
    }
}
