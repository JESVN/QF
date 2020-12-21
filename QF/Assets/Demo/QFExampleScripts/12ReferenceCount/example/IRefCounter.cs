namespace Demo
{
    public interface IRefCounter
    {
        int RefCount { get; }

        void Retain(object refOwner = null);
        void Release(object refOwner = null);
    }
}