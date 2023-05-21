using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mox.Events
{
	public abstract class AGameState : ScriptableObject
	{
		[SerializeField] protected List<Transition> _transitions = new ();

		public virtual bool HasValidComponents(params Component[] components) => true;

		public void OnEnter(params Component[] components)
		{
			if (!HasValidComponents(components)) return;
			if (components.Length > 0 && components[0] is IHandleTransitions handler)
				handler.Register(_transitions.Where(tr=>tr.TriggeringEvent && tr.TargetState));
			InternalEnter(components);
		}

		public void OnExit(params Component[] components)
		{
			if (!HasValidComponents(components)) return;
			if (components.Length > 0 && components[0] is IHandleTransitions handler)
				handler.Unregister(_transitions.Where(tr=>tr.TriggeringEvent && tr.TargetState));
			InternalExit(components);
		}
		
		public bool GetTransition(AGameEvent triggerEvent, out Transition transition)
		{
			foreach (var item in _transitions)
			{
				if (item.TriggeringEvent != triggerEvent) continue;
				transition = item;
				return true;
			}

			transition = null;
			return false;
		}

		protected virtual void InternalEnter(params Component[] components) { }
		protected virtual void InternalExit(params Component[] components) { }
		
	}
	
	[System.Obsolete]
	public abstract class AGameState<T> : ScriptableObject
	{
		[System.Obsolete]
		internal virtual void OnExit(IGameEventListener<T> context) { }

		[System.Obsolete]
		internal virtual void OnEnter(IGameEventListener<T> context) { }

		[System.Obsolete]
		internal void RegisterTransitions(IGameEventListener<T> context)
		{
		}

		[System.Obsolete]
		internal void UnregisterTransitions(IGameEventListener<T> context)
		{
		}

		[System.Obsolete]
		internal bool TryGetTransition(AGameEvent<T> triggerEvent, out Transition<T> transition)
		{
			transition = null;
			return false;
		}
	}
}