using UnityEditor;
using UnityEngine;

namespace Assets.scripts
{
    public static class Utils{
        public static void PrintObj(string title, object obj)
        {
            MonoBehaviour.print($"{title}: {obj}");
        }
    }
    
    
    
}