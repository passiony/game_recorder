using System.Collections.Generic;
using UnityEngine;

namespace Record
{
    public enum EMode
    {
        None,
        Record,
        Replay,
    }

    /// <summary>
    /// 录制总管理器
    /// </summary>
    public class RecordManager : MonoSingleton<RecordManager>
    {
        public string fileName;
        public bool showGUI;

        public EMode mode { get; private set; }

        //录制器
        private Recorder recorder = new Recorder();

        //播放器
        private Replayer replayer = new Replayer();

        //录制单元
        public UnitBase[] units;
        private Dictionary<int, UnitBase> unitDic = new Dictionary<int, UnitBase>();

        private UnitBase _current;

        public UnitBase Current
        {
            get => _current;
            set
            {
                if (showGUI && _current) _current.Hilight(false);
                _current = value;
                if (showGUI && _current) _current.Hilight(true);
            }
        }

        protected override void Init()
        {
            base.Init();
            foreach (var unit in units)
            {
                unitDic.Add(unit.id, unit);
            }

            Current = units[0];

            //显示录制gui和相机
            if (showGUI)
            {
                foreach (var unit in units)
                {
                    unit.gameObject.SetActive(true);
                }
            }
        }

        public void Update()
        {
            if (mode == EMode.Replay)
            {
                replayer.Update();
            }

            if (mode == EMode.Record)
            {
                recorder.Update();
            }
        }

        /// <summary>
        /// 刷新unit信息
        /// </summary>
        public void RefreshUnits()
        {
            if (mode == EMode.Record)
            {
                foreach (var unit in units)
                {
                    RecordUtility.RecordPos(unit);
                }
            }
        }

        /// <summary>
        /// 重置所有Unit
        /// </summary>
        void ResetAllUnit()
        {
            foreach (var unit in units)
            {
                unit.Reset();
            }
        }

        /// <summary>
        /// 获取unit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Unit</returns>
        public UnitBase GetUnit(int id)
        {
            if (unitDic.TryGetValue(id, out UnitBase unit))
            {
                return unit;
            }

            return null;
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


#if UNITY_EDITOR
        public void OnGUI()
        {
            if (showGUI)
            {
                OnMenuGUI();
                if (mode == EMode.Record)
                {
                    OnAnimGUI();
                }
            }
        }
#endif

        private void OnMenuGUI()
        {
            if (mode == EMode.None)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 100, 0, 100, 20), "播放模式"))
                {
                    mode = EMode.Replay;
                    replayer.Init(fileName);
                    ResetAllUnit();
                }

                if (GUI.Button(new Rect(Screen.width / 2, 0, 100, 20), "录制模式"))
                {
                    mode = EMode.Record;
                    recorder.Init(fileName);
                    ResetAllUnit();
                }
            }
            else if (mode == EMode.Replay)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50, 0, 100, 20), replayer.playing ? "暂停" : "播放"))
                {
                    replayer.ResumePlay();
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 50, 20, 100, 20), "停止"))
                {
                    replayer.StopPlay();
                    mode = EMode.None;
                }
            }
            else if (mode == EMode.Record)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50, 0, 100, 20), recorder.recording ? "暂停" : "开始"))
                {
                    recorder.ResumeRecord();
                }

                if (GUI.Button(new Rect(Screen.width / 2 - 50, 20, 100, 20), "停止"))
                {
                    recorder.StopRecord();
                    mode = EMode.None;
                }

                GUI.Label(new Rect(10, 10, 200, 20), "前后左右: WASD 上下: EQ");
            }
        }

        private void OnAnimGUI()
        {
            for (int i = 0; i < units.Length; i++)
            {
                var unit = units[i];
                if (Current == unit)
                {
                    GUI.contentColor = Color.red;
                }

                if (GUI.Button(new Rect(i * 100, 30, 100, 20), unit.name))
                {
                    Current = unit;
                }

                GUI.contentColor = Color.white;
                var anims = units[i].GetAnimationClips();
                if (anims != null)
                {
                    for (int j = 0; j < anims.Length; j++)
                    {
                        if (GUI.Button(new Rect(i * 100, 70 + j * 25, 100, 20), anims[j]))
                        {
                            units[i].RePlayAnim(anims[j]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 开始播放录制文件，用于非录制模式下外部调用，
        /// </summary>
        /// <param name="recName"></param>
        public void PlayRecord(string recName)
        {
            mode = EMode.Replay;
            ResetAllUnit();

            replayer.StopPlay();
            replayer.Init(fileName);
            replayer.ResumePlay();
        }

        public override void Dispose()
        {
            mode = EMode.None;
            replayer.StopPlay();
            Destroy(gameObject);
        }
    }
}