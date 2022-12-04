using UnityEngine;

/// <summary>
/// 移动
/// </summary>
public class MoveCommand : ICommand
{
    public RVector3 dir;
    public float speed;

    public MoveCommand()
    {
        this.type = ECommand.Move;
    }
    public MoveCommand(Vector3 dir, float speed)
    {
        this.type = ECommand.Move;
        this.dir = dir.ToRVector3();
        this.speed = speed;
    }
}