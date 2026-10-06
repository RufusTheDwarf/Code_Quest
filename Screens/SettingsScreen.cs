using CodeQuest.Rendering;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class SettingsScreen
{
    public static void Show()
    {
        string[] options = { "▶ RETOUR", "  TERMINAL DÉVELOPPEUR", "  GESTION DES SAUVEGARDES" };
        int selected = 0;

        FrameBuffer.SafeClear();

        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;

            int titleY = Math.Max(2, h / 6);
            Panel.DrawCenteredText("═══ PARAMÈTRES ═══", titleY, ConsoleColor.Cyan);

            int menuY = titleY + 4;
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

            Panel.DrawCenteredText("↑ ↓ pour choisir  •  Entrée pour valider  •  Échap pour revenir", h - 2, ConsoleColor.DarkGray);

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + options.Length) % options.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % options.Length;
            else if (key == ConsoleKey.Escape) return;
            else if (key == ConsoleKey.Enter)
            {
                if (selected == 0) return;
                if (selected == 1) { DevTerminalScreen.Show(); FrameBuffer.SafeClear(); continue; }
                if (selected == 2) { ShowSaveManagement(); FrameBuffer.SafeClear(); continue; }
            }
        }
    }

    private static void ShowSaveManagement()
    {
        FrameBuffer.SafeClear();
        int h = ConsoleLayout.Height;
        var progress = ProgressService.Load();

        Panel.DrawCenteredText("═══ GESTION DES SAUVEGARDES ═══", Math.Max(2, h / 6), ConsoleColor.Cyan);

        int y0 = Math.Max(4, h / 6 + 3);
        for (int i = 0; i < 3; i++)
        {
            var slot = progress.Slots[i];
            int y = y0 + i * 3;
            if (y >= h - 3) break;

            string info = slot == null
                ? $"SLOT {i + 1} : vide"
                : $"SLOT {i + 1} : {slot.Difficulty} • Ennemi {slot.EnemyIndex + 1} • Niv {slot.PlayerLevel} • {slot.SavedAt:dd/MM HH:mm}";

            Panel.DrawCenteredText(info, y, slot == null ? ConsoleColor.DarkGray : ConsoleColor.White);

            string action = slot == null ? "[ Entrée : créer ]" : "[ Entrée : supprimer ]";
            Panel.DrawCenteredText(action, y + 1, ConsoleColor.DarkGray);
        }

        Panel.DrawCenteredText("Appuyez sur une touche pour revenir...", h - 2, ConsoleColor.DarkGray);
        InputHandler.WaitKey();
    }
}