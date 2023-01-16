using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.UI
{
    public class UICreatorUtil
    {
        public static void GenUICtrl(string filePath, string className)
        {
            if (File.Exists(Application.dataPath + filePath))
            {
                return;
            }
            var sw = new StreamWriter(filePath);
            string content = "using UnityEngine;\n" +
                             "using System.Collections;\n" +
                             "using Framework.Utils;\n" +
                             "\n" +
                             "public class " + className + " : UICtrl" +
                             "{\n" +
                             "\n" +
                             "}\n";
            sw.Write(content);
            sw.Flush();
            sw.Close();
        }
    }
}
