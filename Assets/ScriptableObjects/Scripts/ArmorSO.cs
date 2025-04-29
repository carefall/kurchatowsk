using NaughtyAttributes;
using System;
using UnityEditor;
using UnityEngine;

public class ArmorSO : ItemSO
{

#if UNITY_EDITOR
    [MenuItem("Items/Create Armor")]
    public static void Create()
    {
        var item = CreateInstance<ArmorSO>();
        int id = AssetDatabase.FindAssets("t: ArmorSO", new string[] { "Assets/ScriptableObjects/Items/Armor" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Armor/Armor" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

    [Tooltip("Has built-in helmet")]
    public ArmorType type;
    [HideIf("type", ArmorType.HELMET)]
    public ArmorEffectValue[] armorEffects;
    [HideIf("type", ArmorType.ARMOR)]
    public HelmetEffectValue[] helmetEffects;


    [Serializable]
    public struct ArmorEffectValue
    {
        public ArmorEffect effect;
        [Range(0.01f, 9999.9999f)]
        public float value;
    }

    [Serializable]
    public struct HelmetEffectValue
    {
        public HelmetEffect effect;
        [Range(0.01f, 9999.9999f)]
        public float value;
    }

    public enum ArmorEffect
    {
        DAMAGE_REDUCTION, BREAK_RESISTANCE, MAX_WEIGHT_CHANGE
    }

    public enum HelmetEffect
    {
        NIGHT_VISION, PLAYER_INDURANCE_CHANGE
    }

    public enum ArmorType
    {
        ARMOR, HELMET, COMBINED
    }
}
