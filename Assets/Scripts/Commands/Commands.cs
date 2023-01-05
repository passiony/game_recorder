namespace Record
{
    /// <summary>
    /// 死亡
    /// </summary>
    public class StateCommand : ICommand
    {
        public string state;

        public StateCommand()
        {
            this.type = ECommand.State;
        }
    }


    /// <summary>
    /// 旋转
    /// </summary>
    public class RotateCommand : ICommand
    {
        public RVector3 rot;
        public float speed;

        public RotateCommand()
        {
            this.type = ECommand.Rotate;
        }
    }
    
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
    }
    
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
}