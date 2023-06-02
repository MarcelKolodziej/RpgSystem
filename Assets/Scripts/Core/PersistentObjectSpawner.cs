using System;
using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(PersistentObjectSpawner))]

    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawned = false; // static lives, remeber a value, with application rather then script.

        private void Awake() {
            if (hasSpawned) return;

            SpawnPersistenObjects();

            hasSpawned = true;
        }

        private void SpawnPersistenObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }

}