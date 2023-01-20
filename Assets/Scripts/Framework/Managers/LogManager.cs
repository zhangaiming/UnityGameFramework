using System.IO;
using Framework.Utils;
using UnityEngine;

namespace Framework.Managers
{
    public class LogManager : UnitySingleton<LogManager>
    {
        // public string logPath = Application.persistentDataPath + "/Logs/";
        
        public void Log(object msg)
        {
            Debug.Log(msg);
        }

        public void Log(object msg, Object context)
        {
            Debug.Log(msg, context);
        }

        public void LogWarning(object msg)
        {
            Debug.LogWarning(msg);
        }

        public void LogWarning(object msg, Object context)
        {
            Debug.LogWarning(msg, context);
        }
        
        public void LogError(object msg)
        {
            Debug.LogError(msg);
        }

        public void LogError(object msg, Object context)
        {
            Debug.LogError(msg, context);
        }
    }
}