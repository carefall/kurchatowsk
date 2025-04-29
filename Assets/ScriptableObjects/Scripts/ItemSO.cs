using System;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public string uniqueName;
    public string displayName;
    [Multiline]
    public string description;
    public bool questItem;
    [Range(1, 100000)]
    public int maxStackSize = 1;
    [Range (1, 100000)]
    public int basePrice;
    public GameObject prefab;
    public Vector3 position;
    [Range(0.01f, 100f)]
    public float weight;
    public Sprite sprite;
    public int questIdOnPickup = -1;
    [Range(1, 8)]
    public int inventorySizeX = 1;
    [Range(1, 20)]
    public int inventorySizeY = 1;
}
