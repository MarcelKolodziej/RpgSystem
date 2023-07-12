using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Attribiutes 
{
    public class Health : MonoBehaviour, ISaveable {

        [SerializeField] float regenerationPercentage = 70;
        float healthPoints = -1f;
        Animator _animator;
        private const string Dead = "dead";
        public bool isDead = false;

        private void Start() 
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }
        public void TakeDamage(GameObject instigator, float damage) {
            print(gameObject.name + " took damage " +  damage);
             healthPoints = MathF.Max(healthPoints - damage, 0);
             if (healthPoints == 0) {
                 Death();
                 AwardExperiance(instigator);   
             }
        }

        public float GetHealthPoints() 
         {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
         {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints = MathF.Max(healthPoints, regenerationPercentage);
        }
        private void AwardExperiance(GameObject instigator)
        {
           Experience experiance =  instigator.GetComponent<Experience>();
           if (experiance == null) return;

           experiance.GainExperianceReward(GetComponent<BaseStats>().GetStat(Stat.ExpieranceReward));
        }
        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        public bool IsDead() 
        {
            return isDead;
        }
        public void Death() 
        {
            if (isDead) return;

            Debug.Log("Death come uppon you..");
            isDead = true;
            GetComponent<Animator>().SetTrigger(Dead);
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        public object CaptureState()
        {
            return healthPoints;
        }
        public void RestoreState(object state)
        {
            healthPoints = (float)state;

             if (healthPoints == 0) {
                 Death();
             }
        }
    }

}
