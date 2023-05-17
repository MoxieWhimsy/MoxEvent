﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Mox.Events
{
	public abstract class AGameEventBridge<T, TE> : MonoBehaviour, IGameEventListener<T> where TE : AGameEvent<T>
	{
		[FormerlySerializedAs("gameEvent")] [SerializeField]
		private TE _gameEvent;

		public TE GameEvent => _gameEvent;

		[FormerlySerializedAs("unityEventResponse")] [SerializeField] private UnityEvent<T> _unityEventResponse;

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

		public void OnEventRaised(AGameEvent<T> gameEvent, T item)
		{
			_unityEventResponse?.Invoke(item);
		}

		public void Receive(AGameEvent<T> gameEvent, T item)
		{
			_unityEventResponse?.Invoke(item);
		}
	}
}