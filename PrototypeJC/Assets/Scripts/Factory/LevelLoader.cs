using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField] private List<LevelConfigurationSO> levelConfigs;
    private int levelConfigIndex=0;
    private IService spawnService;
    private IService enemyService;
    private GameObject currentEnvironment;
    private List<Transform> currentPath = new();
    private void Awake() {
        spawnService = ServiceLocator.GetService<SpawnService>();
        LoadLevel();
    }
    public void ChangeLevel() {
        if (levelConfigs == null || levelConfigs.Count == 0) return;

        levelConfigIndex++;
        if (levelConfigIndex >= levelConfigs.Count) { levelConfigIndex = 0; }
        LoadLevel();
    }
    public void LoadLevel() {
        ClearLevel();
        ((SpawnService)spawnService).SpawnLevel(levelConfigs[levelConfigIndex]);

        if (levelConfigs[levelConfigIndex].env != null) {
            currentEnvironment = GameManager.instance.EnvironmentSpawner.Environment;
        }

        if (levelConfigs[levelConfigIndex].path != null && levelConfigs[levelConfigIndex].path.pathPoints.Length > 0) {
            currentPath = GameManager.instance.PathSpawner.Path;
        }

        enemyService = ServiceLocator.GetService<EnemyService>();
        if (levelConfigs[levelConfigIndex].path != null) {
            enemyService.Initialize(new EnemyServiceParamWrapper(GameManager.instance.PathSpawner.Path, levelConfigs[levelConfigIndex].enemyConfigs));
        }

    }


    private void ClearLevel() {
        ((EnemyService)enemyService)?.Cleanup();
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
