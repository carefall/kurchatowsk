using static DialogueStage;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueWindowStalkerOptions : DialogueWindow
{
    [SerializeField] Transform content, options;
    [SerializeField] Image target, player;
    [SerializeField] TextMeshProUGUI targetName, playerName;
    [SerializeField] Sprite system;
    [Multiline]
    [SerializeField] string questNotification, healNotification;
    private ScrollRect scrollRect;

    public new void Start()
    {
        base.Start();
        scrollRect = GetComponent<ScrollRect>();
    }

    internal override void DestroyOptionsAndTexts()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        for (int i = options.childCount - 1; i >= 0; i--)
        {
            Destroy(options.GetChild(i).gameObject);
        }
    }

    internal override void ProcessStartUI(Dialogue dialogue)
    {
        targetName.text = entity.displayName;
        target.sprite = entity.sprite;
        if (dialogue.GetStage(0).text == null || dialogue.GetStage(0).text.Length == 0)
        {
            for (int i = 0; i < dialogue.GetStage(0).answers.Length; i++)
            {
                Answer answer = dialogue.GetStage(0).answers[i];
                if (answer.FitsRequirements(PlayerData.instance))
                {
                    OptionUI option = Instantiate(optionPrefab, options);
                    option.Fill(i + 1, answer);
                }
            }
            return;
        }
        ReplicaUI replica = Instantiate(replicaPrefab, content);
        replica.Fill(entity.sprite, entity.displayName, dialogue.GetStage(0).text);
        for (int i = 0; i < dialogue.GetStage(0).answers.Length; i++)
        {
            Answer answer = dialogue.GetStage(0).answers[i];
            if (answer.FitsRequirements(PlayerData.instance))
            {
                OptionUI option = Instantiate(optionPrefab, options);
                option.Fill(i + 1, answer);
            }
        }
    }

    public override void ProcessNotificationUI(string text, Quest quest)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", string.Format(questNotification, quest.Name, quest.Description));
    }

    internal override void ProcessItemAddUI(string text)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", ""/*message format*/);
    }

    internal override void ProcessItemRemoveUI(string text)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", ""/*message format*/);
    }

    internal override void HealNotification()
    {
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", healNotification);
    }

    internal override void ProcessNextString(string text)
    {
        if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
    }

    internal override void DestroyOptions()
    {
        for (int i = options.childCount - 1; i >= 0; i--)
        {
            Destroy(options.GetChild(i).gameObject);
        }
    }

    internal override void ShowTargetAnswer(int id)
    {
        if (dialogue.GetStage(id).text != null && dialogue.GetStage(id).text.Length != 0)
        {
            ReplicaUI replica = Instantiate(replicaPrefab, content);
            replica.Fill(target.sprite, targetName.text, dialogue.GetStage(id).text);
        }
    }

    internal override void FillAnswers(int id)
    {
        for (int i = 0; i < dialogue.GetStage(id).answers.Length; i++)
        {
            Answer answer = dialogue.GetStage(id).answers[i];
            OptionUI option = Instantiate(optionPrefab, options);
            option.Fill(i + 1, answer);
        }
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
