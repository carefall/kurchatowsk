public class UniqueAnimal : Animal
{
    public override bool Interact(PlayerData data)
    {
        if (!interactable) return false;
        if (base.Interact(data)) return false;
        // code
        return true;
    }
}
