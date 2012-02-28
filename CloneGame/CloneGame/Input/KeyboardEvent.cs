using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CloneGame.Input
{
    class KeyboardEvent
    {
    	public enum KeyboardEventType
    	{
			Pressed,
			Released,
			Held
    	}
        
		public Keys Key { get; set; }
        public GameTime Time { get; set; }
		public bool Shift { get; set; }
		public bool Ctrl { get; set; }
		public bool Alt { get; set; }
		public bool Handled { get; set; }
    	public KeyboardEventType EventType { get; set; }

        public KeyboardEvent(Keys key, GameTime gametime, KeyboardEventType type ,  bool shiftPressed , bool altPressed , bool ctrPressed )
        {
            Key = key;
            Time = gametime;
        	Shift = shiftPressed;
        	Ctrl = ctrPressed;
        	Alt = altPressed;
        	Handled = false;
        	EventType = type;
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
