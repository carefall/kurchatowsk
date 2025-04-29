public class Animal : Entity
{
    public override void Heal()
    {
        // do nothing
    }

    public override bool Interact(PlayerData data)
    {
        if (!interactable) return false;
        // code
        return true;
    }

    public override void TurnHostile(PlayerData data)
    {
        // do nothing
    }
}
