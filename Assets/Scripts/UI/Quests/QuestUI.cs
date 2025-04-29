using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private ObjectiveUI objectivePrefab;
    [SerializeField] private Transform content;

    public void Build(Quest quest, Sprite[] sprites)
    {
        icon.sprite = quest.icon;
        questName.text = quest.Name;
        for (int i = 0; i < quest.objectives.Length; i++)
        {
            Objective objective = quest.objectives[i];
            if (objective.hiddenUntilDone && objective.state <= 0) continue;
            if (objective.hiddenUntilInProgress && objective.state == -1) continue;
            ObjectiveUI objectiveUI = Instantiate(objectivePrefab, content, false);
            objectiveUI.Build(objective, sprites);
        }
    }
}
