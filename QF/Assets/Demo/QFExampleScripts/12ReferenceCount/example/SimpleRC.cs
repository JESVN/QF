using Demo;
public class SimpleRC : IRefCounter
{
    public SimpleRC()
    {
        RefCount = 0;
    }

    public int RefCount { get; private set; }

    public void Retain(object refOwner = null)
    {
        ++RefCount;
    }

    public void Release(object refOwner = null)
    {
        --RefCount;
        if (RefCount == 0)
        {
            OnZeroRef();
        }
    }

    protected virtual void OnZeroRef()
    {
    }
}