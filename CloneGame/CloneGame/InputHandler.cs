using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CloneGame
{
    class InputHandler
    {

        private List<KeyboardEventReciver> recivers;

        public InputHandler()
        {
            recivers = new List<KeyboardEventReciver> ();
        }

        public void RegisterEventReciver(KeyboardEventReciver r)
        {
            recivers.Add(r);
        }

        public void HandleInput(GameTime gametime)
        {
            var keys = Keyboard.GetState().GetPressedKeys();
            List<KeyboardEvent> keyboardEvents = new List<KeyboardEvent>();
            foreach (var key in keys)
            {
                keyboardEvents.Add(new KeyboardEvent(key, gametime));
            }

            foreach (var reciver in recivers)
            {
                reciver.HandleEvent(keyboardEvents);
            }

        }
    }
}
