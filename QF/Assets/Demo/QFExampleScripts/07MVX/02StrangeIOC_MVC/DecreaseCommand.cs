using QFramework;
using QFramework.Example;
public class DecreaseCommand :StrangeCommand
{
    [Inject]
    public StrangeCounterAppModel Model { get; set; }

    public override void Execute()
    {
        Model.Count--;

        SimpleEventSystem.Publish(new UpdateNumberViewEvent()
        {
            Number = Model.Count
        });
    }
}
