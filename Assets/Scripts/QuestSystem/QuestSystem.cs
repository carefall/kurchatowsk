using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestSystem : MonoBehaviour
{

    [SerializeField] private QuestList questList;


    public static QuestSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public Quest GetQuestById(int id)
    {
        return new Quest(questList.GetQuest(id));
    }

    public void ProcessQuest(Quest q, PlayerData playerData)
    {
        bool allowed = true;
        for (int i = 0; i < q.objectives.Length; i++)
        {
            if (!allowed) continue;
            Objective obj = q.objectives[i];
            if (obj.state > 0) continue;
            if (obj.state == -1) obj.state = 0;
            ProcessObjective(q, i, obj, playerData);
            allowed = obj.allowNextParallel;
        }
    }

    private void ProcessObjective(Quest quest, int objId, Objective obj, PlayerData playerData)
    {
        if (obj.targetScene != null && obj.targetScene.Length > 0 && obj.targetScene != SceneManager.GetActiveScene().name)
        {
            return;
        }
        if (obj.type == ObjectiveSO.ObjectiveType.REACH_ZONE)
        {
            GameObject qz = new("Quest Zone " + quest.id + "_" + objId);
            qz.transform.position = obj.targetPosition;
            qz.isStatic = true;
            QuestZone zone = qz.AddComponent<QuestZone>();
            zone.SetIds(quest.id, objId);
            SphereCollider col = qz.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = obj.zoneSize;
        }
        if (obj.type == ObjectiveSO.ObjectiveType.KILL)
        {
            if (playerData.GetDeadUniqueNPCs().Contains(obj.uniqueName))
            {
                playerData.CompleteObjective(quest.id, objId);
                return;
            }
            obj.targetEntity = EntitySystem.GetEntity(obj.faction, obj.uniqueName);
        }
        if (obj.type == ObjectiveSO.ObjectiveType.COLLECT || obj.type == ObjectiveSO.ObjectiveType.COLLECT_ANY)
        {
            if (InventoryWindow.instance.ContainsItem(obj.uniqueName, obj.amount))
            {
                playerData.CompleteObjective(quest, objId);
            }
            if (obj.type == ObjectiveSO.ObjectiveType.COLLECT) obj.targetCollectable = ItemSystem.instance.SpawnItem(obj.uniqueName, obj.targetPosition);
        }
        if (obj.type == ObjectiveSO.ObjectiveType.TALK)
        {
            if (playerData.GetDeadUniqueNPCs().Contains(obj.uniqueName))
            {
                playerData.FailObjective(quest, obj);
                return;
            }
            foreach (Entity entity in FindObjectsByType<Entity>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (entity.uniqueName == obj.uniqueName)
                {
                    obj.targetEntity = entity;
                    return;
                }
            }
        }
        if (obj.type == ObjectiveSO.ObjectiveType.INTERACT)
        {
            foreach (Interactable interactable in FindObjectsByType<Interactable>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (interactable.uniqueName == obj.uniqueName)
                {
                    obj.targetInteractable = interactable;
                    return;
                }
            }
        }
        if (obj.type == ObjectiveSO.ObjectiveType.REACH_ENTITY)
        {
            if (obj.dead)
            {
                foreach (Entity entity in FindObjectsByType<Entity>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (entity.uniqueName != obj.uniqueName) continue;
                    if (entity is not Corpse) return;
                    GameObject qz = new("Entity Zone " + quest.id + "_" + objId);
                    qz.transform.position = entity.transform.position;
                    qz.isStatic = true;
                    QuestZone zone = qz.AddComponent<QuestZone>();
                    zone.SetIds(quest.id, objId);
                    SphereCollider col = qz.AddComponent<SphereCollider>();
                    col.isTrigger = true;
                    col.radius = obj.zoneSize;
                    return;
                }
            }
            else
            {
                foreach (Entity entity in FindObjectsByType<Entity>(FindObjectsInactive.Include, FindObjectsSortMode.None))
                {
                    if (entity.uniqueName != obj.uniqueName) continue;
                    if (entity is Corpse)
                    {
                        playerData.FailObjective(quest, obj);
                        return;
                    }
                    SphereCollider col = entity.gameObject.AddComponent<SphereCollider>();
                    col.isTrigger = true;
                    col.radius = obj.zoneSize;
                    EntityZone ez = entity.gameObject.AddComponent<EntityZone>();
                    ez.SetIds(quest.id, objId);
                    return;
                }
            }
            foreach (Entity entity in FindObjectsByType<Entity>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (entity.uniqueName == obj.uniqueName)
                {
                    obj.targetEntity = entity;
                    return;
                }
            }
        }
    }
}
