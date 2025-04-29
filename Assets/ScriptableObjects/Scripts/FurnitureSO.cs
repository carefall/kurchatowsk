using UnityEditor;

public class FurnitureSO : ItemSO
{
#if UNITY_EDITOR
    [MenuItem("Items/Create Furniture")]
    public static void Create()
    {
        var item = CreateInstance<FurnitureSO>();
        int id = AssetDatabase.FindAssets("t: FurnitureSO", new string[] { "Assets/ScriptableObjects/Items/Furniture" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Furniture/Furniture" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
