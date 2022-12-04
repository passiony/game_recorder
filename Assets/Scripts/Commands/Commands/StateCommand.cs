/// <summary>
/// 死亡
/// </summary>
public class StateCommand : ICommand
{
    public int state;

    public StateCommand()
    {
        this.type = ECommand.State;
    }
}