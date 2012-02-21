using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame
{
	class Camera
	{
		private GraphicsDevice device;

		public Camera(GraphicsDevice device)
		{
			this.device = device;
		}

		public Vector3 Position { get; set; }
		public Vector3 Lookat { get; set; }
		public Quaternion Heading { get; set; }

		public Matrix GetViewMatrix()
		{
			Vector3 camup = new Vector3(0, 1, 0);
			camup = Vector3.Transform(camup, Matrix.CreateFromQuaternion(Heading));
			Vector3 camLookat = new Vector3(0, 0, 1);
			//Quaternion dir = new Quaternion(camLookat, 0);

			camLookat = Vector3.Transform(camLookat, Matrix.CreateFromQuaternion( Heading));
			return Matrix.CreateLookAt(Position, camLookat + Position, camup);
			//Matrix.CreateLookAt()
			//return Matrix.CreateLookAt(Position, Lookat, new Vector3(0, 1, 0));
		}

		public Matrix GetProjectionMatrix()
		{
			return Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, device.Viewport.AspectRatio, 1.0f, 1000.0f);
		}

		public void Registerplayer(Player p)
		{
			p.Changed += new PlayerInfoChangedHandler(PlayerChanged);
		}

		private void PlayerChanged(object sender, PlayerInfoChangedHandlerEventArgs playerInfoChangedHandlerEventArgs)
		{
			this.Position = playerInfoChangedHandlerEventArgs.Position;
			this.Heading = playerInfoChangedHandlerEventArgs.Heading;
		}

		//
		//	projectionMatrix = 

	}
}
