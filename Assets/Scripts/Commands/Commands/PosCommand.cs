using UnityEngine;

/// <summary>
/// 位置
/// </summary>
public class PosCommand : ICommand
{
    public RVector3 pos;
    public RVector3 rot;

    public PosCommand()
    {
        this.type = ECommand.Pos;
    }
}