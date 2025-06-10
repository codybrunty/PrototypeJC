using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public EnvironmentSpawner EnvironmentSpawner { get; private set; }
    public PathSpawner PathSpawner { get; private set; }
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

            RegisterSpawnService();
            //RegisterSaveLoadService();
            //RegisterStatsService();
            //RegisterPlayerPositionService();

        }
        else {
            Destroy(gameObject);
        }
    }
    private void RegisterSpawnService() {
        EnvironmentSpawner = new EnvironmentSpawner(new EnvironmentFactory());
        PathSpawner = new PathSpawner(new PathFactory());

        var spawners = new List<ILevelConfigurationSpawner> {
                EnvironmentSpawner,
                PathSpawner
             };
        ServiceLocator.RegisterService(new SpawnService(spawners));
    }
}