using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFactory : IFactory<EnvironmentData, GameObject> {
    public GameObject Create(EnvironmentData envData) {
        if (envData == null || envData.environmentPrefab == null) {
            Debug.LogError("[EnvironmentFactory] Missing environment prefab!");
            return null;
        }

        return Object.Instantiate(envData.environmentPrefab);
    }
}