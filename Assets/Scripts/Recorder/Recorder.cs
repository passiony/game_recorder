using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Recorder : IRecorder
{
    List<Frame> frames = new List<Frame>();
    private int frameCount = 0;
    private bool recording = false;
    private string fileName;

    public void Init(string fileName)
    {
        this.fileName = fileName;
    }

    public void RecordUnit(RecordUnit unit)
    {
        var pos = unit.transform.position;
        var rot = unit.transform.eulerAngles;
        var cmd = new PosCommand(pos, rot);
        cmd.owner = unit.id;
        RecordFrame(cmd);
    }

    public void StartRecord()
    {
        recording = true;
        Debug.Log("开始录制");

        RecordManager.Instance.RecordUnits();
    }

    public void PauseRecord()
    {
        recording = false;
        Debug.Log("暂停录制");
    }

    public void StopRecord()
    {
        recording = false;

        string json = JsonConvert.SerializeObject(frames);
        string path = Path.Combine(Application.streamingAssetsPath, "Recorders", $"{fileName}.json");
        FileUtility.SafeWriteAllText(path, json);

        Debug.Log("停止录制:" + fileName);
    }

    public void RecordFrame(ICommand cmd)
    {
        if (!recording)
        {
            return;
        }
        var frame = new Frame();
        frame.frame = frameCount;
        frame.cmd = (int)cmd.type;
        frame.data = JsonConvert.SerializeObject(cmd);

        Debug.Log("录入关键帧:" + cmd.type);

        frames.Add(frame);
    }

    public void Update()
    {
        OnKeyInput();
        if (!recording)
        {
            return;
        }

        frameCount++;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 20), "开始/暂停:F12");
        GUI.Label(new Rect(0, 20, 200, 20), "停止:Shift + F12");
        GUI.Label(new Rect(0, 40, 200, 20), "前后左右: WASD");
        GUI.Label(new Rect(0, 60, 200, 20), "上下: EQ");
    }

    private void OnKeyInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.F12))
        {
            StopRecord();
        }
        else if (Input.GetKeyUp(KeyCode.F12))
        {
            if (!recording)
            {
                StartRecord();
            }
            else
            {
                PauseRecord();
            }
        }
    }
}