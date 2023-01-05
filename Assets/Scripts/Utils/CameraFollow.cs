using System;
using Record;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform follow { get; set; }

    public float distanceAway = 3; // distance from the back of the craft
    public float distanceUp = 1; // distance above the craft
    public float smooth = 10; // how smooth the camera movement is

    private GameObject hovercraft; // to store the hovercraft
    private Vector3 targetPosition; // the position the camera is trying to be in

    // public void Awake()
    // {
    //     follow = GameObject.FindWithTag("Player").transform.Find("head");
    // }

    void LateUpdate()
    {
        if (follow == null)
        {
            return;
        }
        // setting the target position to be the correct offset from the hovercraft
        targetPosition = follow.position + Vector3.up * distanceUp - follow.forward * distanceAway;
        // making a smooth transition between it's current position and the position it wants to be in
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        // make sure the camera is looking the right way!
        transform.LookAt(follow);
    }
}