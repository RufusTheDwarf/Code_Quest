namespace CodeQuest.Rendering;

public static class AnsiPalette
{
    public static ConsoleColor FromName(string name) => name switch
    {
        "Black" => ConsoleColor.Black,
        "DarkBlue" => ConsoleColor.DarkBlue,
        "DarkGreen" => ConsoleColor.DarkGreen,
        "DarkCyan" => ConsoleColor.DarkCyan,
        "DarkRed" => ConsoleColor.DarkRed,
        "DarkMagenta" => ConsoleColor.DarkMagenta,
        "DarkYellow" => ConsoleColor.DarkYellow,
        "Gray" => ConsoleColor.Gray,
        "DarkGray" => ConsoleColor.DarkGray,
        "Blue" => ConsoleColor.Blue,
        "Green" => ConsoleColor.Green,
        "Cyan" => ConsoleColor.Cyan,
        "Red" => ConsoleColor.Red,
        "Magenta" => ConsoleColor.Magenta,
        "Yellow" => ConsoleColor.Yellow,
        "White" => ConsoleColor.White,
        _ => ConsoleColor.Gray
    };
}