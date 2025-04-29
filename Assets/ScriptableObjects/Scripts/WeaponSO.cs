using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class WeaponSO : ItemSO
{

#if UNITY_EDITOR
    [MenuItem("Items/Create Weapon")]
    public static void Create()
    {
        var item = CreateInstance<WeaponSO>();
        int id = AssetDatabase.FindAssets("t: WeaponSO", new string[] { "Assets/ScriptableObjects/Items/Weapons" }).Length;
        AssetDatabase.CreateAsset(item, "Assets/ScriptableObjects/Items/Weapons/Weapon" + id + ".asset");
        ItemList itemList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: ItemList")[0]), typeof(ItemList)) as ItemList;
        itemList.items.Add(item);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
    [Range(-1, 128)]
    public int scopeAttachId = -1;
    [Range(-1, 128)]
    public int silencerAttachId = -1;
    [Range(-1, 128)]
    public int barrelAttachId = -1;
    [Range(1, 1000000)]
    public int endurance = 10000;
    public bool unbreakable;
    [ShowIf("WithScope")]
    public Sprite withScope;
    [ShowIf("WithSilencer")]
    public Sprite withSilencer;
    [ShowIf("WithBarrel")]
    public Sprite withBarrel;
    [ShowIf(EConditionOperator.And, "WithScope", "WithBarrel")]
    public Sprite withScopeAndBarrel;
    [ShowIf(EConditionOperator.And, "WithScope", "WithSilencer")]
    public Sprite withScopeAndSilencer;
    [ShowIf(EConditionOperator.And, "WithSilencer", "WithBarrel")]
    public Sprite withSilencerAndBarrel;
    [ShowIf(EConditionOperator.And, "WithScope", "WithBarrel", "WithSilencer")]
    public Sprite withAll;

    public AmmoSO ammoType;
    public BarrelAmmoSO barrelAmmoType;

    public enum SlotType
    {
        MAIN, SECONDARY, GRENADE
    }

    private bool WithScope() => scopeAttachId >= 0;
    private bool WithBarrel() => barrelAttachId >= 0;

    private bool WithSilencer() => silencerAttachId >= 0;
}
