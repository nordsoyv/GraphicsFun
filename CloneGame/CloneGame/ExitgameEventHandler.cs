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

        public void HandleEvent(IEnumerable<KeybuttonEvent> events)
        {
            var esc = events.Where(e=>e.Handled == false).Where(e => e.Key == Microsoft.Xna.Framework.Input.Keys.Escape).Select(e=>e);
            // Allows the game to exit
            if (esc.Count() > 0)
            {
            	esc.First().Handled = true;
                game.Exit();

            }
        }

        public ExitgameEventHandler( Game1 game)
        {
            this.game = game;
        }
    }
}
