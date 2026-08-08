using Assets.Scripts.Damage;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyView : MonoBehaviour, IDamagable
    {
        public void Construct(EnemyModel enemyModel)
        {
            EnemyModel = enemyModel;
        }

        public EnemyModel EnemyModel { get; private set; }

        public EnemyConfigs configs;
        public NavMeshAgent navMeshAgent;

        public void TakeDamage(float damage)
        {
            EnemyModel.healthService.TakeDamage(damage);
        }
    }
}
