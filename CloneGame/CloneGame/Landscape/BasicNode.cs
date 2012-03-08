using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame.Landscape
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



}