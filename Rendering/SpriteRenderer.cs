namespace CodeQuest.Rendering;

public static class SpriteRenderer
{
    public static void DrawSprite(string[] sprite, int centerX, int startY, ConsoleColor color)
    {
        if (sprite == null || sprite.Length == 0) return;

        int maxLen = 0;
        foreach (var l in sprite) if (l.Length > maxLen) maxLen = l.Length;

        int screenW = ConsoleLayout.Width;
        int screenH = ConsoleLayout.Height;

        if (maxLen > screenW - 2) maxLen = screenW - 2;
        int left = Math.Max(0, centerX - maxLen / 2);

        Console.ForegroundColor = color;
        for (int i = 0; i < sprite.Length; i++)
        {
            int y = startY + i;
            if (y < 0 || y >= screenH) continue;

            string line = sprite[i];
            if (left + line.Length > screenW)
                line = line.Substring(0, Math.Max(0, screenW - left));

            ConsoleLayout.SetCursor(left, y);
            Console.Write(line);
        }
        Console.ResetColor();
    }

    public static void DrawSpriteSmall(string[] sprite, int x, int y, ConsoleColor color)
    {
        if (sprite == null) return;
        Console.ForegroundColor = color;
        for (int i = 0; i < sprite.Length; i++)
        {
            int cy = y + i;
            if (cy < 0 || cy >= ConsoleLayout.Height) continue;
            ConsoleLayout.SetCursor(x, cy);
            Console.Write(sprite[i]);
        }
        Console.ResetColor();
    }
}