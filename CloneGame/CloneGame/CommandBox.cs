using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloneGame.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CloneGame
{
	class CommandBox : IKeyboardEventReciver
	{
		private bool hasInput = false;
		private GraphicsDevice device;
		private ContentManager content;
		private Texture2D background;
		private SpriteBatch spriteBatch;

		private int posX, posY;

		private TimeSpan displayTime;

		private string command;
		private SpriteFont font;

		private const int marginX = 5;
		private const int marginY = 5;

		private Keys[] legalKeys = {
    	                           	Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K,
    	                           	Keys.L, Keys.M,Keys.N,Keys.O,Keys.P,Keys.Q, Keys.R, Keys.S,Keys.T,Keys.U,Keys.V,Keys.W,Keys.X,Keys.Y,Keys.Z,Keys.Space

    };

		public CommandBox(GraphicsDevice device, ContentManager Content)
		{
			// TODO: Complete member initialization
			this.device = device;
			this.content = Content;
			spriteBatch = new SpriteBatch(device);
			background = content.Load<Texture2D>("commandbox");
			posX = 0;
			posY = device.Viewport.Height - 30;
			displayTime = TimeSpan.FromSeconds(0);
			command = "";
			font = Content.Load<SpriteFont>("Console");
		}

		public void Draw(GameTime gametime)
		{
			if (hasInput)
			{
				Rectangle drawPos = new Rectangle(posX, posY, background.Width, background.Height);
				spriteBatch.Begin();
				spriteBatch.Draw(background, drawPos, Color.White);
				spriteBatch.DrawString(font, command, new Vector2(posX + marginX, posY + marginY), Color.Black);
				spriteBatch.End();
			}
		}


		public void HandleEvent(List<KeyboardEvent> events)
		{
			var enterEvent = events.Where(e => e.Key == Keys.Enter).Where(e => e.Handled == false).Select(e => e);

			if (enterEvent.Count() > 0)
			{

				var gametime = enterEvent.First().Time;
				if (gametime.TotalGameTime.Subtract(displayTime) > TimeSpan.FromSeconds(0.2f))
				{
					if (hasInput)
					{
						hasInput = false;
						command = "";
					}
					else
					{
						hasInput = true;
					}


					displayTime = gametime.TotalGameTime;
				}
				enterEvent.First().Handled = true;
			}
			else
			{
				if (hasInput)
				{
					foreach (var keyboardEvent in events.Where(e => e.Handled == false).Where(e => e.EventType == KeyboardEvent.KeyboardEventType.Pressed))
					{
						if(legalKeys.Contains(keyboardEvent.Key))
						{
							string letter = keyboardEvent.Key.ToString().ToLower();
							if (keyboardEvent.Shift)
							{
								letter = letter.ToUpper();
							}
							command += letter;
							keyboardEvent.Handled = true;
						}

						if (keyboardEvent.Key.ToString().Length == 1 && keyboardEvent.Key.ToString().CompareTo("A") >= 0 && keyboardEvent.Key.ToString().CompareTo("Z") <= 0)
						{
						}
					}


				}
			}

		}
	}
}
