using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPath", menuName = "Test/PathData")]
public class PathSO : ScriptableObject {
    public Vector3[] pathPoints;
}
