using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using CloneGame.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace CloneGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        private const int WindowWidth = 1024;
        private const int WindowHeight = 768;
        private GraphicsDeviceManager graphics;

        private GraphicsDevice device;
        private Effect effect;
        private GeneratingLeafNode node;
        private Camera camera;
        private Player player;
        //private List<GeneratingLeafNode> nodes;
        //private IGenerator _generator;

        private Landscape landscape;

        private SpriteFont Font;

        private FPSDisplay fpsDisplay;

        private CommandBox commandBox;

        private InputHandler inputHandler;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            device = graphics.GraphicsDevice;
          
            Font = Content.Load<SpriteFont>("Console");
            fpsDisplay = new FPSDisplay(device, Font);
            effect = Content.Load<Effect>("effects");
            player = new Player();
            player.Position = new Vector3(0, 0, 0);
            player.Heading = Quaternion.Identity;
            camera = new Camera(device);
            camera.Registerplayer(player);
            landscape = new Landscape(device,effect);
            landscape.GenerateLandscape();
            commandBox = new CommandBox(device, Content);

            inputHandler = new InputHandler(this.Window);
            inputHandler.RegisterKeyboardEventReciver(new ExitgameEventHandler(this));
			inputHandler.RegisterKeyboardEventReciver(commandBox);
            inputHandler.RegisterKeyboardEventReciver(landscape);
			inputHandler.RegisterKeyboardEventReciver(player);
			inputHandler.RegisterMouseEventReciver(player);

           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            ProcessInput(gameTime);
            //Mouse.SetPosition(WindowWidth / 2, WindowHeight / 2);
        	player.Update(gameTime);
            base.Update(gameTime);
        }

        private void ProcessInput(GameTime gametime)
        {
            //player.GetInput(Mouse.GetState().X - WindowWidth / 2, Mouse.GetState().Y - WindowHeight / 2);
            inputHandler.HandleInput(gametime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            landscape.Draw(camera, gameTime);

            fpsDisplay.Draw(gameTime);
            commandBox.Draw(gameTime);
            base.Draw(gameTime);
        }







    }
}




