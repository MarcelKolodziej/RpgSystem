using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using RPG.Saving;

namespace RPG.Stats
{
    public class Experiance : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiancePoints = 0;

        public void GainExperianceReward(float experiance)
        {
            experiancePoints += experiance;
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
