using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameEvent : ScriptableObject
	{
		protected readonly List<IReceiveGameEvents> _subscribers = new ();
		
		public void Broadcast() => SendInternal(receivers: _subscribers.ToArray());
		public void Send(params IReceiveGameEvents[] receivers) => SendInternal(receivers: receivers);
		public void Subscribe(IReceiveGameEvents receiver) => SubscribeInternal(receiver);

		public void Unsubscribe(IReceiveGameEvents receiver)
			=> UnsubscribeInternal(receiver);

		public static void SubscribeAll(IReceiveGameEvents receiver, params AGameEvent[] gameEvents)
		{
			foreach (var gameEvent in gameEvents.Where(e => e))
			{
				gameEvent.SubscribeInternal(receiver);
			}
		}

		public static void UnsubscribeAll(IReceiveGameEvents receiver, params AGameEvent[] gameEvents)
		{
			foreach (var gameEvent in gameEvents.Where(e => e))
			{
				gameEvent.UnsubscribeInternal(receiver);
			}
		}

		protected void SendInternal(object item = default, System.Type type = default,
			params IReceiveGameEvents[] receivers)
		{
			if (type != default && item is not null && item.GetType() != type) return;
			for (var i = receivers.Length - 1; i >= 0; i--)
			{
				receivers[i].Receive(this, item);
			}
		}

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
		public void Broadcast(T item) => SendInternal(item, typeof(T), _subscribers.ToArray());

		[System.Obsolete("Use Broadcast instead of legacy Raise, or Send for local events")]
		public void Raise(T item)
			=> SendInternal(item, typeof(T), _subscribers.ToArray());
		
		public void Send(T item, params IReceiveGameEvents[] receivers) => SendInternal(item, typeof(T), receivers);

		[System.Obsolete("refactor to use Subscribe instead", true)]
		public void Register(IGameEventListener<T> listener)
		{
			if (listener is not IReceiveGameEvents receiver)
			{
				Debug.LogError($"{nameof(Register)} failed. Please transition to {nameof(Subscribe)}.", this);
			}
			else SubscribeInternal(receiver);
		}

		
		[System.Obsolete("refactor to use Unsubscribe instead", true)]
		public void Unregister(IGameEventListener<T> listener)
		{
			if (listener is not IReceiveGameEvents receiver)
			{
				Debug.LogError($"{nameof(Unregister)} failed. Please transition to {nameof(Unsubscribe)}.", this);
			}
			else UnsubscribeInternal(receiver);
		}
	}
}