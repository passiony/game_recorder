using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Replayer : IRecorder
{
    private Queue<Frame> queue = new Queue<Frame>();
    private bool playing;
    private int frameCount;

    public void Init(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Recorders", $"{fileName}.json");
        string json = FileUtility.SafeReadAllText(path);
        var frames = JsonConvert.DeserializeObject<List<Frame>>(json);
        foreach (var frame in frames)
        {
            queue.Enqueue(frame);
        }
    }

    private void Play()
    {
        Debug.Log("开始播放");
        playing = true;
    }

    public void Update()
    {
        OnKeyInput();

        if (!playing)
        {
            return;
        }

        if (queue.Count == 0)
        {
            playing = false;
            Debug.Log("Replay End");
            return;
        }

        frameCount++;
        var frame = queue.Peek();
        if (frame.frame > frameCount)
        {
            return;
        }

        queue.Dequeue();
        RecordManager.Instance.PlayFrame(frame);
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 50), "播放/暂停:F11");
    }

    private void OnKeyInput()
    {
        if (Input.GetKeyUp(KeyCode.F11))
        {
            Play();
        }
    }
}