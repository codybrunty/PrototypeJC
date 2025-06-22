using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField] private List<LevelConfigurationSO> levelConfigs;
    private int levelConfigIndex=0;
    private SpawnService spawnService;
    private EnemyService enemyService;
    private GameObject currentEnvironment;
    private List<Transform> currentPath = new();
    private Coroutine enemySpawnCoroutine;
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
        spawnService.SpawnLevel(levelConfigs[levelConfigIndex]);

        if (levelConfigs[levelConfigIndex].env != null) {
            currentEnvironment = GameManager.instance.EnvironmentSpawner.Environment;
        }

        if (levelConfigs[levelConfigIndex].path != null && levelConfigs[levelConfigIndex].path.pathPoints.Length > 0) {
            currentPath = GameManager.instance.PathSpawner.Path;
        }

        enemyService = ServiceLocator.GetService<EnemyService>();
        if (levelConfigs[levelConfigIndex].path != null) {
            enemyService.Initialize(GameManager.instance.PathSpawner.Path, levelConfigs[levelConfigIndex].enemyConfigs);
            StartEnemySpawning();
        }

    }
    public void StartEnemySpawning() {
        if (enemySpawnCoroutine != null) { StopCoroutine(enemySpawnCoroutine); }
        enemySpawnCoroutine = StartCoroutine(TrySpawnEnemy());
    }
    private IEnumerator TrySpawnEnemy() {
        while (enemyService.HasEnemiesInQueue()) {
            yield return new WaitForSeconds(Random.Range(.25f, 1.5f));
            EventBus.Publish(new EnemyTrySpawnEvent());
        }

        EventBus.Publish(new GameOverEvent());
    }

    private void ClearLevel() {
        enemyService?.Cleanup();
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
