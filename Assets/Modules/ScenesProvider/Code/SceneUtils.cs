using System.IO;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace verell.ScenesProvider
{
    public static class SceneUtils
    {

#if UNITY_EDITOR
        public static ValueDropdownList<string> GetAllSceneNames()
        {
            var sceneNames = new ValueDropdownList<string>();
            var scenes =  EditorBuildSettings.scenes;
            for (var sceneIndex = 0; sceneIndex < scenes.Length; sceneIndex++)
            {
                var scenePath = scenes[sceneIndex].path;
                var sceneName = Path.GetFileNameWithoutExtension(scenePath);
                sceneNames.Add(new ValueDropdownItem<string>(sceneName, sceneName));
            }

            return sceneNames;
        }
#endif

    }
}