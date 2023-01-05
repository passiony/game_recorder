using UnityEngine;

namespace Record
{
    /// <summary>
    /// 人型玩家Unit
    /// </summary>
    public class UnitPlayer : UnitBase
    {
        public override void Reset()
        {
            base.Reset();
            SetState("None");
            PlayAnim("Idle");
        }

        public override void PlayAnim(string animName)
        {
            animator?.Play(animName);
        }

        public override void Move(Vector3 velocity, float speed)
        {
            transform.position += transform.right * (velocity.x * speed * Time.fixedDeltaTime * 0.8f);
            transform.position += transform.forward * (velocity.z * speed * Time.fixedDeltaTime);
            var vector = transform.up * (velocity.y * speed * Time.fixedDeltaTime * 0.5f);
            if (transform.position.y + vector.y < 0)
            {
                vector.y = -transform.position.y;
            }

            transform.position += vector;

            // 动画blend控制，可自定义
            // animator.SetFloat("up", velocity.y);
            // animator.SetFloat("horizon", velocity.x);
            // animator.SetFloat("vertical", velocity.z);
        }

        public override void Rotate(float velocity)
        {
            transform.localEulerAngles += Vector3.up * velocity * Time.fixedDeltaTime;
        }

        public override void SetRotate(Vector3 angle)
        {
            transform.localEulerAngles = angle;
        }
    }
}