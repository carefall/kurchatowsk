using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueStage.Answer;

public abstract class DialogueWindow : MonoBehaviour
{
    [SerializeField] internal ReplicaUI replicaPrefab;
    [SerializeField] internal OptionUI optionPrefab;
    [SerializeField] QuestSystem questSystem;
    [HideInInspector]
    public Dialogue dialogue;
    internal Entity entity;

    public static DialogueWindow instance;

    public static Action<string, Dialogue, int> OnDialogueStageChange;

    public void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void StartDialogue(Entity entity, Dialogue dialogue)
    {
        gameObject.SetActive(true);
        this.dialogue = dialogue;
        this.entity = entity;
        ProcessStartUI(dialogue);
        OnDialogueStageChange.Invoke(entity.uniqueName, dialogue, 0);
        PlayerData.instance.FollowFace(entity.face);
    }

    internal abstract void ProcessStartUI(Dialogue dialogue);

    public void ShowQuestNotification(string text, int id, int questId)
    {
        Quest quest = questSystem.GetQuestById(questId);
        PlayerData.instance.AddQuest(quest);
        ProcessNotificationUI(text, quest);
        Next(id);
    }

    public abstract void ProcessNotificationUI(string text, Quest quest);

    public void ReceiveItems(string text, int id, Item[] items)
    {
        InventoryWindow.instance.AddItems(items);
        ProcessItemAddUI(text);
        Next(id);
    }

    internal abstract void ProcessItemAddUI(string text);

    public void GiveItems(string text, int id, Item[] items)
    {
        InventoryWindow.instance.RemoveItems(items);
        ProcessItemRemoveUI(text);
        Next(id);
    }

    internal abstract void ProcessItemRemoveUI(string text);

    public void HealTarget()
    {
        InventoryWindow.instance.RemoveWeakestMedkit();
        entity.Heal();
        CloseDialogue();
    }

    public void TurnHostile()
    {
        entity.TurnHostile(PlayerData.instance);
        CloseDialogue();
    }

    public void Heal(int id)
    {
        PlayerData.instance.Heal();
        HealNotification();
        Next(id);
    }

    internal abstract void HealNotification();

    public void Next(int id)
    {
        OnDialogueStageChange.Invoke(entity.uniqueName, dialogue, id);
        DestroyOptions();
        if (id < 0 || dialogue.GetStage(id) == null)
        {
            CloseDialogue();
            return;
        }
        ShowTargetAnswer(id);
        FillAnswers(id);
    }

    internal abstract void FillAnswers(int id);

    internal abstract void ShowTargetAnswer(int id);

    public void Next(string text, int id)
    {
        ProcessNextString(text);
        Next(id);
    }

    internal abstract void DestroyOptions();

    internal abstract void ProcessNextString(string text);

    public void CloseDialogue()
    {
        PlayerData.instance.viewingDialogue = false;
        DestroyOptionsAndTexts();
        gameObject.SetActive(false);
    }

    internal abstract void DestroyOptionsAndTexts();
}
