using CodeQuest.Data;
using CodeQuest.Rendering;
using CodeQuest.Services;
using CodeQuest.UI;

namespace CodeQuest.Screens;

public static class BossWarningScreen
{
    public static void Show(GameSession session)
    {
        var enemy = session.CurrentEnemy;
        if (enemy == null) return;

        var story = BossStories.Get(session.Config.Level);
        var choices = PowerUpCatalog.PickThree();
        int selected = 0;
        bool choosing = false;

        FrameBuffer.SafeClear();

        // ── Phase 1 : histoire ─────────────────────────────────
        while (!choosing)
        {
            if (FrameBuffer.NeedsClear())
                FrameBuffer.SafeClear();

            int h = ConsoleLayout.Height;
            int w = ConsoleLayout.Width;

            Panel.DrawCenteredText("⚠  BOSS APPROCHE  ⚠", 2, ConsoleColor.Red);

            // Sprite boss (réduit si trop grand)
            int spriteStartY = 4;
            SpriteRenderer.DrawSprite(enemy.SpriteLarge, w / 2, spriteStartY, AnsiPalette.FromName(enemy.Color));

            int nameY = spriteStartY + enemy.SpriteLarge.Length + 1;
            Panel.DrawCenteredText(enemy.Name, nameY, AnsiPalette.FromName(enemy.Color));

            int storyY = nameY + 2;
            foreach (var line in story)
            {
                if (storyY >= h - 3) break;
                Panel.DrawCenteredText(line, storyY, ConsoleColor.Gray);
                storyY++;
            }

            Panel.DrawCenteredText("Appuie sur une touche pour choisir ton powerup...", h - 2, ConsoleColor.DarkGray);

            InputHandler.WaitKey();
            choosing = true;
        }

        // ── Phase 2 : choix powerup ────────────────────────────
        while (true)
        {
            FrameBuffer.SafeClear();
            int h = ConsoleLayout.Height;

            Panel.DrawCenteredText("✦  CHOISIS TON POWERUP  ✦", Math.Max(2, h / 5), ConsoleColor.Magenta);
            Panel.DrawCenteredText("(gratuit, pour t'aider contre le boss)", Math.Max(3, h / 5 + 2), ConsoleColor.DarkGray);

            int startY = h / 2 - 3;
            for (int i = 0; i < choices.Count; i++)
            {
                var p = choices[i];
                int y = startY + i * 2;
                string line = $"{p.Icon}  {p.Name}  —  {p.Description}";

                if (i == selected)
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
            if (key == ConsoleKey.UpArrow) selected = (selected - 1 + choices.Count) % choices.Count;
            else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % choices.Count;
            else if (key == ConsoleKey.Enter) break;
        }

        session.AddPowerUp(choices[selected]);
    }
}