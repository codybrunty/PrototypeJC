using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Test/Level Configuration Data")]
public class LevelConfiguration : ScriptableObject {
    public EnvironmentData env;
    public PathData path;
}