/// <summary>
/// 动画
/// </summary>
public class AnimCommand : ICommand
{
    public string animName;
    
    public AnimCommand()
    {
        this.type = ECommand.Anim;
    }
}