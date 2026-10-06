using CodeQuest.Data;
using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class DifficultyScreen
{
    public static Difficulty Show()
    {
        var configs = new[] { Difficulty.Easy, Difficulty.Normal, Difficulty.Hard };
        int selected = 0;

        FrameBuffer.SafeClear();

        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;

            int titleY = Math.Max(2, h / 6);
            Panel.DrawCenteredText("CHOISIS TA DIFFICULTÉ", titleY, ConsoleColor.Cyan);

            int startY = titleY + 3;
            for (int i = 0; i < configs.Length; i++)
            {
                var cfg = DifficultyConfigs.Get(configs[i]);
                int blockY = startY + i * 6;
                if (blockY >= h - 2) break;

                bool isSelected = i == selected;
                var accent = AnsiPalette.FromName(cfg.Accent);

                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = accent;
                }

                string header = $"  {cfg.Name}  ";
                int hx = Math.Max(0, ConsoleLayout.CenterX(header.Length));
                ConsoleLayout.SetCursor(hx, blockY);
                Console.Write(header);
                Console.ResetColor();

                if (isSelected)
                {
                    Panel.DrawCenteredText(cfg.Subtitle, blockY + 1, ConsoleColor.White);
                    Panel.DrawCenteredText($"{cfg.DifficultyLabel} • {cfg.EnemyCount} ennemis", blockY + 2, ConsoleColor.DarkGray);
                    Panel.DrawCenteredText("▶ Entrée pour commencer", blockY + 4, ConsoleColor.Cyan);
                }
                else
                {
                    Panel.DrawCenteredText(cfg.Subtitle, blockY + 1, ConsoleColor.DarkGray);
                }
            }

            int helpY = Math.Min(h - 3, startY + configs.Length * 6 + 1);
            Panel.DrawCenteredText("↑ ↓ pour choisir • Entrée pour valider", helpY, ConsoleColor.DarkGray);

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + configs.Length) % configs.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % configs.Length;
            else if (key == ConsoleKey.Enter) return configs[selected];
            else if (key == ConsoleKey.Escape) return Difficulty.Easy;
        }
    }
}