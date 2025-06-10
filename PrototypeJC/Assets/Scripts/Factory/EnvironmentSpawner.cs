using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : ILevelConfigurationSpawner {
    private readonly IFactory<LevelConfigurationSO, GameObject> factory;
    public GameObject Environment { get; private set; }

    public EnvironmentSpawner(IFactory<LevelConfigurationSO, GameObject> factory) {
        this.factory = factory;
    }

    public void Spawn(LevelConfigurationSO config) {
        if (config.env == null) return;
        Environment = factory.Create(config);
    }
}