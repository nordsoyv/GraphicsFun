using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame
{
	class BasicNode : INode
	{
		protected VertexBuffer vertexBuffer;
		protected int numVertices;
		protected GraphicsDevice device;
		protected List<BoundingBox> boundingBoxes;
		protected Vector3 location;
		protected int sideSize;

		public virtual void Draw(GameTime time)
		{
			if (numVertices <= 0) return;
			device.SetVertexBuffer(vertexBuffer);
			device.DrawPrimitives(PrimitiveType.TriangleList, 0, numVertices / 3);
		}


	}

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