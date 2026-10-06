namespace CodeQuest.Rendering;

public static class ConsoleLayout
{
    public static int Width => Console.WindowWidth;
    public static int Height => Console.WindowHeight;

    public static bool IsTooSmall => Width < 80 || Height < 24;

    public static int CenterX(int contentWidth) => Math.Max(0, (Width - contentWidth) / 2);

    public static int CenterY(int contentHeight) => Math.Max(0, (Height - contentHeight) / 2);

    public static int SafeWidth => Math.Max(1, Width - 4);

    public static int PanelWidth(int maxWidth) => Math.Min(SafeWidth, maxWidth);

    public static int LeftZoneWidth => Math.Max(20, (int)(Width * 0.25));

    public static int RightZoneWidth => Math.Max(30, Width - LeftZoneWidth - 4);

    public static void SetCursor(int x, int y)
    {
        if (x < 0 || y < 0 || x >= Width || y >= Height) return;
        try { Console.SetCursorPosition(x, y); } catch { }
    }
}