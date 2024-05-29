using System;
using System.Collections.Generic;
using UnityEngine;
using verell.Architecture;

namespace Pathfinder.GameCharacter
{
    [Serializable]
    public sealed class CharacterSaveData : SharedObject, ICharacterSaveData
    {
        public string SaveTypeName => nameof(CharacterSaveData);

        public List<Vector3> MovePoints = new List<Vector3>();

        bool ICharacterSaveData.TryGetMovePoints(out IReadOnlyList<Vector3> movePoints)
        {
            movePoints = default;
            if (MovePoints == null || MovePoints.Count == 0)
                return false;

            movePoints = MovePoints;
            return true;
        }

        void ICharacterSaveData.SetMovePoints(List<Vector3> movePoints)
        {
            MovePoints = movePoints;
        }
    }
}