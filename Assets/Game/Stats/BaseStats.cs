using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace RPG.Stats 
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        int currentLevel = 0;
        public event Action onLevelUp;
        [SerializeField] private GameObject levelup_Particle; 

        private void Start() 
        {
            currentLevel = CalculateLevel();
            Experience experiance = GetComponent<Experience>();
            if (experiance != null)
            {
                experiance.onExperianceGained += UpdateLevel;
            }
        }
        private void PlayParticle()
        {
            Instantiate(levelup_Particle, transform);
        }
        private void UpdateLevel() 
        {
           int newLevel = CalculateLevel();
           if (newLevel > currentLevel)
           {
                currentLevel = newLevel;
                print("Levelling up!");
                PlayParticle();
                onLevelUp();
           }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }

            return currentLevel;
        }
        public int CalculateLevel()
        {   
            Experience experiance = GetComponent<Experience>();
            if (experiance == null) return startingLevel;

            float currentXP = GetComponent<Experience>().GetPoints();

            int penultimateLevel = progression.GetLevels(Stat.ExperianceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUP = progression.GetStat(Stat.ExperianceToLevelUp, characterClass, level);
                if (XPToLevelUP > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;            
                
        }
    }
}
