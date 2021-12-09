using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Mox.Events
{
	public abstract class AGlobalEvent<T> : AGameEvent<T>
	{
		#if ODIN_INSPECTOR
		[ShowInInspector, ReadOnly]
		#endif
		private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

		public override void Raise(T item)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(this, item);
			}
		}

		internal override void Register(IGameEventListener<T> listener)
		{
			if (!eventListeners.Contains(listener))
			{
				eventListeners.Add(listener);
			}
		}

		internal override void Unregister(IGameEventListener<T> listener)
		{
			if (eventListeners.Contains(listener))
			{
				eventListeners.Remove(listener);
			}
		}
	}
}