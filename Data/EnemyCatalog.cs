using CodeQuest.Models;

namespace CodeQuest.Data;

public static class EnemyCatalog
{
    public static List<Enemy> BuildEnemies(DifficultyConfig config)
    {
        int hp = config.EnemyHpBase;

        return config.Level switch
        {
            Difficulty.Easy => new List<Enemy>
            {
                new Enemy
                {
                    Name = "BUG",
                    Description = "Une petite erreur qui se glisse partout.",
                    MaxHp = hp, Hp = hp,
                    Color = "Green",
                    DangerLevel = "★☆☆☆☆",
                    SpriteLarge = new[]
                    {
                        "     ▄▄▄▄▄     ",
                        "   ▄███████▄   ",
                        "  ███████████  ",
                        " ███▀     ▀███ ",
                        " ██   ███   ██ ",
                        " ██  █████  ██ ",
                        " ██   ███   ██ ",
                        " ███▄     ▄███ ",
                        "  ███████████  ",
                        "   ▀███████▀   ",
                        "     ▀▀▀▀▀     "
                    },
                    SpriteSmall = new[] { "▄▄▄▄", "████", "▀▀▀▀" }
                },
                new Enemy
                {
                    Name = "TYPO",
                    Description = "Une faute de frappe qui casse tout.",
                    MaxHp = hp, Hp = hp,
                    Color = "Green",
                    DangerLevel = "★☆☆☆☆",
                    SpriteLarge = new[]
                    {
                        "  ╔═══════╗  ",
                        "  ║  TYPO ║  ",
                        "  ╚═══════╝  ",
                        "   ╱     ╲   ",
                        "  ╱  ▓▓▓  ╲  ",
                        "  ╲  ▓▓▓  ╱  ",
                        "   ╲     ╱   ",
                        "    ╚═══╝    "
                    },
                    SpriteSmall = new[] { "╔═══╗", "║TYP║", "╚═══╝" }
                },
                new Enemy
                {
                    Name = "NULL POINTER",
                    Description = "Il pointe vers rien. Et ça le rend furieux.",
                    MaxHp = hp + 1, Hp = hp + 1,
                    Color = "DarkGreen",
                    DangerLevel = "★★☆☆☆",
                    SpriteLarge = new[]
                    {
                        "     ╔═══╗     ",
                        "     ║ 0 ║     ",
                        "     ╚═══╝     ",
                        "    ╱     ╲    ",
                        "   ╱  ▓▓▓  ╲   ",
                        "  ╱  ▓▓▓▓▓  ╲  ",
                        "  ╲  ▓▓▓▓▓  ╱  ",
                        "   ╲  ▓▓▓  ╱   ",
                        "    ╲     ╱    ",
                        "     ╚═══╝     "
                    },
                    SpriteSmall = new[] { "╔═══╗", "║ 0 ║", "╚═══╝" }
                },
                new Enemy
                {
                    Name = "SYNTAX ERROR",
                    Description = "Il attend un point-virgule. Il attend toujours.",
                    MaxHp = hp + 1, Hp = hp + 1,
                    Color = "DarkGreen",
                    DangerLevel = "★★☆☆☆",
                    SpriteLarge = new[]
                    {
                        "  ▄▄▄▄▄▄▄▄▄  ",
                        " ███████████ ",
                        "██  ███  ███",
                        "██  ███  ███",
                        "██  ███  ███",
                        " ███████████ ",
                        "  ▀▀▀▀▀▀▀▀▀  "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄", "█ █ █", "▀▀▀▀▀" }
                },
                new Enemy
                {
                    Name = "MINI-BUG",
                    Description = "Le boss des débuts. Plus gros, plus méchant.",
                    MaxHp = hp + 2, Hp = hp + 2,
                    Color = "Cyan",
                    DangerLevel = "★★★☆☆",
                    IsBoss = true,
                    SpriteLarge = new[]
                    {
                        "    ▄▄▄▄▄▄▄    ",
                        "  ▄█████████▄  ",
                        " █████████████ ",
                        "████▀     ▀████",
                        "███   ███   ███",
                        "███  █████  ███",
                        "███   ███   ███",
                        "████▄     ▄████",
                        " █████████████ ",
                        "  ▀█████████▀  ",
                        "    ▀▀▀▀▀▀▀    "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄▄", "██████", "▀▀▀▀▀▀" }
                }
            },

            Difficulty.Normal => new List<Enemy>
            {
                new Enemy
                {
                    Name = "MALWARE",
                    Description = "Un programme qui se propage et détruit.",
                    MaxHp = hp, Hp = hp,
                    Color = "Yellow",
                    DangerLevel = "★★★☆☆",
                    SpriteLarge = new[]
                    {
                        "  ▄▄▄▄▄▄▄▄▄  ",
                        " ███▀   ▀███ ",
                        "███  ▓▓▓  ███",
                        "███ ▓▓▓▓▓ ███",
                        "███  ▓▓▓  ███",
                        " ███▄   ▄███ ",
                        "  ▀███████▀  ",
                        "    ▀▀▀▀▀    "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄", "█▓▓▓█", "▀▀▀▀▀" }
                },
                new Enemy
                {
                    Name = "RANSOMWARE",
                    Description = "Il chiffre tout. Il ne rend rien.",
                    MaxHp = hp, Hp = hp,
                    Color = "DarkYellow",
                    DangerLevel = "★★★☆☆",
                    SpriteLarge = new[]
                    {
                        "   ╔═══════╗   ",
                        "   ║   ▓   ║   ",
                        "   ║  ▓▓▓  ║   ",
                        "   ║ ▓▓▓▓▓ ║   ",
                        "   ╚═══════╝   ",
                        "  ╱         ╲  ",
                        " ╱   ▓▓▓▓▓   ╲ ",
                        "╲   ▓▓▓▓▓▓▓   ╱",
                        " ╲   ▓▓▓▓▓   ╱ ",
                        "  ╲         ╱  ",
                        "   ╚═══════╝   "
                    },
                    SpriteSmall = new[] { "╔═══╗", "║▓▓▓║", "╚═══╝" }
                },
                new Enemy
                {
                    Name = "HACKER",
                    Description = "Il cherche des failles dans ton code.",
                    MaxHp = hp + 1, Hp = hp + 1,
                    Color = "White",
                    DangerLevel = "★★★★☆",
                    SpriteLarge = new[]
                    {
                        "    ▄▄▄▄▄▄▄    ",
                        "  ▄█████████▄  ",
                        " ███▀     ▀███ ",
                        "███   ███   ███",
                        "███  █████  ███",
                        "███   ███   ███",
                        " ███▄     ▄███ ",
                        "  ▀█████████▀  ",
                        "    ▀▀▀▀▀▀▀    "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄", "█████", "▀▀▀▀▀" }
                },
                new Enemy
                {
                    Name = "STACK ERROR",
                    Description = "Débordement de pile. Le programme s'effondre.",
                    MaxHp = hp + 1, Hp = hp + 1,
                    Color = "DarkYellow",
                    DangerLevel = "★★★★☆",
                    SpriteLarge = new[]
                    {
                        " ╔═══════════╗ ",
                        " ║ ▓▓▓▓▓▓▓▓▓ ║ ",
                        " ║ ▓▓▓▓▓▓▓▓▓ ║ ",
                        " ║ ▓▓▓▓▓▓▓▓▓ ║ ",
                        " ║ ▓▓▓▓▓▓▓▓▓ ║ ",
                        " ╚═══════════╝ ",
                        "    ╲     ╱    ",
                        "     ╲   ╱     ",
                        "      ╲ ╱      ",
                        "       V       "
                    },
                    SpriteSmall = new[] { "╔═════╗", "║▓▓▓▓▓║", "╚═════╝" }
                },
                new Enemy
                {
                    Name = "MEMORY LEAK",
                    Description = "Il consomme ta RAM jusqu'à l'asphyxie.",
                    MaxHp = hp + 2, Hp = hp + 2,
                    Color = "Cyan",
                    DangerLevel = "★★★★★",
                    IsBoss = true,
                    SpriteLarge = new[]
                    {
                        "  ▄▄▄▄▄▄▄▄▄▄▄  ",
                        " █████████████ ",
                        "███▀       ▀███",
                        "███  ▓▓▓▓▓  ███",
                        "███ ▓▓▓▓▓▓▓ ███",
                        "███  ▓▓▓▓▓  ███",
                        " ███▄     ▄███ ",
                        "  ▀█████████▀  ",
                        "    ▀▀▀▀▀▀▀    ",
                        "   ░░░░░░░░░   ",
                        "  ░░░░░░░░░░░  "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄▄▄", "███████", "▀▀▀▀▀▀▀" }
                }
            },

            Difficulty.Hard => new List<Enemy>
            {
                new Enemy
                {
                    Name = "SEGFAULT",
                    Description = "Accès mémoire interdit. Crash immédiat.",
                    MaxHp = hp, Hp = hp,
                    Color = "Red",
                    DangerLevel = "★★★★★",
                    SpriteLarge = new[]
                    {
                        "  ▄▄▄▄▄▄▄▄▄▄▄  ",
                        " █████████████ ",
                        "███▀       ▀███",
                        "███  ▓▓▓▓▓  ███",
                        "███ ▓▓▓▓▓▓▓ ███",
                        "███  ▓▓▓▓▓  ███",
                        " ███▄     ▄███ ",
                        "  ▀█████████▀  ",
                        "    ▀▀▀▀▀▀▀    ",
                        "   ╲       ╱   ",
                        "    ╲     ╱    ",
                        "     ╲   ╱     ",
                        "      ╲ ╱      ",
                        "       V       "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄▄▄", "███████", "▀▀▀▀▀▀▀" }
                },
                new Enemy
                {
                    Name = "INJECTION SQL",
                    Description = "Il s'infiltre dans tes requêtes. Corrompt tout.",
                    MaxHp = hp, Hp = hp,
                    Color = "DarkRed",
                    DangerLevel = "★★★★★",
                    SpriteLarge = new[]
                    {
                        " ╔═════════════╗ ",
                        " ║ ▓ ▓ ▓ ▓ ▓ ▓ ║ ",
                        " ║  ▓ ▓ ▓ ▓ ▓  ║ ",
                        " ║ ▓ ▓ ▓ ▓ ▓ ▓ ║ ",
                        " ║  ▓ ▓ ▓ ▓ ▓  ║ ",
                        " ║ ▓ ▓ ▓ ▓ ▓ ▓ ║ ",
                        " ╚═════════════╝ ",
                        "    ╲       ╱   ",
                        "     ╲     ╱    ",
                        "      ╲   ╱     ",
                        "       ╲ ╱      ",
                        "        V       "
                    },
                    SpriteSmall = new[] { "╔═══════╗", "║▓▓▓▓▓▓▓║", "╚═══════╝" }
                },
                new Enemy
                {
                    Name = "DEADLOCK",
                    Description = "Deux processus s'attendent. Pour toujours.",
                    MaxHp = hp + 1, Hp = hp + 1,
                    Color = "Magenta",
                    DangerLevel = "★★★★★",
                    SpriteLarge = new[]
                    {
                        "  ▄▄▄▄▄▄▄▄▄▄  ",
                        " ████████████ ",
                        "███▀       ▀██",
                        "███  ▓   ▓  ██",
                        "███ ▓▓   ▓▓ ██",
                        "███  ▓   ▓  ██",
                        " ███▄     ▄██ ",
                        "  ▀████████▀  ",
                        "    ▀▀▀▀▀▀    ",
                        "  ╔══════════╗  ",
                        "  ║ SINCE    ║  ",
                        "  ║ FOREVER  ║  ",
                        "  ╚══════════╝  "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄▄", "█▓▓▓▓█", "▀▀▀▀▀▀" }
                },
                new Enemy
                {
                    Name = "KERNEL PANIC",
                    Description = "Le cœur du système s'arrête. Tout s'effondre.",
                    MaxHp = hp + 1, Hp = hp + 1,
                    Color = "DarkRed",
                    DangerLevel = "★★★★★",
                    SpriteLarge = new[]
                    {
                        "  ██████████  ",
                        " ███▀    ▀███ ",
                        "███  ▓▓▓  ███",
                        "███ ▓▓▓▓▓ ███",
                        "███  ▓▓▓  ███",
                        " ███▄   ▄███ ",
                        "  ▀███████▀  ",
                        "    ▀▀▀▀▀    ",
                        "  ╔═══════╗  ",
                        "  ║ FATAL ║  ",
                        "  ║ ERROR ║  ",
                        "  ╚═══════╝  "
                    },
                    SpriteSmall = new[] { "█████", "█▓▓▓█", "▀▀▀▀▀" }
                },
                new Enemy
                {
                    Name = "THE COMPILER",
                    Description = "Le boss final. Il compile ton destin.",
                    MaxHp = hp + 3, Hp = hp + 3,
                    Color = "Red",
                    DangerLevel = "★★★★★",
                    IsBoss = true,
                    SpriteLarge = new[]
                    {
                        "    ▄▄▄▄▄▄▄▄▄    ",
                        "  ▄███████████▄  ",
                        " ███████████████ ",
                        "█████▀     ▀█████",
                        "████  █████  ████",
                        "████ ███████ ████",
                        "████  █████  ████",
                        " █████▄   ▄█████ ",
                        "  ▀████████████▀  ",
                        "    ▀▀▀▀▀▀▀▀▀    ",
                        "   ╔═══════════╗   ",
                        "   ║ COMPILING ║   ",
                        "   ║   DESTINY ║   ",
                        "   ╚═══════════╝   "
                    },
                    SpriteSmall = new[] { "▄▄▄▄▄▄▄▄", "████████", "▀▀▀▀▀▀▀▀" }
                }
            },

            _ => new List<Enemy>()
        };
    }
}