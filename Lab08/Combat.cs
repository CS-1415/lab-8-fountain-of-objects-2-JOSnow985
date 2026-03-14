namespace Lab08;

public static class Combat   // Class for methods that handle combat interactions
{
    public static void CombatLoop(Player player, Monster monster){}
    public static void Attack(Player player, Monster monster)
    {   // At least 1 damage is dealt
        monster.Health -= Math.Max(1, player.GearStats.Attack - monster.MonStats.Defense);
        if (monster.Health > 0)
            player.Health -= Math.Max(1, monster.MonStats.Attack - player.GearStats.Attack);
    }
    public static void Loot(Player player, Monster monster)
    {
        foreach(Item item in monster.Inventory)
            player.Inventory.Add(item);

        monster.Inventory = [];
    }
    public static void RemoveMonsterAt(int x, int y){}
}