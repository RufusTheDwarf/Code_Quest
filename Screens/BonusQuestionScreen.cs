using CodeQuest.Data;
using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class BonusQuestionScreen
{
    public static void Show(GameSession session)
    {
        var question = BonusQuestions.Get(session.Config.Level);
        var rng = new Random();
        var shuffled = (string[])question.Answers.Clone();
        for (int i = shuffled.Length - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }
        int correctIndex = Array.IndexOf(shuffled, question.Answers[question.CorrectIndex]);

        int selected = 0;
        FrameBuffer.SafeClear();

        // ── Phase 1 : question ─────────────────────────────────
        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;
            int w = ConsoleLayout.Width;

            Panel.DrawCenteredText("═══════ QUESTION BONUS ═══════", Math.Max(2, h / 5), ConsoleColor.Magenta);
            Panel.DrawCenteredText("Une seule chance. Réponds juste.", Math.Max(3, h / 5 + 2), ConsoleColor.DarkGray);

            var qLines = TextWrapper.Wrap(question.Text, Math.Min(w - 10, 70));
            int qY = h / 2 - 4;
            foreach (var line in qLines)
            {
                Panel.DrawCenteredText(line, qY, ConsoleColor.White);
                qY++;
            }

            qY += 1;
            int boxW = 40;
            int boxX = Math.Max(0, ConsoleLayout.CenterX(boxW));

            for (int i = 0; i < shuffled.Length; i++)
            {
                string prefix = (i == selected) ? "▶ " : "  ";
                string content = (prefix + shuffled[i]).PadRight(boxW);
                if (content.Length > boxW) content = content.Substring(0, boxW);

                ConsoleLayout.SetCursor(boxX, qY);
                if (i == selected)
                {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write(content);
                Console.ResetColor();
                qY++;
            }

            Panel.DrawCenteredText("↑ ↓ pour choisir  •  Entrée pour valider", h - 2, ConsoleColor.DarkGray);

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + shuffled.Length) % shuffled.Length;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % shuffled.Length;
            else if (key == ConsoleKey.Enter) break;
        }

        bool correct = selected == correctIndex;

        // ── Phase 2 : résultat ─────────────────────────────────
        FrameBuffer.SafeClear();
        if (!correct)
        {
            int h = ConsoleLayout.Height;
            Panel.DrawCenteredText("✘ Raté.", h / 2 - 2, ConsoleColor.Red);
            Panel.DrawCenteredText($"Bonne réponse : {shuffled[correctIndex]}", h / 2, ConsoleColor.Cyan);
            Panel.DrawCenteredText(question.Explanation, h / 2 + 2, ConsoleColor.Gray);
            Panel.DrawCenteredText("Appuie pour continuer...", h - 2, ConsoleColor.DarkGray);
            InputHandler.WaitKey();
            return;
        }

        // Bonne réponse → choix powerup
        var choices = PowerUpCatalog.PickThree();
        int pSel = 0;

        while (true)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;
            Panel.DrawCenteredText("✦ BONNE RÉPONSE ! Choisis ton powerup.", Math.Max(2, h / 5), ConsoleColor.Green);

            int startY = h / 2 - 3;
            for (int i = 0; i < choices.Count; i++)
            {
                var p = choices[i];
                int y = startY + i * 2;
                string line = $"{p.Icon}  {p.Name}  —  {p.Description}";

                if (i == pSel)
                {
                    int x = Math.Max(0, ConsoleLayout.CenterX(line.Length + 4));
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                    ConsoleLayout.SetCursor(x, y);
                    Console.Write("  " + line + "  ");
                    Console.ResetColor();
                }
                else
                {
                    Panel.DrawCenteredText(line, y, AnsiPalette.FromName(p.Color));
                }
            }

            Panel.DrawCenteredText("↑ ↓ pour choisir  •  Entrée pour valider", h - 2, ConsoleColor.DarkGray);

            var key = InputHandler.WaitKey();
            if (key == ConsoleKey.UpArrow) pSel = (pSel - 1 + choices.Count) % choices.Count;
            else if (key == ConsoleKey.DownArrow) pSel = (pSel + 1) % choices.Count;
            else if (key == ConsoleKey.Enter) break;
        }

        session.AddPowerUp(choices[pSel]);
    }
}