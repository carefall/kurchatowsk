using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Objective", menuName = "Quests/Create objective", order = 1)]
public class ObjectiveSO : ScriptableObject
{
    public string Name;
    public ObjectiveType type;
    [ShowIf("type", ObjectiveType.REACH_ENTITY)]
    public bool dead;
    public bool hiddenUntilDone;
    public bool hiddenUntilInProgress;
    public bool hiddenTarget;
    public bool allowNextParallel;
    public bool failQuestOnFail;
    public string targetZone;
    public string targetScene;
    [ShowIf("type", ObjectiveType.REACH_ZONE)]
    public float zoneSize;
    [ShowIf("type", ObjectiveType.REACH_ZONE)]
    public Vector3 targetPosition;
    private bool ShowName() => type != ObjectiveType.REACH_ZONE && type != ObjectiveType.COLLECT && type != ObjectiveType.COLLECT_ANY;
    [Tooltip("Unique name of collectible, entity or interactable")]
    [ShowIf("ShowName")]
    public string uniqueName;
    private bool ShowItem() => type == ObjectiveType.COLLECT || type == ObjectiveType.COLLECT_ANY;
    [ShowIf("ShowItem")]
    public ItemSO targetItem;
    [ShowIf("type", ObjectiveType.TALK)]
    public Dialogue targetDialogue;
    [ShowIf("type", ObjectiveType.TALK)]
    public int targetDialogueStageId;
    [ShowIf("type", ObjectiveType.KILL_ANY)]
    public string faction;
    public bool ShowAmount() { return type == ObjectiveType.KILL_ANY || type == ObjectiveType.COLLECT_ANY; }
    [ShowIf("ShowAmount")]
    public int amount = 1;


    public enum ObjectiveType
    {
        REACH_ZONE, KILL, KILL_ANY, COLLECT, COLLECT_ANY, TALK, INTERACT, REACH_ENTITY, HEAL
    }
}