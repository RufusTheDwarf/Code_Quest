using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class BonusLevelScreen
{
    public static void Show()
    {
        FrameBuffer.SafeClear();
        int h = ConsoleLayout.Height;

        Panel.DrawCenteredText("═══ NIVEAUX BONUS ═══", h / 2 - 2, ConsoleColor.Magenta);
        Panel.DrawCenteredText("Bientôt disponible...", h / 2, ConsoleColor.Gray);
        Panel.DrawCenteredText("Appuyez sur une touche pour revenir", h / 2 + 2, ConsoleColor.DarkGray);

        InputHandler.WaitKey();
    }
}