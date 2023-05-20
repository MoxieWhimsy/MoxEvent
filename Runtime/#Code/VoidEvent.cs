using UnityEngine;

namespace Mox.Events
{
	[CreateAssetMenu(fileName = "New Void Event", menuName = "Event/Void")]
	public class VoidEvent : AGameEvent<Void>
	{
		public void Send(params IReceiveGameEvents[] receivers) => SendInternal(receivers: receivers);
		
		[System.Obsolete("Use Broadcast instead of legacy Raise, or Send for local events")]
		public void Raise() => Raise(new Void());
	}
}