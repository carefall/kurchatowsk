using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemList", menuName = "Items/Create List", order = 1)]
public class ItemList : ScriptableObject
{
    public List<ItemSO> items;

    public ItemSO GetItem(string uniqueName)
    {
        foreach (var item in items)
        {
            if (item.uniqueName == uniqueName) return item;
        }
        return null;
    }
}
