using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame.Landscape
{
	class LandscapeNode : BasicNode
	{
		private int numTriangles;
		private int startBufferPos;
		public LandscapeNode(GraphicsDevice device, Vector3 pos, int size)
		{
			this.device = device;
			location = pos;
			sideSize = size;
			boundingBoxes = new List<BoundingBox>();
		}
		public void SetData(int startBufferPos, int numTris, List<BoundingBox> boundingBoxs)
		{
			this.startBufferPos = startBufferPos;
			this.numTriangles = numTris;
			boundingBoxes = boundingBoxs;
		}

		public override void Draw(GameTime time)
		{

			device.DrawPrimitives(PrimitiveType.TriangleList, startBufferPos, numTriangles);
			//base.Draw(time);
		}



	}
}