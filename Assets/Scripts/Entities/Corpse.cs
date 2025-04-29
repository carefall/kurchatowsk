public class Corpse : Entity
{
    public override void Heal()
    {
        // do nothing
    }

    public override bool Interact(PlayerData data)
    {
        return false;
    }

    public override void TurnHostile(PlayerData data)
    {
        // do nothing
    }
}
