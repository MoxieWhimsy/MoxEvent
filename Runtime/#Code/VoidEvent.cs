using UnityEngine;

namespace Mox.Events
{
	[CreateAssetMenu(fileName = "New Void Event", menuName = "Event/Void")]
	public class VoidEvent : AGameEvent<Void>
	{
		[System.Obsolete("Use Broadcast instead of legacy Raise, or Send for local events")]
		public void Raise() => SendInternal(receivers: _subscribers.ToArray());

	}
}