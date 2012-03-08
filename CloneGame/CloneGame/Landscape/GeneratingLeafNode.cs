using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame.Landscape
{
	class GeneratingLeafNode : BasicNode
	{
		Random r = new Random();

		public GeneratingLeafNode(GraphicsDevice device, IGenerator generator, Vector3 pos, int size)
		{
			this.device = device;
			location = pos;
			sideSize = size;
			boundingBoxes = new List<BoundingBox>();
			BuildBuffers(generator);

		}

		

		private void BuildBuffers(IGenerator generator)
		{
			List<MyOwnVertexFormat> vertices = new List<MyOwnVertexFormat>();
			
			for (int x = 0; x < sideSize; x++)
			{
				for (int z = 0; z < sideSize; z++)
				{

					for (int y = 0; y < sideSize; y++)
					{
						double sample = generator.Sample(new Vector3(x, y, z) + location);
						if ( sample > 0.6f)
						{
							CreateVertices(vertices, x, y, z, new Color((float)sample, (float)sample, (float)sample));
						}
						
					}
				}
			}

			if(vertices.Count > 0)
			{
				vertexBuffer = new VertexBuffer(device, MyOwnVertexFormat.VertexDeclaration, vertices.Count, BufferUsage.WriteOnly);
				vertexBuffer.SetData(vertices.ToArray());
			}

			numVertices = vertices.Count;
		}


		private void AddTriangle(List<int> indexes, int startIndex, int i1, int i2, int i3)
		{
			indexes.Add(startIndex + i1);
			indexes.Add(startIndex + i2);
			indexes.Add(startIndex + i3);
		}

		private void AddTriangle(List<MyOwnVertexFormat> vertices, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 normal, Color color)
		{
			vertices.Add(new MyOwnVertexFormat(v0, normal, color));
			vertices.Add(new MyOwnVertexFormat(v1, normal, color));
			vertices.Add(new MyOwnVertexFormat(v2, normal, color));
		}

		private void CreateVertices(List<MyOwnVertexFormat> vertices, float x, float z)
		{
			float y = 0;
			CreateVertices(vertices, x, y, z, Color.White);

		}

		private void CreateVertices(List<MyOwnVertexFormat> vertices, float x, float y, float z, Color color)
		{
			Vector3 pos0 = new Vector3(x, y, z) + location;
			Vector3 pos1 = new Vector3(x + 1.0f, y, z) + location;
			Vector3 pos2 = new Vector3(x, y, z + 1.0f) + location;
			Vector3 pos3 = new Vector3(x + 1, y, z + 1.0f) + location;

			Vector3 pos4 = new Vector3(x, y + 1.0f, z) + location;
			Vector3 pos5 = new Vector3(x + 1.0f, y + 1.0f, z) + location;
			Vector3 pos6 = new Vector3(x, y + 1.0f, z + 1.0f) + location;
			Vector3 pos7 = new Vector3(x + 1.0f, y + 1.0f, z + 1.0f) + location;

			Vector3 normalBottom = new Vector3(0, -1, 0);
			Vector3 normalTop = new Vector3(0, 1, 0);
			Vector3 normalLeft = new Vector3(-1, 0, 0);
			Vector3 normalRight = new Vector3(1, 0, 0);
			Vector3 normalBack = new Vector3(0, 0, -1);
			Vector3 normalFront = new Vector3(0, 0, 1);

			boundingBoxes.Add(new BoundingBox( pos0,pos7));

			
			
			
			//bottom
			AddTriangle(vertices, pos0, pos2, pos1, normalBottom, color);
			AddTriangle(vertices, pos1, pos2, pos3, normalBottom, color);
			//top
			AddTriangle(vertices, pos4, pos5, pos6, normalTop, color);
			AddTriangle(vertices, pos6, pos5, pos7, normalTop, color);
			// left 
			AddTriangle(vertices, pos6, pos2, pos4, normalLeft, color);
			AddTriangle(vertices, pos2, pos0, pos4, normalLeft, color);
			//right
			AddTriangle(vertices, pos1, pos3, pos5, normalRight, color);
			AddTriangle(vertices, pos5, pos3, pos7, normalRight, color);
			//back
			AddTriangle(vertices, pos0, pos1, pos4, normalBack, color);
			AddTriangle(vertices, pos4, pos1, pos5, normalBack, color);

			//front
			AddTriangle(vertices, pos7, pos3, pos6, normalFront, color);
			AddTriangle(vertices, pos6, pos3, pos2, normalFront, color);


		}

		private void CreateIndexes(List<int> indexes, int startIndex)
		{
			//bottom
			AddTriangle(indexes, startIndex, 0, 1, 2);
			AddTriangle(indexes, startIndex, 1, 3, 2);

			//top
			AddTriangle(indexes, startIndex, 4, 6, 5);
			AddTriangle(indexes, startIndex, 6, 7, 5);

			// left
			AddTriangle(indexes, startIndex, 6, 4, 2);
			AddTriangle(indexes, startIndex, 2, 4, 0);

			//right
			AddTriangle(indexes, startIndex, 1, 5, 3);
			AddTriangle(indexes, startIndex, 5, 7, 3);

			//back
			AddTriangle(indexes, startIndex, 0, 4, 1);
			AddTriangle(indexes, startIndex, 4, 5, 1);

			//front
			AddTriangle(indexes, startIndex, 7, 6, 3);
			AddTriangle(indexes, startIndex, 6, 2, 3);


		}




	}
}
