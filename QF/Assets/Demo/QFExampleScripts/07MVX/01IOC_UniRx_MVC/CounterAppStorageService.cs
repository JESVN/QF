using UniRx;
using UnityEngine;
public interface IStorageService
{
    ReactiveProperty<int> CreateIntReactiveProperty(string key, int defaultValue = 0);
}

public class CounterAppStorageService : IStorageService
{
    public ReactiveProperty<int> CreateIntReactiveProperty(string key,int defaultValue = 0)
    {
        var initValue = PlayerPrefs.GetInt(key,defaultValue);

        var property = new ReactiveProperty<int>(initValue);

        property.Subscribe(value =>
        {
            Debug.Log($"更新IOC_UniRx_MVC{value}");
            PlayerPrefs.SetInt(key, value);
        });
        return property;
    }
}