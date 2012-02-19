using Microsoft.Xna.Framework;

namespace CloneGame
{
	internal interface IGenerator
	{
		double Sample(Vector3 position);
	}
}