using System;
using verell.Architecture;
using verell.Saves;

namespace Pathfinder.GameStates
{
    [Serializable]
    public sealed class TestGameplayStorage : SharedObject, IBaseSaveData
    {
        public string SaveTypeName => "TestGameplayStorage";
        public int PlayClickedCount;
    }
}