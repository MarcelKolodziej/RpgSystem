using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attribiutes 
{
    
    public class ExperianceDisplay : MonoBehaviour
    {
       Experiance experiance; 

       private void Awake() {
            experiance = GameObject.FindWithTag("Player").GetComponent<Experiance>();
       }       

        private void Update()
        {
            GetComponent<Text>().text = String.Format("{0:0}", experiance.GetPoints());
        }
    }
}
