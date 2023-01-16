using Framework.Managers;
using Framework.Utils;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameApp : UnitySingleton<GameApp>
    {
        public void InitGame()
        {
            EnterMainScene();
            // 游戏具体的初始化逻辑放到具体场景中的某一个Manager中进行
        }

        public void EnterMainScene()
        {
            // 加载主场景、背景、3D场景、UI等元素
            // 当前框架下，整个游戏仅一个游戏Scene，其中场景的切换等都交由代码中动态进行
            GameObject mapPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Maps/MapPrefabs/WorldMap.prefab");
            var map = Instantiate(mapPrefab);
            map.name = mapPrefab.name;
            
            // 挂载游戏管理类GameManager
            // end
            
            UIManager.Instance.ShowUI("UIHome");
            // end
        }
    }
}