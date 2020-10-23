using QFramework;
using UnityEngine;
/// <summary>
/// IOC Container 单例
/// </summary>
public class CounterApp : MonoBehaviour,ISingleton 
{
    public static QFrameworkContainer Container
    {
        get
        {
            return mApp.mContainer;
        }
    }

    QFrameworkContainer mContainer;

    private static CounterApp mApp 
    {
        get 
        { 
            return MonoSingletonProperty<CounterApp>.Instance;
        }
    }

    public void OnSingletonInit ()
    {
        mContainer = new QFrameworkContainer ();

        mContainer.Register<ICounterApiService, CounterApiService>();

        mContainer.Register<IStorageService, CounterAppStorageService>();

        var model = new CounterAppQFModel();

        // 注册 Model 实例
        mContainer.RegisterInstance(model, true);

        // 手动初始化 Model
        model.Init();
    }
}
