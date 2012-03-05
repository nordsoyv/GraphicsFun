using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CloneGame.Input
{
    
	class KeyboardEvent
	{
		public bool Handled { get; set; }
		public GameTime Time { get; set; }
		public bool Shift { get; set; }
		public bool Ctrl { get; set; }
		public bool Alt { get; set; }
	
	}
	
	class KeybuttonEvent : KeyboardEvent
    {

		public enum KeybuttonEventType
    	{
			Pressed,
			Released,
			Held
    	}

		public Keys Key { get; set; }
    	public KeybuttonEventType EventType { get; set; }

        public KeybuttonEvent(Keys key,GameTime gameTime, KeybuttonEventType type, bool shiftPressed, bool altPressed, bool ctrlPressed)
        {
            Key = key;
            Time = gameTime;
        	Shift = shiftPressed;
        	Ctrl = ctrlPressed;
        	Alt = altPressed;
        	Handled = false;
        	EventType = type;
        }

    }

	class CharButtonEvent : KeyboardEvent
	{

		public char Key { get; set; }
		public CharButtonEvent(CharacterEventArgs args)
		{
			Key = args.Character;
			Alt = args.AltPressed;
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
