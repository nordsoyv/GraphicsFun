using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CloneGame
{
	public delegate void PlayerInfoChangedHandler(object sender, PlayerInfoChangedHandlerEventArgs e);

	public class PlayerInfoChangedHandlerEventArgs : EventArgs
	{
		public Vector3 Position { get; set; }
		public Quaternion Heading { get; set; }
	}

	class Player
	{
		private Vector3 position;
		private Quaternion heading;

		public Vector3 Position
		{
			get { return position; }
			set
			{
				position = value;
				PlayerInfoChangedHandlerEventArgs e = new PlayerInfoChangedHandlerEventArgs();
				e.Heading = heading;
				e.Position = position;
				OnChanged(e);
			}
		}
		public Quaternion Heading
		{
			get { return heading; }
			set
			{
				heading = value;
				PlayerInfoChangedHandlerEventArgs e = new PlayerInfoChangedHandlerEventArgs();
				e.Heading = heading;
				e.Position = position;
				OnChanged(e);
			}
		}

		public event PlayerInfoChangedHandler Changed;


		// Invoke the Changed event; called whenever player changes
		protected virtual void OnChanged(PlayerInfoChangedHandlerEventArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}


		public void GetInput(KeyboardState state)
		{


			Vector3 movementVector = new Vector3(0);
			if (state.IsKeyDown(Keys.W))
			{
				movementVector += new Vector3(0, 0, 0.1f);
			}
			if (state.IsKeyDown(Keys.S))
			{
				movementVector += new Vector3(0, 0, -0.1f);
			}

			if (state.IsKeyDown(Keys.A))
			{
				movementVector += new Vector3(0.1f, 0, 0);
			}

			if (state.IsKeyDown(Keys.D))
			{
				movementVector += new Vector3(-0.1f, 0, 0);
			}
			if (state.IsKeyDown(Keys.Space))
			{
				movementVector += new Vector3(0, 0.1f, 0);
			}
			if (state.IsKeyDown(Keys.LeftAlt))
			{
				movementVector += new Vector3(0, -0.1f, 0);
			}

			movementVector = Vector3.Transform(movementVector, Matrix.CreateFromQuaternion(heading));

			Position += movementVector;



		}

		public void GetInput(MouseState state)
		{
			int x =state.X;
			int y = state.Y;
			if(x != 0)
			{
				
			}
			if(y != 0)
			{
				
			}

		}
	}
}
