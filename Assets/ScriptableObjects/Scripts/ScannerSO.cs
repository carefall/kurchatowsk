using UnityEditor;
using UnityEngine;

public class ScannerSO : ItemSO
{
#if UNITY_EDITOR
    [MenuItem("Items/Create Scanner")]
    public static void Create()
    {
        var item = CreateInstance<ScannerSO>();
        int id = AssetDatabase.FindAssets("t: ScannerSO", new string[] { "Assets/ScriptableObjects/Items/Scanners" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Scanners/Scanner" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

    [Range(0, 10)]
    public int anomalyScanLevel = 0;
    [Range(0, 10)]
    public int artifactScanLevel = 1;

}
