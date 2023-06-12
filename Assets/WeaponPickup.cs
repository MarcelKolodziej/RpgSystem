using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat {
    
    public class WeaponPickup : MonoBehaviour {

        [SerializeField] Weapons weapon = null;
        [SerializeField] float respawnTime = 5f;
        private GameObject[] allChildren; 


        private CapsuleCollider _capsuleCollider;
        private void Start() {
            _capsuleCollider = GetComponent<CapsuleCollider>();
        }

        private void OnTriggerEnter(Collider other) {
            // check if colliding with a player 
            if (other.gameObject.tag == "Player") 
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        // private void HidePickUp(bool shouldShow)
        // {
        //     GetComponent<Collider>().enabled = shouldShow;
            
        //     transform.GetChild(0).gameObject.SetActive(false);
        // }
        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);                 
            }
        }

    }

}
