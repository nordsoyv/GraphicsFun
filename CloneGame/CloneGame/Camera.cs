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

		public Camera( GraphicsDevice device)
		{
			this.device = device;
		}

		public Vector3  Position { get; set; }
		public Vector3 Lookat { get; set; }

		public Matrix  GetViewMatrix()
		{
			return Matrix.CreateLookAt(Position, Lookat, new Vector3(0, 1, 0));
		}

		public Matrix GetProjectionMatrix()
		{
			return Matrix.CreatePerspectiveFieldOfView( MathHelper.PiOver4, device.Viewport.AspectRatio, 1.0f,1000.0f);
		}


			//
		//	projectionMatrix = 
		
	}
}
