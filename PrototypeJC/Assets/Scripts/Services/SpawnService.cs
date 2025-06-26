using System.Collections.Generic;
using UnityEngine;

public class SpawnService: IService {
    private readonly List<ILevelConfigurationSpawner> spawners;

    public SpawnService(List<ILevelConfigurationSpawner> spawners) {
        this.spawners = spawners;
    }

    public void Initialize(ServiceInitParamWrapper initParams) {
        SpawnServiceParamWrapper pars = (SpawnServiceParamWrapper)initParams;
        //pars.blah
    }
    public void Execute() {
        throw new System.NotImplementedException();
    }
    public void SpawnLevel(LevelConfigurationSO config) {
        foreach (var spawner in spawners) {
            spawner.Spawn(config);
        }
    }
}