using CodeQuest.Models;
using CodeQuest.Rendering;
using CodeQuest.Services;

namespace CodeQuest.Screens;

public static class CombatScreen
{
    public static void Draw(
        GameSession session,
        Question question,
        string[] shuffledAnswers,
        int selected,
        int correctIndex,
        string? message,
        ConsoleColor messageColor)
    {
        int w = ConsoleLayout.Width;
        int h = ConsoleLayout.Height;
        var cfg = session.Config;
        var enemy = session.CurrentEnemy;
        var player = session.Player;

        if (enemy == null) return;

        bool stressMode = cfg.IsMultiPhase && enemy.IsBoss;

        // ── Bandeau haut (mode stress = décoré) ────────────────
        int titleY = 1;
        if (stressMode)
        {
            string stressBar = new string('▓', Math.Max(10, w - 4));
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            ConsoleLayout.SetCursor(2, titleY);
            Console.Write(stressBar);
            Console.ResetColor();
            titleY++;
        }

        string title = $" {cfg.TitlePrefix} DIFFICULTÉ : {cfg.Name} {cfg.TitleSuffix} ";
        if (stressMode) title += $"   ▓ PHASE {enemy.CurrentPhase}/{enemy.TotalPhases} ▓";
        if (title.Length > w - 2) title = title.Substring(0, Math.Max(0, w - 2));
        Panel.DrawCenteredText(title, titleY, stressMode ? ConsoleColor.Magenta : AnsiPalette.FromName(cfg.Accent));

        // ── Sprite ennemi ──────────────────────────────────────
        int spriteH = enemy.SpriteLarge.Length;
        int spriteY = Math.Max(titleY + 2, h / 4);
        int reservedBelow = 16;
        if (spriteY + spriteH + reservedBelow > h - 10)
            spriteY = Math.Max(titleY + 2, h - 10 - reservedBelow - spriteH);

        SpriteRenderer.DrawSprite(enemy.SpriteLarge, w / 2, spriteY,
            stressMode ? ConsoleColor.Magenta : AnsiPalette.FromName(enemy.Color));

        // ── Nom + barre ennemi ─────────────────────────────────
        int infoY = spriteY + spriteH + 2;
        Panel.DrawCenteredText(enemy.Name, infoY,
            stressMode ? ConsoleColor.Magenta : AnsiPalette.FromName(enemy.Color));

        string eBar = MakeBar(enemy.Hp, enemy.MaxHp, 24);
        Panel.DrawCenteredText($"[{eBar}] {Math.Max(0, enemy.Hp)}/{enemy.MaxHp}", infoY + 1, ConsoleColor.Red);

        // ── Séparateur ─────────────────────────────────────────
        int sepY = infoY + 3;
        int sepW = Math.Min(60, w - 8);
        char sepChar = stressMode ? '▓' : cfg.Separator[0];
        Panel.DrawSeparator(ConsoleLayout.CenterX(sepW), sepY, sepW,
            stressMode ? ConsoleColor.DarkMagenta : ConsoleColor.DarkGray, sepChar);

        // ── Question ───────────────────────────────────────────
        int qY = sepY + 2;
        var qLines = TextWrapper.Wrap(question.Text, Math.Min(w - 10, 70));
        foreach (var line in qLines)
        {
            if (qY >= h - 6) break;
            Panel.DrawCenteredText(line, qY, ConsoleColor.White);
            qY++;
        }

        // ── Réponses ───────────────────────────────────────────
        qY += 1;
        int maxAnsLen = 0;
        foreach (var a in shuffledAnswers) if (a.Length > maxAnsLen) maxAnsLen = a.Length;
        int boxW = Math.Min(w - 8, maxAnsLen + 8);
        if (boxW < 20) boxW = Math.Min(20, w - 4);
        int boxX = Math.Max(0, ConsoleLayout.CenterX(boxW));

        for (int i = 0; i < shuffledAnswers.Length; i++)
        {
            if (qY >= h - 8) break;

            string prefix = (i == selected) ? "▶ " : "  ";
            string content = prefix + shuffledAnswers[i];
            if (content.Length > boxW) content = content.Substring(0, boxW);
            content = content.PadRight(boxW);

            ConsoleLayout.SetCursor(boxX, qY);

            if (i == correctIndex && correctIndex >= 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (i == selected)
            {
                Console.BackgroundColor = stressMode ? ConsoleColor.DarkMagenta : ConsoleColor.DarkCyan;
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

        // ── Séparateur bas ─────────────────────────────────────
        qY += 1;
        if (qY < h - 8)
        {
            Panel.DrawSeparator(ConsoleLayout.CenterX(sepW), qY, sepW,
                stressMode ? ConsoleColor.DarkMagenta : ConsoleColor.DarkGray, sepChar);
            qY += 2;
        }

        // ── Infos joueur ───────────────────────────────────────
        int playerY = Math.Min(qY, h - 7);
        Panel.DrawCenteredText($"NIVEAU {player.Level}  •  XP : {player.Xp}", playerY, ConsoleColor.Cyan);

        string pBar = MakeBar(player.Hp, player.MaxHp, 24);
        if (playerY + 1 < h - 1)
            Panel.DrawCenteredText($"[{pBar}] {player.Hp}/{player.MaxHp}", playerY + 1, ConsoleColor.Green);

        if (session.ActivePowerUps.Count > 0 && playerY + 2 < h - 1)
        {
            var icons = session.ActivePowerUps.Select(p => p.Icon).ToArray();
            Panel.DrawCenteredText("POWERUPS : " + string.Join(" ", icons), playerY + 2, ConsoleColor.Magenta);
        }

        // ── Liste ennemis + map ────────────────────────────────
        DrawEnemyList(session);
        DrawProgressMap(session);

        // ── Message / aide ─────────────────────────────────────
        int msgY = h - 2;
        if (!string.IsNullOrEmpty(message))
            Panel.DrawCenteredText(message, msgY, messageColor);
        else
        {
            string help = stressMode
                ? "▓ ↑ ↓  •  Entrée  •  Échap pause ▓"
                : "↑ ↓ pour choisir  •  Entrée  •  Échap pour pause";
            Panel.DrawCenteredText(help, msgY, stressMode ? ConsoleColor.DarkMagenta : ConsoleColor.DarkGray);
        }
    }

    private static void DrawEnemyList(GameSession session)
    {
        int listX = 2;
        int listCount = session.Enemies.Count;
        int listY = ConsoleLayout.Height - 3 - listCount - 1;
        if (listY < 2) return;

        Console.ForegroundColor = ConsoleColor.DarkGray;
        ConsoleLayout.SetCursor(listX, listY);
        Console.Write($"ENNEMIS — {session.CurrentEnemyIndex + 1} / {listCount}");
        Console.ResetColor();

        for (int i = 0; i < listCount; i++)
        {
            int y = listY + 1 + i;
            if (y >= ConsoleLayout.Height - 2) break;

            var e = session.Enemies[i];
            bool isCurrent = (i == session.CurrentEnemyIndex);
            string line = (isCurrent ? "▶ " : "  ") + e.Name + (e.IsDefeated ? " ✓" : "");

            Console.ForegroundColor = e.IsDefeated ? ConsoleColor.DarkGray
                : isCurrent ? AnsiPalette.FromName(e.Color)
                : ConsoleColor.Gray;
            ConsoleLayout.SetCursor(listX, y);
            Console.Write(line);
            Console.ResetColor();
        }
    }

    private static void DrawProgressMap(GameSession session)
    {
        int mapX = 2;
        int mapY = ConsoleLayout.Height - 2;
        if (mapY < 2) return;

        Console.ForegroundColor = ConsoleColor.DarkGray;
        ConsoleLayout.SetCursor(mapX, mapY);
        Console.Write("MAP ");
        Console.ResetColor();

        for (int i = 0; i < session.Enemies.Count; i++)
        {
            var e = session.Enemies[i];
            bool isCurrent = (i == session.CurrentEnemyIndex);
            bool isDefeated = e.IsDefeated;

            char glyph = isDefeated ? '●' : (isCurrent ? '◉' : '○');
            ConsoleColor color = isDefeated ? ConsoleColor.DarkGray
                : isCurrent ? ConsoleColor.Yellow
                : ConsoleColor.Gray;

            Console.ForegroundColor = color;
            ConsoleLayout.SetCursor(mapX + 4 + i * 4, mapY);
            Console.Write(glyph);
            Console.ResetColor();

            if (i < session.Enemies.Count - 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ConsoleLayout.SetCursor(mapX + 5 + i * 4, mapY);
                Console.Write("━━");
                Console.ResetColor();
            }
        }
    }

    private static string MakeBar(int current, int max, int width)
    {
        if (max <= 0) max = 1;
        current = Math.Clamp(current, 0, max);
        int filled = (int)Math.Round((double)current / max * width);
        return new string('█', filled) + new string('░', width - filled);
    }
}