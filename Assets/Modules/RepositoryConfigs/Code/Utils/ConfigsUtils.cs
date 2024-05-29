using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace verell.RepositoryConfigs
{
    public static class ConfigsUtils
    {
#if UNITY_EDITOR
        public static List<T> FindAssetsByType<T>(string filter) where T : Object
        {
            var guids = AssetDatabase.FindAssets($"t:{filter}");
            var configs = new List<T>();
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var config = AssetDatabase.LoadAssetAtPath<T>(path);
                if (config == null) 
                    continue;
                
                configs.Add(config);
            }

            return configs;
        }
#endif
    }
}