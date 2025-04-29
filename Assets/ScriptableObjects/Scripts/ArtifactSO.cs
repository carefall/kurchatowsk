using System;
using UnityEditor;
using UnityEngine;

public class ArtifactSO : ItemSO
{

#if UNITY_EDITOR
    [MenuItem("Items/Create Artifact")]
    public static void Create()
    {
        var item = CreateInstance<ArtifactSO>();
        int id = AssetDatabase.FindAssets("t: ArtifactSO", new string[] { "Assets/ScriptableObjects/Items/Artifacts" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Artifacts/Artifact" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

    [Range(0, 9999.9999F)]
    public float radioactivity = 0.1f;
    public EffectValue[] effectValues;

    [Serializable]
    public struct EffectValue
    {
        public EffectType type;
        [Range(-9999.9999f, 9999.9999f)]
        public float value;
    }

    public enum EffectType
    {
        CHANGE_HEALTH_REGEN, CHANGE_RADIATION
    }

}
