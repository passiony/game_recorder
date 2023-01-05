using System;
using Quick;
using UnityEngine;

namespace Record
{
    /// <summary>
    /// Unit基类
    /// </summary>
    public abstract class UnitBase : MonoBehaviour
    {
        public int id;
        public string[] animates;
        
        protected ICommandReceiver receiver;
        protected string state;
        protected Animator animator;
        protected Vector3 velocity;
        
        protected virtual void Awake()
        {
            receiver = new ICommandReceiver();
            AddCommands();
            animator = this.GetComponent<Animator>();
        }

        protected virtual void AddCommands()
        {
            receiver.AddCommand<PosCommand>(ECommand.Pos, CheckPosCommand);
            receiver.AddCommand<MoveCommand>(ECommand.Move, CheckMoveCommand);
            receiver.AddCommand<RotateCommand>(ECommand.Rotate, CheckRotateCommand);
            receiver.AddCommand<StateCommand>(ECommand.State, CheckStateCommand);
            receiver.AddCommand<AnimCommand>(ECommand.Anim, CheckAnimCommand);
        }

        public void Command(ICommand cmd)
        {
            receiver.Command(cmd);
        }

        private ECommandReply CheckRotateCommand(RotateCommand command)
        {
            this.SetRotate(command.rot.ToVector3());
            return ECommandReply.Y;
        }

        private ECommandReply CheckPosCommand(PosCommand command)
        {
            transform.position = command.pos.ToVector3();
            transform.localEulerAngles = command.rot.ToVector3();
            return ECommandReply.Y;
        }

        private ECommandReply CheckMoveCommand(MoveCommand command)
        {
            velocity.x = command.dir.x;
            velocity.y = command.dir.y;
            velocity.z = command.dir.z;
            this.Move(velocity, command.speed);
            return ECommandReply.Y;
        }

        private ECommandReply CheckStateCommand(StateCommand command)
        {
            SetState(command.state);
            return ECommandReply.Y;
        }

        private ECommandReply CheckAnimCommand(AnimCommand command)
        {
            PlayAnim(command.animName);
            return ECommandReply.Y;
        }
        
        
        public virtual void Reset()
        {
            gameObject.SetActive(true);
        }

        public virtual void SetState(string state)
        {
            this.state = state;
        }

        public virtual void SetRotate(Vector3 angle)
        {
            transform.localEulerAngles = angle;
        }

        public virtual void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        public virtual void PlayAnim(string animName)
        {
        }

        public virtual void Move(Vector3 velocity, float speed)
        {
        }

        public virtual void Rotate(float velocity)
        {
        }

        public string[] GetAnimationClips()
        {
            return animates;
        }

        public void Hilight(bool enable)
        {
            if (enable)
            {
                gameObject.AddComponent<Outline>().OutlineColor = Color.green;
            }
            else
            {
                var outline = gameObject.GetComponent<Outline>();
                if (outline)
                {
                    Destroy(outline);
                }
            }
        }
    }
}