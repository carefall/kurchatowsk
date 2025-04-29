using UnityEditor;

public class SimpleItemSO : ItemSO
{
#if UNITY_EDITOR
    [MenuItem("Items/Create Simple Item")]
    public static void Create()
    {
        var item = CreateInstance<SimpleItemSO>();
        int id = AssetDatabase.FindAssets("t: SimpleItemSO", new string[] { "Assets/ScriptableObjects/Items/SimpleItems" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/SimpleItems/SimpleItem" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
