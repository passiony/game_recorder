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
    public PosCommand(Vector3 pos, Vector3 rot)
    {
        this.type = ECommand.Pos;
        this.pos = pos.ToRVector3();
        this.rot = rot.ToRVector3();
    }
    public PosCommand(RVector3 pos, RVector3 rot)
    {
        this.type = ECommand.Pos;
        this.pos = pos;
        this.rot = rot;
    }
}