using System;
using LibNoise;
using Microsoft.Xna.Framework;

namespace CloneGame.Landscape
{
	internal class PerlinGenerator : IGenerator
	{
		private Perlin _basis;


		public PerlinGenerator()
		{
			var r = new Random();
			_basis = new Perlin();
			Seed = r.Next();
			Init();
		}

		public PerlinGenerator(int seed)
		{
			_basis = new Perlin();
			Seed = seed;
			Init();
		}

		public int Seed { get { return _basis.Seed; } set { _basis.Seed = value; } }

		public int OctaveCount
		{
			get { return _basis.OctaveCount; }
			set { _basis.OctaveCount = value; }
		}

		public double Frequency
		{
			get { return _basis.Frequency; }
			set { _basis.Frequency = value; }
		}

		public double Persistence
		{
			get { return _basis.Persistence; }
			set { _basis.Persistence = value; }
		}

		public NoiseQuality NoiseQuality
		{
			get { return _basis.NoiseQuality; }
			set { _basis.NoiseQuality = value; }
		}

		private void Init()
		{
			OctaveCount = 8;
			Frequency = 0.1f;
			Persistence = 0.01f;
			NoiseQuality = NoiseQuality.Standard;
		}

		public double Sample(Vector3 position)
		{

			double sample = _basis.GetValue((double)position.X + 0.01f, (double)position.Y + 0.01f, (double)position.Z + 0.01f);
			sample += 1;
			sample /= 2;
			return sample;
		}
	}
}