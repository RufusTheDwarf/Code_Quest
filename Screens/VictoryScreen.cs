using CodeQuest.Rendering;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class VictoryScreen
{
    public static int Show(GameSession session)
    {
        string[] options = { "▶ RETOUR AU MENU", "  REJOUER", "  QUITTER" };
        int selected = 0;

        while (true)
        {
            FrameBuffer.SafeClear();
            int h = ConsoleLayout.Height;

            Panel.DrawCenteredText("══════════════════════════════════════", h / 2 - 8, ConsoleColor.Green);
            Panel.DrawCenteredText("VICTOIRE !", h / 2 - 6, ConsoleColor.Green);
            Panel.DrawCenteredText("══════════════════════════════════════", h / 2 - 5, ConsoleColor.Green);
            Panel.DrawCenteredText("Tous les ennemis ont été vaincus.", h / 2 - 3, ConsoleColor.White);
            Panel.DrawCenteredText($"Score : {session.ScorePercent} %", h / 2 - 1, ConsoleColor.Yellow);
            Panel.DrawCenteredText($"XP : {session.Player.Xp}", h / 2, ConsoleColor.Cyan);
            Panel.DrawCenteredText($"Niveau : {session.Player.Level}", h / 2 + 1, ConsoleColor.Cyan);

            int menuY = h / 2 + 4;
            for (int i = 0; i < options.Length; i++)
            {
                int y = menuY + i * 2;
                if (i == selected)
                {
                    int x = ConsoleLayout.CenterX(options[i].Length + 4);
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    ConsoleLayout.SetCursor(Math.Max(0, x), y);
                    Console.Write("  " + options[i] + "  ");
                    Console.ResetColor();
                }
                else
                {
                    Panel.DrawCenteredText("  " + options[i] + "  ", y, ConsoleColor.Gray);
                }
            }

            Panel.DrawCenteredText("↑ ↓ pour choisir • Entrée pour valider", h - 2, ConsoleColor.DarkGray);

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + options.Length) % options.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % options.Length;
            else if (key == ConsoleKey.Enter) return selected;
        }
    }
}