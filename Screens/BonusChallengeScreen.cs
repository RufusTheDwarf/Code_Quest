using CodeQuest.Data;
using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class BonusChallengeScreen
{
    // Retourne true si le joueur a gagné
    public static bool Run(BonusLevel level)
    {
        var primary = AnsiPalette.FromName(level.PrimaryColor);
        var secondary = AnsiPalette.FromName(level.SecondaryColor);

        var rng = new Random();
        var questions = QuestionBank.Build()
            .Where(q => q.Difficulty == Difficulty.Hard || q.Difficulty == Difficulty.Extreme)
            .OrderBy(_ => rng.Next())
            .Take(level.QuestionCount)
            .ToList();

        int hp = 3;
        int correct = 0;
        int wrong = 0;
        int qIndex = 0;

        foreach (var q in questions)
        {
            qIndex++;
            var (shuffled, correctIndex) = Shuffle(q, rng);
            int selected = 0;

            // ── Sélection de la réponse ────────────────────────
            FrameBuffer.SafeClear();
            while (true)
            {
                if (FrameBuffer.NeedsClear())
                    FrameBuffer.SafeClear();

                DrawQuestion(level, q, shuffled, selected, qIndex, hp, primary, secondary);
                var key = InputHandler.WaitKey();

                if (key == ConsoleKey.Escape)
                {
                    // Confirmer abandon
                    if (ConfirmAbandon()) return false;
                    FrameBuffer.SafeClear();
                    continue;
                }
                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + shuffled.Length) % shuffled.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % shuffled.Length;
                else if (key == ConsoleKey.Enter) break;
            }

            bool ok = selected == correctIndex;

            // ── Résultat ───────────────────────────────────────
            FrameBuffer.SafeClear();
            if (ok) correct++;
            else wrong++;

            if (!ok && level.NoMistakes)
            {
                DrawResult(false, "Erreur fatale. Défi perdu.", correct, wrong, primary);
                return false;
            }

            if (!ok) hp--;

            if (hp <= 0)
            {
                DrawResult(false, "Plus de PV. Défi perdu.", correct, wrong, primary);
                return false;
            }

            // Feedback court
            int h = ConsoleLayout.Height;
            Panel.DrawCenteredText(ok ? "✦ Bonne réponse" : "✘ Mauvaise réponse",
                h / 2, ok ? ConsoleColor.Green : ConsoleColor.Red);
            Panel.DrawCenteredText($"Question {qIndex} / {questions.Count}  •  PV {hp}",
                h / 2 + 2, ConsoleColor.Gray);
            Panel.DrawCenteredText("Appuie pour continuer...", h - 2, ConsoleColor.DarkGray);
            InputHandler.WaitKey();
        }

        // ── Victoire ───────────────────────────────────────────
        FrameBuffer.SafeClear();
        DrawResult(true, $"Défi réussi ! Titre débloqué : {level.RewardTitle}", correct, wrong, primary);
        return true;
    }

    private static void DrawQuestion(BonusLevel level, Question q, string[] answers,
        int selected, int qIndex, int hp, ConsoleColor primary, ConsoleColor secondary)
    {
        int h = ConsoleLayout.Height;
        int w = ConsoleLayout.Width;

        // Bandeau haut
        Panel.DrawCenteredText($"═══ {level.Name} ═══", 1, primary);
        Panel.DrawCenteredText($"Question {qIndex} / {level.QuestionCount}   •   PV {hp}",
            2, secondary);

        // Cadre décoratif
        int frameW = Math.Min(w - 6, 76);
        int frameH = Math.Min(h - 6, 18);
        int frameX = Math.Max(0, ConsoleLayout.CenterX(frameW));
        int frameY = Math.Max(4, (h - frameH) / 2);
        Panel.DrawFrame(frameX, frameY, frameW, frameH, secondary, "═", "║");

        // Question
        var qLines = TextWrapper.Wrap(q.Text, frameW - 4);
        int qy = frameY + 2;
        foreach (var line in qLines)
        {
            Panel.DrawCenteredText(line, qy, ConsoleColor.White);
            qy++;
        }

        // Réponses
        qy += 2;
        int boxW = frameW - 6;
        int boxX = Math.Max(0, ConsoleLayout.CenterX(boxW));
        for (int i = 0; i < answers.Length; i++)
        {
            string prefix = (i == selected) ? "▶ " : "  ";
            string content = (prefix + answers[i]).PadRight(boxW);
            if (content.Length > boxW) content = content.Substring(0, boxW);

            ConsoleLayout.SetCursor(boxX, qy);
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
            qy++;
        }

        Panel.DrawCenteredText("↑ ↓  •  Entrée  •  Échap pour abandonner",
            h - 2, ConsoleColor.DarkGray);
    }

    private static void DrawResult(bool won, string message, int correct, int wrong, ConsoleColor primary)
    {
        int h = ConsoleLayout.Height;

        Panel.DrawCenteredText(won ? "═══════ DÉFI RÉUSSI ═══════" : "═══════ DÉFI ÉCHOUÉ ═══════",
            h / 2 - 5, won ? ConsoleColor.Green : ConsoleColor.Red);
        Panel.DrawCenteredText(message, h / 2 - 2, ConsoleColor.White);
        Panel.DrawCenteredText($"Bonnes : {correct}   •   Erreurs : {wrong}", h / 2, ConsoleColor.Gray);

        Panel.DrawCenteredText("Appuie pour revenir aux niveaux bonus...", h - 2, ConsoleColor.DarkGray);
        InputHandler.WaitKey();
    }

    private static bool ConfirmAbandon()
    {
        int h = ConsoleLayout.Height;
        FrameBuffer.SafeClear();
        Panel.DrawCenteredText("Abandonner le défi ?", h / 2 - 2, ConsoleColor.Red);
        Panel.DrawCenteredText("Entrée = Oui   •   Échap = Non", h / 2, ConsoleColor.Gray);

        var k = InputHandler.WaitKey();
        return k == ConsoleKey.Enter;
    }

    private static (string[] shuffled, int correctIndex) Shuffle(Question q, Random rng)
    {
        var s = (string[])q.Answers.Clone();
        int n = s.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (s[k], s[n]) = (s[n], s[k]);
        }
        int ci = Array.IndexOf(s, q.Answers[q.CorrectIndex]);
        return (s, ci);
    }
}