
using UnityEngine;
using RPG.Saving;
using Newtonsoft.Json.Linq;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, IJsonSaveable
    {
        [SerializeField] float regenerationPercentage = 70f;
        float healthPoint = -1f;
        bool isDead = false;
        void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if (healthPoint < 0)
            {
                healthPoint = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        public bool IsDead { get => isDead; }

        private void RegenerateHealth()
        {
            float regenerationHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            healthPoint = Mathf.Max(healthPoint, regenerationHealthPoints);
        }

        public JToken CaptureAsJToken()
        {
            return JToken.FromObject(healthPoint);
        }

        public void RestoreFromJToken(JToken state)
        {
            healthPoint = state.ToObject<float>();
            if (healthPoint <= 0)
            {
                Die();
            }
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + "took damage" + damage);
            healthPoint = Mathf.Max(healthPoint - damage, 0);
            if (healthPoint == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetHealthPoints()
        {
            Debug.Log($"healthPoint:{healthPoint}");
            return healthPoint;
        }

        public float GetMaxHealthPoint()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
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
