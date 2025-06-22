using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Test/Enemy")]
public class EnemyConfigurationSO : ScriptableObject {
    public GameObject enemyPrefab;
    public float waitTime = 5f;
}