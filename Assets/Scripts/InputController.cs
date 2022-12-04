using UnityEngine;

/// <summary>
/// 输入控制
/// </summary>
public class InputController : MonoBehaviour
{
    //任意切换Unit
    protected RecordUnit curUnit;

    void Start()
    {
        curUnit = GameObject.FindObjectOfType<RecordUnit>();
    }

    void Update()
    {
        if (RecordManager.Instance.mode == EMode.Record)
        {
            int x = (int)Input.GetAxisRaw("Horizontal");
            int z = (int)Input.GetAxisRaw("Vertical");

            int y1 = Input.GetKey(KeyCode.E) ? 1 : 0;
            int y2 = Input.GetKey(KeyCode.Q) ? -1 : 0;
            int y = y1 + y2;

            curUnit.Move(x, y, z);
        }
    }
}