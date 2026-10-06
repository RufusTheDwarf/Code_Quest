using CodeQuest.Data;
using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class DifficultyScreen
{
    public static Difficulty Show()
    {
        var configs = new[] { Difficulty.Easy, Difficulty.Normal, Difficulty.Hard, Difficulty.Extreme };
        int selected = 0;

        FrameBuffer.SafeClear();

        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            var progress = ProgressService.Load();
            int h = ConsoleLayout.Height;

            int titleY = Math.Max(1, h / 8);
            Panel.DrawCenteredText("CHOISIS TA DIFFICULTÉ", titleY, ConsoleColor.Cyan);

            int startY = titleY + 2;
            for (int i = 0; i < configs.Length; i++)
            {
                var cfg = DifficultyConfigs.Get(configs[i]);
                bool unlocked = progress.IsDifficultyUnlocked(configs[i]);
                int blockY = startY + i * 5;
                if (blockY >= h - 2) break;

                bool isSelected = (i == selected);
                var accent = AnsiPalette.FromName(cfg.Accent);

                // Bandeau de titre
                string header;
                if (unlocked)
                    header = $"  {cfg.Name}  ";
                else
                    header = $"  🔒 {cfg.Name}  ";

                if (isSelected)
                {
                    Console.BackgroundColor = unlocked ? ConsoleColor.DarkCyan : ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = unlocked ? accent : ConsoleColor.DarkGray;
                }

                int hx = Math.Max(0, ConsoleLayout.CenterX(header.Length));
                ConsoleLayout.SetCursor(hx, blockY);
                Console.Write(header);
                Console.ResetColor();

                if (!unlocked)
                {
                    string requirement = configs[i] switch
                    {
                        Difficulty.Normal => "Termine FACILE pour débloquer",
                        Difficulty.Hard => "Termine NORMAL pour débloquer",
                        Difficulty.Extreme => "Termine DIFFICILE pour débloquer",
                        _ => ""
                    };
                    Panel.DrawCenteredText(requirement, blockY + 1, ConsoleColor.DarkGray);
                }
                else
                {
                    if (isSelected)
                    {
                        Panel.DrawCenteredText(cfg.Subtitle, blockY + 1, ConsoleColor.White);
                        Panel.DrawCenteredText($"{cfg.DifficultyLabel} • {cfg.EnemyCount} ennemis", blockY + 2, ConsoleColor.DarkGray);
                        Panel.DrawCenteredText("▶ Entrée pour commencer", blockY + 3, ConsoleColor.Cyan);
                    }
                    else
                    {
                        Panel.DrawCenteredText(cfg.Subtitle, blockY + 1, ConsoleColor.DarkGray);
                    }
                }
            }

            int helpY = Math.Min(h - 3, startY + configs.Length * 5);
            Panel.DrawCenteredText("↑ ↓ pour choisir  •  Échap pour revenir", helpY, ConsoleColor.DarkGray);

            if (progress.AreBonusLevelsVisible)
            {
                Panel.DrawCenteredText("← → pour accéder aux niveaux bonus", Math.Min(h - 2, helpY + 1), ConsoleColor.Magenta);
            }

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + configs.Length) % configs.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % configs.Length;
            else if (key == ConsoleKey.Escape) return Difficulty.Easy;
            else if (key == ConsoleKey.Enter)
            {
                if (progress.IsDifficultyUnlocked(configs[selected]))
                    return configs[selected];
                // sinon : ne rien faire
            }
            else if (key == ConsoleKey.RightArrow && progress.AreBonusLevelsVisible)
            {
                BonusLevelScreen.Show();
                FrameBuffer.SafeClear();
            }
        }
    }
}