class AmmoPackItem : Item
{
    public override ItemUseResult OnUse(Player user)
    {
        user.Ammo += 20;
        return ItemUseResult.Consume;
    }

}