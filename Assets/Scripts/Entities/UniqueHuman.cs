public class UniqueHuman : Human
{
    public override bool Interact(PlayerData data)
    {
        if (!interactable) return false;
        if (base.Interact(data)) return false;
        // stuff
        return true;
    }
}
