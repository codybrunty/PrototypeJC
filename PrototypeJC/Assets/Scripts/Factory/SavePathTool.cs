using UnityEditor;
using UnityEngine;

public class SavePathTool {
    [MenuItem("Test/Save Path Data From Selected")]
    public static void SavePath() {
        var selected = Selection.transforms;
        var asset = ScriptableObject.CreateInstance<PathData>();
        asset.pathPoints = new Vector3[selected.Length];

        for (int i = 0; i < selected.Length; i++) {
            asset.pathPoints[i] = selected[i].position;
        }

        string path = EditorUtility.SaveFilePanelInProject("Save PathData", "NewPath", "asset", "Save path points as asset");
        if (!string.IsNullOrEmpty(path)) {
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            Debug.Log($"PathData created with {selected.Length} points.");
        }
    }
}
