using System.Collections.Generic;
using UnityEngine;

public class ServiceInitParamWrapper { }

public class EnemyServiceParamWrapper : ServiceInitParamWrapper {
    private List<Transform> path;
    private EnemyConfigurationSO[] enemyConfigs;

    public EnemyServiceParamWrapper(List<Transform> path, EnemyConfigurationSO[] enemyConfigs) {
        this.path = path;
        this.enemyConfigs = enemyConfigs;
    }

    public List<Transform> Path => path;
    public EnemyConfigurationSO[] EnemyConfigs => enemyConfigs;
}

public class SpawnServiceParamWrapper : ServiceInitParamWrapper {
    public SpawnServiceParamWrapper() { }
}