using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueStage.Answer;
using static SaveData;

public class InventoryWindow : MonoBehaviour
{

    public InventoryCell[,] cells = new InventoryCell[8, 20];
    public List<ItemSO> medkits = new();
    [SerializeField] RectTransform itemPrefab;
    [SerializeField] Transform content;
    [SerializeField] InventoryCell cellPrefab;
    [SerializeField] TextMeshProUGUI weightText;
    [SerializeField] TextMeshProUGUI moneyText;
    private float weight = 0;

    public static InventoryWindow instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < cells.GetLength(1); i++)
        {
            for (int j = 0; j < cells.GetLength(0); j++)
            {
                cells[j, i] = Instantiate(cellPrefab, content);
                cells[j, i].name = $"Cell_{i}_{j}";
            }
        }
        gameObject.SetActive(false);
    }

    public float GetWeight()
    {
        return weight;
    }

    public void Fill(InventoryItem[] items)
    {   
        foreach (InventoryItem item in items)
        {
            ItemSO itemSO = ItemSystem.instance.GetItem(item.itemUniqueName);
            PutItem(item.position.x, item.position.y, itemSO);
        }
    }

    public void PutItem(int x, int y, ItemSO itemSO)
    {
        cells[x, y].origin = true;
        ItemUI itemUI = Instantiate(itemPrefab.gameObject, cells[x, y].transform).GetComponent<ItemUI>();
        itemUI.item = itemSO;
        itemUI.amount = 1;
        (itemUI.transform as RectTransform).localPosition = new Vector3(10, -10, 0);
        (itemUI.transform as RectTransform).sizeDelta
            = new Vector2(100 * itemSO.inventorySizeX - 20, 100 * itemSO.inventorySizeY - 20);
        for (int i = x; i < x + itemSO.inventorySizeX; i++)
        {
            for (int j = y; j < y + itemSO.inventorySizeY; j++)
            {
                cells[i, j].occupied = true;
                cells[i, j].item = itemUI;
            }
        }
    }

    public bool AddItem(ItemSO item)
    {
        ItemUI slot = GetAvailableSlotWithItem(item);
        if (slot != null)
        {
            slot.amount++;
            return true;
        }
        int x = item.inventorySizeX;
        int y = item.inventorySizeY;
        for (int j = 0; j < 20; j++)
        {
            for (int i = 0; i < 8; i++)
            {
                if (cells[i, j].occupied) continue;
                if (i > 8 - x) continue;
                if (j > 20 - y) return false;
                if (!HasSpace(i, j, x, y)) continue;
                PutItem(i, j, ItemSystem.instance.GetItem(item.uniqueName));
                return true;
            }
        }
        return false;
    }

    private bool HasSpace(int i, int j, int x, int y)
    {
        for (int a = i; a < i + x; a++)
        {
            for (int b = j; b < j + y; b++)
            {
                if (cells[a, b].occupied) return false;
            }
        }
        return true;
    }

    public ItemUI GetAvailableSlotWithItem(ItemSO item)
    {
        var stuff = cells.Cast<InventoryCell>().Where(cell => cell.origin && cell.item.item.uniqueName == item.uniqueName && cell.item.amount < cell.item.item.maxStackSize).Select(cell => cell.item);
        if (stuff.Count() > 0) return stuff.First();
        return null;
    }

    public void View(int money, float maxWeight)
    {
        gameObject.SetActive(true);
        foreach (InventoryCell cell in cells)
        {
            if (!cell.origin) continue;
            cell.item.Process();
        }
        weight = 0;
        foreach (InventoryCell item in cells.Cast<InventoryCell>().Where(cell => cell.origin))
        {
            weight += item.item.amount * item.item.item.weight;
        }
        weightText.text = $"Вес: {weight:F1} / {maxWeight:F1} кг";
        moneyText.text = $"{money} руб.";
        if (weight >= maxWeight) weightText.color = Color.red;
        else weightText.color = Color.white;
    }

    public InventoryItem[] GetContent()
    {
        List<InventoryItem> items = new();
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                InventoryCell cell = cells[i, j];
                if (!cell.origin) continue;
                items.Add(ToInvItem(cell.item, i, j));
            }
        }
        return items.ToArray();
    }

    private InventoryItem ToInvItem(ItemUI item, int x, int y)
    {
        return new()
        {
            position = new(x, y),
            amount = item.amount,
            itemUniqueName = item.item.uniqueName
        };
    }

    public void AddItems(Item[] items)
    {

    }

    public void PickUp(ItemSO itemSO)
    {

    }

    public void RemoveItems(Item[] items)
    {

    }

    public void RemoveWeakestMedkit()
    {

    }

    public void RemoveItem(ItemUI item)
    {

    }

    internal bool HasAnyMedkit()
    {
        return cells.Cast<InventoryCell>().Where(cell => cell.origin).Any(cell => (from medkit in medkits select medkit.uniqueName).Contains(cell.item.item.uniqueName));
    }

    internal bool ContainsItem(string name, int amount)
    {
        int counter = 0;
        foreach (InventoryCell icell in cells.Cast<InventoryCell>().Where(cell => cell.origin && cell.item.item.uniqueName == name))
        {
            counter += icell.item.amount;
        }
        return counter >= amount;
    }

    internal bool ContainsItems(Item[] items)
    {
        foreach (Item item in items)
        {
            if (!ContainsItem(item.requiredItemUniqueName, item.requiredItemAmount)) return false;
        }
        return true;
    }
}
