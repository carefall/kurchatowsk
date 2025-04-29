using System;
using UnityEditor;
using UnityEngine;

public class MedicineSO : ItemSO
{
#if UNITY_EDITOR
    [MenuItem("Items/Create Medical Item")]
    public static void Create()
    {
        var item = CreateInstance<MedicineSO>();
        int id = AssetDatabase.FindAssets("t: MedicineSO", new string[] { "Assets/ScriptableObjects/Items/Medicine" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Medicine/Medicine" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

    public MedicalEffectValue[] medicalEffectValues;

    [Serializable]
    public struct MedicalEffectValue
    {
        public MedicalEffect effect;
        [Range(-9999.9999f, 9999.9999f)]
        public float value;
    }

    public enum MedicalEffect
    {
        CHANGE_HUNGER, CHANGE_HEALTH, CHANGE_BLEEDING, CHANGE_RADIATION
    }
}
