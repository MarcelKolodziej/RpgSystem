using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace RPG.Stats
{
    public class Experiance : MonoBehaviour
    {
        [SerializeField] float experiancePoints = 0;

        public void GainExperianceReward(float experiance)
        {
            experiancePoints += experiance;
        }
    }
}
