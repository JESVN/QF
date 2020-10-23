using UniRx;
using UnityEngine;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public class UpdateNumberViewEvent
	{
		public int Number { get; set; }
	}
	public partial class StrangeIOC_MVC : ViewController
	{
		IQFrameworkContainer mContainer = new QFrameworkContainer();
		void Awake()
		{
			mContainer.RegisterInstance(new StrangeCounterAppModel());
			// 注册事件
			SimpleEventSystem.GetEvent<StrangeCommand>()
				.Subscribe(command =>
				{
					// 执行之前要进行注入
					mContainer.Inject(command);

					command.Execute();

				}).AddTo(this);
		}
		void Start()
		{
			// 注册视图更新事件
			SimpleEventSystem.GetEvent<UpdateNumberViewEvent>()
				.Subscribe(updateEvent =>
				{
					Debug.Log($"更新StrangeIOC_MVC{updateEvent.Number}");
					this.Btn.Number.text = updateEvent.Number.ToString();

				}).AddTo(this);


			this.Btn.BtnAdd.onClick.AddListener(() =>
			{
				SimpleEventSystem.Publish<StrangeCommand>(new IncreaseCommand());
			});

			this.Btn.BtnSub.onClick.AddListener(() =>
			{
				SimpleEventSystem.Publish<StrangeCommand>(new DecreaseCommand());
			});
		}
	}
}
