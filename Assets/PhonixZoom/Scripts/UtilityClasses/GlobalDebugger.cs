using SimpleJSON;
using UnityEngine;
namespace _Gh_Debugger
{
    public static class GlobalDebugger 
    {
        [SerializeField]
        static bool useLog;

        public static void Log(object message, Object sender)
        {
            if (useLog)
                Debug.Log(message, sender);
        }
        
    }
}