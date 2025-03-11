// Player.cs
using System;

class Player
{
    public Room CurrentRoom { get; set; }
    private int health;

    public Player()
    {
        CurrentRoom = null;
        health = 100;
    }

    public int GetHealth()
    {
        return health;
    }

    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0) health = 0;
        Console.WriteLine($"You took {amount} damage. Current health: {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 100) health = 100;
        Console.WriteLine($"You healed {amount} health. Current health: {health}");
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void Status()
    {
        Console.WriteLine($"Player health: {health}");
    }

    public void Move()
    {
        Damage(5); // Player loses 5 health when moving
        if (!IsAlive())
        {
            Console.WriteLine("You have lost too much blood and died...");
        }
    }
    
    public void RestoreHealth()
    {
        Heal(5);
        if (!IsAlive())
        {
            Console.WriteLine("You are too weak to heal...");
        }
    }
}

  