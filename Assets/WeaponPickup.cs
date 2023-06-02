using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    
    public class WeaponPickup : MonoBehaviour {

        [SerializeField] Weapons weapon;

        private void OnTriggerEnter(Collider other) {
            // check if colliding with a player 
            if (other.tag == "Player") {
            print("Player detected");
                // pickup object 
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                // destroy
                Destroy(gameObject);
            }
        }
    }
}
