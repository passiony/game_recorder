using UnityEngine;

public static class RecordUtility
{
    public static Vector3 ToVector3(this RVector3 rv)
    {
        return new Vector3(rv.x, rv.y, rv.z);
    }

    public static RVector3 ToRVector3(this Vector3 v)
    {
        return new RVector3(v.x, v.y, v.z);
    }

    public static void RecordMove(RecordUnit unit, Vector3 dir, float speed)
    {
        var cmd = new MoveCommand();
        cmd.dir = dir.ToRVector3();
        cmd.speed = speed;
        cmd.owner = unit.id;
        RecordManager.Instance.RecordCommand(cmd);
    }

    public static void RecordPos(RecordUnit unit)
    {
        var cmd = new PosCommand();
        cmd.pos = unit.transform.position.ToRVector3();
        cmd.rot = unit.transform.eulerAngles.ToRVector3();
        cmd.owner = unit.id;
        RecordManager.Instance.RecordCommand(cmd);
    }

    public static void RecordAnim(RecordUnit unit, string aniName)
    {
        var cmd = new AnimCommand();
        cmd.animName = aniName;
        cmd.owner = unit.id;
        RecordManager.Instance.RecordCommand(cmd);
    }

    public static void RecordState(RecordUnit unit, int state)
    {
        var cmd = new StateCommand();
        cmd.state = state;
        cmd.owner = unit.id;
        RecordManager.Instance.RecordCommand(cmd);
    }
}