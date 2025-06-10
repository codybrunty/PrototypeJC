using System.Collections.Generic;
using UnityEngine;

public class SpawnService {
    private readonly List<ILevelConfigurationSpawner> spawners;

    public SpawnService(List<ILevelConfigurationSpawner> spawners) {
        this.spawners = spawners;
    }

    public void SpawnLevel(LevelConfigurationSO config) {
        foreach (var spawner in spawners) {
            spawner.Spawn(config);
        }
    }
}