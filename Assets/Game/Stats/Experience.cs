using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using RPG.Saving;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiancePoints = 0;

        public event Action onExperianceGained;

        public void GainExperianceReward(float experience)
        {
            experiancePoints += experience;
            onExperianceGained();
        }
        public object CaptureState()
        {
            return experiancePoints;
        }

        public void RestoreState(object state)
        {
            experiancePoints = (float)state;
        }

        public float GetPoints()
        {
            return experiancePoints;
        }


    }
}
