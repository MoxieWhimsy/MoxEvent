using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameEvent<T> : ScriptableObject
	{
		public abstract void Raise(T item);

		public abstract void Register(IGameEventListener<T> listener);

		public abstract void Unregister(IGameEventListener<T> listener);
	}
}