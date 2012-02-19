using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneGame
{
	class Landscape
	{


		private const int NODE_SIZE = 4;
		private List<GeneratingLeafNode> _nodes;


		private GraphicsDevice _device;
		private PerlinGenerator _generator;

		public Landscape(GraphicsDevice device)
		{
			_device = device;
		}

		public void GenerateLandscape()
		{
			_generator = new PerlinGenerator();
			CreateNodes();
		}


		public void Draw(GameTime gameTime)
		{
			foreach (var leafNode in _nodes)
			{
				leafNode.Draw(gameTime);
			}

		}

		void CreateNodes()
		{
			float startX = -2 * NODE_SIZE;
			float startZ = -2 * NODE_SIZE;
			float startY = -2 * NODE_SIZE;
			int cubeSize = 4;
			_nodes = new List<GeneratingLeafNode>();

			for (int x = 0; x < cubeSize; x++)
			{
				for (int y = 0; y < cubeSize; y++)
				{
					for (int z = 0; z < cubeSize; z++)
					{
						_nodes.Add(new GeneratingLeafNode(_device, _generator,
						                                  new Vector3(startX + NODE_SIZE*x, startY + NODE_SIZE*y, startZ + NODE_SIZE*z),
						                                  NODE_SIZE));
					}
				}
			}
		}
	}

}
