using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField] private LevelConfigurationSO levelConfig;
    private SpawnService spawnService;
    private GameObject currentEnvironment;
    private List<Transform> currentPath = new();
    private void Awake() {
        spawnService = ServiceLocator.GetService<SpawnService>();
        LoadLevel();
    }

    [ContextMenu("LoadLevel")]
    private void LoadLevel() {
        ClearLevel();
        spawnService.SpawnLevel(levelConfig);

        if (levelConfig.env != null) {
            currentEnvironment = GameManager.instance.EnvironmentSpawner.Environment;
        }

        if (levelConfig.path != null && levelConfig.path.pathPoints.Length > 0) {
            currentPath = GameManager.instance.PathSpawner.Path;
        }
    }

    private void ClearLevel() {
        if (currentEnvironment != null) {
            DestroyImmediate(currentEnvironment);
            currentEnvironment = null;
        }

        if (currentPath != null) {
            foreach (Transform point in currentPath) {
                if (point != null)
                    DestroyImmediate(point.gameObject);
            }
            currentPath.Clear();
        }
    }
}
