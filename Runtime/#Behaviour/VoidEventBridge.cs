using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Mox.Events
{
	public class VoidEventBridge : MonoBehaviour, IReceiveGameEvents
	{
		[FormerlySerializedAs("gameEvent")] [SerializeField]
		private VoidEvent _gameEvent;

		public VoidEvent GameEvent => _gameEvent;

		[FormerlySerializedAs("unityEventResponse")]
		[SerializeField] private UnityEvent _unityEventResponse;

		private void OnEnable()
		{
			if (!_gameEvent) return;

			_gameEvent.Subscribe(this);
		}

		private void OnDisable()
		{
			if (!_gameEvent) return;

			_gameEvent.Unsubscribe(this);
		}

		public void Receive(AGameEvent gameEvent, object item)
		{
			Debug.Assert(item is null or Void);
			if (!_gameEvent || !gameEvent || _gameEvent != gameEvent) return;
			_unityEventResponse?.Invoke();
		}
	}
}