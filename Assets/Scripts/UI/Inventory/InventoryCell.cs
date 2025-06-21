using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    public bool origin, occupied;
    public ItemUI item;
    private bool click;
    public void Down()
    {
        if (!occupied) return;
        if (click == true)
        {
            if (item is WeaponSO)
            {

            }
            return;
        }
        click = true;
        StartCoroutine("Stop");
    }
    public void Up()
    {
        if (click)
        {
            
        }
        else
        {
            InventoryWindow.instance.StopDrag(this);
        }
            click = false;
        StopCoroutine("Stop");
    }
    private IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.2f);
        click = false;
        InventoryWindow.instance.StartDrag(item.origin, item.item.sprite, item.item.inventorySizeX, item.item.inventorySizeY);
    }
}
