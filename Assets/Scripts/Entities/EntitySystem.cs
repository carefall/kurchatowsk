using System;
using System.Linq;
using UnityEngine;

public class EntitySystem : MonoBehaviour
{
    public static Entity GetEntity(string faction, string uniqueName)
    {
        foreach(Entity entity in FindObjectsByType<Entity>(FindObjectsSortMode.None))
        {
            if (uniqueName == null || uniqueName.Length == 0)
            {
                if (entity.faction == faction) return entity;
            }
            if (entity.uniqueName == uniqueName) return entity;
        }
        return null;
    }

    public static void InitPopulateWorld(SaveData data)
    {
        foreach (Entity e in FindObjectsByType<Entity>(FindObjectsSortMode.None))
        {
            if (e.uniqueName != null && e.uniqueName.Length > 0)
            {
                if (data.deadUniqueNPCs.Contains(e.uniqueName))
                {
                    e.Despawn();
                }
            }
        }
    }
}
