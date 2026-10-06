namespace CodeQuest.Rendering;

public static class FrameBuffer
{
    private static int _lastWidth = -1;
    private static int _lastHeight = -1;

    public static bool NeedsClear()
    {
        int w = ConsoleLayout.Width;
        int h = ConsoleLayout.Height;

        if (w != _lastWidth || h != _lastHeight)
        {
            _lastWidth = w;
            _lastHeight = h;
            return true;
        }
        return false;
    }

    public static void SafeClear()
    {
        try { Console.Clear(); } catch { }

        // On resynchronise : la taille courante devient la taille "connue"
        _lastWidth = ConsoleLayout.Width;
        _lastHeight = ConsoleLayout.Height;
    }

    public static void FlushInputBuffer()
    {
        while (Console.KeyAvailable)
            Console.ReadKey(true);
    }
}