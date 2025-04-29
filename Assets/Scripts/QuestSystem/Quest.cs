using UnityEngine;

public class Quest
{
    public string Name;
    public string Description;
    public Sprite icon;
    public int id;
    public Objective[] objectives;

    public Quest(QuestSO quest)
    {
        Name = quest.Name;
        Description = quest.Description;
        icon = quest.icon;
        id = quest.id;
        objectives = new Objective[quest.objectives.Length];
        for (int i = 0; i < objectives.Length; i++)
        {
            objectives[i] = new Objective(quest.objectives[i]);
        }
    }
}
