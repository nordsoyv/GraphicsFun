using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibNoise;
using Microsoft.Xna.Framework;

namespace CloneGame
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

	internal class PerlinGenerator : IGenerator
	{
		private Perlin _basis;
		private int _seed;

		public PerlinGenerator()
		{
			var r = new Random();
			_seed = r.Next();
			Init();
		}

		public PerlinGenerator(int seed)
		{
		//	_basis = new GradientNoiseBasis();
			_seed = seed;
			Init();
		}

		private void Init()
		{
			_basis = new LibNoise.Perlin();
			_basis.Seed = _seed;
			_basis.OctaveCount = 8;
			_basis.Frequency = 0.1f;
			_basis.Persistence = 0.01f;
			_basis.NoiseQuality = NoiseQuality.Standard;
			
		}

		public double Sample(Vector3 position)
		{
			//Console.Write("Vector: " + position.X + ", " + position.Y + ", " + position.Z + ", seed: " + _seed);

			double sample = _basis.GetValue((double)position.X + 0.01f, (double)position.Y + 0.01f, (double)position.Z + 0.01f);
			//double sample = _basis.GradientCoherentNoise((double)position.X + 0.01f, (double)position.Y + 0.01f, (double)position.Z + 0.01f, _seed, NoiseQuality.Standard);
			//Console.WriteLine(" " + sample.ToString());
			sample += 1;
			sample /= 2;
			return sample;
		}
	}
}
