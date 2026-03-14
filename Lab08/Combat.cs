namespace Lab08;

public static class Combat   // Class for methods that handle combat interactions
{
    public static void CombatLoop(Player player, Monster monster, ref Map map)
    {
        // Print a display for combat
        if (monster.hasSpecAtk)
            if (Monster.rng.Next(1,101) > 80)
            {
                (int currentX, int currentY) = (player.X, player.Y);
                monster.SpecialAttack(player);
                if ((player.X, player.Y) != (currentX, currentY))
                    return;
            }

        while (player.Health > 0 && monster.Health > 0)
            Attack(player, monster);

        if (player.IsDead)
            return;
        else
        {
            Loot(player, monster);
            RemoveMonsterAt(monster.X, monster.Y, ref map);
        }

        return;
    }
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
    public static void RemoveMonsterAt(int x, int y, ref Map map)
    {
        map.UpdateRoomDataAt(x, y, true);
        map.MonsterList.RemoveAll(mon => mon.X == x && mon.Y == y);
    }
}