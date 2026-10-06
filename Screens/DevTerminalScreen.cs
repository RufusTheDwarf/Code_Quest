using CodeQuest.Rendering;
using CodeQuest.Services;

namespace CodeQuest.Screens;

public static class DevTerminalScreen
{
    public static void Show()
    {
        FrameBuffer.SafeClear();
        int h = ConsoleLayout.Height;

        Panel.DrawCenteredText("═══ TERMINAL DÉVELOPPEUR ═══", Math.Max(2, h / 6), ConsoleColor.DarkGreen);

        int y = h / 2;
        Panel.DrawCenteredText("Entrez le code de développeur :", y - 2, ConsoleColor.Gray);

        Console.ForegroundColor = ConsoleColor.Green;
        ConsoleLayout.SetCursor(Math.Max(0, ConsoleLayout.CenterX(30)), y);
        Console.Write("> ");
        Console.ResetColor();

        // Position après "> "
        int inputX = Math.Max(0, ConsoleLayout.CenterX(30)) + 2;
        ConsoleLayout.SetCursor(inputX, y);

        // Réactiver le curseur pour la saisie
        try { Console.CursorVisible = true; } catch { }

        string input = "";
        while (true)
        {
            var key = Console.ReadKey(intercept: false);
            if (key.Key == ConsoleKey.Enter) break;
            if (key.Key == ConsoleKey.Escape)
            {
                try { Console.CursorVisible = false; } catch { }
                return;
            }
            if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    int px = inputX + input.Length;
                    ConsoleLayout.SetCursor(px, y);
                    Console.Write(" ");
                    ConsoleLayout.SetCursor(px, y);
                }
                continue;
            }
            if (!char.IsControl(key.KeyChar) && input.Length < 30)
            {
                input += key.KeyChar;
            }
        }

        try { Console.CursorVisible = false; } catch { }

        if (input.Trim().Equals("console.unlock", StringComparison.OrdinalIgnoreCase))
        {
            ProgressService.UnlockAll();
            Panel.DrawCenteredText("✔ Code développeur reconnu.", y + 2, ConsoleColor.Green);
            Panel.DrawCenteredText("Tout est débloqué.", y + 3, ConsoleColor.Green);
        }
        else
        {
            Panel.DrawCenteredText("✘ Code invalide.", y + 2, ConsoleColor.Red);
        }

        Panel.DrawCenteredText("Appuyez sur une touche pour revenir...", h - 2, ConsoleColor.DarkGray);
        UI.InputHandler.WaitKey();
    }
}