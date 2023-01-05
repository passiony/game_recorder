using UnityEngine;

namespace Record
{
    /// <summary>
    /// 录制静态工具类
    /// </summary>
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
        
        public static void RePlayAnim(this UnitBase unit, string animName)
        {
            unit.PlayAnim(animName);
            RecordAnim(unit, animName);
        }

        public static void ReSetState(this UnitBase unit,string state)
        {
            unit.SetState(state);
            RecordState(unit, state);
        }

        public static void ReMove(this UnitBase unit,Vector3 move, float speed)
        {
            unit.Move(move, speed);
            RecordMove(unit, move, speed);
        }

        public static void ReRotate(this UnitBase unit,float value,float speed)
        {
            unit.Rotate(value * speed);
            RecordRotate(unit, unit.transform.localEulerAngles, speed);
        }
        
        public static void RecordMove(UnitBase unit, Vector3 dir, float speed)
        {
            var cmd = new MoveCommand();
            cmd.dir = dir.ToRVector3();
            cmd.speed = speed;
            cmd.owner = unit.id;
            RecordManager.Instance.RecordCommand(cmd);
        }

        public static void RecordRotate(UnitBase unit, Vector3 rot, float speed)
        {
            var cmd = new RotateCommand();
            cmd.rot = rot.ToRVector3();
            cmd.speed = speed;
            cmd.owner = unit.id;
            RecordManager.Instance.RecordCommand(cmd);
        }
        
        public static void RecordPos(UnitBase unit)
        {
            var cmd = new PosCommand();
            cmd.pos = unit.transform.position.ToRVector3();
            cmd.rot = unit.transform.localEulerAngles.ToRVector3();
            cmd.owner = unit.id;
            RecordManager.Instance.RecordCommand(cmd);
        }

        public static void RecordAnim(UnitBase unit, string aniName)
        {
            var cmd = new AnimCommand();
            cmd.animName = aniName;
            cmd.owner = unit.id;
            RecordManager.Instance.RecordCommand(cmd);
        }

        public static void RecordState(UnitBase unit, string state)
        {
            var cmd = new StateCommand();
            cmd.state = state;
            cmd.owner = unit.id;
            RecordManager.Instance.RecordCommand(cmd);
        }
    }
}