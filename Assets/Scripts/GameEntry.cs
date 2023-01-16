using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Framework.Managers;
using UnityEngine;

// 游戏的启动入口，会在游戏启动时进行游戏的初始化
public class GameEntry : MonoBehaviour
{
    void Awake()
    {
        // 初始化游戏框架
        InitFramework();
        // end
        
        // 检查资源更新
        CheckHotUpdate();
        // end
        
        // 初始化游戏逻辑
        InitGameLogic();
        // end
    }

    void CheckHotUpdate()
    {
        // 获取服务器资源+代码的版本
        // end
        // 拉去下载列表
        // end
        // 下载更新资源
        // end
    }

    void InitFramework()
    {
        gameObject.AddComponent<TimerManager>();
        gameObject.AddComponent<ResourceManager>();
        gameObject.AddComponent<UIManager>();
    }

    void InitGameLogic()
    {
        gameObject.AddComponent<GameApp>();
        GameApp.Instance.InitGame();
    }
}
