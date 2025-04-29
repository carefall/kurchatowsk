using TMPro;
using UnityEngine;

public class CurrentQuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName, questDescription;

    public static CurrentQuestUI instance;

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void ViewQuest(Quest q)
    {
        if (q == null)
        {
            Debug.Log("null");
            questName.text = "";
            questDescription.text = "";
            return;
        }
        questName.text = q.Name;
        questDescription.text = q.Description;
        gameObject.SetActive(true);
    }
}
