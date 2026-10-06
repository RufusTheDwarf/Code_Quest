using CodeQuest.Models;

namespace CodeQuest.Data;

public static class BossStories
{
    public static string[] Get(Difficulty d) => d switch
    {
        Difficulty.Easy => new[]
        {
            "Un grondement résonne dans le circuit.",
            "Le MINI-BUG, ancêtre de toutes les erreurs,",
            "s'éveille pour la première fois.",
            "",
            "Il est petit. Mais il est le premier.",
            "Le premier d'une longue lignée de bugs.",
            "",
            "Prépare-toi."
        },
        Difficulty.Normal => new[]
        {
            "La RAM frissonne. Quelque chose grossit.",
            "MEMORY LEAK a été invoqué par les processus",
            "que tu as laissés derrière toi.",
            "",
            "Il ne t'attaque pas. Il attend.",
            "Il te regarde te noyer dans ta propre mémoire.",
            "",
            "Prépare-toi."
        },
        Difficulty.Hard => new[]
        {
            "Le compilateur s'est tu. Le silence est total.",
            "THE COMPILER, maître absolu du code,",
            "a décidé de te juger.",
            "",
            "Chaque ligne que tu as écrite dans ta vie",
            "va être compilée, vérifiée, pesée.",
            "",
            "Prépare-toi."
        },
        Difficulty.Extreme => new[]
        {
            "Tu as atteint le seuil.",
            "Au-delà du code, au-delà du système,",
            "il y a l'ARCHITECTE.",
            "",
            "Il n'est pas un bug. Il n'est pas une erreur.",
            "Il est celui qui a écrit la première ligne.",
            "",
            "Il te regarde depuis le début.",
            "Il a toujours su que tu viendrais.",
            "",
            "Prépare-toi."
        },
        _ => new[] { "Prépare-toi." }
    };
}