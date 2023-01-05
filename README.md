## 简介

用于UnityEngine的一个游戏录制功能

## PlayerManager:总控制器

属性简介：

FileName：设置录制或者播放的文件名

ShowGUI：录制时显示录制UI，真实游戏环境，可以隐藏GUI

Units: 游戏场景中所有的录制单元

## record
录制器
## replay
播放器
## RecordInput
RecordInput:控制器，根据需要选择Unit，通过wasdeq控制Unit移动

## Unit

UnitBase:录制器和播放器的单元基类

UnitPlayer:用于玩家录制类

UnitItem:用于物品录制类

Unitxxx：...自行扩展
