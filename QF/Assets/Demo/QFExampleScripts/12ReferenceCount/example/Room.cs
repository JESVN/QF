using QFramework;
using Light = Demo.Light;
public class Room:SimpleRC
{
    private Light mLight = new Light();
    
    public void EnterPeople()
    {
        if (RefCount == 0)
        {
            mLight.Open();
        }

        Retain();

        Log.I("一个人走进房间,房间里当前有{0}个人",RefCount);
    }

    public void LeavePeople()
    {
        Release();
        Log.I("一个人走出房间,房间里当前有{0}个人", RefCount);
    }
    protected override void OnZeroRef()
    {
        mLight.Close();
    }
}