using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Combat {
    public class Health : MonoBehaviour {
        [SerializeField] float health = 100f;
        public void TakeDamage(float damage) {
             health = MathF.Max(health - damage, 0);
             print(health);
        } 
    }
}
