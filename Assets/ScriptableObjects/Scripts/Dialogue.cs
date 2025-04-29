using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Dialogue : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("Dialogues/Create Dialogue")]
    public static void CreateDialogue()
    {
        Dialogue dialogue = CreateInstance<Dialogue>();
        int id = AssetDatabase.FindAssets("t: Dialogue").Length;
        Directory.CreateDirectory(Application.dataPath + "/ScriptableObjects/Dialogues/Dialogue" + id + "/DialogueStages");
        DialogueStage stage = CreateInstance<DialogueStage>();
        AssetDatabase.CreateAsset(stage, "Assets/ScriptableObjects/Dialogues/Dialogue" + id + "/DialogueStages/DialogueStage0.asset");
        dialogue.stages = new DialogueStage[] { stage };
        AssetDatabase.CreateAsset(dialogue, "Assets/ScriptableObjects/Dialogues/Dialogue" + id + "/Dialogue" + id + ".asset");
        AssetDatabase.Refresh();
    }
#endif

    public bool autoSelect;
    public DialogueStage[] stages;
    public Requirements requirements;
    public bool escapable = true;

    public DialogueStage GetStage(int id)
    {
        foreach (var stage in stages)
        {
            if (stage.id == id) return stage;
        }
        return null;
    }

    public bool FitsRequirements(int[] completedQuests, Dictionary<int, int[]> questsInProgess)
    {
        if (requirements.completedQuests.Length > 0)
        {
            foreach (int quest in requirements.completedQuests)
            {
                if (!completedQuests.Contains(quest)) return false;
            }
        }
        if (requirements.questsNotInProgress.Length > 0)
        {
            foreach (int quest in questsInProgess.Keys)
            {
                if (requirements.questsNotInProgress.Contains(quest)) return false;
            }
        }
        if (requirements.questInProgress != -1)
        {
            if (!questsInProgess.ContainsKey(requirements.questInProgress)) return false;
            if (!questsInProgess[requirements.questInProgress].Contains(requirements.stageInProgress)) return false;
        }
        return true;
    }



    [Serializable]
    public struct Requirements
    {
        public int[] completedQuests;
        public int[] questsNotInProgress;
        public int questInProgress;
        public int stageInProgress;
    }

}
