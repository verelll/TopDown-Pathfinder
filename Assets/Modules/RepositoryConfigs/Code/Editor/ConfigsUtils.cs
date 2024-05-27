using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace verell.RepositoryConfigs
{
    public static class ConfigsUtils
    {
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
    }
}