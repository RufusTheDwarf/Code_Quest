using CodeQuest.Models;

namespace CodeQuest.Data;

public static class BonusLevelCatalog
{
    public static List<BonusLevel> All() => new()
    {
        new BonusLevel
        {
            Name = "LE SURVIVANT",
            Subtitle = "10 questions d'affilée sans soin.",
            Description = "Un marathon. 10 questions tirées parmi les plus dures. " +
                          "Tu commences avec 3 PV, aucun powerup, aucun répit.",
            PrimaryColor = "Green",
            SecondaryColor = "Cyan",
            QuestionCount = 10,
            NoMistakes = false,
            RewardTitle = "Survivant du Code"
        },
        new BonusLevel
        {
            Name = "SANS FAUTE",
            Subtitle = "10 questions, 0 erreur autorisée.",
            Description = "L'épreuve ultime. Une seule mauvaise réponse " +
                          "et c'est terminé. Aucune seconde chance.",
            PrimaryColor = "Red",
            SecondaryColor = "Magenta",
            QuestionCount = 10,
            NoMistakes = true,
            RewardTitle = "L'Impeccable"
        }
    };
}