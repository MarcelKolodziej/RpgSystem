using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Core {
    public class Health : MonoBehaviour {

        [SerializeField] float healthPoints = 100f;
        Animator _animator;
        private const string Dead = "dead";
        public bool isDead = false;
        public void TakeDamage(float damage) {
             healthPoints = MathF.Max(healthPoints - damage, 0);
             if (healthPoints == 0) {
                 Death();
             }
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
        
    }
}

