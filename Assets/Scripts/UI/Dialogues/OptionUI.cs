using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogueStage;
using static DialogueStage.Answer;

public class OptionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button b;

    public int id;
    private Answer answer;

    public void Fill(int id, Answer answer)
    {
        this.id = id;
        text.text =$"{id+1}. {answer.answer}";
        this.answer = answer;
        b.enabled = true;
        text.raycastTarget = true;
        PlayerButtons.OnNumberPressed += ProcessKeyboard;
    }
    public void Clear()
    {
        b.enabled=false;
        text.raycastTarget=false;
        text.text = "";
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
