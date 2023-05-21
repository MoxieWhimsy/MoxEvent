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
				handler.Register(_transitions.Where(tr => tr.HasTargetAndTrigger));
			InternalEnter(components);
		}

		public void OnExit(params Component[] components)
		{
			if (!HasValidComponents(components)) return;
			if (components.Length > 0 && components[0] is IHandleTransitions handler)
				handler.Unregister(_transitions.Where(tr => tr.HasTargetAndTrigger));
			InternalExit(components);
		}
		
		public bool GetTransition(AGameEvent triggerEvent, out Transition transition)
		{
			transition = _transitions.FirstOrDefault(t => t.Has(triggerEvent));
			return transition != default;
		}

		protected virtual void InternalEnter(params Component[] components) { }
		protected virtual void InternalExit(params Component[] components) { }

		#if UNITY_EDITOR
		private void OnValidate()
		{
			foreach (var transition in _transitions)
			{
				transition.ReValidate();
			}
		}
		#endif
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