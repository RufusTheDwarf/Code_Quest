using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.Screens;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest;

public static class Program
{
    private const int XpPerEnemy = 20;

    public static void Main()
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
        }
        catch { }

        bool running = true;

        while (running)
        {
            if (ConsoleLayout.IsTooSmall)
            {
                Console.Clear();
                int h = ConsoleLayout.Height;
                if (h > 4)
                {
                    Panel.DrawCenteredText("Fenêtre trop petite.", h / 2 - 1, ConsoleColor.Red);
                    Panel.DrawCenteredText("Redimensionnez ou passez en plein écran (F11).", h / 2 + 1, ConsoleColor.Yellow);
                    Panel.DrawCenteredText("Appuyez sur une touche pour continuer...", h / 2 + 3, ConsoleColor.DarkGray);
                    InputHandler.WaitKey();
                }
                continue;
            }

            int menuChoice = MainMenuScreen.Show();
            if (menuChoice == 3) { running = false; break; }
            if (menuChoice != 0) continue;

            Difficulty difficulty = DifficultyScreen.Show();

            bool replay;
            do
            {
                replay = false;
                var session = new GameSession(difficulty);
                int result = RunGame(session);

                if (result == 0) replay = false;
                else if (result == 1) replay = true;
                else { running = false; break; }
            } while (replay);
        }

        try { Console.CursorVisible = true; } catch { }
    }

    private static int RunGame(GameSession session)
    {
        var player = session.Player;

        while (!session.IsVictory && !session.IsDefeat)
        {
            var enemy = session.CurrentEnemy;
            if (enemy == null) break;

            // Avertissement boss
            if (enemy.IsBoss && !session.BossWarningShown)
            {
                BossWarningScreen.Show(session);
                session.BossWarningShown = true;
                FrameBuffer.SafeClear();
            }

            // Question bonus (avant ennemi 3)
            if (!session.BonusQuestionAsked && session.CurrentEnemyIndex == 2)
            {
                BonusQuestionScreen.Show(session);
                session.BonusQuestionAsked = true;
                FrameBuffer.SafeClear();
            }

            // Combat
            var question = session.NextQuestion();
            var (shuffled, correctIndex) = session.ShuffleAnswers(question);

            int selected = 0;
            int displayCorrect = -1;
            bool abandoned = false;

            FrameBuffer.SafeClear();

            while (true)
            {
                if (FrameBuffer.NeedsClear())
                    FrameBuffer.SafeClear();

                int reveal = session.HasVision ? correctIndex : -1;
                CombatScreen.Draw(session, question, shuffled, selected, reveal, null, ConsoleColor.White);
                var key = InputHandler.WaitKey();

                if (key == ConsoleKey.Escape)
                {
                    int pauseResult = PauseMenuScreen.Show(session);
                    if (pauseResult == 1) { abandoned = true; break; }
                    if (pauseResult == 2) return 2;
                    FrameBuffer.SafeClear();
                    continue;
                }

                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + shuffled.Length) % shuffled.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % shuffled.Length;
                else if (key == ConsoleKey.Enter) break;
            }

            if (abandoned) return 0;

            bool correct = (selected == correctIndex);
            string selectedAnswer = shuffled[selected];

            if (correct)
            {
                player.CorrectCount++;
                int dmg = session.ComputeDamage();
                enemy.Hp -= dmg;
            }
            else
            {
                player.WrongCount++;
                if (!session.ConsumeShield())
                    player.Hp--;
            }

            FrameBuffer.SafeClear();
            displayCorrect = correctIndex;
            CombatScreen.Draw(session, question, shuffled, selected, displayCorrect, null, ConsoleColor.White);

            FeedbackScreen.Show(question, correct, selectedAnswer);

            // ── Boss multi-phases ? ────────────────────────────
            if (enemy.Hp <= 0 && enemy.CurrentPhase < enemy.TotalPhases)
            {
                enemy.CurrentPhase++;
                enemy.Hp = enemy.MaxHp;
                ExtremePhaseTransitionScreen.Show(enemy);
                continue;
            }

            // ── Ennemi vaincu ? ───────────────────────────────
            if (enemy.IsDefeated)
            {
                int oldLevel = player.Level;
                player.AddXp(XpPerEnemy);

                EnemyDefeatedScreen.Show(enemy, XpPerEnemy);

                if (player.Level > oldLevel)
                    LevelUpScreen.Show(player.Level, player.MaxHp - 1, player.MaxHp);

                session.AdvanceEnemy();
            }

            if (!player.IsAlive) break;
        }

        if (session.IsVictory)
        {
            ProgressService.Load().MarkCompleted(session.Config.Level);
            ProgressService.Save();
            return VictoryScreen.Show(session);
        }
        else
        {
            return GameOverScreen.Show(session);
        }
    }
}