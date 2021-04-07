using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mox.Events
{
	public abstract class AGlobalEvent<T> : AGameEvent<T>
	{
		private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

		public override void Raise(T item)
		{
			for (int i = eventListeners.Count - 1; i >= 0; i--)
			{
				eventListeners[i].OnEventRaised(item);
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