using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaTools {
    public class PersistentObjectSpawner : NinjaMonoBehaviour {

        [Tooltip("This prefab will only be spawned once and persist between scenes.")]
        [SerializeField] GameObject persistentObjectPrefab = null;

        static bool hasSpawned = false;

        private void Awake() {
            if(hasSpawned) return;
            SpawnPersistenObject();
            hasSpawned = true;
        }

        private void SpawnPersistenObject() {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}


