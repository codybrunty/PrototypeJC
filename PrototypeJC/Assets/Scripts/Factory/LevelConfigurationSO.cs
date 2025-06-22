using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Test/Level Configuration Data")]
public class LevelConfigurationSO : ScriptableObject {
    public EnvironmentSO env;
    public PathSO path;
    public EnemyConfigurationSO[] enemyConfigs;
}