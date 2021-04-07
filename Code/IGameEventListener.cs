namespace Mox.Events
{
	public interface IGameEventListener<T>
	{
		void OnEventRaised(T item);
	}
}