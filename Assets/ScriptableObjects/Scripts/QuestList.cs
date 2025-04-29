using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestList", menuName = "Quests/Create List", order = 1)]
public class QuestList : ScriptableObject
{
    public List<QuestSO> quests;

    public QuestSO GetQuest(int id)
    {
        foreach (var quest in quests)
        {
            if (quest.id == id) return quest;
        }
        return null;
    }
}
