using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;


namespace CloneGame
{
    class KeyboardEvent
    {
        public Keys Key { get; set; }
        public GameTime Time { get; set; }

        public KeyboardEvent(Keys key, GameTime gametime)
        {
            Key = key;
            Time = gametime;
        }

    }

    class MouseEvent
    {
        public GameTime Time { get; set; }


    }
}
