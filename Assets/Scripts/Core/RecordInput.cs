using UnityEngine;

namespace Record
{
    /// <summary>
    /// 录制器的输入控制器
    /// </summary>
    public class RecordInput
    {
        public float moveSpeed = 1;
        public float rotSpeed = 50;

        private bool stoping;
        private Vector3 velocity;

        // Update is called once per frame
        public void Update()
        {
            int x = (int)Input.GetAxisRaw("Horizontal");
            int z = (int)Input.GetAxisRaw("Vertical");

            int y1 = Input.GetKey(KeyCode.E) ? 1 : 0;
            int y2 = Input.GetKey(KeyCode.Q) ? -1 : 0;
            int y = y1 + y2;

            if (x != 0 || y != 0 || z != 0)
            {
                stoping = false;
                velocity.Set(x, y, z);
                RecordManager.Instance.Current.ReMove(velocity, moveSpeed);
            }
            else if (!stoping)
            {
                stoping = true;
                velocity.Set(0, 0, 0);
                RecordManager.Instance.Current.ReMove(velocity, moveSpeed);
            }

            if (Input.GetMouseButton(0))
            {
                var rotate = Input.GetAxisRaw("Mouse X");
                RecordManager.Instance.Current.ReRotate(rotate, rotSpeed);
            }
        }
    }
}