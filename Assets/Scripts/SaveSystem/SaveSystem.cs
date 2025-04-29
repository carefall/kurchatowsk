using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static SaveData;

public class SaveSystem
{

    private static readonly string path;

    static SaveSystem()
    {
#if UNITY_EDITOR
        path = Application.dataPath + "/save.json";
#else
            path = Application.persistentDataPath + "/save.json";
#endif
    }

    public static void CreateNewSaveFile()
    {
        File.Create(path).Close();
        SaveData data = new();
        var states = new QuestStates
        {
            id = 0,
            states = new int[] { 0, -1 },
            amounts = new int[] { 0, 0 },
        };
        QuestStates[] quests = new QuestStates[1];
        quests[0] = states;
        data.quests = quests;
        data.completedQuests = new int[0];
        data.failedQuests = new int[0];
        data.deadUniqueNPCs = new string[0];
        data.currentQuest = 0;
        List<InventoryItem> items = new();
        items.Add(new SaveData.InventoryItem()
        {
            position = new Vector2Int(0, 0),
            itemUniqueName = "i_med_medkit",
            amount = 1
        });
         
        data.inventoryContent = items.ToArray();
        File.WriteAllText(path, JsonUtility.ToJson(data, true));
    }

    public static SaveData GetSaveData()
    {
        return (SaveData)JsonUtility.FromJson(File.ReadAllText(path), typeof(SaveData));
    }

    public static bool SaveDataExists()
    {
        return File.Exists(path);
    }

    public static void Save(PlayerData data)
    {
        SaveData save = new()
        {
            x = data.gameObject.transform.position.x,
            y = data.gameObject.transform.position.y,
            z = data.gameObject.transform.position.z,
            xRot = data.gameObject.transform.localEulerAngles.x,
            yRot = data.gameObject.transform.localEulerAngles.y,
            zRot = data.gameObject.transform.localEulerAngles.z,
            level = data.GetLevel(),
            hp = data.GetHp(),
            timeStampInTicks = DateTime.Now.Ticks,
            quests = QuestsDataToStates(data.questsCache),
            completedQuests = data.GetCompletedQuests(),
            failedQuests = data.GetFailedQuests(),
            deadUniqueNPCs = data.GetDeadUniqueNPCs(),
            currentQuest = data.GetCurrentQuest().id,
            inventoryContent = InventoryWindow.instance.GetContent()
        };
        File.WriteAllText(path, JsonUtility.ToJson(save, true));
    }

    private static QuestStates[] QuestsDataToStates(List<Quest> cache)
    {
        QuestStates[] questsStates = new QuestStates[cache.Count];
        for (int i = 0; i < cache.Count; i++)
        {
            int[] states = new int[cache[i].objectives.Length];
            int[] amounts = new int[cache[i].objectives.Length];
            for (int j = 0; j < cache[i].objectives.Length; j++)
            {
                states[j] = cache[i].objectives[j].state;
                amounts[j] = cache[i].objectives[j].currentAmount;
            }
            QuestStates questStates = new()
            {
                id = cache[i].id,
                states = states,
                amounts = amounts
            };
            questsStates[i] = questStates;
        }
        return questsStates;
    }

}
