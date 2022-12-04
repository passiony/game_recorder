﻿/// <summary>
/// 动画
/// </summary>
public class AnimCommand : ICommand
{
    public string animName;
    
    public AnimCommand()
    {
        this.type = ECommand.Anim;
    }

    public AnimCommand(string animName)
    {
        this.animName = animName;
    }
}