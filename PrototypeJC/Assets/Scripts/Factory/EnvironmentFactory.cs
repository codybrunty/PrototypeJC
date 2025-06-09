using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFactory : IFactory<EnvironmentData, GameObject> {
    public GameObject Create(EnvironmentData envData) {
        return Object.Instantiate(envData.environmentPrefab);
    }
}