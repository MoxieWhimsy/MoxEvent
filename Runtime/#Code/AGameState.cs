using System.Collections.Generic;
using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameState<T> : ScriptableObject
	{
		[SerializeField]
		List<Transition<T>> _transitions = new List<Transition<T>>();

		internal virtual void OnExit(IGameEventListener<T> context) { }

		internal virtual void OnEnter(IGameEventListener<T> context) { }

		internal void RegisterTransitions(IGameEventListener<T> context)
		{
			_transitions.ForEach((transition) => {
				if (transition == null || transition.trigger == null || transition.target == null)
					return;

				transition.trigger.Register(context);
			});
		}

		internal void UnregisterTransitions(IGameEventListener<T> context)
		{
			_transitions.ForEach((transition) =>
			{
				if (transition == null || transition.trigger == null || transition.target == null)
					return;

				transition.trigger.Unregister(context);
			});
		}

		internal bool TryGetTransition(AGameEvent<T> triggerEvent, out Transition<T> transition)
		{
			foreach (var item in _transitions)
			{
				if (item.trigger == triggerEvent)
				{
					transition = item;
					return true;
				}
			}

			transition = null;
			return false;
		}
	}
}