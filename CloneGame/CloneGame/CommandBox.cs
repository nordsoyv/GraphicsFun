using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CloneGame
{
    class CommandBox
    {
        private bool hasInput = false;
        private GraphicsDevice device;
        private ContentManager content;
        private Texture2D background;
        private SpriteBatch spriteBatch;

        private int posX, posY;

        private TimeSpan showTime;
        public CommandBox(GraphicsDevice device, ContentManager Content)
        {
            // TODO: Complete member initialization
            this.device = device;
            this.content = Content;
            spriteBatch = new SpriteBatch(device);
            background = content.Load<Texture2D>("commandbox");
            posX = 0;
            posY = device.Viewport.Height - 30;
            showTime = TimeSpan.FromSeconds(0);
        }

        public void GetInput(GameTime gametime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (gametime.TotalGameTime.Subtract(showTime) > TimeSpan.FromSeconds(0.2f))
                {
                    hasInput = !hasInput;
                    showTime = gametime.TotalGameTime;
                }


                
            }
        }


        public void Draw(GameTime gametime)
        {
            if (hasInput)
            {
                Rectangle drawPos = new Rectangle(posX,posY,background.Width,background.Height);
                spriteBatch.Begin();
                spriteBatch.Draw(background, drawPos, Color.White);
                spriteBatch.End();
            }
        }

    }
}
