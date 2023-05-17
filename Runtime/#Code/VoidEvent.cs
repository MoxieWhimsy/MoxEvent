using UnityEngine;

namespace Mox.Events
{
	[CreateAssetMenu(fileName = "New Void Event", menuName = "Event/Void")]
	public class VoidEvent : AGameEvent<Void>
	{
		public void Send(params IGameEventListener<Void>[] receivers) => SendInternal(receivers: receivers);
		
		[System.Obsolete]
		public void Raise() => Raise(new Void());
	}
}