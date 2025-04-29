using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static SaveData;

public class PlayerData : MonoBehaviour
{
    private int level;

    private int hp;

    private int currentQuest;

    internal List<Quest> questsCache = new();

    private List<int> completedQuests = new();

    private List<int> failedQuests = new();

    private List<string> deadUniqueNPCs = new();

    private float weight;

    private int money;

    private bool overweight;

    public bool viewingInventory, viewingDialogue, viewingQuestList;

    public static PlayerData instance;

    [SerializeField] private AudioClip questClip, objectiveClip, questFailClip, objectiveFailClip;
    [SerializeField] private float maxWeight;

    public void Heal()
    {
        hp = 100;
    }

    private void Update()
    {
        if (InventoryWindow.instance == null) return;
        weight = InventoryWindow.instance.GetWeight();
        if (weight >= maxWeight)
        {
            overweight = true;
        } else
        {
            overweight = false;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Compass.instance.Fill(gameObject.GetComponentInChildren<Camera>());
    }

    public int GetMoney()
    {
        return money;
    }

    public float GetMaxWeight()
    {
        return maxWeight;
    }

    public int GetLevel()
    {
        return level;
    }

    public int[] GetCompletedQuests()
    {
        return completedQuests.ToArray();
    }

    public int GetHp()
    {
        return hp;
    }

    public Dictionary<int, int[]> GetQuestsInProgress()
    {
        Dictionary<int, int[]> questsInProgress = new();
        foreach (Quest q in questsCache)
        {
            List<int> objectives = new();
            bool allowed = true;
            for (int i = 0; i < q.objectives.Length; i++)
            {
                if (q.objectives[i].state != 0) continue;
                if (!allowed) break;
                objectives.Add(i);
                allowed = q.objectives[i].allowNextParallel;
            }
            questsInProgress.Add(q.id, objectives.ToArray());
        }
        return questsInProgress;
    }

    public Objective GetCurrentObjective()
    {
        Quest q = GetCurrentQuest();
        if (q == null) return null;
        return GetQuestObjective(q);
    }

    private Objective GetQuestObjective(Quest q)
    {
        for (int i = 0; i < q.objectives.Length; i++)
        {
            if (q.objectives[i].state == 0 && !q.objectives[i].hiddenUntilDone) return q.objectives[i];
        }
        return null;
    }

    public Quest GetCurrentQuest()
    {
        foreach (Quest quest in questsCache)
        {
            if (quest.id == currentQuest) return quest;
        }
        return null;
    }

    public void Fill(int hp, int level, SaveData.QuestStates[] quests, int currentQuest, int[] completedQuests, int[] failedQuests, string[] deadUniqueNPCs, InventoryItem[] items)
    {
        this.hp = hp;
        this.level = level;
        foreach (SaveData.QuestStates stat in quests)
        {
            Quest q = QuestSystem.instance.GetQuestById(stat.id);
            questsCache.Add(q);
            for (int i = 0; i < q.objectives.Length; i++)
            {
                q.objectives[i].state = stat.states[i];
                q.objectives[i].currentAmount = stat.amounts[i];
            }
            QuestSystem.instance.ProcessQuest(q, this);
        }
        this.currentQuest = currentQuest;
        this.deadUniqueNPCs = deadUniqueNPCs.ToList();
        this.failedQuests = failedQuests.ToList();
        Entity.OnUniqueNPCDeath += AddDeadUniqueNPC;
        Entity.OnFactionMemberDeath += ProcessFactionMemberDeath;
        DialogueWindow.OnDialogueStageChange += ProcessDialogueStageChange;
        Collectable.OnItemPickup += ProcessItemPickup;
        Entity.OnEntityHeal += ProcessEntityHeal;
        InventoryWindow.instance.Fill(items);
        if (completedQuests != null && completedQuests.Length > 0) this.completedQuests = completedQuests.ToList();
    }

    public void AddQuest(Quest quest)
    {
        foreach (Quest q in questsCache)
        {
            if (q.id == quest.id) return;
        }
        questsCache.Add(quest);
        for (int i = 0; i < quest.objectives.Length; i++)
        {
            quest.objectives[i].currentAmount = 0;
        }
        QuestSystem.instance.ProcessQuest(quest, this);
        if (currentQuest == -1)
        {
            SwitchCurrentQuest();
        }
    }

    internal void CompleteObjective(Quest quest, int objectiveId)
    {
        GetComponent<AudioSource>().clip = objectiveClip;
        quest.objectives[objectiveId].state = 2;
        if (objectiveId == quest.objectives.Length - 1) CompleteQuest(quest);
        GetComponent<AudioSource>().Play();
        QuestSystem.instance.ProcessQuest(quest, this);
    }

    internal void CompleteObjective(int questId, int objectiveId)
    {
        CompleteObjective(GetQuestById(questId), objectiveId);
    }

    private void CompleteQuest(Quest quest)
    {
        questsCache.Remove(quest);
        completedQuests.Add(quest.id);
        GetComponent<AudioSource>().clip = questClip;
        if (currentQuest == quest.id) SwitchCurrentQuest();
    }

    private void SwitchCurrentQuest()
    {
        currentQuest = -1;
        if (questsCache.Count > 0)
        {
            currentQuest = questsCache[0].id;
        }
    }

    private Quest GetQuestById(int id)
    {
        foreach (Quest q in questsCache)
        {
            if (q.id == id) return q;
        }
        return null;
    }

    internal void FailObjective(Quest quest, Objective objective)
    {
        GetComponent<AudioSource>().clip = objectiveFailClip;
        objective.state = 1;
        if (objective.failQuestOnFail) FailQuest(quest);
    }

    internal void FailQuest(Quest quest)
    {
        questsCache.Remove(quest);
        failedQuests.Add(quest.id);
        GetComponent<AudioSource>().clip = questFailClip;
        if (currentQuest == quest.id) SwitchCurrentQuest();
    }

    public string[] GetDeadUniqueNPCs()
    {
        return deadUniqueNPCs.ToArray();
    }

    public int[] GetFailedQuests()
    {
        return failedQuests.ToArray();
    }

    private void ProcessItemPickup(ItemSO item)
    {
        Dictionary<Quest, int> toComplete = new();
        foreach (Quest quest in questsCache)
        {
            bool allowed = true;
            for (int i = 0; i < quest.objectives.Length; i++)
            {
                if (!allowed) break;
                Objective objective = quest.objectives[i];
                if (objective.state != 0) continue;
                if (objective.type != ObjectiveSO.ObjectiveType.COLLECT && objective.type != ObjectiveSO.ObjectiveType.COLLECT_ANY)
                {
                    allowed = objective.allowNextParallel;
                    continue;
                }
                if (objective.uniqueName == item.uniqueName)
                {
                    objective.currentAmount++;
                    if (objective.currentAmount == objective.amount)
                    {
                        toComplete.Add(quest, i);
                    }
                }
            }
        }
        foreach (var pair in toComplete)
        {
            CompleteObjective(pair.Key, pair.Value);
        }
    }

    private void ProcessEntityHeal(string entityName)
    {
        Dictionary<Quest, int> toComplete = new();
        foreach (Quest quest in questsCache)
        {
            bool allowed = true;
            for (int i = 0; i < quest.objectives.Length; i++)
            {
                if (!allowed) break;
                Objective objective = quest.objectives[i];
                if (objective.state != 0) continue;
                if (objective.type != ObjectiveSO.ObjectiveType.HEAL)
                {
                    allowed = objective.allowNextParallel;
                    continue;
                }
                if (objective.uniqueName == entityName)
                {
                    toComplete.Add(quest, i);
                }
            }
        }
        foreach (var pair in toComplete)
        {
            CompleteObjective(pair.Key, pair.Value);
        }
    }

    private void ProcessDialogueStageChange(string uniqueName, Dialogue dialogue, int stage)
    {
        Dictionary<Quest, int> toComplete = new();
        foreach (Quest quest in questsCache)
        {
            bool allowed = true;
            for (int i = 0; i < quest.objectives.Length; i++)
            {
                if (!allowed) break;
                Objective objective = quest.objectives[i];
                if (objective.state != 0) continue;
                if (objective.type != ObjectiveSO.ObjectiveType.TALK || objective.targetDialogue != dialogue)
                {
                    allowed = objective.allowNextParallel;
                    continue;
                }
                if (objective.targetDialogueStageId == stage && uniqueName == objective.uniqueName)
                {
                    toComplete.Add(quest, i);
                }
            }
        }
        foreach (var pair in toComplete)
        {
            CompleteObjective(pair.Key, pair.Value);
        }
    }

    private void ProcessFactionMemberDeath(string faction)
    {
        foreach (Quest quest in questsCache)
        {
            for (int i = 0; i < quest.objectives.Length; i++)
            {
                Objective objective = quest.objectives[i];
                if (objective.state != 0) continue;
                if (objective.type != ObjectiveSO.ObjectiveType.KILL_ANY) continue;
                if (objective.faction == null || objective.faction.Length == 0) continue;
                if (faction != objective.faction) continue;
                objective.currentAmount++;
                if (objective.currentAmount == objective.amount) CompleteObjective(quest, i);
            }
        }
    }

    private void AddDeadUniqueNPC(string uniqueName)
    {
        deadUniqueNPCs.Add(uniqueName);
        foreach (Quest quest in questsCache)
        {
            for (int i = 0; i < quest.objectives.Length; i++)
            {
                Objective objective = quest.objectives[i];
                if (objective.state != 0) continue;
                if (objective.uniqueName == null || objective.uniqueName.Length == 0) continue;
                if (objective.uniqueName == uniqueName)
                {
                    if (objective.type == ObjectiveSO.ObjectiveType.TALK) FailObjective(quest, objective);
                    else CompleteObjective(quest, i);
                }
            }
        }
    }
    public void FollowFace(Transform face)
    {
        GetComponent<PlayerController>().entityFace = face;
    }
}

