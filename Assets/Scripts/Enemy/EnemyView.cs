using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : MonoBehaviour
    {
        public EnemyConfigs enemyConfigs;
        public NavMeshAgent navMeshAgent;
    }
}
