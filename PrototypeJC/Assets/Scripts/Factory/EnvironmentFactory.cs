using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentFactory : IFactory<LevelConfigurationSO, GameObject> {
    public GameObject Create(LevelConfigurationSO levelConfig) {
        return Object.Instantiate(levelConfig.env.environmentPrefab);
    }
}