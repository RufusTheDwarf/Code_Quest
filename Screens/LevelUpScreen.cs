using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class LevelUpScreen
{
    public static void Show(int level, int oldMaxHp, int newMaxHp)
    {
        FrameBuffer.SafeClear();
        int h = ConsoleLayout.Height;

        Panel.DrawCenteredText("══════════════════════════════════════", h / 2 - 3, ConsoleColor.Magenta);
        Panel.DrawCenteredText("⭐ LEVEL UP ! ⭐", h / 2 - 1, ConsoleColor.Magenta);
        Panel.DrawCenteredText($"Niveau {level} atteint !", h / 2, ConsoleColor.White);
        Panel.DrawCenteredText($"PV maximum : {oldMaxHp} → {newMaxHp}", h / 2 + 2, ConsoleColor.Green);
        Panel.DrawCenteredText("══════════════════════════════════════", h / 2 + 4, ConsoleColor.Magenta);

        Panel.DrawCenteredText("Appuyez sur une touche pour continuer...", h - 2, ConsoleColor.DarkGray);
        InputHandler.WaitKey();
    }
}