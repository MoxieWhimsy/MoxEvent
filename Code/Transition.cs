using Sirenix.OdinInspector;

namespace Mox.Events
{
	[System.Serializable]
	public class Transition<T>
	{
		[HorizontalGroup]
		public AGameEvent<T> trigger;
		[HorizontalGroup]
		public AGameState<T> target;
	}
}