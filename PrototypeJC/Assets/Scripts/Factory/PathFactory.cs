using System.Collections.Generic;
using UnityEngine;

public class PathFactory : IFactory<LevelConfigurationSO, List<Transform>> {
    public List<Transform> Create(LevelConfigurationSO levelConfig) {
        var result = new List<Transform>();

        for (int i = 0; i < levelConfig.path.pathPoints.Length; i++) {
            var point = new GameObject($"PathPoint_{i}").transform;
            point.position = levelConfig.path.pathPoints[i];

            //temp add sphere on path point
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.SetParent(point);
            sphere.transform.localPosition = Vector3.zero;
            sphere.transform.localScale = Vector3.one * 0.25f;

            result.Add(point);
        }

        return result;
    }
}
