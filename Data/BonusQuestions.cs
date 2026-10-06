using CodeQuest.Models;

namespace CodeQuest.Data;

public static class BonusQuestions
{
    // Une question bonus par difficulté.
    // Format : question très précise, 3 réponses très proches.
    public static Question Get(Difficulty d) => d switch
    {
        Difficulty.Easy => new Question
        {
            Difficulty = Difficulty.Easy,
            Text = "En binaire, quelle est la valeur décimale du nombre 1010 ?",
            Answers = new[] { "8", "10", "12" },
            CorrectIndex = 1,
            Explanation = "1010 en binaire = 1×8 + 0×4 + 1×2 + 0×1 = 10. Piège classique : on croit souvent que c'est 8 (1000) ou 12 (1100).",
            Hint = "Puissances de 2 : 8 + 2 = 10."
        },
        Difficulty.Normal => new Question
        {
            Difficulty = Difficulty.Normal,
            Text = "En notation CIDR, que représente /24 dans 192.168.1.0/24 ?",
            Answers = new[] { "24 adresses disponibles", "24 bits de masque réseau", "24 hôtes maximum" },
            CorrectIndex = 1,
            Explanation = "/24 signifie que les 24 premiers bits identifient le réseau. Le masque est 255.255.255.0, ce qui laisse 8 bits pour les hôtes (256 adresses, 254 utilisables).",
            Hint = "CIDR = nombre de bits à 1 dans le masque."
        },
        Difficulty.Hard => new Question
        {
            Difficulty = Difficulty.Hard,
            Text = "En complexité algorithmique, quelle est la différence entre O(2^n) et O(n!) ?",
            Answers = new[]
            {
                "O(2^n) est strictement plus lent que O(n!)",
                "O(n!) est strictement plus lent que O(2^n) pour n grand",
                "Les deux sont équivalents asymptotiquement"
            },
            CorrectIndex = 1,
            Explanation = "Pour n grand, n! croît bien plus vite que 2^n. Ex : 2^10 = 1024 mais 10! = 3 628 800. O(n!) est le pire des cas classiques.",
            Hint = "Factorielle écrase toute exponentielle."
        },
        Difficulty.Extreme => new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Dans le contexte de la mémoire virtuelle, quelle est la différence exacte entre 'page fault' et 'segmentation fault' ?",
            Answers = new[]
            {
                "Page fault = accès à une page non chargée (récupérable) ; segmentation fault = accès mémoire invalide (fatal)",
                "Page fault = erreur disque ; segmentation fault = erreur CPU",
                "Les deux sont identiques, seul le nom change selon l'OS"
            },
            CorrectIndex = 0,
            Explanation = "Un page fault est un mécanisme normal : l'OS charge la page depuis le disque et l'exécution reprend. Un segmentation fault est une violation d'accès irrécupérable : le processus est tué.",
            Hint = "Page fault = rattrapable. Segfault = fatal."
        },
        _ => new Question
        {
            Difficulty = Difficulty.Easy,
            Text = "Question bonus",
            Answers = new[] { "A", "B", "C" },
            CorrectIndex = 0,
            Explanation = "",
            Hint = ""
        }
    };
}