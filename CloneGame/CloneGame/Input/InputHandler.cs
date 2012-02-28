using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CloneGame.Input
{
	class InputHandler
	{

		private List<IKeyboardEventReciver> keyboardRecivers;
		private List<IMouseEventReciver> mouseRecivers;
		private List<ICharEventReciver> charRecivers;

		private KeyboardState oldState;

		private int lastMouseX, lastMouseY;

		private List<CharButtonEvent> charEvents;

		private List<KeybuttonEvent> keyboardEvents;

		public InputHandler(GameWindow window)
		{
			keyboardRecivers = new List<IKeyboardEventReciver>();
			mouseRecivers = new List<IMouseEventReciver>();
			charRecivers = new List<ICharEventReciver>();

			oldState = Keyboard.GetState();
			lastMouseY = lastMouseX = 0;

			InputSystem.Initialize(window);

			InputSystem.CharEntered += CharEventHandler;
			InputSystem.KeyDown += KeyboardDownEventHandler;
			InputSystem.KeyUp += KeyboardUpEventHandler;
			//InputSystem.MouseDoubleClick += MouseEventHandler;

			charEvents = new List<CharButtonEvent>();
			keyboardEvents = new List<KeybuttonEvent>();

		}

		private void MouseEventHandler(object  sender, MouseEventArgs eventArgs)
		{
			
		}

		private void KeyboardDownEventHandler(object  sender, KeyEventArgs eventArgs)
		{
			Console.WriteLine(eventArgs.KeyCode + "  DOWN ");
		/*	var e = new KeybuttonEvent(eventArgs.KeyCode);
			e.EventType = KeybuttonEvent.KeybuttonEventType.Pressed;
			e.Alt = InputSystem.AltDown;
			e.Ctrl = InputSystem.CtrlDown;
			e.Shift = InputSystem.ShiftDown;
			
			keyboardEvents.Add(e);
			*/
		}


		private void KeyboardUpEventHandler(object sender, KeyEventArgs eventArgs)
		{
			Console.WriteLine(eventArgs.KeyCode + "  UP " );
		}


		private void CharEventHandler(object sender, CharacterEventArgs eventArgs)
		{
			Console.WriteLine(eventArgs.Character + " " + eventArgs.RepeatCount);
			var e = new CharButtonEvent(eventArgs);
			charEvents.Add(e);

		}

		public void RegisterKeyboardEventReciver(IKeyboardEventReciver r)
		{
			keyboardRecivers.Add(r);
		}

		

		public void RegisterCharEventReciver(ICharEventReciver r)
		{
			charRecivers.Add(r);
		}

	public void RegisterMouseEventReciver(IMouseEventReciver r)
		{
			mouseRecivers.Add(r);
		}



		public void HandleInput(GameTime gametime)
		{
			HandleKeyboardInput(gametime);
			HandleMouseInput(gametime);
		}

		private void HandleMouseInput(GameTime gameTime)
		{
			List<MouseEvent> mouseEvents = new List<MouseEvent>();
			var state = Mouse.GetState();
			if (state.X != lastMouseX || state.Y != lastMouseY) // movement
			{
				MouseEvent mEvent = new MouseEvent(gameTime);
				mEvent.EventType = MouseEvent.MouseEventType.Movement;
				mEvent.XMovement = state.X - lastMouseX;
				mEvent.YMovement = state.Y - lastMouseY;
				lastMouseY = state.Y;
				lastMouseX = state.X;
				mouseEvents.Add(mEvent);
				
				Console.WriteLine(lastMouseX + " " + lastMouseY + ", " + state.X + " " + state.Y + ", " + mEvent.XMovement+ ", " + mEvent.YMovement);
			}

			if (mouseEvents.Count > 0)
			{
				foreach (var reciver in mouseRecivers)
				{
					reciver.HandleEvent(mouseEvents);
				}

			}


		}

		private void HandleKeyboardInput(GameTime gametime)
		{
			foreach (var charEvent in charEvents)
			{
				charEvent.Time = gametime;
			}
			if (charEvents.Count > 0)
			{
				foreach (var charEventReciver in charRecivers)
				{
					charEventReciver.HandleEvent(charEvents);
				}
			
				charEvents.Clear();
			}
			
			var newState = Keyboard.GetState();
			var keys = newState.GetPressedKeys();
			bool altPressed = Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt);
			bool shiftPressed = Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift);
			bool ctrlPressed = Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.RightControl);
			
			foreach (var key in keys)
			{
				if(oldState.IsKeyDown(key)  ) // pressed before , this is a holding event
				{
					keyboardEvents.Add(new KeybuttonEvent(key, gametime,KeybuttonEvent.KeybuttonEventType.Held, shiftPressed, altPressed, ctrlPressed));	
				}else // New push
				{
					keyboardEvents.Add(new KeybuttonEvent(key, gametime,KeybuttonEvent.KeybuttonEventType.Pressed, shiftPressed, altPressed, ctrlPressed));	
				}
				
			}

			
			oldState = newState;
			
			foreach (var reciver in keyboardRecivers)
			{
				reciver.HandleEvent(keyboardEvents);
				
				//keyboardEvents.RemoveAll(e => e.Handled == true);
			}

		
		}
	}
}
