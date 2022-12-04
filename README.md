## 简介

用于UnityEngine的一个游戏录制功能

PlayerManager:总控制器，
属性简介：
mode：控制录制还是播放模式
fileName：设置录制或者播放的文件名
timeScale：时间缩放，控制录制和播放的速率

record：录制器
replay：播放器

Unit:录制器和播放器的单元类
ReplayUnit:Unit播放基类
RecordUnit:Unit录制基类

InputController:控制器，根据需要选择Unit，通过wasdeq控制Unit移动