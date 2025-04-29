using System.Collections.Generic;
using UnityEngine;

public class QuestWindow : MonoBehaviour
{

    [SerializeField] private QuestUI questPrefab;
    [SerializeField] private Sprite[] sprites = new Sprite[3];

    public static QuestWindow instance;

    private Transform content;
    private void Awake()
    {
        content = transform.GetChild(0);
    }

    private void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public Sprite GetStateSprite(int state)
    {
        return sprites[state];
    }

    public void ViewQuests(List<Quest> questsCache)
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        foreach (var data in questsCache)
        {
            QuestUI questUI = Instantiate(questPrefab, content, false);
            questUI.Build(data, sprites);
        }
        gameObject.SetActive(true);
    }
}
