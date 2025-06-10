using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : ILevelConfigurationSpawner {
    private readonly IFactory<LevelConfigurationSO, List<Transform>> factory;
    public List<Transform> Path { get; private set; }

    public PathSpawner(IFactory<LevelConfigurationSO, List<Transform>> factory) {
        this.factory = factory;
    }

    public void Spawn(LevelConfigurationSO config) {
        if (config.path == null || config.path.pathPoints.Length == 0) return;
        Path = factory.Create(config);
    }
}