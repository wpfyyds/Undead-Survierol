# Undead Survivor - 僵尸生存 Roguelike 游戏


一个基于Unity引擎开发的3D僵尸生存roguelike游戏，玩家需要在无尽的僵尸浪潮中生存，通过击败敌人获得经验升级，选择不同的能力组合来构建独特的战斗风格。

# 🎮 游戏特色
核心玩法
Roguelike生存体验 - 每次游戏都是全新的挑战，随机的能力选择让每局游戏都充满变数

动态难度系统 - 随着时间推移，僵尸会变得越来越强大，考验玩家的策略和操作

多样化武器系统 - 包含近战武器、远程枪械等多种战斗方式

永久死亡机制 - 死亡后重新开始，但每次尝试都能积累游戏经验

技术亮点
模块化架构设计 - 采用高度模块化的代码结构，便于功能扩展和维护

状态机驱动的AI系统 - 为僵尸敌人实现了智能的行为状态机

事件驱动的UI系统 - 响应式UI设计，实时反馈游戏状态变化

可配置的游戏平衡 - 通过ScriptableObject实现游戏参数的灵活调整

# 🛠 技术架构
主要系统模块
## 1. 角色控制系统 (Assets/Scripts/Player/)
PlayerController.cs - 玩家移动和输入处理

PlayerAnimation.cs - 角色动画状态管理

PlayerStats.cs - 玩家属性管理系统

PlayerHealth.cs - 生命值管理和伤害处理

## 2. 战斗系统 (Assets/Scripts/Weapon/)
Weapon.cs - 武器基类，支持多种武器类型

MeleeWeapon.cs - 近战武器实现

Gun.cs - 枪械武器系统

Damage.cs - 伤害计算和传递

Bullet.cs - 子弹物理和碰撞检测

## 3. 敌人AI系统 (Assets/Scripts/Enemy/)
Enemy.cs - 敌人基类

EnemyAI.cs - 智能行为决策

ZombieController.cs - 僵尸特定行为

状态机实现：巡逻、追击、攻击、死亡等状态

## 4. Roguelike系统 (Assets/Scripts/Roguelike/)
随机升级选择机制

能力组合系统

游戏进度管理

经验值和等级系统

## 5. UI管理系统 (Assets/Scripts/UI/)
UIManager.cs - 界面统一管理

HealthBar.cs - 生命值显示

ExperienceBar.cs - 经验进度显示

GameOverScreen.cs - 游戏结束界面

## 6. 游戏管理 (Assets/Scripts/Game/)
GameManager.cs - 游戏状态管理

AudioManager.cs - 音频系统管理

ObjectPool.cs - 对象池优化

# 🚀 安装和运行
环境要求
Unity 2022.3 LTS 或更高版本

.NET Framework 4.x

运行步骤
克隆项目到本地

bash
git clone https://github.com/wpfyyds/Undead-Survierol.git
使用Unity Hub打开项目

打开 Assets/Scenes/Main.unity 场景

点击运行按钮开始游戏

控制方式
WASD - 移动角色

鼠标 - 瞄准和攻击方向

鼠标左键 - 攻击

空格键 - 特殊技能/闪避

ESC - 暂停游戏

# 📁 项目结构
text
Undead-Survivor/
├── Assets/
│   ├── Scripts/           # 核心游戏脚本
│   │   ├── Player/        # 玩家相关系统
│   │   ├── Enemy/         # 敌人AI系统
│   │   ├── Weapon/        # 武器战斗系统
│   │   ├── UI/           # 用户界面
│   │   ├── Roguelike/    # Roguelike机制
│   │   ├── Game/         # 游戏管理
│   │   └── Utilities/    # 工具类
│   ├── Prefabs/          # 预制体资源
│   ├── Scenes/           # 游戏场景
│   ├── Audio/            # 音效资源
│   ├── Materials/        # 材质和着色器
│   ├── Models/           # 3D模型
│   ├── Animations/       # 动画控制器
│   └── Textures/         # 纹理贴图
├── ProjectSettings/      # Unity项目设置
└── Packages/            # Unity包管理
