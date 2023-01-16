using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Framework.Utils
{
    public class UICtrl : MonoBehaviour
    {
        public Dictionary<string, GameObject> view = new();
        
        void LoadView(GameObject root, string path)
        {
            foreach (Transform trans in root.transform)
            {
                string relPath = path + trans.gameObject.name;
                if (view.ContainsKey(relPath)) continue;
                view.Add(relPath, trans.gameObject);
                LoadView(trans.gameObject, relPath + "/");
            }
        }
        
        protected virtual void Awake()
        {
            // 将视图中的所有物体与其对应相对路径关联起来，方便后续编码过程中查找各个组件
            LoadView(gameObject, "");
            // end
        }

        public void AddButtonListener(string viewName, UnityAction onClickHandler)
        {
            var bt = view[viewName].GetComponent<Button>();
            if (bt == null)
            {
                Debug.LogWarning("no button component");
            }
            bt.onClick.AddListener(onClickHandler);
        }
    }
}