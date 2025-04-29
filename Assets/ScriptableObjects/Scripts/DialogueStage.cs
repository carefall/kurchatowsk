using NaughtyAttributes;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DialogueStage : ScriptableObject
{
#if UNITY_EDITOR
    [MenuItem("Dialogues/Create Dialogue Stage")]
    public static void CreateDialogueStage()
    {
        var obj = Selection.activeObject;
        if (obj == null || obj is not Dialogue dialogue)
        {
            EditorUtility.DisplayDialog("Undefined dialogue", "Please select a dialog scriptable object", "Ok");
            return;
        }
        string assetPath = AssetDatabase.GetAssetPath(obj);
        string path = "";
        string[] parts = assetPath.Split("/");
        for (int i = 0; i < parts.Length - 1; i++)
        {
            path += parts[i] + "/";
        }
        path += "DialogueStages/";
        DialogueStage stage = CreateInstance<DialogueStage>();
        int id = AssetDatabase.FindAssets("t: DialogueStage", new string[] { path }).Length;
        stage.id = id;
        AssetDatabase.CreateAsset(stage, path + "DialogueStage" + id + ".asset");
        var stages = dialogue.stages.ToList();
        stages.Add(stage);
        dialogue.stages = stages.ToArray();
        AssetDatabase.Refresh();
    }
#endif
    [Multiline]
    public string text;
    public int id;

    public Answer[] answers;

    [Serializable]
    public struct Answer
    {
        [Multiline]
        public string answer;

        private bool NonClosing() => action != AnswerAction.CLOSE_DIALOGUE && action != AnswerAction.HEAL_TARGET && action != AnswerAction.TURN_HOSTILE;
        [ShowIf("NonClosing")]
        [AllowNesting]
        [Multiline]
        public string dialogueAnswer;
        public AnswerAction action;
        [ShowIf("NonClosing")]
        [AllowNesting]
        public int nextStageId;
        public int givenQuestOnAnswer;
        public Item[] requiredItems;
        public Item[] receivedItems;

        [Serializable]
        public struct Item
        {
            public string requiredItemUniqueName;
            public int requiredItemAmount;
        }

        public bool FitsRequirements(PlayerData data)
        {
            if (action != AnswerAction.HEAL_TARGET && action == AnswerAction.GIVE_ITEMS) return true;
            if (action == AnswerAction.HEAL_TARGET)
            {
                return InventoryWindow.instance.HasAnyMedkit();
            } else
            {
                return InventoryWindow.instance.ContainsItems(requiredItems);
            }
        }

    }

    public enum AnswerAction
    {
        NONE, CLOSE_DIALOGUE, TURN_HOSTILE, GIVE_QUEST, HEAL_TARGET, HEAL_PLAYER, GIVE_ITEMS, RECEIVE_ITEMS
    }


}
