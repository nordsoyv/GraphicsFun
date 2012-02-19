using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame
{
	struct MyOwnVertexFormat
	{
		public Vector3 position;
		private Vector3 normal; 
		public Color color;
		
//		private Vector2 texCoord;
		

		public MyOwnVertexFormat(Vector3 position, Vector3 normal, Color color)
		{
			this.position = position;
			this.color = color;
//			this.texCoord = texCoord;
			this.normal = normal;
		}

		public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
		(
			new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
			new VertexElement(sizeof(float) * 3 , VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
			new VertexElement(sizeof(float) * (3+3), VertexElementFormat.Color, VertexElementUsage.Color, 0)
//			new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
			
		);
	}


}