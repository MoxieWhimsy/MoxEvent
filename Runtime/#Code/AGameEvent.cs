using System.Collections.Generic;
using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameEvent : ScriptableObject
	{
		protected readonly List<IReceiveGameEvents> _subscribers = new ();
		public void Subscribe(IReceiveGameEvents receiver) => SubscribeInternal(receiver);

		public void Unsubscribe(IReceiveGameEvents receiver)
			=> UnsubscribeInternal(receiver);

		protected void SubscribeInternal(IReceiveGameEvents receiver)
		{
			if (_subscribers.Contains(receiver)) return;
			_subscribers.Add(receiver);
		}

		protected void UnsubscribeInternal(params IReceiveGameEvents[] receivers)
		{
			foreach (var receiver in receivers)
			{
				_subscribers.Remove(receiver);	
			}
		}
	}
	
	public abstract class AGameEvent<T> : AGameEvent
	{
		public void Broadcast(T item = default) => SendInternal(item, _subscribers.ToArray());

		[System.Obsolete]
		public void Raise(T item)
			=> SendInternal(item, _subscribers.ToArray());
		
		public void Send(T item, params IReceiveGameEvents[] receivers) => SendInternal(item, receivers);

		[System.Obsolete("refactor to use Subscribe instead")]
		public void Register(IGameEventListener<T> listener)
		{
			if (listener is not IReceiveGameEvents receiver)
			{
				Debug.LogError($"register failed", this);
			}
			else SubscribeInternal(receiver);
		}

		
		[System.Obsolete]
		public void Unregister(IGameEventListener<T> listener)
			=> UnsubscribeInternal(listener is IReceiveGameEvents receiver ? receiver : null);
		
		protected void SendInternal(T item = default, params IReceiveGameEvents[] receivers)
		{
			for (var i = receivers.Length - 1; i >= 0; i--)
			{
				receivers[i].Receive(this, item);
			}
		}
	}
}