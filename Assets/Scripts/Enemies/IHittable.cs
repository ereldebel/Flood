namespace Enemies
{
	/// <summary>
	/// An interface for components that can be hit by bullets.
	/// </summary>
	public interface IHittable
	{
		void TakeHit(bool killedByPlayer);
	}
}