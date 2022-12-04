using System;
using UnityEngine;

/// <summary>
/// 录制unit
/// </summary>
public class RecordUnit : MonoBehaviour
{
    public int id;
    public EMode mode { get; set; }

    protected int state;
    protected Animator animator;
    
    protected Vector3Int last_dir = Vector3Int.zero;
    protected Vector3Int curr_dir = Vector3Int.zero;
    protected Vector3 dir = Vector3.zero;
    
    public void PlayAnim(string animName)
    {
        animator.Play(animName);
    }
    
    public void SetState(int state)
    {
        this.state = state;
    }
    
    public void Move(int x,int y,int z)
    {
        curr_dir.x = x;
        curr_dir.y = y;
        curr_dir.z = z;

        if (curr_dir != last_dir)
        {
            RecordUtility.RecordMove(this,curr_dir,10);
        }

        dir.x = curr_dir.x;
        dir.y = curr_dir.y;
        dir.z = curr_dir.z;
        transform.position += dir * 10 * Time.deltaTime;

        last_dir.x = curr_dir.x;
        last_dir.z = curr_dir.z;
    }

}