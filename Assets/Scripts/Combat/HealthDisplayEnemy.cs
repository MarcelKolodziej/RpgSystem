using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attribiutes 
{
    public class HealthDisplayEnemy : MonoBehaviour
    {
        Fighter fighter;
        private void Awake() {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (fighter.GetTarget() == null)
            {
                GetComponent<Text>().text = "N/A";
                return;
            }
            Health health = fighter.GetTarget();
            GetComponent<Text>().text = String.Format("{0:0} / {1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }

}
