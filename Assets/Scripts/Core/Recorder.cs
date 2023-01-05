using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Record
{
    public class Recorder : IRecorder
    {
        public int frameCount{ get; private set; }
        public bool recording { get; private set; }
        public string fileName{ get; private set; }
        
        private RecordInput controller;
        private List<Frame> frames = new List<Frame>();

        public void Init(string fileName)
        {
            this.fileName = fileName;
            controller = new RecordInput();
        }

        public void StartRecord()
        {
            recording = true;
            Debug.Log("开始录制");

            RecordManager.Instance.RefreshUnits();
        }

        public void PauseRecord()
        {
            recording = false;
            Debug.Log("暂停录制");
        }

        public void StopRecord()
        {
            recording = false;
            frameCount = 0;
            
            string json = JsonConvert.SerializeObject(frames);
            string path = Path.Combine(Application.streamingAssetsPath, "Recorders", $"{fileName}.json");
            FileUtility.SafeWriteAllText(path, json);

            Debug.Log("停止录制:" + fileName);
        }

        public void ResumeRecord()
        {
            if (recording)
            {
                PauseRecord();
            }
            else
            {
                StartRecord();
            }
        }
        
        public void RecordCommand(ICommand cmd)
        {
            if (!recording)
            {
                return;
            }

            var frame = new Frame();
            frame.frame = frameCount;
            frame.cmd = (int)cmd.type;
            frame.data = JsonConvert.SerializeObject(cmd);

            // Debug.Log("录入关键帧:" + cmd.type);
            frames.Add(frame);
        }

        public void Update()
        {
            controller.Update();

            if (!recording)
            {
                return;
            }
            frameCount++;
        }
    }
}