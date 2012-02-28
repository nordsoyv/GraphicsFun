using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;

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
		//	InputSystem.KeyDown += KeyboardDownEventHandler;
		//	InputSystem.KeyUp += KeyboardUpEventHandler;
			//InputSystem.MouseDoubleClick += MouseEventHandler;

			charEvents = new List<CharButtonEvent>();
			keyboardEvents = new List<KeybuttonEvent>();

		}

		private void MouseEventHandler(object sender, MouseEventArgs eventArgs)
		{

		}

		private void KeyboardDownEventHandler(object sender, KeyEventArgs eventArgs)
		{
//			Console.WriteLine(eventArgs.KeyCode + "  DOWN ");
			KeybuttonEvent e = new KeybuttonEvent(eventArgs.KeyCode, null, KeybuttonEvent.KeybuttonEventType.Pressed,
			                                      InputSystem.ShiftDown, InputSystem.AltDown, InputSystem.CtrlDown);
			keyboardEvents.Add(e);
		}


		private void KeyboardUpEventHandler(object sender, KeyEventArgs eventArgs)
		{
			KeybuttonEvent e = new KeybuttonEvent(eventArgs.KeyCode, null, KeybuttonEvent.KeybuttonEventType.Released,
												  InputSystem.ShiftDown, InputSystem.AltDown, InputSystem.CtrlDown);
			keyboardEvents.Add(e);
		}


		private void CharEventHandler(object sender, CharacterEventArgs eventArgs)
		{
			charEvents.Add(new CharButtonEvent(eventArgs));
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

				Console.WriteLine(lastMouseX + " " + lastMouseY + ", " + state.X + " " + state.Y + ", " + mEvent.XMovement + ", " + mEvent.YMovement);
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
			HandleKeyboardCharEvents(gametime);
			HandleKeyboardPollingEvents(gametime);
		}

		private void HandleKeyboardPollingEvents(GameTime gametime)
		{
			var newState = Keyboard.GetState();
			var keys = newState.GetPressedKeys();
			bool altPressed = Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt);
			bool shiftPressed = Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift);
			bool ctrlPressed = Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.RightControl);

			foreach (var key in keys)
			{
				if (oldState.IsKeyDown(key)) // pressed before , this is a holding event
				{
					keyboardEvents.Add(new KeybuttonEvent(key, gametime, KeybuttonEvent.KeybuttonEventType.Held, shiftPressed, altPressed, ctrlPressed));
				}
				else // New push
				{
					keyboardEvents.Add(new KeybuttonEvent(key, gametime, KeybuttonEvent.KeybuttonEventType.Pressed, shiftPressed, altPressed, ctrlPressed));
				}

			}


			oldState = newState;

			foreach (var reciver in keyboardRecivers)
			{
				reciver.HandleEvent(keyboardEvents.Where(e=>e.Handled == false));
			}
			keyboardEvents.Clear();
		}

		private void HandleKeyboardCharEvents(GameTime gametime)
		{
			if (charEvents.Count > 0)
			{
				foreach (var charEvent in charEvents)
				{
					charEvent.Time = gametime;
				} 
				foreach (var charEventReciver in charRecivers)
				{
					charEventReciver.HandleEvent(charEvents);
				}

				charEvents.Clear();
			}
		}
	}
}
