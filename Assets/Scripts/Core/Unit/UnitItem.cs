using UnityEngine;

namespace Record
{
    /// <summary>
    /// 物体Unit
    /// </summary>
    public class UnitItem : UnitBase
    {
        public override void PlayAnim(string animName){}

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