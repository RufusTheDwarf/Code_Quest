using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class MainMenuScreen
{
    private static readonly string[] Logo = new[]
    {
        "  ██████╗ ██████╗ ██████╗ ███████╗    ██████╗ ██╗   ██╗███████╗███████╗████████╗",
        " ██╔════╝██╔═══██╗██╔══██╗██╔════╝   ██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝",
        " ██║     ██║   ██║██║  ██║█████╗     ██║   ██║██║   ██║█████╗  ███████╗   ██║   ",
        " ██║     ██║   ██║██║  ██║██╔══╝     ██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║   ",
        " ╚██████╗╚██████╔╝██████╔╝███████╗   ╚██████╔╝╚██████╔╝███████╗███████║   ██║   ",
        "  ╚═════╝ ╚═════╝ ╚═════╝ ╚══════╝    ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝   "
    };

    public static int Show()
    {
        string[] options = { "▶ JOUER", "  AIDE / RÈGLES", "  QUITTER" };
        int selected = 0;

        FrameBuffer.SafeClear();

        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;
            int w = ConsoleLayout.Width;

            int logoY = Math.Max(2, h / 4 - 4);
            for (int i = 0; i < Logo.Length; i++)
            {
                int y = logoY + i;
                if (y < 0 || y >= h) continue;
                int x = Math.Max(0, ConsoleLayout.CenterX(Logo[i].Length));
                Console.ForegroundColor = i < 2 ? ConsoleColor.Cyan : ConsoleColor.DarkCyan;
                ConsoleLayout.SetCursor(x, y);
                Console.Write(Logo[i]);
            }
            Console.ResetColor();

            int subtitleY = logoY + Logo.Length + 1;
            Panel.DrawCenteredText("LE RPG DES DÉFIS INFORMATIQUES", subtitleY, ConsoleColor.DarkGray);

            int menuY = subtitleY + 3;
            for (int i = 0; i < options.Length; i++)
            {
                int y = menuY + i * 2;
                if (y >= h) break;

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

            int helpY = Math.Min(h - 3, menuY + options.Length * 2 + 1);
            Panel.DrawCenteredText("↑ ↓ pour choisir • Entrée pour valider", helpY, ConsoleColor.DarkGray);

            if (Platform.FullscreenDetector.ShouldShowFullscreenHint())
            {
                int warnY = Math.Min(h - 2, helpY + 1);
                Panel.DrawCenteredText("Appuyez sur F11 pour passer en plein écran", warnY, ConsoleColor.Yellow);
            }

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + options.Length) % options.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % options.Length;
            else if (key == ConsoleKey.Enter)
            {
                if (selected == 0) return 0;
                if (selected == 1) { ShowRules(); FrameBuffer.SafeClear(); continue; }
                if (selected == 2) return 2;
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
            "  ● Réponds correctement aux questions pour infliger",
            "    des dégâts à l'ennemi.",
            "",
            "  ● Une mauvaise réponse te coûte 1 PV.",
            "",
            "  ● Chaque ennemi vaincu rapporte 20 XP.",
            "",
            "  ● Tous les 40 XP, tu montes de niveau et tes PV max",
            "    augmentent de 1.",
            "",
            "  ● Vaincs tous les ennemis, y compris le boss final,",
            "    pour remporter la victoire.",
            "",
            "  ● ↑ ↓ pour choisir • Entrée pour valider.",
            "",
            "  ● F11 pour passer en plein écran.",
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