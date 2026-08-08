using VContainer.Unity;

namespace Assets.Scripts.Health
{
    public class HealthService : IInitializable
    {
        public float maxHealth;
        private float currentHealth;

        public void Initialize()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;

            if (currentHealth < 0)
                currentHealth = 0;
        }
    }
}
