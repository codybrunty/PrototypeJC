using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField] private LevelConfigurationSO levelConfig;
    private SpawnService spawnService;

    private void Awake() {
        spawnService = ServiceLocator.GetService<SpawnService>();
        LoadLevel();
    }

    private void LoadLevel() {
        spawnService.SpawnLevel(levelConfig);

        //Do Something With This Later...
        GameObject environment = GameManager.instance.EnvironmentSpawner.Environment;
        List<Transform> path = GameManager.instance.PathSpawner.Path;
    }
}
