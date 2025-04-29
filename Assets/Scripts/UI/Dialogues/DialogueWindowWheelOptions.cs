using static DialogueStage;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueWindowWheelOptions : DialogueWindow
{
    [SerializeField] Transform options;
    [SerializeField] Image target, player;
    [SerializeField] TextMeshProUGUI targetName, playerName;
    [Multiline]
    [SerializeField] string questNotification, healNotification;
    [SerializeField] Animator wheel;

    public new void Start()
    {
        base.Start();
    }

    internal override void DestroyOptionsAndTexts()
    {
        for (int i = options.childCount - 1; i >= 0; i--)
        {
            for (int j = 0; j < options.GetChild(i).childCount; j++)
            {
                OptionUI opt = options.GetChild(i).GetChild(j).GetComponent<OptionUI>();
                opt.Clear();
            }
        }
    }

    internal override void ProcessStartUI(Dialogue dialogue)
    {
        //
        wheel.Play("start");
        FillAnswers(0);
    }

    public override void ProcessNotificationUI(string text, Quest quest)
    {
        /*if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", string.Format(questNotification, quest.Name, quest.Description));*/
    }

    internal override void ProcessItemAddUI(string text)
    {
        /*if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", "");*/
    }

    internal override void ProcessItemRemoveUI(string text)
    {
        /*if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }
        ReplicaUI notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", ""message format);*/
    }

    internal override void HealNotification()
    {
        /* notification = Instantiate(replicaPrefab, content);
        notification.Fill(system, "Система", healNotification);*/
    }

    internal override void ProcessNextString(string text)
    {
        /*if (text != null && text.Length > 0)
        {
            ReplicaUI playerReplica = Instantiate(replicaPrefab, content);
            playerReplica.Fill(player.sprite, playerName.text, text);
        }*/
    }

    internal override void DestroyOptions()
    {
        for (int i = options.childCount - 1; i >= 0; i--)
        {
            for(int j = 0; j < options.GetChild(i).childCount; j++)
            {
                OptionUI opt=options.GetChild(i).GetChild(j).GetComponent<OptionUI>();
                opt.Clear();
            }
        }
    }

    internal override void ShowTargetAnswer(int id)
    {
        /*if (dialogue.GetStage(id).text != null && dialogue.GetStage(id).text.Length != 0)
        {
            ReplicaUI replica = Instantiate(replicaPrefab, content);
            replica.Fill(target.sprite, targetName.text, dialogue.GetStage(id).text);
        }*/
    }

    internal override void FillAnswers(int id)
    {

        int n = dialogue.GetStage(0).answers.Length;
        if (n == 1)
        {
            Answer answer = dialogue.GetStage(id).answers[0];
            Transform o = options.GetChild(0).transform;
            OptionUI opt = o.GetChild(0).GetComponent<OptionUI>();
            opt.Fill(0, answer);
            // OptionUI option = Instantiate(optionPrefab, options);

        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                Answer answer = dialogue.GetStage(id).answers[i];
                Transform o = options.GetChild(i).transform;
                OptionUI opt = o.GetChild(i).GetComponent<OptionUI>();
                opt.Fill(i, answer);
            }
        }

    }
}
