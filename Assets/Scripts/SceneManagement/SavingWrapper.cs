using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement {
public class SavingWrapper : MonoBehaviour
{
    const string defaultSaveFile = "save";
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.L)) {
        Load();
      }   
         if(Input.GetKeyDown(KeyCode.S)) {
         Console.WriteLine("game Saved");
         Save();
      }   
    }
        private void Load() {
            // call a saving system load
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        private void Save() {
            // call a saving system save
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }
    }

}
