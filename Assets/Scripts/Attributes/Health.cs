using UnityEngine;
using RPG.Saving;
using Newtonsoft.Json.Linq;
using RPG.Stats;
using RPG.Core;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float healthPoint = 100f;
        bool isDead = false;
        void Start()
        {
            healthPoint = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead { get => isDead; }
        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(healthPoint);
        }

        public void RestoreFromJToken(JToken state)
        {
            healthPoint = state.ToObject<float>();
            //UpdateState();
        }

        public void RestoreState(object state)
        {
            healthPoint = (float)state;
            if (healthPoint == 0)
            {
                Die();
            }
        }

        public void TakeDamage(float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if (healthPoint == 0)
            {
                Die();
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoint / GetComponent<BaseStats>().GetHealth());
        }

        private void Die()
        {
            if (isDead) return;
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
