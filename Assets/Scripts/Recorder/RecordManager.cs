using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public enum EMode
{
    Record,
    Replay,
}

public class RecordManager : MonoSingleton<RecordManager>
{
    public EMode mode;
    public string fileName;
    public float timeScale = 1;

    //录制器
    private Recorder recorder = new Recorder();
    //播放器
    private Replayer replayer = new Replayer();
    //录制单元
    private Dictionary<int, Unit> units = new Dictionary<int, Unit>();
    
    void Awake()
    {
        Time.timeScale = timeScale;
        if (mode == EMode.Record)
        {
            recorder.Init(fileName);
        }
        else
        {
            replayer.Init(fileName);
        }
    }

    public void FixedUpdate()
    {
        if (mode == EMode.Record)
        {
            recorder.Update();
        }
        else
        {
            replayer.Update();
        }
    }

    /// <summary>
    /// 注册unit信息
    /// </summary>
    /// <param name="unit"></param>
    public void Rigister(Unit unit)
    {
        units.Add(unit.id, unit);
        unit.mode = this.mode;
    }

    /// <summary>
    /// 刷新unit信息
    /// </summary>
    public void RefreshUnits()
    {
        if (mode == EMode.Record)
        {
            foreach (var pair in units)
            {
                RecordUtility.RecordPos(pair.Value);
            }
        }
    }
    
    /// <summary>
    /// 记录关键帧
    /// </summary>
    /// <param name="command"></param>
    public void RecordCommand(ICommand command)
    {
        if (mode == EMode.Record)
        {
            recorder.RecordCommand(command);
        }
    }

    /// <summary>
    /// 播放关键帧
    /// </summary>
    /// <param name="frame">帧</param>
    public void PlayFrame(Frame frame)
    {
        if (mode == EMode.Replay)
        {
            var cmdType = (ECommand)frame.cmd;
            Debug.Log($"PlayFrame:{frame.frame}, cmd:" + cmdType);
            switch (cmdType)
            {
                case ECommand.Pos:
                {
                    var cmd = JsonConvert.DeserializeObject<PosCommand>(frame.data);
                    var unit = GetUnit(cmd.owner);
                    unit.Command(cmd);
                    break;
                }
                case ECommand.Move:
                {
                    var cmd = JsonConvert.DeserializeObject<MoveCommand>(frame.data);
                    var unit = GetUnit(cmd.owner);
                    unit.Command(cmd);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Unit GetUnit(int id)
    {
        if (units.TryGetValue(id, out Unit unit))
        {
            return unit;
        }

        return null;
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (mode == EMode.Record)
        {
            recorder.OnGUI();
        }
        else
        {
            replayer.OnGUI();
        }
    }
#endif
}