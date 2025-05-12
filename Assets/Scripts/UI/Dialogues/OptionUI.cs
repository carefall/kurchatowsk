using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueStage;

public class OptionUI : MonoBehaviour
{
    private TextMeshProUGUI textAndButton;

    public int id;
    private Answer answer;

    public void Fill(int id, Answer answer)
    {
        textAndButton = GetComponentInChildren<TextMeshProUGUI>();
        this.id = id;
        textAndButton.text =$"{id+1}. {answer.answer}";
        this.answer = answer;
        textAndButton.GetComponent<Button>().enabled = true;
        textAndButton.raycastTarget = true;
        PlayerButtons.OnNumberPressed += ProcessKeyboard;
    }
    public void Clear()
    {
        textAndButton.GetComponent<Button>().enabled = false;
        textAndButton.raycastTarget=false;
        textAndButton.text = "";
    }

    private void ProcessKeyboard(int num)
    {
        if (id+1 == num) Process();
    }

    private void OnDestroy()
    {
        PlayerButtons.OnNumberPressed -= ProcessKeyboard;
    }

    public void Process()
    {
        if (answer.action == AnswerAction.CLOSE_DIALOGUE)
        {
            DialogueWindow.instance.CloseDialogue();
            return;
        }
        else if (answer.action == AnswerAction.GIVE_QUEST)
        {
            DialogueWindow.instance.ShowQuestNotification(answer.dialogueAnswer, answer.nextStageId, answer.givenQuestOnAnswer);
        }
        else if (answer.action == AnswerAction.TURN_HOSTILE)
        {
            DialogueWindow.instance.TurnHostile();
        }
        else if (answer.action == AnswerAction.HEAL_TARGET)
        {
            DialogueWindow.instance.HealTarget();
        }
        else if (answer.action == AnswerAction.GIVE_ITEMS)
        {
            DialogueWindow.instance.GiveItems(answer.dialogueAnswer, answer.nextStageId, answer.requiredItems);
        }
        else if (answer.action == AnswerAction.RECEIVE_ITEMS)
        {
            DialogueWindow.instance.ReceiveItems(answer.dialogueAnswer, answer.nextStageId, answer.receivedItems);
        }
        else if (answer.action == AnswerAction.HEAL_PLAYER)
        {
            DialogueWindow.instance.Heal(answer.nextStageId);
        }
        else
        {
            DialogueWindow.instance.Next(answer.dialogueAnswer, answer.nextStageId);
        }
    }


}
