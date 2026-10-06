using CodeQuest.Models;

namespace CodeQuest.Data;

public static class PowerUpCatalog
{
    private static readonly Random _rng = new();

    public static List<PowerUp> All() => new()
    {
        new PowerUp
        {
            Type = PowerUpType.Heal,
            Name = "SOIN",
            Description = "+2 PV immédiatement",
            Icon = "[+]",
            Color = "Green"
        },
        new PowerUp
        {
            Type = PowerUpType.DamageBoost,
            Name = "PUISSANCE",
            Description = "+1 dégât à chaque bonne réponse",
            Icon = "[!]",
            Color = "Red"
        },
        new PowerUp
        {
            Type = PowerUpType.Shield,
            Name = "BOUCLIER",
            Description = "Absorbe la prochaine erreur",
            Icon = "[#]",
            Color = "Cyan"
        },
        new PowerUp
        {
            Type = PowerUpType.Vision,
            Name = "VISION",
            Description = "Révèle la bonne réponse",
            Icon = "[?]",
            Color = "Yellow"
        }
    };

    public static List<PowerUp> PickThree()
    {
        var all = All();
        // Fisher-Yates
        for (int i = all.Count - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (all[i], all[j]) = (all[j], all[i]);
        }
        return all.Take(3).ToList();
    }
}