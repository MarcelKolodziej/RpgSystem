using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats 
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int currentLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetHealth()
        {
            return progression.GetStat(stat, characterClass, currentLevel);
        }

        public float GetExperianceReward()
        {
            return 10;
        }

    }

}
