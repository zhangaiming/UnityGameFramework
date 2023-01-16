using Framework.Utils;
using UnityEditor;
using UnityEngine;

namespace Framework.Managers
{
    /// <summary>
    /// 为了避免开发过程中调整和测试资源时的重复工作，
    /// 资源的加载在编辑器模式和发布模式下的具体实现有所不同。
    /// 1.开发模式下，资源使用AssetDatabase直接从文件夹中加载资源，以减少重复打包工作；
    /// 2.发布模式下，使用Addressable或AssetBundle从包中进行加载，以支持热更新。
    /// </summary>
    public class ResourceManager : UnitySingleton<ResourceManager>
    {
        public T GetAssetCache<T>(string name) where T : Object
        {
#if UNITY_EDITOR
            string path = "Assets/AssetPackage/" + name;
            var target = AssetDatabase.LoadAssetAtPath<T>(path);
            return target;
#else
            // 这里使用Addressable或AssetBundle进行资源加载
            throw new NotImplementException();
#endif
        }
    }
}