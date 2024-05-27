using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace verell.Saves
{
    public static class SavesUtils
    {
        private const string SaveDataPropertyName = "SaveTypeName";
        
        [MenuItem("verell/Saves/Open Saves Folder", priority = 0)]
        public static void OpenSavesFolder()
        {
            var destination = Application.persistentDataPath;
            if(!Directory.Exists(destination))
            {
                Debug.Log("[SavesUtils] Can't find the path to the folder. Start play and try again!");
                return;
            }
            
            Process.Start(destination);
        }
        
        [MenuItem("verell/Saves/Clear Saves")]
        public static void ClearSaves()
        {
            var types = GetAllSaveTypes();
            foreach (var saveDataType in types)
            {
                var containerSaveName = string.Empty;
                
                if (saveDataType != null)
                {
                    if (saveDataType.GetProperty(SaveDataPropertyName) != null)
                    {
                        var value = saveDataType.GetProperty(SaveDataPropertyName);
                        containerSaveName = value.GetValue(null) as string;
                    }
                }
                
                if (string.IsNullOrEmpty(containerSaveName)) 
                    continue;
            
                var destination = $"{Application.persistentDataPath}/{containerSaveName}.dat";
                if (!File.Exists(destination)) 
                    continue;
            
                File.Delete(destination);
            }
            
            PlayerPrefs.DeleteAll();
            Debug.Log("[SavesUtils] Saves deleted only for Files and Prefs! Completed!");
        }

        private static IReadOnlyList<Type> GetAllSaveTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IBaseSaveData).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList();
        }
    }
}