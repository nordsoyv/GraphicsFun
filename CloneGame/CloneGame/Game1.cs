using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CloneGame
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{


		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private GraphicsDevice device;
		private Effect effect;
		private GeneratingLeafNode node;
		private Camera camera;

		//private List<GeneratingLeafNode> nodes;
		//private IGenerator _generator;

		private Landscape landscape;

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
			graphics.PreferredBackBufferWidth = 500;
			graphics.PreferredBackBufferHeight = 500;
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
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(device);
			effect = Content.Load<Effect>("effects");
			camera = new Camera(device);
			camera.Position = new Vector3(30,30, -40);
			camera.Lookat = new Vector3(0, 0, 0);
			landscape = new Landscape(device);
			landscape.GenerateLandscape();


			// TODO: use this.Content to load your game content here
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
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			if (Keyboard.GetState().IsKeyDown(Keys.N))
			{
				landscape.GenerateLandscape();
			}



			// TODO: Add your update logic here



			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
			//device.Clear(Color.Black);
			RasterizerState rs = new RasterizerState();
			//rs.CullMode = CullMode.CullClockwiseFace;
			//	rs.FillMode = FillMode.WireFrame;
			device.RasterizerState = rs;


			effect.CurrentTechnique = effect.Techniques["Colored"];
			effect.Parameters["xView"].SetValue(camera.GetViewMatrix());
			effect.Parameters["xProjection"].SetValue(camera.GetProjectionMatrix());
			effect.Parameters["xWorld"].SetValue(Matrix.Identity);
			Vector3 lightDirection = new Vector3(-1.0f, -2.0f, 4.0f);
			lightDirection.Normalize();
			effect.Parameters["xLightDirection"].SetValue(lightDirection);
			effect.Parameters["xAmbient"].SetValue(0.3f);
			effect.Parameters["xEnableLighting"].SetValue(true);


			foreach (EffectPass pass in effect.CurrentTechnique.Passes)
			{
				pass.Apply();

				landscape.Draw(gameTime);
			}

			// TODO: Add your drawing code here


			base.Draw(gameTime);
		}



		



		}
	}




