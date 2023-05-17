namespace Mox.Events
{
	public interface IGameEventListener<T>
	{
		void OnEventRaised(AGameEvent<T> gameEvent, T item);
		void Receive(AGameEvent<T> gameEvent, T item);
	}
}