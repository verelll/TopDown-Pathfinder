using System.Collections.Generic;
using UnityEngine;
using verell.Saves;

namespace Pathfinder.GameCharacter
{
    public interface ICharacterSaveData : IBaseSaveData
    {
        bool TryGetMovePoints(out IReadOnlyList<Vector3> movePoints);
        void SetMovePoints(List<Vector3> movePoints);
    }
}