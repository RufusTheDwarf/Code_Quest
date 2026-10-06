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
            if (menuChoice != 0) continue; // Paramètres/Aide déjà gérés dans MainMenuScreen

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

            var question = session.NextQuestion();
            var (shuffled, correctIndex) = session.ShuffleAnswers(question);

            int selected = 0;
            int displayCorrect = -1;

            FrameBuffer.SafeClear();

            while (true)
            {
                if (FrameBuffer.NeedsClear())
                    FrameBuffer.SafeClear();

                CombatScreen.Draw(session, question, shuffled, selected, displayCorrect, null, ConsoleColor.White);
                var key = InputHandler.WaitKey();

                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + shuffled.Length) % shuffled.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % shuffled.Length;
                else if (key == ConsoleKey.Enter) break;
            }

            bool correct = (selected == correctIndex);
            string selectedAnswer = shuffled[selected];

            if (correct) { player.CorrectCount++; enemy.Hp--; }
            else { player.WrongCount++; player.Hp--; }

            FrameBuffer.SafeClear();
            displayCorrect = correctIndex;
            CombatScreen.Draw(session, question, shuffled, selected, displayCorrect, null, ConsoleColor.White);

            FeedbackScreen.Show(question, correct, selectedAnswer);

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