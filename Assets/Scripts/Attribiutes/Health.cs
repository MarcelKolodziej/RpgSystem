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

        [SerializeField] float healthPoints = 100f;
        Animator _animator;
        private const string Dead = "dead";
        public bool isDead = false;
        private void Start() {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }
        public void TakeDamage(GameObject instigator, float damage) {
             healthPoints = MathF.Max(healthPoints - damage, 0);
             if (healthPoints == 0) {
                 Death();
                 AwardExperiance(instigator);   

             }
        }

        private void AwardExperiance(GameObject instigator)
        {
           Experiance experiance =  instigator.GetComponent<Experiance>();
           if (experiance == null) return;

           experiance.GainExperianceReward(GetComponent<BaseStats>().GetExperianceReward());
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
        }

        public bool IsDead() {
            return isDead;
        }

        public void Death() {
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
