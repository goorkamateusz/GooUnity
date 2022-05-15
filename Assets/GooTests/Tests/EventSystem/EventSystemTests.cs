using Assets.Goo.Tools.EventSystem;
using NUnit.Framework;

namespace Assets.GooTests.Tests.EventSystem
{
    public class EventSystemTests
    {
        class Event : IEvent<Event> { }

        class Listener : IEventListener<Event>
        {
            public Event Received { get; private set; }

            public void OnTrigger(Event e) => Received = e;
        }

        [Test]
        public void ListenerInitalState()
        {
            var listener = new Listener();
            Assert.IsFalse(listener.IsEventSubscribed());
        }

        [Test]
        public void ListenerIsSubscribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent();
            Assert.IsTrue(listener.IsEventSubscribed());
        }

        [Test]
        public void ListenerUnSubsribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent();
            listener.UnsubscribeEvent();
            Assert.IsFalse(listener.IsEventSubscribed());
        }

        [Test]
        public void ListenerGetSubsribed()
        {
            var listener = new Listener();
            listener.SubscribeEvent();
            var @event = new Event();
            @event.Send();
            Assert.AreSame(@event, listener.Received);
        }
    }
}