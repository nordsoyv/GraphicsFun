using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CloneGame.Input
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
        public enum MouseEventType
        {
        	Click,
			Drag,
			Movement
        }
		
		public GameTime Time { get; set; }
    	public MouseEventType EventType { get; set; }
		public int XMovement { get; set; }
		public int YMovement { get; set; }


    	public MouseEvent(GameTime gameTime)
    	{
    		this.Time = gameTime;
    	}
    }
}
