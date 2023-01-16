using System;
using System.Collections.Generic;
using Framework.Utils;
using UnityEngine;

namespace Framework.Managers
{
    public class UIManager : UnitySingleton<UIManager>
    {
        string uiPrefabRoot = "GUI/UIPrefabs/";
        // 场景中的Canvas物体
        GameObject canvasObj;

        // 当前已经显示（激活）的视图对应的Ctrl
        Dictionary<string, UICtrl> activeCtrl = new();

        protected override void Awake()
        {
            base.Awake();
            canvasObj = GameObject.Find("Canvas");
            if (canvasObj == null)
            {
                Debug.LogError("Canvas not found!!");
            }
        }

        public UICtrl GetUICtrl(string name)
        {
            if (!activeCtrl.ContainsKey(name))
            {
                return null;
            }

            return activeCtrl[name];
        }
        
        public UICtrl ShowUI(string name, Transform parent = null)
        {
            if (activeCtrl.ContainsKey(name))
            {
                return activeCtrl[name];
            }
            
            var prefab = ResourceManager.Instance.GetAssetCache<GameObject>(uiPrefabRoot + name + ".prefab");
            if (prefab == null)
            {
                Debug.LogError($"UI prefab named {name} not found in {uiPrefabRoot}!");
            }

            if (parent == null)
            {
                parent = canvasObj.transform;
            }
            
            var view = Instantiate(prefab, parent, false);
            view.name = name;

            var ctrlType = Type.GetType(name + "_Ctrl");
            var ctrl = view.AddComponent(ctrlType) as UICtrl;
            activeCtrl.Add(name, ctrl);

            return ctrl;
        }

        public void CloseUI(string name)
        {
            var ctrl = GetUICtrl(name);
            if (ctrl == null) return;
            activeCtrl.Remove(name);
            Destroy(ctrl.gameObject);
        }
    }
}