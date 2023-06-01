using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement {

    // TODO: bug fadeOut na scenkach.\
    public class Portal : MonoBehaviour {
        enum DestinationIdentifier 
        {
            A, B, C, D, E
        }

    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    [SerializeField]  float fadeOutTime = 1f;
    [SerializeField]  float fadeInTime = 2f;
    [SerializeField]  float fadeWaitTime = 0.5f;
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player") {
                StartCoroutine(Transition());
                Debug.Log("Scene Loading...");
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to Load not set.");
                yield break;
            }
            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            print(fader);
            yield return fader.FadeOut(fadeOutTime);

            // Save current level
            FindObjectOfType<SavingWrapper>().Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            // Load current level 
           FindObjectOfType<SavingWrapper>().Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            // wait for camera stabilization
            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Debug.Log("Scene Loaded");
            Destroy(gameObject);
        }

        private Portal GetOtherPortal() {
            foreach (Portal portal in FindObjectsOfType<Portal>()) {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                print(destination);

                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
