using CodeQuest.Data;
using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class BonusLevelScreen
{
    public static void Show()
    {
        var levels = BonusLevelCatalog.All();
        int selected = 0;

        FrameBuffer.SafeClear();

        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;
            int w = ConsoleLayout.Width;

            // Bandeau
            int titleY = Math.Max(2, h / 8);
            Panel.DrawCenteredText("═══════  NIVEAUX BONUS  ═══════", titleY, ConsoleColor.Magenta);
            Panel.DrawCenteredText("Réservés à ceux qui ont tout vaincu.", titleY + 1, ConsoleColor.DarkGray);

            int startY = titleY + 3;
            for (int i = 0; i < levels.Count; i++)
            {
                var lvl = levels[i];
                int blockY = startY + i * 7;
                if (blockY >= h - 2) break;

                bool isSelected = (i == selected);
                var primary = AnsiPalette.FromName(lvl.PrimaryColor);
                var secondary = AnsiPalette.FromName(lvl.SecondaryColor);

                // Titre coloré
                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.ForegroundColor = primary;
                }
                string header = $"  {lvl.Name}  ";
                int hx = Math.Max(0, ConsoleLayout.CenterX(header.Length));
                ConsoleLayout.SetCursor(hx, blockY);
                Console.Write(header);
                Console.ResetColor();

                // Sous-titre + description wrappée
                Panel.DrawCenteredText(lvl.Subtitle, blockY + 1, secondary);

                var descLines = TextWrapper.Wrap(lvl.Description, Math.Min(w - 10, 70));
                int dy = blockY + 2;
                foreach (var line in descLines)
                {
                    if (dy >= h - 2) break;
                    Panel.DrawCenteredText(line, dy, ConsoleColor.Gray);
                    dy++;
                }

                if (isSelected)
                {
                    if (dy < h - 2)
                        Panel.DrawCenteredText("▶ Entrée pour lancer le défi", dy + 1, ConsoleColor.Magenta);
                }
            }

            Panel.DrawCenteredText("↑ ↓ pour choisir  •  Échap pour revenir", h - 2, ConsoleColor.DarkGray);

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + levels.Count) % levels.Count;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % levels.Count;
            else if (key == ConsoleKey.Escape) return;
            else if (key == ConsoleKey.Enter)
            {
                bool won = BonusChallengeScreen.Run(levels[selected]);
                if (won)
                {
                    var progress = Services.ProgressService.Load();
                    progress.Bonus1Unlocked = true;
                    if (levels[selected].Name == "SANS FAUTE") progress.Bonus2Unlocked = true;
                    Services.ProgressService.Save();
                }
                FrameBuffer.SafeClear();
            }
        }
    }
}