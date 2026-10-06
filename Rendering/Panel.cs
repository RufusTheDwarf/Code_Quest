namespace CodeQuest.Rendering;

public static class Panel
{
    public static void DrawFrame(int x, int y, int width, int height, ConsoleColor color, string topChar = "─", string sideChar = "│")
    {
        if (width < 2 || height < 2) return;

        Console.ForegroundColor = color;

        // Top border
        ConsoleLayout.SetCursor(x, y);
        Console.Write("┌" + new string(topChar[0], width - 2) + "┐");

        // Sides
        for (int i = 1; i < height - 1; i++)
        {
            ConsoleLayout.SetCursor(x, y + i);
            Console.Write(sideChar);
            ConsoleLayout.SetCursor(x + width - 1, y + i);
            Console.Write(sideChar);
        }

        // Bottom border
        ConsoleLayout.SetCursor(x, y + height - 1);
        Console.Write("└" + new string(topChar[0], width - 2) + "┘");

        Console.ResetColor();
    }

    public static void DrawSeparator(int x, int y, int width, ConsoleColor color, char c = '─')
    {
        if (width <= 0) return;
        Console.ForegroundColor = color;
        ConsoleLayout.SetCursor(x, y);
        Console.Write(new string(c, width));
        Console.ResetColor();
    }

    public static void DrawCenteredText(string text, int y, ConsoleColor color)
    {
        if (y < 0 || y >= ConsoleLayout.Height) return;
        int x = ConsoleLayout.CenterX(text.Length);
        Console.ForegroundColor = color;
        ConsoleLayout.SetCursor(x, y);
        Console.Write(text);
        Console.ResetColor();
    }
}