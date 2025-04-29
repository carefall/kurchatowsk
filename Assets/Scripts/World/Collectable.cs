using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    [SerializeField] private ItemSO item;

    public static Action<ItemSO> OnItemPickup;

    public void SetItem(ItemSO item)
    {
        this.item = item;
    }

    public ItemSO GetItem()
    {
        return item;
    }

    public void Collect()
    {
        if (InventoryWindow.instance.AddItem(item))
        {
            OnItemPickup?.Invoke(item);
            Destroy(gameObject);
        } else
        {
            NotEnoughSpaceText.instance.Show();
        }
    }
}
