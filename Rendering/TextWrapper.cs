namespace CodeQuest.Rendering;

public static class TextWrapper
{
    public static List<string> Wrap(string text, int maxWidth)
    {
        var lines = new List<string>();
        if (string.IsNullOrEmpty(text)) return lines;
        if (maxWidth <= 0) maxWidth = 1;

        foreach (var paragraph in text.Split('\n'))
        {
            var words = paragraph.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var current = "";

            foreach (var word in words)
            {
                // Mot plus long que la largeur max → on le coupe
                if (word.Length > maxWidth)
                {
                    if (current.Length > 0) { lines.Add(current); current = ""; }

                    for (int i = 0; i < word.Length; i += maxWidth)
                        lines.Add(word.Substring(i, Math.Min(maxWidth, word.Length - i)));

                    continue;
                }

                if (current.Length == 0)
                {
                    current = word;
                }
                else if (current.Length + 1 + word.Length <= maxWidth)
                {
                    current += " " + word;
                }
                else
                {
                    lines.Add(current);
                    current = word;
                }
            }

            if (current.Length > 0) lines.Add(current);
        }

        return lines;
    }
}