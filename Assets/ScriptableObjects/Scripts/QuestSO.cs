using UnityEngine;

public class QuestSO : ScriptableObject
{
    public string Name;
    [Multiline]
    public string Description;
    public Sprite icon;
    public int id;
    public ObjectiveSO[] objectives;
}
