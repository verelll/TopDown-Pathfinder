using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Pathfinder.GameCharacter
{
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class CharacterBehaviour : SerializedMonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        public NavMeshAgent Agent => _agent;
    }
}