using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : MonoBehaviour
    {
        [Inject]
        public void Construct(EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
        }

        public EnemyModel EnemyModel { get; private set; }

        public EnemyConfigs configs;
        public NavMeshAgent navMeshAgent;
    }
}
