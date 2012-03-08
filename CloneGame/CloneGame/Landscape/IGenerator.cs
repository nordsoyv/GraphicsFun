using Microsoft.Xna.Framework;

namespace CloneGame.Landscape
{
	internal interface IGenerator
	{
		double Sample(Vector3 position);
	}
}