using System;
using UnityEngine;

/// <summary>
/// 播放unit
/// </summary>
public class ReplayUnit : RecordUnit
{
    protected ICommandReceiver receiver;
    protected Vector3 velocity;

    protected virtual void Awake()
    {
        receiver = new ICommandReceiver();
        AddCommands();
    }

    void AddCommands()
    {
        receiver.AddCommand<PosCommand>(ECommand.Pos, CheckPosCommand);
        receiver.AddCommand<MoveCommand>(ECommand.Move, CheckMoveCommand);
        receiver.AddCommand<StateCommand>(ECommand.State, CheckStateCommand);
        receiver.AddCommand<AnimCommand>(ECommand.Anim, CheckAnimCommand);
    }

    private ECommandReply CheckPosCommand(PosCommand command)
    {
        transform.position = command.pos.ToVector3();
        return ECommandReply.Y;
    }

    private ECommandReply CheckMoveCommand(MoveCommand command)
    {
        velocity.x = command.dir.x * command.speed;
        velocity.y = command.dir.y * command.speed;
        velocity.z = command.dir.z * command.speed;

        return ECommandReply.Y;
    }
    
    private ECommandReply CheckStateCommand(StateCommand command)
    {
        return ECommandReply.Y;
    }

    private ECommandReply CheckAnimCommand(AnimCommand command)
    {
        return ECommandReply.Y;
    }

    private void Update()
    {
        if (mode == EMode.Replay)
        {
            transform.position += velocity * Time.deltaTime;
        }
    }
    
    public void Command(ICommand cmd)
    {
        receiver.Command(cmd);
    }
}