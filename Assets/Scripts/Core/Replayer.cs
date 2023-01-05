using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Record
{
    /// <summary>
    /// 播放器
    /// </summary>
    public class Replayer : IRecorder
    {
        private Queue<Frame> queue = new Queue<Frame>();
        public bool playing { get; private set; }
        public int frameCount{ get; private set; }

        public void Init(string fileName)
        {
            string path = Path.Combine(Application.streamingAssetsPath, "Recorders", $"{fileName}.json");
            string json = FileUtility.SafeReadAllText(path);
            var frames = JsonConvert.DeserializeObject<List<Frame>>(json);
            frameCount = 0;
            
            foreach (var frame in frames)
            {
                queue.Enqueue(frame);
            }
        }

        public void ResumePlay()
        {
            if (playing)
            {
                Debug.Log("暂停播放");
                playing = false;
            }
            else
            {
                Debug.Log("开始播放");
                playing = true;
            }
        }
        
        public void StopPlay()
        {
            playing = false;
            frameCount = 0;
            queue.Clear();
        }

        public void Update()
        {
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
            while (queue.Count > 0)
            {
                var frame = queue.Peek();
                if (frameCount >= frame.frame)
                {
                    queue.Dequeue();
                    PlayFrame(frame);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 播放关键帧
        /// </summary>
        /// <param name="frame">帧</param>
        public void PlayFrame(Frame frame)
        {
            var cmdType = (ECommand)frame.cmd;
            ICommand cmd = null;
            switch (cmdType)
            {
                case ECommand.Pos:
                    cmd = JsonConvert.DeserializeObject<PosCommand>(frame.data);
                    // Debug.Log($"PlayFrame:{frame.frame}, ,pos:{(cmd as PosCommand).pos}");
                    break;
                case ECommand.Move:
                    cmd = JsonConvert.DeserializeObject<MoveCommand>(frame.data);
                    // Debug.Log($"PlayFrame:{frame.frame}, ,move:{(cmd as MoveCommand).dir}");
                    break;
                case ECommand.Rotate:
                    cmd = JsonConvert.DeserializeObject<RotateCommand>(frame.data);
                    // Debug.Log($"PlayFrame:{frame.frame}, ,rot:{(cmd as RotateCommand).rot}");
                    break;
                case ECommand.Anim:
                    cmd = JsonConvert.DeserializeObject<AnimCommand>(frame.data);
                    // Debug.Log($"PlayFrame:{frame.frame}, ,anim:{(cmd as AnimCommand).animName}");
                    break;
                case ECommand.State:
                    cmd = JsonConvert.DeserializeObject<StateCommand>(frame.data);
                    // Debug.Log($"PlayFrame:{frame.frame}, ,state:{(cmd as StateCommand).state}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var unit = RecordManager.Instance.GetUnit(cmd.owner);
            if (unit == null)
            {
                Debug.LogError("找不到Unit:" + cmd.owner);
            }

            unit.Command(cmd);
        }
    }
}