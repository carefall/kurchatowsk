using UnityEditor;
using UnityEngine;

public class BarrelAmmoSO : AmmoSO
{
#if UNITY_EDITOR
    [MenuItem("Items/Create Barrel Ammo")]
    public static void CreateBarrel()
    {
        var item = CreateInstance<BarrelAmmoSO>();
        int id = AssetDatabase.FindAssets("t: BarrelAmmoSO", new string[] { "Assets/ScriptableObjects/Items/BarrelAmmo" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/BarrelAmmo/BarrelAmmo" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
    [Range(0.1f, 128f)]
    public float explosionRadius;
}
