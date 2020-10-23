using UniRx;
// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class IOC_UniRx_MVC : ViewController
	{
		[Inject]
		public CounterAppQFModel Model { get; set; }

		// 新增
		[Inject]
		public ICounterApiService ApiService { get; set; }

		void Start()
		{
			CounterApp.Container.Inject (this);

			// Model -> View
			Model.Count.Select (count => count.ToString ())
				.SubscribeToText (this.Btn.Number)
				.AddTo(this);

			// 新增
			Model.SomeData.SubscribeToText(this.Btn.ResultText)
				.AddTo(this);

			// View -> Model
			// 这里使用 UniRx 风格
			this.Btn.BtnAdd.OnClickAsObservable ()
				.Subscribe ((_)=> Model.Count.Value++); 

			this.Btn.BtnSub.OnClickAsObservable ()
				.Subscribe ((_) => Model.Count.Value--);

			// 新增
			this.Btn.BtnRequest.OnClickAsObservable()
				.Subscribe((_) =>
				{
					ApiService.RequestSomeData(someData =>
					{
						Model.SomeData.Value = someData;
					});
				});
		}
	}
}
