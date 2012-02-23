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


		private float yaw;
		private float pitch;

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


			if (state.IsKeyDown(Keys.W))
			{
				MoveZ(0.1f);
			}
			if (state.IsKeyDown(Keys.S))
			{
				MoveZ(-0.1f);
			}

			if (state.IsKeyDown(Keys.A))
			{
				MoveX(0.1f);
			}

			if (state.IsKeyDown(Keys.D))
			{
				MoveX(-0.1f);
			}
			if (state.IsKeyDown(Keys.Space))
			{
				MoveY(0.1f);
			}
			if (state.IsKeyDown(Keys.LeftAlt))
			{
				MoveY(-0.1f);
			}




		}

		private void MoveX(float amount)
		{
			Position += Vector3.Transform(new Vector3(amount, 0.0f, 0.0f), Heading);
		}


		private void MoveY(float amount)
		{
			Position += new Vector3(0, amount, 0);
		}


		private void MoveZ(float amount)
		{
			Position += Vector3.Transform(new Vector3(0.0f, 0.0f, amount), Heading);
		}

		private void RotateZ(float xrmod)
		{
			pitch += xrmod;
			if (pitch > MathHelper.PiOver2) pitch = MathHelper.PiOver2;
			if (pitch < -MathHelper.PiOver2) pitch = -MathHelper.PiOver2;
		}

		private void RotateX(float zrmod)
		{

			yaw += zrmod;
			yaw = yaw % MathHelper.TwoPi;
		}


		public void GetInput(int x, int y)
		{
			Console.Out.WriteLine("(X,Y) : (" + x + "," + y + ")");
			Console.WriteLine("Pitch, Yaw : " + pitch + " , " + yaw);
			if (x != 0)
			{
				RotateX(x * -0.005f);
			}
			if (y != 0)
			{
				RotateZ(y * -0.005f);
			}

			Heading = Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0);
			Heading.Normalize();

		}

	}
}
