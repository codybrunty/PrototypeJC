using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService: IService {
    private List<Transform> entrancePath;
    private List<Enemy> enemySlots; // Size == entrancePath.Count
    private Queue<EnemyConfigurationSO> enemyQueue = new();
    private Coroutine enemySpawnCoroutine;

    public bool HasEnemiesInQueue() { return enemyQueue.Count > 0; }

    public void Initialize(ServiceInitParamWrapper initParams) {
        EnemyServiceParamWrapper pars = (EnemyServiceParamWrapper)initParams;
        entrancePath = pars.Path;
        enemySlots = new List<Enemy>(new Enemy[pars.Path.Count]);
        SpawnEnemies(pars.EnemyConfigs);
        EventBus.Subscribe<EnemyTrySpawnEvent>(TrySpawnFromQueue);
        StartEnemySpawning();
    }
    public void Execute() {
        throw new System.NotImplementedException();
    }
    public void StartEnemySpawning() {
        if (enemySpawnCoroutine != null) { CoroutineRunner.Instance().StopCoroutine(enemySpawnCoroutine); }
        enemySpawnCoroutine = CoroutineRunner.Instance().StartCoroutine(TrySpawnEnemy());
    }

    private IEnumerator TrySpawnEnemy() {
        while (HasEnemiesInQueue()) {
            yield return new WaitForSeconds(Random.Range(.25f, 1.5f));
            EventBus.Publish(new EnemyTrySpawnEvent());
        }

        EventBus.Publish(new GameOverEvent());
    }


    public void SpawnEnemies(EnemyConfigurationSO[] enemyConfigs) {
        foreach (var enemy in enemyConfigs) {
            enemyQueue.Enqueue(enemy);
        }
    }
    public void SpawnEnemy(EnemyConfigurationSO enemyConfig) {
        int lastIndex = entrancePath.Count - 1;
        if (enemyConfig.enemyPrefab == null) {
            Debug.LogWarning("Enemy prefab missing in config.");
            return;
        }
        if (enemySlots[lastIndex] == null) {
            GameObject enemyGO = GameObject.Instantiate(enemyConfig.enemyPrefab, entrancePath[lastIndex].position, Quaternion.identity);
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.Initialize(enemyConfig.waitTime, this, entrancePath, lastIndex);
            enemySlots[lastIndex] = enemy;
        }
    }
    public bool IsSlotEmpty(int index) {
        return enemySlots[index] == null;
    }

    public void MoveEnemy(Enemy enemy, int fromIndex, int toIndex) {
        enemySlots[toIndex] = enemy;
        enemySlots[fromIndex] = null;
    }
    public void EnemyLeft(int index, Enemy who) {
        if (enemySlots[index] == who) {
            enemySlots[index] = null;
        }
    }

    private void TrySpawnFromQueue(EnemyTrySpawnEvent empty) {
        if (enemyQueue.Count == 0) return;

        int lastIndex = entrancePath.Count - 1;
        if (enemySlots[lastIndex] == null) {
            SpawnEnemy(enemyQueue.Dequeue());
        }
    }
    public void Cleanup() {
        EventBus.Unsubscribe<EnemyTrySpawnEvent>(TrySpawnFromQueue);

        if (enemySlots != null) {
            foreach (var enemy in enemySlots) {
                if (enemy != null) {
                    GameObject.Destroy(enemy.gameObject);
                }
            }
            enemySlots.Clear();
        }

        enemyQueue?.Clear();
        entrancePath = null;
        enemySlots = null;
    }


}
