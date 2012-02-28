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
	class CommandBox : ICharEventReciver
	{
		private bool hasInput = false;
		private GraphicsDevice device;
		private ContentManager content;
		private Texture2D background;
		private SpriteBatch spriteBatch;

		private int posX, posY;
		private string command;
		private SpriteFont font;

		private const int marginX = 5;
		private const int marginY = 5;
		private const int MaxLength = 128;

		public CommandBox(GraphicsDevice device, ContentManager Content)
		{
			// TODO: Complete member initialization
			this.device = device;
			this.content = Content;
			spriteBatch = new SpriteBatch(device);
			background = content.Load<Texture2D>("commandbox");
			posX = 0;
			posY = device.Viewport.Height - 30;
			command = "";
			font = Content.Load<SpriteFont>("Console");
		}

		public void Draw(GameTime gametime)
		{
			if (hasInput)
			{
				Rectangle drawPos = new Rectangle(posX, posY, background.Width, background.Height);
				Vector2 pos = new Vector2(posX + marginX, posY + marginY);
				string tmpCommand = command;
				spriteBatch.Begin();
				spriteBatch.Draw(background, drawPos, Color.White);
				while (font.MeasureString(tmpCommand).X >= background.Width - (marginX *2))
				{
					tmpCommand = tmpCommand.Substring(1);
				}

				spriteBatch.DrawString(font, tmpCommand, pos, Color.Gray);	
				
				spriteBatch.End();
			}
		}

		public void HandleEvent(List<CharButtonEvent> events)
		{
			var enterEvent = events.Where(e => e.Key.Equals('\r')).Where(e => e.Handled == false).Select(e => e);
			var backSpace = events.Where(e => e.Key.Equals('\b')).Where(e => e.Handled == false).Select(e => e);
			if (enterEvent.Count() > 0)
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
				enterEvent.First().Handled = true;
			}
			else if (backSpace.Count() > 0)
			{
				if (command.Length > 0)
				{
					command = command.Substring(0, command.Length - 1);
				}
			}
			else
			{
				foreach (var charButtonEvent in events)
				{
					if (command.Length < MaxLength)
					{
						command += charButtonEvent.Key;
					}
				}
			}
		}
	}
}
