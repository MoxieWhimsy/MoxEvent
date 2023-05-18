namespace Mox.Events
{
    public interface IReceiveGameEvents
    {
        void Receive(AGameEvent gameEvent, object item);
    }
}