using UnityEngine;

public class ItemSystem : MonoBehaviour
{
    public static ItemSystem instance;

    [SerializeField] private ItemList items;

    void Awake()
    {
        instance = this;
    }


    public ItemSO GetItem(string uniqueName)
    {
        return items.GetItem(uniqueName);
    }

    public Collectable SpawnItem(string uniqueName, Vector3 position)
    {
        ItemSO item = items.GetItem(uniqueName);
        if (item == null) return null;
        GameObject go = Instantiate(item.prefab, transform);
        go.name = uniqueName;
        go.transform.localPosition = position;
        Collectable collectable = go.AddComponent<Collectable>();
        collectable.SetItem(item);
        return collectable;
    }

    public Collectable SpawnItem(string uniqueName)
    {
        ItemSO item = items.GetItem(uniqueName);
        if (item == null) return null;
        GameObject go = Instantiate(item.prefab, transform);
        go.name = uniqueName;
        go.transform.localPosition = item.position;
        Collectable collectable = go.AddComponent<Collectable>();
        collectable.SetItem(item);
        return collectable;
    }
}
