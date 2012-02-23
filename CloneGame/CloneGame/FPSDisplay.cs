using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame
{
	class FPSDisplay
	{
		private SpriteFont font;
		private SpriteBatch spriteBatch;

		public FPSDisplay(GraphicsDevice device, SpriteFont font)
		{
			this.font = font;
			spriteBatch = new SpriteBatch(device);
		}


		public void Draw(GameTime gameTime)
		{


			spriteBatch.Begin();

			// Draw Hello World
			string fps = (1000.0f /gameTime.ElapsedGameTime.Milliseconds).ToString();
			string output = "Hello World";

			Vector2 fontPos = new Vector2(0, 0);

			
			spriteBatch.DrawString(font, fps, fontPos, Color.White);

			spriteBatch.End();



		}
	}
}
