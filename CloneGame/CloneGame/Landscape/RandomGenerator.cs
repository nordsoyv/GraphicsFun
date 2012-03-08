using System;
using Microsoft.Xna.Framework;

namespace CloneGame.Landscape
{
	class RandomGenerator : IGenerator
	{
		private Random rand;

		public RandomGenerator()
		{
			rand = new Random();
		}

		public double Sample(Vector3 position)
		{
			return rand.NextDouble();
		}

	}
}
