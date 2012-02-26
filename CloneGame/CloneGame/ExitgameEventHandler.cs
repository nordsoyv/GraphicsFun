using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloneGame
{
    class ExitgameEventHandler : KeyboardEventReciver
    {
        private Game1 game;

        public void HandleEvent(List<KeyboardEvent> events)
        {
            var esc = events.Where(e => e.Key == Microsoft.Xna.Framework.Input.Keys.Escape).Select(e=>e);
            // Allows the game to exit
            if (esc.Count() > 0)
            {
                events.Remove(esc.First());
                game.Exit();

            }
        }

        public ExitgameEventHandler( Game1 game)
        {
            this.game = game;
        }
    }
}
