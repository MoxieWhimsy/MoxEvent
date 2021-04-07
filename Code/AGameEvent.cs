using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameEvent<T> : ScriptableObject
	{
		public abstract void Raise(T item);

		internal abstract void RegisterListener(IGameEventListener<T> listener);

		internal abstract void UnregisterListener(IGameEventListener<T> listener);
	}
}