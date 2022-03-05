namespace Assets.Goo.Tools.EventSystem
{
    public interface IEventListener
    {
    }

    public interface IEventListener<T> : IEventListener
    {
        void OnTrigger(T e);
    }
}