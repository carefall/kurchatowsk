using System.Linq;
using UnityEditor;
using UnityEngine;
using static ObjectiveSO;

public class CreateObjectiveMenu : EditorWindow
{

    private string Name;
    private bool hiddenUntilDone;
    private bool hiddenUntilInProgress;
    private bool hiddenTarget;
    private bool allowNextParallel;
    private bool failQuestOnFail;
    private bool dead;
    private ObjectiveType type;
    private float zoneSize;
    private Vector3 targetPosition;
    private string uniqueName;
    private string faction;
    private int amount = 1;
    private Dialogue targetDialogue;
    private int targetDialogueStageId;
    private string targetScene;
    private string targetZone;
    private static QuestSO quest;

    [MenuItem("Quests/Create objective")]
    public static void CreateQuest()
    {
        var obj = Selection.activeObject;
        if (obj == null || obj is not QuestSO quest)
        {
            EditorUtility.DisplayDialog("Undefined quest", "Please select a quest scriptable object", "Ok");
            return;
        }
        CreateObjectiveMenu.quest = quest;
        CreateObjectiveMenu menu = GetWindow<CreateObjectiveMenu>();
        menu.titleContent = new GUIContent("New Objective");
    }

    private void OnGUI()
    {
        Name = EditorGUILayout.TextField("Objective name", Name);
        type = (ObjectiveType) EditorGUILayout.EnumPopup("Objective type", type);
        targetScene = EditorGUILayout.TextField("Target scene", targetScene);
        targetZone = EditorGUILayout.TextField("Target zone", targetZone);
        if (type == ObjectiveType.REACH_ZONE || type == ObjectiveType.REACH_ENTITY)
        {
            if (type != ObjectiveType.REACH_ENTITY) targetPosition = EditorGUILayout.Vector3Field("Target position", targetPosition);
            else dead = EditorGUILayout.Toggle("Dead entity?", dead);
            zoneSize = EditorGUILayout.FloatField("Zone size", zoneSize);
        }
        if (type == ObjectiveType.KILL || type == ObjectiveType.KILL_ANY)
        {
                faction = EditorGUILayout.TextField("Faction", faction);
        }
        if (type == ObjectiveType.KILL_ANY || type == ObjectiveType.COLLECT_ANY)
        {
            amount = EditorGUILayout.IntField("Amount", amount);
        }
        if (type != ObjectiveType.REACH_ZONE)
        {
            uniqueName = EditorGUILayout.TextField("Unique name", uniqueName);
        }
        if (type == ObjectiveType.TALK)
        {
            targetDialogue = (Dialogue) EditorGUILayout.ObjectField("Target dialogue", targetDialogue, typeof(Dialogue),false);
            targetDialogueStageId = EditorGUILayout.IntField("Target DialogueStage ID", targetDialogueStageId);
        }
        hiddenUntilDone = EditorGUILayout.Toggle("Hidden until done", hiddenUntilDone);
        hiddenUntilInProgress = EditorGUILayout.Toggle("Hidden until in progress", hiddenUntilInProgress);
        hiddenTarget = EditorGUILayout.Toggle("Hidden target", hiddenTarget);
        allowNextParallel = EditorGUILayout.Toggle("Allow next objective in parallel", allowNextParallel);
        failQuestOnFail = EditorGUILayout.Toggle("Fail quest on objective fail", failQuestOnFail);
        if (GUILayout.Button("Create Objective"))
        {
            ObjectiveSO obj = CreateInstance<ObjectiveSO>();
            obj.Name = Name;
            obj.hiddenUntilDone = hiddenUntilDone;
            obj.hiddenTarget = hiddenTarget;
            obj.allowNextParallel = allowNextParallel;
            obj.targetPosition = targetPosition;
            obj.amount = amount;
            obj.faction = faction;
            obj.uniqueName = uniqueName;
            obj.zoneSize = zoneSize;
            obj.failQuestOnFail = failQuestOnFail;
            obj.targetDialogue = targetDialogue;
            obj.targetDialogueStageId = targetDialogueStageId;
            obj.targetScene = targetScene;
            obj.dead = dead;
            obj.type = type;
            obj.hiddenUntilInProgress = hiddenUntilInProgress;
            obj.targetZone = targetZone;
            int id = AssetDatabase.FindAssets("t: ObjectiveSO", new string[] { "Assets/ScriptableObjects/Quests/Quest" + quest.id + "/Objectives" }).Length;
            AssetDatabase.CreateAsset(obj, "Assets/ScriptableObjects/Quests/Quest" + quest.id + "/Objectives/Objective" + id + ".asset");
            var objectives = quest.objectives.ToList();
            objectives.Add(obj);
            quest.objectives = objectives.ToArray();
            AssetDatabase.Refresh();
            Close();
        }
    }
}