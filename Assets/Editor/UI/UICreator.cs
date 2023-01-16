using System;
using UnityEditor;
using UnityEngine;

namespace Editor.UI
{
    public class UICreator : EditorWindow
    {
        static string outputPath = "Assets/Scripts/Game/UIControllers/";
        
        [MenuItem("UIFramework/UICreator")]
        public static void CreateUI()
        {
            var win = GetWindow<UICreator>();
            win.titleContent.text = "UICreator";
            win.Show();
        }

        void OnGUI()
        {
            bool hasSelection = Selection.activeGameObject != null;
            // 绘制界面
            if (!hasSelection)
            {
                GUILayout.Label("请选中UI预制体");
            }
            else
            {
                GUILayout.Label("已选中：" + Selection.activeGameObject.name);
                GUILayout.Label("将输出文件：" + outputPath + Selection.activeGameObject.name + "_Ctrl.cs");
                
            }

            EditorGUI.BeginDisabledGroup(!hasSelection);
            if (GUILayout.Button("生成UI代码文件"))
            {
                if (Selection.activeGameObject != null)
                {
                    string className = Selection.activeGameObject.name + "_Ctrl";
                    UICreatorUtil.GenUICtrl(outputPath + Selection.activeGameObject.name + "_Ctrl.cs", className);
                    AssetDatabase.Refresh();
                }
            }
            EditorGUI.EndDisabledGroup();
        }

        void OnSelectionChange()
        {
            Repaint();
        }
    }
}
