namespace CodeQuest.Models;

public enum PowerUpType
{
    Heal,          // +2 PV immédiat (instantané)
    DamageBoost,   // +1 dégât par bonne réponse (permanent partie)
    Shield,        // absorbe la prochaine erreur (une fois)
    Vision         // révèle la bonne réponse (permanent partie)
}

public class PowerUp
{
    public PowerUpType Type { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public string Icon { get; init; } = "";
    public string Color { get; init; } = "White";
}