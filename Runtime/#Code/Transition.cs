#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Mox.Events
{
	[System.Serializable]
	public class Transition<T>
	{
		#if ODIN_INSPECTOR
		[HorizontalGroup]
		#endif
		public AGameEvent<T> trigger;
		#if ODIN_INSPECTOR
		[HorizontalGroup]
		#endif
		public AGameState<T> target;
	}
}