using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats 
{
    
    public class ExperianceDisplay : MonoBehaviour
    {
       Experience experience; 

       private void Awake() {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
       }       

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", experience.GetPoints());
        }
    }
}
