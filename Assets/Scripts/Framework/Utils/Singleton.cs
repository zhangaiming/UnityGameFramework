using UnityEngine;

namespace Framework.Utils
{
    public abstract class Singleton<T> where T : new()
    {
        static T      instance;
        static object mutex = new();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)   // 获取锁的过程中instance可能会更改，因此再判断一次
                        {
                            instance = new T();
                        }
                    }
                }

                return instance;
            }
        }
    }

    public class UnitySingleton<T> : MonoBehaviour where T : Component
    {
        static T instance = null;

        public static T Instance
        {
            get
            {
                // 为什么这里不需要锁？
                if (instance == null)
                {
                    // 现查找一下当前场景中是否存在目标类型的物体（组件），若是则直接作为实例
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new();
                        instance = obj.AddComponent<T>();
                        obj.hideFlags = HideFlags.DontSave;
                        obj.name = typeof(T).Name;
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                // 保证场景中只存在一个实例
                Destroy(gameObject);
            }
        }
    }
}