using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace verell.RepositoryConfigs
{
    [CreateAssetMenu(fileName = nameof(RepositoryConfig))]
    public sealed class RepositoryConfig : SerializedScriptableObject
    {
        private const string ConfigName = "RepositoryConfig.asset";
        
        [SerializeField]
        private Dictionary<Type, SingleScriptableObject> _singlePairs;
        
        [SerializeField] 
        private Dictionary<Type, List<MultiScriptableObject>>  _multiPairs;
        
        internal static RepositoryConfig Instance
        {
            get
            {
                if (_instance) 
                    return _instance;
                
#if UNITY_EDITOR
                var type  = typeof(RepositoryConfig);
                var paths = AssetDatabase.GetAllAssetPaths();
                var path  = paths.FirstOrDefault(p => p.EndsWith(ConfigName));
                _instance = (RepositoryConfig) AssetDatabase.LoadAssetAtPath(path, type);
#else
				_instance = Resources.LoadAll<RepositoryConfig>("").First();
#endif

                return _instance;
            }
        }
        
        private static RepositoryConfig _instance;

        internal T GetConfig<T>() where T : SingleScriptableObject
        {
            if (!_singlePairs.TryGetValue(typeof(T), out var so) || so == null) 
                return default;
            
            return so as T;
        }

        internal IReadOnlyList<T> GetConfigs<T>() where T : MultiScriptableObject
        {
            if (!_multiPairs.TryGetValue(typeof(T), out var soList) || soList == null) 
                return default;
            
            var result = new List<T>();
            foreach (var so in soList)
            {
                if(so == null)
                    continue;
                
                result.Add(so as T);
            }

            return result;

        }
        
#if UNITY_EDITOR
        
        [Button, PropertyOrder(0)]
        private void Refresh()
        {
            _singlePairs ??= new Dictionary<Type, SingleScriptableObject>();
            _multiPairs ??= new Dictionary<Type, List<MultiScriptableObject>>();

            var singleConfigs = ConfigsUtils.FindAssetsByType<SingleScriptableObject>(nameof(SingleScriptableObject));
            foreach (var config in singleConfigs)
            {
                var type = config.GetType();
                if (_singlePairs.ContainsKey(type))
                {
                    Debug.LogWarning($"[RepositoryConfig] Singleton with same type already exist! Type: {type}");
                    continue;
                }
                
                _singlePairs[type] = config;
            }
            
            var multiConfigs =  ConfigsUtils.FindAssetsByType<MultiScriptableObject>(nameof(MultiScriptableObject));
            foreach (var config in multiConfigs)
            {
                var type = config.GetType();
                if (!_multiPairs.ContainsKey(type))
                    _multiPairs[type] = new List<MultiScriptableObject>();
                
                _multiPairs[type].Add(config);
            }
        }
        
#endif
    }
}