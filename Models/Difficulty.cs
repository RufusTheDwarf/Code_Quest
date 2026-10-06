namespace CodeQuest.Models;

public enum Difficulty
{
    Easy = 1,
    Normal = 2,
    Hard = 3,
    Extreme = 4
}

public class DifficultyConfig
{
    public Difficulty Level { get; init; }
    public string Name { get; init; } = "";
    public string Subtitle { get; init; } = "";
    public string Description { get; init; } = "";
    public string DifficultyLabel { get; init; } = "";
    public int EnemyHpBase { get; init; }
    public int EnemyCount { get; init; }
    public string[] ThemeColors { get; init; } = Array.Empty<string>();
    public string BorderTop { get; init; } = "═";
    public string BorderSide { get; init; } = "║";
    public string Separator { get; init; } = "─";
    public string TitlePrefix { get; init; } = "═══";
    public string TitleSuffix { get; init; } = "═══";
    public string Accent { get; init; } = "";
    public bool IsMultiPhase { get; init; }
}