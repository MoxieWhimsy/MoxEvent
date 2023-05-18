using UnityEngine;
using UnityEngine.Serialization;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Mox.Events
{
	[System.Serializable]
	public class Transition
	{
		[field: SerializeField] public AGameEvent TriggeringEvent { get; private set; }
		[field: SerializeField] public AGameState TargetState { get; private set; }

		public System.Type StateType => TargetState.GetType();
		public static string TriggerProperty => $"<{nameof(TriggeringEvent)}>k__BackingField";
		public static string TargetProperty => $"<{nameof(TargetState)}>k__BackingField";

	}
	
	[System.Serializable]
	[System.Obsolete]
	public class Transition<T> : Transition
	{
		#if ODIN_INSPECTOR
		[HorizontalGroup]
		#endif
		public AGameEvent<T> trigger;
		#if ODIN_INSPECTOR
		[HorizontalGroup]
		#endif
		public AGameState<T> target;
	}
}