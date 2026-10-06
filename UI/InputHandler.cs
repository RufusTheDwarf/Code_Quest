namespace CodeQuest.UI;

public static class InputHandler
{
    public static ConsoleKey WaitKey()
    {
        while (Console.KeyAvailable)
            Console.ReadKey(true);
        return Console.ReadKey(true).Key;
    }

    public static int Navigate(int selected, int count, ConsoleKey key)
    {
        return key switch
        {
            ConsoleKey.UpArrow => (selected - 1 + count) % count,
            ConsoleKey.DownArrow => (selected + 1) % count,
            _ => selected
        };
    }
}