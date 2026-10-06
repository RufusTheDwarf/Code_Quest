using CodeQuest.Services;

namespace CodeQuest.Rendering;

public static class ProgressMap
{
    public static void Draw(GameSession session, int x, int y)
    {
        if (y < 0 || y >= ConsoleLayout.Height) return;

        int count = session.Enemies.Count;
        // Largeur d'un bloc : 4 chars nom + 1 séparateur
        // Format : [X]--[X]--[*]--[ ]--[ ]
        //           BUG  TYP  NUL  SYN  MIN

        Console.ForegroundColor = ConsoleColor.DarkGray;
        ConsoleLayout.SetCursor(x, y);
        Console.Write("MAP  ");
        Console.ResetColor();

        int cursorX = x + 5;
        for (int i = 0; i < count; i++)
        {
            var e = session.Enemies[i];
            bool isCurrent = (i == session.CurrentEnemyIndex);
            bool isDefeated = e.IsDefeated;

            string tag = isDefeated ? "[X]" : (isCurrent ? "[*]" : "[ ]");
            ConsoleColor color = isDefeated ? ConsoleColor.Green
                : isCurrent ? ConsoleColor.Yellow
                : ConsoleColor.Gray;

            Console.ForegroundColor = color;
            ConsoleLayout.SetCursor(cursorX, y);
            Console.Write(tag);
            Console.ResetColor();

            // Ligne de liaison
            if (i < count - 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ConsoleLayout.SetCursor(cursorX + 3, y);
                Console.Write("--");
                Console.ResetColor();
            }

            cursorX += 5;
        }

        // Ligne des noms (tronqués à 3 chars, en majuscules)
        int nameY = y + 1;
        if (nameY >= ConsoleLayout.Height) return;

        cursorX = x + 5;
        for (int i = 0; i < count; i++)
        {
            var e = session.Enemies[i];
            string name = e.Name.Length > 3 ? e.Name.Substring(0, 3) : e.Name;
            name = name.ToUpperInvariant();

            Console.ForegroundColor = e.IsDefeated ? ConsoleColor.DarkGray
                : (i == session.CurrentEnemyIndex) ? AnsiPalette.FromName(e.Color)
                : ConsoleColor.Gray;

            ConsoleLayout.SetCursor(cursorX, nameY);
            Console.Write(name.PadRight(3));
            Console.ResetColor();

            cursorX += 5;
        }
    }
}