namespace Mox.Events
{
    [System.Obsolete("use AGameEventBridge instead", true)]
    public class AGameEventListener<T, TE> : AGameEventBridge<T, TE> where TE : AGameEvent<T>
    {
    }
}