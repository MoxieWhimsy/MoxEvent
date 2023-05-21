using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mox.Events
{
	[System.Serializable]
	public class Transition
	{
		[HideInInspector]
		[field: SerializeField] public AGameEvent TriggeringEvent { get; private set; }
		[field: SerializeField] public AGameState TargetState { get; private set; }
		[SerializeField] private AGameEvent[] _triggers;

		public IEnumerable<AGameEvent> Triggers => _triggers;

		public bool HasTargetAndTrigger => TargetState && (TriggeringEvent || _triggers.Length > 0);

		public bool Has(AGameEvent gameEvent) => _triggers.Contains(gameEvent) || gameEvent == TriggeringEvent;
		public static string TriggerProperty => $"<{nameof(TriggeringEvent)}>k__BackingField";
		public static string TargetProperty => $"<{nameof(TargetState)}>k__BackingField";

		#if UNITY_EDITOR
		public Transition(AGameState targetState, params Object[] parameters)
		{
			TargetState = targetState;
			_triggers = parameters.OfType<AGameEvent>().ToArray();
		}

		public void ReValidate()
		{
			if (!TriggeringEvent) return;
			_triggers = _triggers.Append(TriggeringEvent).ToArray();
			TriggeringEvent = null;
		}
		#endif
	}
	
	[System.Serializable]
	[System.Obsolete]
	public class Transition<T> : Transition
	{
		public Transition(AGameState targetState, params Object[] parameters) : base(targetState, parameters)
		{
		}
	}
}