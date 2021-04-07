using UnityEngine;

namespace Mox.Events
{
	[CreateAssetMenu(fileName = "New Void Event", menuName = "Event/Void")]
	public class VoidEvent : AGlobalEvent<Void>
	{
		public void Raise() => Raise(new Void());
	}
}