using UnityEditor;
using UnityEngine;

public class AmmoSO : ItemSO
{
    [Range(1, 10000)]
    public int damage;
#if UNITY_EDITOR
    [MenuItem("Items/Create Ammo")]
    public static void Create()
    {
        var item = CreateInstance<AmmoSO>();
        int id = AssetDatabase.FindAssets("t: AmmoSO", new string[] { "Assets/ScriptableObjects/Items/Ammo" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Ammo/Ammo" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
