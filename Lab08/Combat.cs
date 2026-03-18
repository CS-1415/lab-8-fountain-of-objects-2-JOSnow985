namespace Lab08;

public static class Combat
{
    public static void CombatLoop(Player player, Monster monster, ref Map map)
    {
        Console.Clear();

        if (monster.hasSpecAtk)
            if (Monster.rng.Next(1,101) > 80)
            {
                (int currentX, int currentY) = (player.X, player.Y);
                monster.SpecialAttack(player);
                if ((player.X, player.Y) != (currentX, currentY))
                    return;
            }

        Printer.ColorPrint("--- Combat Encounter ---");
        Printer.ColorPrint($"--- Player vs {monster.Name} ---");
        Printer.ColorPrint($"--- HP: {player.Health} vs HP: {monster.Health} ---\n");
        Printer.ColorPrint(monster.Feedback);
        Thread.Sleep(750);
        Console.WriteLine();

        while (player.Health > 0 && monster.Health > 0)
        {
            (int originalPlayerHealth, int originalMonHealth) = (player.Health, monster.Health);
            Attack(player, monster);
            if (originalMonHealth != monster.Health)
                Printer.ColorPrint($"Player attacked {monster.Name} for {originalMonHealth - monster.Health}!");
            Thread.Sleep(500);

            if (originalPlayerHealth != player.Health)
                Printer.ColorPrint($"{monster.Name} attacked Player for {originalPlayerHealth - player.Health}! \n");
            Thread.Sleep(500);  // Combat's not instant
        }

        if (player.Health <= 0)
            Printer.ColorPrint("Player has died!\n");

        if(monster.Health <= 0)
            Printer.ColorPrint($"{monster.Name} has died!\n");

        if (!player.IsDead)
        {
            (var oldWeaponInfo, var oldArmorInfo) = (player.Weapon.Info, player.Armor.Info);
            if (monster.Inventory.Count > 0)
            {
                Printer.ColorPrint($"{monster.Name} had loot:");
                Printer.PrintInventory(monster.Inventory);
            }
            Loot(player, monster);
            RemoveMonsterAt(monster.X, monster.Y, ref map);
            if (player.Weapon.Info != oldWeaponInfo)
            {
                Printer.ColorPrint($"\nYour weapon takes a new shape, it's now level {player.Weapon.Level}!\n");
                Printer.ColorPrint($"{player.Weapon.Info.Name}");
                Printer.ColorPrint($"  {player.Weapon.Info.Description}");
            }

            if (player.Armor.Info != oldArmorInfo)
            {
                Printer.ColorPrint($"\nYour armor takes a new shape, it's now level {player.Armor.Level}!\n");
                Printer.ColorPrint($"{player.Armor.Info.Name}");
                Printer.ColorPrint($"  {player.Armor.Info.Description}");
            }
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
        player.UpdateGear();
    }
    public static void RemoveMonsterAt(int x, int y, ref Map map)
    {
        map.UpdateRoomDataAt(x, y, true);
        map.MonsterList.RemoveAll(mon => mon.X == x && mon.Y == y);
    }
}