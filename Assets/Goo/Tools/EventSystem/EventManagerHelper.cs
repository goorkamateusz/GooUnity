namespace Assets.Goo.Tools.EventSystem
{
    public static class EventManagerHelper
    {
        public static void SubscribeEvent<T>(this IEventListener<T> listener)
        {
            EventManager.Instance.Subscribe(listener);
        }

        public static void UnsubscribeEvent<T>(this IEventListener<T> listener)
        {
            EventManager.Instance.Unsubscribe(listener);
        }
    }
}