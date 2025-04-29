using UnityEngine;
using UnityEngine.SceneManagement;
using static ObjectiveSO;

public class Objective
{
    public string Name;
    public ObjectiveType type;
    public bool hiddenUntilDone;
    public bool hiddenUntilInProgress;
    public bool hiddenTarget;
    public bool allowNextParallel;
    public bool failQuestOnFail;
    public float zoneSize;
    public Vector3 targetPosition;
    public Dialogue targetDialogue;
    public int targetDialogueStageId;
    public Entity targetEntity;
    public Collectable targetCollectable;
    public Interactable targetInteractable;
    public string uniqueName;
    public string faction;
    public int amount;
    public int currentAmount;
    public int state = -1;
    public string targetScene;
    public string targetZone;
    public bool dead;


    public Objective(ObjectiveSO objective)
    {
        Name = objective.Name;
        type = objective.type;
        hiddenUntilDone = objective.hiddenUntilDone;
        hiddenUntilInProgress = objective.hiddenUntilInProgress;
        hiddenTarget = objective.hiddenTarget;
        allowNextParallel = objective.allowNextParallel;
        failQuestOnFail = objective.failQuestOnFail;
        zoneSize = objective.zoneSize;
        targetPosition = objective.targetPosition;
        targetDialogue = objective.targetDialogue;
        targetDialogueStageId = objective.targetDialogueStageId;
        targetScene = objective.targetScene;
        targetZone = objective.targetZone;
        uniqueName = objective.uniqueName;
        faction = objective.faction;
        amount = objective.amount;
        state = -1;
        dead = objective.dead;
    }

    public Vector3 GetCoordinates()
    {
        if (hiddenTarget) return new Vector3(-9999, -9999, -9999);
        if (targetScene != null && targetScene.Length > 0 && targetScene != SceneManager.GetActiveScene().name)
        {
            return PortalSystem.instance.GetPortal(targetScene).position;
        }
        if (type == ObjectiveType.INTERACT)
        {
            return targetInteractable.transform.position;
        }
        else if (type == ObjectiveType.COLLECT)
        {
            return targetCollectable.transform.position;
        }
        else if (type == ObjectiveType.REACH_ZONE)
        {
            return targetPosition;
        }
        else if (type == ObjectiveType.KILL || type == ObjectiveType.TALK)
        {
            return targetEntity.transform.position;
        }
        return new Vector3(-9999, -9999, -9999);
    }
}
