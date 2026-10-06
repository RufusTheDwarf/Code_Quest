namespace CodeQuest.Models;

public class Enemy
{
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public int MaxHp { get; set; }
    public int Hp { get; set; }
    public string[] SpriteLarge { get; init; } = Array.Empty<string>();
    public string[] SpriteSmall { get; init; } = Array.Empty<string>();
    public string Color { get; init; } = "White";
    public string DangerLevel { get; init; } = "";
    public bool IsBoss { get; init; }

    public bool IsDefeated => Hp <= 0;

    public Enemy Clone() => new()
    {
        Name = Name,
        Description = Description,
        MaxHp = MaxHp,
        Hp = MaxHp,
        SpriteLarge = SpriteLarge,
        SpriteSmall = SpriteSmall,
        Color = Color,
        DangerLevel = DangerLevel,
        IsBoss = IsBoss
    };
}