using CodeQuest.Rendering;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class PauseMenuScreen
{
    // 0 = reprendre, 1 = retour menu, 2 = quitter
    public static int Show(GameSession session)
    {
        string[] options = { "▶ REPRENDRE", "  RÈGLES", "  RETOUR AU MENU", "  QUITTER" };
        int selected = 0;

        FrameBuffer.SafeClear();

        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;
            int w = ConsoleLayout.Width;
            var cfg = session.Config;

            // Cadre central
            int boxW = Math.Min(60, w - 6);
            int boxH = 14;
            int boxX = Math.Max(0, ConsoleLayout.CenterX(boxW));
            int boxY = Math.Max(1, (h - boxH) / 2);

            // Titre "PAUSE"
            Panel.DrawCenteredText($" {cfg.TitlePrefix} PAUSE {cfg.TitleSuffix} ", boxY - 1, AnsiPalette.FromName(cfg.Accent));

            // Cadre
            Panel.DrawFrame(boxX, boxY, boxW, boxH, ConsoleColor.DarkGray, "─", "│");

            // Contexte partie
            string ctxLine = $"Difficulté : {cfg.Name}   •   Ennemi {session.CurrentEnemyIndex + 1}/{session.Enemies.Count}";
            Panel.DrawCenteredText(ctxLine, boxY + 2, ConsoleColor.DarkGray);

            // Options
            int menuY = boxY + 4;
            for (int i = 0; i < options.Length; i++)
            {
                int y = menuY + i * 2;
                if (y >= boxY + boxH - 1) break;

                if (i == selected)
                {
                    int x = Math.Max(0, ConsoleLayout.CenterX(options[i].Length + 4));
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    ConsoleLayout.SetCursor(x, y);
                    Console.Write("  " + options[i] + "  ");
                    Console.ResetColor();
                }
                else
                {
                    Panel.DrawCenteredText("  " + options[i] + "  ", y, ConsoleColor.Gray);
                }
            }

            Panel.DrawCenteredText("↑ ↓ pour choisir  •  Entrée pour valider  •  Échap pour reprendre", h - 2, ConsoleColor.DarkGray);

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.Escape) return 0;
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + options.Length) % options.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % options.Length;
            else if (key == ConsoleKey.Enter)
            {
                switch (selected)
                {
                    case 0: return 0;
                    case 1: ShowRules(); FrameBuffer.SafeClear(); break;
                    case 2: return 1;
                    case 3: return 2;
                }
            }
        }
    }

    private static void ShowRules()
    {
        FrameBuffer.SafeClear();
        var lines = new[]
        {
            "══════════════════════════════════════════════════════",
            "                    RÈGLES DU JEU                    ",
            "══════════════════════════════════════════════════════",
            "",
            "  ● Réponds correctement pour infliger des dégâts.",
            "  ● Une mauvaise réponse te coûte 1 PV.",
            "  ● Chaque ennemi vaincu rapporte 20 XP.",
            "  ● Tous les 40 XP : +1 niveau, +1 PV max.",
            "  ● Vaincs tous les ennemis pour la victoire.",
            "  ● Échap pendant une partie ouvre ce menu.",
            "",
            "══════════════════════════════════════════════════════"
        };

        int startY = Math.Max(1, (ConsoleLayout.Height - lines.Length) / 2);
        for (int i = 0; i < lines.Length; i++)
        {
            int y = startY + i;
            if (y >= ConsoleLayout.Height) break;
            var color = lines[i].StartsWith("═") ? ConsoleColor.Cyan
                      : lines[i].StartsWith("  ●") ? ConsoleColor.White
                      : ConsoleColor.Gray;
            Panel.DrawCenteredText(lines[i], y, color);
        }

        Panel.DrawCenteredText("Appuyez sur une touche pour revenir...", ConsoleLayout.Height - 2, ConsoleColor.DarkGray);
        InputHandler.WaitKey();
    }
}