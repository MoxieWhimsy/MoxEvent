namespace Mox.Events
{
	[System.Obsolete]
	public interface IGameEventListener<T>
	{
		void OnEventRaised(AGameEvent<T> gameEvent, T item);
	}
}