using System.Collections.Generic;
using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameEvent : ScriptableObject
	{
	}
	
	public abstract class AGameEvent<T> : AGameEvent
	{
		private readonly List<IGameEventListener<T>> _subscribers = new ();

		public void Broadcast(T item = default) => SendInternal(item, _subscribers.ToArray());

		[System.Obsolete]
		public void Raise(T item)
		{
			for (int i = _subscribers.Count - 1; i >= 0; i--)
			{
				_subscribers[i].OnEventRaised(this, item);
			}
		}

		public void Send(T item, params IGameEventListener<T>[] receivers) => SendInternal(item, receivers);

		[System.Obsolete]
		public void Register(IGameEventListener<T> receiver)
			=> SubscribeInternal(receiver);

		public void Subscribe(IGameEventListener<T> receiver) => SubscribeInternal(receiver);

		[System.Obsolete]
		public void Unregister(IGameEventListener<T> receiver)
			=> UnsubscribeInternal(receiver);
		public void Unsubscribe(IGameEventListener<T> receiver)
			=> UnsubscribeInternal(receiver);
		
		protected void SendInternal(T item = default, params IGameEventListener<T>[] receivers)
		{
			for (var i = receivers.Length - 1; i >= 0; i--)
			{
				receivers[i].Receive(this, item);
			}
		}

		private void SubscribeInternal(IGameEventListener<T> receiver)
		{
			if (_subscribers.Contains(receiver)) return;
			_subscribers.Add(receiver);
		}

		private void UnsubscribeInternal(params IGameEventListener<T>[] receivers)
		{
			foreach (var receiver in receivers)
			{
				_subscribers.Remove(receiver);	
			}
		}
	}
}