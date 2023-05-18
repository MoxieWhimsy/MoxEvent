using System.Collections.Generic;

namespace Mox.Events
{
    public interface IHandleTransitions
    {
        void Receive(AGameEvent gameEvent, object item);
        void Register(IEnumerable<Transition> transitions);
        void Unregister(IEnumerable<Transition> transitions);
    }
}