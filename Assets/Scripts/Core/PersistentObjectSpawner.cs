using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistenceObjectPrefab;
        static bool hasSpawned = false;

        void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistenceObject = Instantiate(persistenceObjectPrefab);
            DontDestroyOnLoad(persistenceObject);
        }
    }

}