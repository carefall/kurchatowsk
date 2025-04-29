using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateQuestMenu : EditorWindow
{

    private string Name;
    private string Description;
    private Sprite icon;

    [MenuItem("Quests/Create quest")]
    public static void CreateQuest()
    {
        CreateQuestMenu menu = GetWindow<CreateQuestMenu>();
        menu.titleContent = new GUIContent("New Quest");
    }

    private void OnGUI()
    {
        Name = EditorGUILayout.TextField("Quest Name", Name);
        Description = EditorGUILayout.TextField("Quest Description", Description);
        icon = EditorGUILayout.ObjectField("Quest Icon", icon, typeof(Sprite), false) as Sprite;
        if (GUILayout.Button("Create Quest"))
        {
            QuestSO quest = CreateInstance<QuestSO>();
            quest.Name = Name;
            quest.Description = Description;
            quest.icon = icon;
            quest.objectives = new ObjectiveSO[0];
            int id = AssetDatabase.FindAssets("t: QuestSO").Length;
            quest.id = id;
            Directory.CreateDirectory(Application.dataPath + "/ScriptableObjects/Quests/Quest" + id + "/Objectives");
            AssetDatabase.CreateAsset(quest, "Assets/ScriptableObjects/Quests/Quest" + id + "/Quest" + id + ".asset");
            AssetDatabase.Refresh();
            QuestList questList = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("t: QuestList")[0]), typeof(QuestList)) as QuestList;
            questList.quests.Add(quest);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Close();
        }
    }
}