using CodeQuest.Models;

namespace CodeQuest.Data;

public static class ExtremeQuestions
{
    public static List<Question> Build() => new()
    {
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Quelle est la complexité temporelle moyenne du tri rapide (quicksort) ?",
            Answers = new[] { "O(n)", "O(n log n)", "O(n²)", "O(log n)" },
            CorrectIndex = 1,
            Explanation = "En moyenne, le quicksort divise le tableau en deux à chaque étape, soit O(n log n). Son pire cas reste O(n²) si le pivot est mal choisi.",
            Hint = "Diviser pour régner → O(n log n)."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Que se passe-t-il lors d'une attaque par 'buffer overflow' ?",
            Answers = new[] { "Les données sont chiffrées", "Un débordement écrit au-delà de la mémoire allouée", "Le CPU surchauffe", "Le disque dur est effacé" },
            CorrectIndex = 1,
            Explanation = "Un buffer overflow écrit au-delà de la zone mémoire prévue, pouvant corrompre des données adjacentes ou exécuter du code arbitraire.",
            Hint = "Overflow = écrire là où on ne devrait pas."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce qu'une attaque par 'side-channel' ?",
            Answers = new[] { "Exploiter les fuites d'information physiques (temps, courant, sons)", "Injecter du SQL", "Forcer un mot de passe", "Saturer un serveur" },
            CorrectIndex = 0,
            Explanation = "Une attaque side-channel exploite des fuites non-fonctionnelles : temps d'exécution, consommation électrique, émissions électromagnétiques.",
            Hint = "Side-channel = information qui fuit à côté."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Que signifie 'zero-copy' en programmation système ?",
            Answers = new[] { "Copier sans allouer", "Éviter les copies inutiles de données entre zones mémoire", "Copier zéro fichier", "Ne rien copier du tout" },
            CorrectIndex = 1,
            Explanation = "Le zero-copy évite les copies intermédiaires entre noyau et espace utilisateur, par exemple via mmap ou sendfile.",
            Hint = "Zero-copy = pas de copie intermédiaire."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce que le 'cache coherency' dans un système multi-cœurs ?",
            Answers = new[] { "La cohérence des couleurs d'affichage", "La garantie que tous les cœurs voient la même valeur mémoire", "Le nettoyage du cache disque", "La synchronisation des horloges" },
            CorrectIndex = 1,
            Explanation = "Le cache coherency garantit que tous les cœurs voient la même valeur pour une même adresse mémoire, malgré les caches locaux.",
            Hint = "Coherency = tout le monde voit la même chose."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce que le 'branch prediction' dans un CPU moderne ?",
            Answers = new[] { "Prédire le résultat d'un calcul", "Deviner la prochaine branche d'exécution pour précharger", "Prédire les pannes", "Deviner le mot de passe" },
            CorrectIndex = 1,
            Explanation = "Le CPU devine la branche probable à l'avance pour précharger les instructions. Une mauvaise prédiction coûte cher.",
            Hint = "Branch prediction = parier sur la prochaine instruction."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Que fait un 'garbage collector' de type 'mark-and-sweep' ?",
            Answers = new[] { "Marque puis supprime les objets inaccessibles", "Copie la mémoire", "Compte les références", "Compresse le tas" },
            CorrectIndex = 0,
            Explanation = "Le mark-and-sweep marque les objets atteignables depuis la racine, puis libère ceux qui ne le sont pas.",
            Hint = "Mark = atteignable, sweep = nettoyage."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce qu'une 'race condition' ?",
            Answers = new[] { "Un bug de chronomètre", "Un comportement imprévisible dû à l'ordre d'accès concurrent", "Une course entre processus", "Une compétition réseau" },
            CorrectIndex = 1,
            Explanation = "Une race condition survient quand plusieurs threads accèdent à une ressource partagée sans synchronisation, causant un résultat non déterministe.",
            Hint = "Race = qui arrive en premier, imprévisible."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce que le 'kernel bypass' ?",
            Answers = new[] { "Contourner le noyau pour accéder directement au matériel", "Redémarrer le noyau", "Chiffrer le noyau", "Éviter les syscalls" },
            CorrectIndex = 0,
            Explanation = "Le kernel bypass (ex : DPDK) permet à une application d'accéder directement au matériel réseau, en évitant le noyau, pour des performances extrêmes.",
            Hint = "Bypass = passer par-dessus le noyau."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Que signifie 'Byzantine fault tolerance' ?",
            Answers = new[] { "Tolérer des pannes arbitraires ou malveillantes", "Tolérer des pannes de courant", "Un protocole de chiffrement byzantin", "Une attaque de l'Empire romain" },
            CorrectIndex = 0,
            Explanation = "BFT est la capacité d'un système distribué à fonctionner correctement même si certains nœuds envoient des informations erronées ou malveillantes.",
            Hint = "Byzantine = comportement arbitraire, même menteur."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce que 'spectre' dans le contexte des vulnérabilités CPU ?",
            Answers = new[] { "Un virus de 1999", "Une attaque exploitant l'exécution spéculative", "Un fantôme dans le BIOS", "Un type de RAM" },
            CorrectIndex = 1,
            Explanation = "Spectre exploite l'exécution spéculative du CPU pour lire des données normalement inaccessibles via des canaux auxiliaires.",
            Hint = "Spectre = fantôme dans l'exécution spéculative."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Que garantit le théorème CAP pour un système distribué ?",
            Answers = new[] { "Consistance, Disponibilité, Tolérance au partitionnement : 2 sur 3", "Un système parfait existe", "Le temps ne dépasse jamais 1s", "Les données se dupliquent seules" },
            CorrectIndex = 0,
            Explanation = "Le théorème CAP stipule qu'un système distribué ne peut garantir simultanément que deux des trois propriétés : Consistance, Disponibilité, tolérance au Partitionnement.",
            Hint = "CAP = 2 sur 3 maximum."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce que le 'TLB' (Translation Lookaside Buffer) ?",
            Answers = new[] { "Un cache de traductions d'adresses virtuelles en physiques", "Un buffer vidéo", "Un registre du CPU", "Un disque SSD" },
            CorrectIndex = 0,
            Explanation = "Le TLB est un cache matériel qui accélère la traduction des adresses virtuelles en adresses physiques.",
            Hint = "TLB = cache de traduction d'adresses."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Que signifie 'fuzzing' en sécurité informatique ?",
            Answers = new[] { "Injecter des entrées aléatoires pour trouver des bugs", "Flouter une image", "Faire un flou artistique", "Un chiffrement flou" },
            CorrectIndex = 0,
            Explanation = "Le fuzzing consiste à envoyer des entrées aléatoires ou semi-aléatoires à un programme pour déclencher des comportements inattendus (crashes, failles).",
            Hint = "Fuzzing = entrées aléatoires à gogo."
        },
        new Question
        {
            Difficulty = Difficulty.Extreme,
            Text = "Qu'est-ce que 'lock-free' en programmation concurrente ?",
            Answers = new[] { "Un algorithme sans verrou utilisant des primitives atomiques", "Un programme sans serrure", "Un logiciel open-source", "Un verrou gratuit" },
            CorrectIndex = 0,
            Explanation = "Un algorithme lock-free garantit qu'au moins un thread progresse à chaque étape, sans utiliser de verrou bloquant.",
            Hint = "Lock-free = pas de verrou bloquant."
        }
    };
}