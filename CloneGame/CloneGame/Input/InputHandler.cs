using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CloneGame.Input
{
	class InputHandler
	{

		private List<IKeyboardEventReciver> keyboardRecivers;
		private List<IMouseEventReciver> mouseRecivers;

		private int lastMouseX, lastMouseY;
		public InputHandler()
		{
			keyboardRecivers = new List<IKeyboardEventReciver>();
			mouseRecivers = new List<IMouseEventReciver>();
			lastMouseY = lastMouseX = 0;
		}

		public void RegisterKeyboardEventReciver(IKeyboardEventReciver r)
		{
			keyboardRecivers.Add(r);
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
			var keys = Keyboard.GetState().GetPressedKeys();
			bool altPressed = Keyboard.GetState().IsKeyDown(Keys.LeftAlt) || Keyboard.GetState().IsKeyDown(Keys.RightAlt);
			bool shiftPressed = Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift);
			bool ctrlPressed = Keyboard.GetState().IsKeyDown(Keys.LeftControl) || Keyboard.GetState().IsKeyDown(Keys.RightControl);
			List<KeyboardEvent> keyboardEvents = new List<KeyboardEvent>();
			foreach (var key in keys)
			{
				keyboardEvents.Add(new KeyboardEvent(key, gametime, shiftPressed, altPressed, ctrlPressed));
			}

			foreach (var reciver in keyboardRecivers)
			{
				reciver.HandleEvent(keyboardEvents);
			}
		}
	}
}
