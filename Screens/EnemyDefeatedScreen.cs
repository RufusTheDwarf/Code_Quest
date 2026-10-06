using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class EnemyDefeatedScreen
{
    public static void Show(Enemy enemy, int xpGained)
    {
        FrameBuffer.SafeClear();
        int h = ConsoleLayout.Height;

        Panel.DrawCenteredText("══════════════════════════════════════", h / 2 - 3, ConsoleColor.Cyan);
        Panel.DrawCenteredText($"{enemy.Name} VAINCU !", h / 2 - 1, ConsoleColor.Cyan);
        Panel.DrawCenteredText($"+{xpGained} XP", h / 2 + 1, ConsoleColor.Yellow);
        Panel.DrawCenteredText("══════════════════════════════════════", h / 2 + 3, ConsoleColor.Cyan);

        Panel.DrawCenteredText("Appuyez sur une touche pour continuer...", h - 2, ConsoleColor.DarkGray);
        InputHandler.WaitKey();
    }
}