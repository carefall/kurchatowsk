using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    public ItemSO item;
    public int amount;
    public InventoryCell origin;

    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image sprite;

    public void Process()
    {
        sprite.sprite = item.sprite;
        amountText.text = $"x{amount}";
    }
    

}
