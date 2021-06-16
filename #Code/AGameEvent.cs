using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameEvent<T> : ScriptableObject
	{
		public abstract void Raise(T item);

		internal abstract void Register(IGameEventListener<T> listener);

		internal abstract void Unregister(IGameEventListener<T> listener);
	}
}