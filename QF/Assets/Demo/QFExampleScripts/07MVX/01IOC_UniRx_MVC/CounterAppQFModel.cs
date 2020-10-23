using QFramework;
using UniRx;
/// <summary>
/// Model层
/// </summary>
public class CounterAppQFModel
{
    public ReactiveProperty<int> Count { get; private set; }

    public ReactiveProperty<string> SomeData = new ReactiveProperty<string>("");

    [Inject]
    public IStorageService StorageService { get; set; }

    public void Init()
    {
        Count = StorageService.CreateIntReactiveProperty("count", 0);
    }
}