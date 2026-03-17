namespace Lab08;

public static class Combat   // Class for methods that handle combat interactions
{
    public static void CombatLoop(Player player, Monster monster, ref Map map)
    {
        if (monster.hasSpecAtk)
            if (Monster.rng.Next(1,101) > 80)
            {
                (int currentX, int currentY) = (player.X, player.Y);
                monster.SpecialAttack(player);
                if ((player.X, player.Y) != (currentX, currentY))
                    return;
            }

        Console.Clear();
        Printer.ColorPrint("--- Combat Encounter ---");
        Printer.ColorPrint($"--- Player vs {monster.Name} ---");
        Printer.ColorPrint($"--- HP: {player.Health} vs HP: {monster.Health} ---");
        Printer.ColorPrint(monster.Feedback);
        Thread.Sleep(1000);
        Console.WriteLine();

        while (player.Health > 0 && monster.Health > 0)
        {
            (int originalPlayerHealth, int originalMonHealth) = (player.Health, monster.Health);
            Attack(player, monster);
            if (originalMonHealth != monster.Health)
                Printer.ColorPrint($"Player attacked {monster.Name} for {originalMonHealth - monster.Health}!");
            Thread.Sleep(500);

            if (originalPlayerHealth != player.Health)
                Printer.ColorPrint($"{monster.Name} attacked Player for {originalPlayerHealth - player.Health}!");
            Thread.Sleep(500);  // Combat's not instant...
        }

        if (player.Health <= 0)
            Printer.ColorPrint("Player has died!");

        if(monster.Health <= 0)
            Printer.ColorPrint($"{monster.Name} has died!");

        if (!player.IsDead)
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