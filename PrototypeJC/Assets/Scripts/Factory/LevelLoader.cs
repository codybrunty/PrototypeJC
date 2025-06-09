using UnityEngine;

public class LevelLoader : MonoBehaviour {
    [SerializeField] private LevelConfiguration levelConfig;
    private SpawnService spawnService;

    private void Awake() {
        RegisterSpawnService();
        LoadLevel();
    }

    private void RegisterSpawnService() {
        ServiceLocator.RegisterService(new SpawnService(new EnvironmentFactory(), new PathFactory()));
        spawnService = ServiceLocator.GetService<SpawnService>();
    }

    private void LoadLevel() {
        spawnService.SpawnLevel(levelConfig);
    }

}
