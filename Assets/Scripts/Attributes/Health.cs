
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
            healthPoint = GetComponent<BaseStats>().GetStat(Stat.Health);
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

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if (healthPoint == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100 * (healthPoint / GetComponent<BaseStats>().GetStat(Stat.Health));
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
