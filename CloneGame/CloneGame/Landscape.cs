using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloneGame.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CloneGame
{
	class Landscape : IKeyboardEventReciver
	{


		private const int NODE_SIZE = 4;
		private List<LandscapeNode> _nodes;


		private GraphicsDevice _device;
		private PerlinGenerator _generator;

		private VertexBuffer buffer;

		List<MyOwnVertexFormat> vertices = new List<MyOwnVertexFormat>();
        private Effect _effect;
		public Landscape(GraphicsDevice device, Effect effect)
		{
			_device = device;
			_generator = new PerlinGenerator();
            _effect = effect;
		}

		public void GenerateLandscape()
		{
			Random r = new Random();
			_generator.Seed = r.Next();
			_nodes = new List<LandscapeNode>();
			CreateNodes();
		}


		public void Draw(Camera camera, GameTime gameTime)
		{
            _device.BlendState = BlendState.Opaque;
            _device.DepthStencilState = DepthStencilState.Default;

            _effect.CurrentTechnique = _effect.Techniques["Colored"];
            _effect.Parameters["xView"].SetValue(camera.GetViewMatrix());
            _effect.Parameters["xProjection"].SetValue(camera.GetProjectionMatrix());
            _effect.Parameters["xWorld"].SetValue(Matrix.Identity);
            Vector3 lightDirection = new Vector3(-1.0f, -2.0f, 4.0f);
            lightDirection.Normalize();
            _effect.Parameters["xLightDirection"].SetValue(lightDirection);
            _effect.Parameters["xAmbient"].SetValue(0.3f);
            _effect.Parameters["xEnableLighting"].SetValue(true);

            _device.SetVertexBuffer(buffer);
			
            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (var leafNode in _nodes)
                {
                    leafNode.Draw(gameTime);
                }
            }





					}

		void CreateNodes()
		{
			int cubeSize = 4;
			
			float startX = -(cubeSize/2)  * NODE_SIZE;
			float startZ = -(cubeSize / 2) * NODE_SIZE;
			float startY = -(cubeSize / 2) * NODE_SIZE;
			
			

			for (int x = 0; x < cubeSize; x++)
			{
				for (int y = 0; y < cubeSize; y++)
				{
					for (int z = 0; z < cubeSize; z++)
					{
						//_nodes.Add(new GeneratingLeafNode(_device, _generator,
//						                                  new Vector3(startX + NODE_SIZE*x, startY + NODE_SIZE*y, startZ + NODE_SIZE*z),
	//					                                  NODE_SIZE));

						BuildBuffers(new Vector3(startX + NODE_SIZE * x, startY + NODE_SIZE * y, startZ + NODE_SIZE * z));
					}
				}
			}

			buffer = new VertexBuffer(_device, MyOwnVertexFormat.VertexDeclaration, vertices.Count, BufferUsage.WriteOnly);
			buffer.SetData(vertices.ToArray());


		}



		private void BuildBuffers(Vector3 location)
		{
			
			List<BoundingBox> boundingBoxs = new List<BoundingBox>();

			for (int x = 0; x < NODE_SIZE; x++)
			{
				for (int z = 0; z < NODE_SIZE; z++)
				{

					for (int y = 0; y < NODE_SIZE; y++)
					{
						double sample = _generator.Sample(new Vector3(x, y, z) + location);
						int startBufferPos = vertices.Count;
						if (sample > 0.6f)
						{
							CreateVertices(vertices,boundingBoxs,location, x, y, z, new Color((float)sample, (float)sample, (float)sample));
						}
						int numTris = (vertices.Count -  startBufferPos)/3;
						if(numTris != 0)
						{
							var node = new LandscapeNode(_device, location, NODE_SIZE);
							node.SetData(startBufferPos,numTris, boundingBoxs);
							_nodes.Add(node);	
						}
						
					}
				}
			}

			
		}

		private void CreateVertices(List<MyOwnVertexFormat> vertices,List<BoundingBox> boundingBoxes , Vector3 location, float x, float y, float z, Color color)
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

			boundingBoxes.Add(new BoundingBox(pos0, pos7));




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


		private void AddTriangle(List<MyOwnVertexFormat> vertices, Vector3 v0, Vector3 v1, Vector3 v2, Vector3 normal, Color color)
		{
			vertices.Add(new MyOwnVertexFormat(v0, normal, color));
			vertices.Add(new MyOwnVertexFormat(v1, normal, color));
			vertices.Add(new MyOwnVertexFormat(v2, normal, color));
		}



        public void HandleEvent(List<KeyboardEvent> events)
        {
            var nButton = events.Where(e=>e.Handled == false).Where(e => e.Key == Microsoft.Xna.Framework.Input.Keys.N).Select(e => e);
            if (nButton.Count() > 0)
            {
            	nButton.First().Handled = true;
                this.GenerateLandscape();
            }
        }

	}

}
