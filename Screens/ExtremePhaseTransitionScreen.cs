using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class ExtremePhaseTransitionScreen
{
    public static void Show(Enemy enemy)
    {
        FrameBuffer.SafeClear();
        int h = ConsoleLayout.Height;
        int w = ConsoleLayout.Width;

        string phaseLine = $"▓▓▓  PHASE {enemy.CurrentPhase} / {enemy.TotalPhases}  ▓▓▓";

        // Bordure supérieure décorée
        int topY = Math.Max(2, h / 4);
        Panel.DrawCenteredText("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓", topY, ConsoleColor.DarkMagenta);
        Panel.DrawCenteredText(phaseLine, topY + 1, ConsoleColor.Magenta);
        Panel.DrawCenteredText("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓", topY + 2, ConsoleColor.DarkMagenta);

        // Sprite
        SpriteRenderer.DrawSprite(enemy.SpriteLarge, w / 2, topY + 4, ConsoleColor.Magenta);

        // Nom + avertissement
        int infoY = topY + 4 + enemy.SpriteLarge.Length + 2;
        Panel.DrawCenteredText(enemy.Name.ToUpperInvariant(), infoY, ConsoleColor.Magenta);
        Panel.DrawCenteredText("Il se relève. Plus fort.", infoY + 2, ConsoleColor.Red);
        Panel.DrawCenteredText("Sa forme change...", infoY + 3, ConsoleColor.DarkGray);

        // Bandeau bas
        int botY = Math.Min(h - 3, infoY + 5);
        Panel.DrawCenteredText("▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓", botY, ConsoleColor.DarkMagenta);

        Panel.DrawCenteredText("Appuie pour continuer...", h - 2, ConsoleColor.DarkGray);
        InputHandler.WaitKey();
    }
}