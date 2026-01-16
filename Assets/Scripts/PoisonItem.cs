class PoisonItem : Item
{

    public override ItemUseResult OnUse(Player user)
    {
        user.ChangeHealth(-20);
        return ItemUseResult.Consume;
    }
}