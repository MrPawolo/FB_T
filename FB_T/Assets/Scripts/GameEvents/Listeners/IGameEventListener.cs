
namespace ML.GameEvents
{
    public interface IGameEventListener<T>
    {
        void OnEventInvoke(T param);
    }
}
