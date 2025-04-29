using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int level = 1;

    public int hp;

    public float x = 128, y = 31, z = 128;

    public float xRot = 0, yRot = 0, zRot = 0;

    public int currentQuest = 0;

    public long timeStampInTicks = System.DateTime.Now.Ticks;

    public int[] completedQuests;

    public int[] failedQuests;

    public string[] deadUniqueNPCs;

    public QuestStates[] quests;

    public InventoryItem[] inventoryContent;

    [Serializable]
    public struct QuestStates
    {
        public int id;
        public int[] states;
        public int[] amounts;
    }

    [Serializable]
    public struct InventoryItem
    {
        public Vector2Int position;
        public string itemUniqueName;
        public int amount;
    }
}
