using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    
    public class WeaponPickup : MonoBehaviour {

        [SerializeField] Weapons weapon = null;

        private void OnTriggerEnter(Collider other) {
            // check if colliding with a player 
            if (other.gameObject.tag == "Player") 
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                // destroy
                Destroy(gameObject);
            }
        }
    }
}
