using Sirenix.OdinInspector;
using UnityEngine;
using verell.RepositoryConfigs;
using verell.ScenesProvider;

namespace Pathfinder.Locations
{
    [CreateAssetMenu(
        menuName = "verell/Locations/" + nameof(LocationsConfig), 
        fileName = nameof(LocationsConfig))]
    public sealed class LocationsConfig : SingleScriptableObject
    {
        [SerializeField, ValueDropdown(nameof(_sceneNames))] 
        private string _defaultLocation;

        private ValueDropdownList<string> _sceneNames => SceneUtils.GetAllSceneNames();
        
        public string DefaultLocationName => _defaultLocation;
    }
}