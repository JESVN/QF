using System;
using UniRx;
public interface ICounterApiService
{
    void RequestSomeData(Action<string> onResponse);
}

public class CounterApiService : ICounterApiService
{
    public void RequestSomeData(Action<string> onResponse)
    {
        // 延时一秒，用来请求网络数据
        Observable.Timer(TimeSpan.FromSeconds(1.0f)).Subscribe(_ =>
        {
            onResponse("数据请求成功");
        });
    }
}
