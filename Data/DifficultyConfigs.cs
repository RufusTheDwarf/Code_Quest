using CodeQuest.Models;

namespace CodeQuest.Data;

public static class DifficultyConfigs
{
    public static readonly Dictionary<Difficulty, DifficultyConfig> All = new()
    {
        [Difficulty.Easy] = new DifficultyConfig
        {
            Level = Difficulty.Easy,
            Name = "FACILE",
            Subtitle = "Une première expédition dans le monde informatique.",
            Description = "Des bugs simples et des concepts accessibles t'attendent.",
            DifficultyLabel = "INITIATION",
            EnemyHpBase = 3,
            EnemyCount = 5,
            ThemeColors = new[] { "Green", "DarkGreen", "Cyan" },
            BorderTop = "═",
            BorderSide = "║",
            Separator = "─",
            TitlePrefix = "╔══",
            TitleSuffix = "══╗",
            Accent = "Green"
        },
        [Difficulty.Normal] = new DifficultyConfig
        {
            Level = Difficulty.Normal,
            Name = "NORMAL",
            Subtitle = "Les menaces techniques se font plus sérieuses.",
            Description = "Des ennemis plus coriaces et des questions plus techniques.",
            DifficultyLabel = "INTERMÉDIAIRE",
            EnemyHpBase = 4,
            EnemyCount = 5,
            ThemeColors = new[] { "Yellow", "DarkYellow", "White" },
            BorderTop = "━",
            BorderSide = "┃",
            Separator = "─",
            TitlePrefix = "┏━━",
            TitleSuffix = "━━┓",
            Accent = "Yellow"
        },
        [Difficulty.Hard] = new DifficultyConfig
        {
            Level = Difficulty.Hard,
            Name = "DIFFICILE",
            Subtitle = "Le noyau du système. Peu en reviennent.",
            Description = "Sécurité, mémoire, réseau, compilation. Le vrai test.",
            DifficultyLabel = "EXPERT",
            EnemyHpBase = 5,
            EnemyCount = 5,
            ThemeColors = new[] { "Red", "DarkRed", "Magenta" },
            BorderTop = "▓",
            BorderSide = "█",
            Separator = "═",
            TitlePrefix = "▓▓▓",
            TitleSuffix = "▓▓▓",
            Accent = "Red"
        }
    };

    public static DifficultyConfig Get(Difficulty d) => All[d];
}