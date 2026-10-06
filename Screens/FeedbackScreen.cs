using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class FeedbackScreen
{
    public static void Show(Question question, bool correct, string selectedAnswer)
    {
        FrameBuffer.SafeClear();

        var lines = correct
            ? FeedbackService.BuildCorrectFeedback(question)
            : FeedbackService.BuildWrongFeedback(question, selectedAnswer);

        int startY = Math.Max(2, (ConsoleLayout.Height - lines.Length) / 2);

        for (int i = 0; i < lines.Length; i++)
        {
            int y = startY + i;
            if (y >= ConsoleLayout.Height) break;

            var line = lines[i];
            var color = line.StartsWith("✦") ? (correct ? ConsoleColor.Green : ConsoleColor.Red)
                      : line.StartsWith("ASTUCE") || line.StartsWith("POUR") ? ConsoleColor.Yellow
                      : line.StartsWith("Bonne réponse") ? ConsoleColor.Cyan
                      : ConsoleColor.Gray;

            var wrapped = TextWrapper.Wrap(line, Math.Min(ConsoleLayout.Width - 8, 76));
            foreach (var wl in wrapped)
            {
                if (y >= ConsoleLayout.Height) break;
                Panel.DrawCenteredText(wl, y, color);
                y++;
            }
        }

        Panel.DrawCenteredText("Appuyez sur une touche pour continuer...", ConsoleLayout.Height - 2, ConsoleColor.DarkGray);
        InputHandler.WaitKey();
    }
}