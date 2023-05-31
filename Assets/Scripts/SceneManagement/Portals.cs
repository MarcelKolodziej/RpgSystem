using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement {
    public class Portals : MonoBehaviour {
        enum DestinationIdentifier 
        {
            A, B, C, D, E, F, G
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
                Debug.Log("Scene Loanding...");
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

            yield return fader.FadeOut(fadeOutTime);

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            Portals otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Debug.Log("Scene Loaded");
            Destroy(gameObject);
        }

        private Portals GetOtherPortal() {
            foreach (Portals portal in FindObjectsOfType<Portals>()) {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                print(destination);

                return portal;
            }
            return null;
        }

        private void UpdatePlayer(Portals otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }
    }
}
