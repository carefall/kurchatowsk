public class Human : Entity
{

    public Dialogue[] dialogues;

    public Dialogue healDialogue, genericDialogue;

    public bool disabled = false;

    public bool hostile = false;

    public bool busy = false;

    public string[] hostileToFactions;

    public string[] nonHostileToNPCs;

    public override void Heal()
    {
        disabled = false;
        OnEntityHeal?.Invoke(uniqueName);
    }

    public override bool Interact(PlayerData data)
    {
        if (disabled)
        {
            data.viewingDialogue = true;
            DialogueWindow.instance.StartDialogue(this, healDialogue);
            return true;
        }
        if (!interactable) return false;
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (dialogues[i].autoSelect)
            {
                if (dialogues[i].FitsRequirements(data.GetCompletedQuests(), data.GetQuestsInProgress()))
                {
                    data.viewingDialogue = true;
                    DialogueWindow.instance.StartDialogue(this, dialogues[i]);
                    return true;
                }
            }
        }
        for (int i = 0; i < dialogues.Length; i++)
        {
            if (!dialogues[i].autoSelect)
            {
                if (dialogues[i].FitsRequirements(data.GetCompletedQuests(), data.GetQuestsInProgress()))
                {
                    data.viewingDialogue = true;
                    DialogueWindow.instance.StartDialogue(this, dialogues[i]);
                    return true;
                }
            }
        }
        if (genericDialogue != null)
        {
            data.viewingDialogue = true;
            DialogueWindow.instance.StartDialogue(this, genericDialogue);
            return true;
        }
        return false;
    }

    public override void TurnHostile(PlayerData data)
    {
        // turnHostile logic
    }
}
