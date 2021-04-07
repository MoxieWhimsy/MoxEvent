using UnityEngine;
using UnityEngine.Events;

namespace Mox.Events
{
	public abstract class AGameEventListener<T, E> : MonoBehaviour,
		IGameEventListener<T> where E : AGameEvent<T>
{
		[SerializeField] private E gameEvent;
		public E GameEvent => gameEvent;

		[SerializeField] private UnityEvent<T> unityEventResponse;

		private void OnEnable()
		{
			if (gameEvent == null) return;

			GameEvent.Register(this);
		}

		private void OnDisable()
		{
			if (gameEvent == null) return;

			GameEvent.Unregister(this);
		}

		public void OnEventRaised(T item)
		{
			if (unityEventResponse == null) return;

			unityEventResponse.Invoke(item);
		}
	}
}