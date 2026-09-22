using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Question
{
    public string Text;
    public string[] Answers;
    public int CorrectIndex;
    public string Explanation;
    public int Difficulty;
}

class Enemy
{
    public string Name;
    public string Emoji;
    public int MaxHp;
    public int Hp;
}

class Program
{
    static Random rng = new Random();

    static void Main()
    {
        try { Console.OutputEncoding = System.Text.Encoding.UTF8; } catch { }

        ShowIntro();
        int difficulty = ChooseDifficulty();
        int enemyHp = difficulty == 1 ? 3 : difficulty == 2 ? 4 : 5;

        List<Question> bank = BuildQuestionBank().Where(q => q.Difficulty <= difficulty).ToList();
        Shuffle(bank);
        int qIndex = 0;

        int playerMaxHp = 5;
        int playerHp = playerMaxHp;
        int level = 1;
        int xp = 0;
        int correctCount = 0;
        int wrongCount = 0;

        List<Enemy> enemies = new List<Enemy>
        {
            new Enemy { Name = "Bug", Emoji = "🐛", MaxHp = enemyHp, Hp = enemyHp },
            new Enemy { Name = "Malware", Emoji = "🤖", MaxHp = enemyHp, Hp = enemyHp },
            new Enemy { Name = "Hacker", Emoji = "👾", MaxHp = enemyHp, Hp = enemyHp },
            new Enemy { Name = "Segmentation Fault", Emoji = "💀", MaxHp = enemyHp, Hp = enemyHp },
            new Enemy { Name = "Final Boss : The Compiler", Emoji = "🐉", MaxHp = enemyHp + 2, Hp = enemyHp + 2 },
        };

        foreach (var enemy in enemies)
        {
            Console.Clear();
            PrintEnemyIntro(enemy);

            while (enemy.Hp > 0 && playerHp > 0)
            {
                PrintStatus(playerHp, playerMaxHp, level, enemy);

                if (qIndex >= bank.Count)
                {
                    Shuffle(bank);
                    qIndex = 0;
                }
                Question q = bank[qIndex++];

                bool correct = AskQuestion(q);

                if (correct)
                {
                    correctCount++;
                    enemy.Hp--;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✅ Bonne réponse !\n");
                    Console.ResetColor();
                    Console.WriteLine(q.Explanation);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n⚔️  {enemy.Name} perd 1 PV !");
                    Console.ResetColor();
                }
                else
                {
                    wrongCount++;
                    playerHp--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n❌ Mauvaise réponse !\n");
                    Console.ResetColor();
                    Console.WriteLine("La bonne réponse était :");
                    Console.WriteLine($"{q.CorrectIndex + 1}. {q.Answers[q.CorrectIndex]}");
                    Console.WriteLine($"\n💥 {enemy.Name} contre-attaque !");
                    Console.WriteLine("Vous perdez 1 PV.");
                }

                Thread.Sleep(700);
                Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                Console.ReadKey(true);
                Console.Clear();

                if (playerHp <= 0) break;

                if (enemy.Hp <= 0)
                {
                    xp += 20;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n🎉 {enemy.Name} VAINCU !");
                    Console.WriteLine("+20 XP");
                    Console.ResetColor();

                    int newLevel = 1 + xp / 40;
                    if (newLevel > level)
                    {
                        int oldMaxHp = playerMaxHp;
                        level = newLevel;
                        playerMaxHp++;
                        playerHp = Math.Min(playerHp + 1, playerMaxHp);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\n⭐ LEVEL UP !");
                        Console.WriteLine($"Vous êtes maintenant niveau {level} !");
                        Console.WriteLine($"PV maximum : {oldMaxHp} → {playerMaxHp}");
                        Console.ResetColor();
                    }

                    Thread.Sleep(900);
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey(true);
                    Console.Clear();
                }
            }

            if (playerHp <= 0) break;
        }

        if (playerHp <= 0)
            ShowGameOver(level, correctCount, wrongCount);
        else
            ShowVictory(level, correctCount, wrongCount);
    }

    static void ShowIntro()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("========================================");
        Console.WriteLine("          ⚔️  CODE QUEST  ⚔️");
        Console.WriteLine("       LE RPG QUIZ INFORMATIQUE");
        Console.WriteLine("========================================");
        Console.ResetColor();
        Console.WriteLine("\nBienvenue dans Code Quest !\n");
        Console.WriteLine("Réponds correctement aux questions");
        Console.WriteLine("d'informatique pour vaincre tes ennemis.\n");
    }

    static int ChooseDifficulty()
    {
        while (true)
        {
            Console.WriteLine("Choisis ta difficulté :\n");
            Console.WriteLine("1. Facile");
            Console.WriteLine("2. Normal");
            Console.WriteLine("3. Difficile");
            Console.Write("\nVotre choix : ");
            string input = Console.ReadLine();
            if (input == "1" || input == "2" || input == "3")
                return int.Parse(input);
            Console.WriteLine("\nChoix invalide.\n");
        }
    }

    static void PrintEnemyIntro(Enemy enemy)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"   Un ennemi apparaît : {enemy.Emoji} {enemy.Name}");
        Console.WriteLine("----------------------------------------");
        Console.ResetColor();
        Thread.Sleep(500);
    }

    static void PrintStatus(int playerHp, int playerMaxHp, int level, Enemy enemy)
    {
        Console.WriteLine($"Niveau {level}   PV : {playerHp}/{playerMaxHp}   |   {enemy.Emoji} {enemy.Name} PV : {Math.Max(enemy.Hp, 0)}/{enemy.MaxHp}\n");
    }

    static bool AskQuestion(Question q)
    {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("              💻 QUESTION");
        Console.WriteLine("----------------------------------------\n");
        Console.WriteLine(q.Text + "\n");
        for (int i = 0; i < q.Answers.Length; i++)
            Console.WriteLine($"{i + 1}. {q.Answers[i]}");

        int choice = -1;
        while (choice < 1 || choice > 4)
        {
            Console.Write("\nVotre réponse : ");
            string input = Console.ReadLine();
            int.TryParse(input, out choice);
            if (choice < 1 || choice > 4)
                Console.WriteLine("Entrez un nombre entre 1 et 4.");
        }

        return choice - 1 == q.CorrectIndex;
    }

    static void ShowVictory(int level, int correct, int wrong)
    {
        Console.Clear();
        int total = correct + wrong;
        int score = total == 0 ? 0 : (int)Math.Round(100.0 * correct / total);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("========================================");
        Console.WriteLine("             🏆 VICTOIRE !");
        Console.WriteLine("========================================");
        Console.ResetColor();
        Console.WriteLine("\nVous avez vaincu le Boss !\n");
        Console.WriteLine($"Niveau : {level}");
        Console.WriteLine($"Bonnes réponses : {correct}");
        Console.WriteLine($"Mauvaises réponses : {wrong}");
        Console.WriteLine($"\nScore : {score}%\n");
        Console.WriteLine("========================================");
        Console.WriteLine("       MERCI D'AVOIR JOUÉ !");
        Console.WriteLine("========================================\n");

        if (score < 50)
            Console.WriteLine("Il reste encore quelques bugs à corriger ! 🐛");
        else if (score < 80)
            Console.WriteLine("Pas mal ! Ton code compile presque ! 💻");
        else
            Console.WriteLine("Excellent ! Tu as clairement le niveau pour coder ! 🚀");
    }

    static void ShowGameOver(int level, int correct, int wrong)
    {
        Console.Clear();
        int total = correct + wrong;
        int score = total == 0 ? 0 : (int)Math.Round(100.0 * correct / total);

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("========================================");
        Console.WriteLine("             💀 GAME OVER");
        Console.WriteLine("========================================");
        Console.ResetColor();
        Console.WriteLine("\nVous avez été vaincu...\n");
        Console.WriteLine($"Niveau : {level}");
        Console.WriteLine($"Bonnes réponses : {correct}");
        Console.WriteLine($"Mauvaises réponses : {wrong}");
        Console.WriteLine($"\nScore : {score}%\n");
        Console.WriteLine("========================================");
        Console.WriteLine("       MERCI D'AVOIR JOUÉ !");
        Console.WriteLine("========================================");
    }

    static void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    static List<Question> BuildQuestionBank()
    {
        return new List<Question>
        {
            // Facile
            new Question { Difficulty=1, Text="Que signifie CPU ?", Answers=new[]{"Central Processing Unit","Computer Personal User","Central Program Utility","Computer Processing User"}, CorrectIndex=0, Explanation="Le CPU est le processeur, le cerveau de l'ordinateur." },
            new Question { Difficulty=1, Text="À quoi sert une souris ?", Answers=new[]{"A imprimer des documents","A naviguer et cliquer à l'écran","A stocker des fichiers","A se connecter à Internet"}, CorrectIndex=1, Explanation="La souris permet de déplacer le curseur et de cliquer." },
            new Question { Difficulty=1, Text="Qu'est-ce qu'un navigateur web ?", Answers=new[]{"Un logiciel pour écrire du code","Un logiciel pour naviguer sur Internet","Un antivirus","Un système d'exploitation"}, CorrectIndex=1, Explanation="Chrome, Firefox ou Edge sont des navigateurs web." },
            new Question { Difficulty=1, Text="Combien de bits contient un octet ?", Answers=new[]{"4","8","16","10"}, CorrectIndex=1, Explanation="Un octet (byte) vaut 8 bits." },
            new Question { Difficulty=1, Text="Que signifie Wi-Fi ?", Answers=new[]{"Wireless Fidelity","Wired Fidelity","Wireless File","Web Fidelity"}, CorrectIndex=0, Explanation="Le Wi-Fi permet une connexion réseau sans fil." },
            new Question { Difficulty=1, Text="Quel composant sert à afficher une image ?", Answers=new[]{"Le disque dur","La carte graphique","Le clavier","La RAM"}, CorrectIndex=1, Explanation="La carte graphique (GPU) traite et affiche les images." },
            new Question { Difficulty=1, Text="Qu'est-ce qu'un fichier ?", Answers=new[]{"Un dossier physique","Un ensemble de données stocké sous un nom","Un virus","Un type de câble"}, CorrectIndex=1, Explanation="Un fichier regroupe des données identifiées par un nom." },
            new Question { Difficulty=1, Text="Quel appareil permet de stocker des fichiers ?", Answers=new[]{"Le clavier","La souris","Le disque dur","L'écran"}, CorrectIndex=2, Explanation="Le disque dur (ou SSD) stocke les données durablement." },
            new Question { Difficulty=1, Text="Que signifie USB ?", Answers=new[]{"Universal Serial Bus","United System Board","Universal System Bus","Unified Serial Board"}, CorrectIndex=0, Explanation="L'USB est un standard de connexion universel." },
            new Question { Difficulty=1, Text="Qu'est-ce qu'un système d'exploitation ?", Answers=new[]{"Un jeu vidéo","Un logiciel qui gère le matériel et les programmes","Un langage de programmation","Un type de câble"}, CorrectIndex=1, Explanation="Windows, macOS et Linux sont des systèmes d'exploitation." },
            new Question { Difficulty=1, Text="Que signifie RAM ?", Answers=new[]{"Random Access Memory","Read Access Memory","Rapid Access Memory","Random Application Memory"}, CorrectIndex=0, Explanation="La RAM est la mémoire vive, temporaire et rapide." },
            new Question { Difficulty=1, Text="Quel raccourci permet de copier un texte sous Windows ?", Answers=new[]{"Ctrl+V","Ctrl+C","Ctrl+X","Ctrl+Z"}, CorrectIndex=1, Explanation="Ctrl+C copie, Ctrl+V colle, Ctrl+X coupe." },
            new Question { Difficulty=1, Text="Qu'est-ce qu'un mot de passe fort ?", Answers=new[]{"Un mot simple facile à retenir","Une combinaison longue de lettres, chiffres et symboles","Le prénom de l'utilisateur","Une suite de chiffres identiques"}, CorrectIndex=1, Explanation="Plus un mot de passe est long et varié, plus il est difficile à casser." },
            new Question { Difficulty=1, Text="Quel type de fichier est une image ?", Answers=new[]{".exe",".jpg",".mp3",".txt"}, CorrectIndex=1, Explanation=".jpg est un format d'image très courant." },
            new Question { Difficulty=1, Text="Qu'est-ce que le cloud ?", Answers=new[]{"Un logiciel antivirus","Un espace de stockage en ligne","Un type de processeur","Un langage de programmation"}, CorrectIndex=1, Explanation="Le cloud permet de stocker et d'accéder à des données via Internet." },

            // Normal
            new Question { Difficulty=2, Text="Quelle est la différence entre RAM et stockage ?", Answers=new[]{"La RAM est permanente, le stockage est temporaire","La RAM est temporaire et rapide, le stockage est permanent","Les deux sont identiques","La RAM sert à imprimer"}, CorrectIndex=1, Explanation="La RAM perd ses données à l'extinction, contrairement au stockage." },
            new Question { Difficulty=2, Text="Quel est le rôle principal du processeur (CPU) ?", Answers=new[]{"Stocker les fichiers","Exécuter les instructions et calculs","Afficher l'image","Se connecter au Wi-Fi"}, CorrectIndex=1, Explanation="Le CPU exécute les instructions des programmes." },
            new Question { Difficulty=2, Text="Qu'est-ce qu'une adresse IP ?", Answers=new[]{"Un mot de passe","Un identifiant unique d'un appareil sur un réseau","Un type de virus","Un nom de domaine"}, CorrectIndex=1, Explanation="Une adresse IP identifie un appareil sur un réseau." },
            new Question { Difficulty=2, Text="A quoi sert le DNS ?", Answers=new[]{"Chiffrer les données","Traduire les noms de domaine en adresses IP","Compresser les fichiers","Gérer la mémoire"}, CorrectIndex=1, Explanation="Le DNS traduit par exemple google.com en adresse IP." },
            new Question { Difficulty=2, Text="Quelle est la différence entre HTTP et HTTPS ?", Answers=new[]{"Aucune différence","HTTPS est chiffré et sécurisé","HTTP est plus rapide","HTTPS est plus ancien"}, CorrectIndex=1, Explanation="Le S de HTTPS signifie que la connexion est chiffrée (TLS)." },
            new Question { Difficulty=2, Text="A quoi sert HTML dans le développement web ?", Answers=new[]{"A styliser les pages","A structurer le contenu des pages","A gérer les bases de données","A sécuriser le site"}, CorrectIndex=1, Explanation="HTML structure le contenu d'une page web." },
            new Question { Difficulty=2, Text="A quoi sert CSS ?", Answers=new[]{"Structurer le contenu","Ajouter de l'interactivité","Mettre en forme et styliser les pages","Gérer le serveur"}, CorrectIndex=2, Explanation="CSS gère l'apparence visuelle des pages web." },
            new Question { Difficulty=2, Text="A quoi sert JavaScript principalement ?", Answers=new[]{"Styliser les pages","Rendre les pages web interactives","Stocker les données sur disque","Chiffrer les mots de passe"}, CorrectIndex=1, Explanation="JavaScript ajoute du comportement et de l'interactivité aux pages." },
            new Question { Difficulty=2, Text="Qu'est-ce que Git ?", Answers=new[]{"Un langage de programmation","Un système de gestion de versions","Un navigateur web","Un antivirus"}, CorrectIndex=1, Explanation="Git permet de suivre l'historique des modifications d'un code." },
            new Question { Difficulty=2, Text="Qu'est-ce qu'une base de données ?", Answers=new[]{"Un logiciel de dessin","Un système organisé pour stocker et gérer des données","Un type de processeur","Un protocole réseau"}, CorrectIndex=1, Explanation="Une base de données organise et stocke des informations de façon structurée." },
            new Question { Difficulty=2, Text="Qu'est-ce qu'un algorithme ?", Answers=new[]{"Un virus informatique","Une suite d'instructions pour résoudre un problème","Un type de mémoire","Un langage de programmation"}, CorrectIndex=1, Explanation="Un algorithme est une suite d'étapes logiques pour résoudre un problème." },
            new Question { Difficulty=2, Text="Quel est le rôle d'un système d'exploitation ?", Answers=new[]{"Gérer le matériel et exécuter les logiciels","Naviguer sur Internet uniquement","Stocker des mots de passe","Afficher des images"}, CorrectIndex=0, Explanation="L'OS fait le lien entre le matériel et les logiciels." },
            new Question { Difficulty=2, Text="Quel terme désigne un réseau d'ordinateurs interconnectés ?", Answers=new[]{"Un cluster","Un réseau informatique","Une base de données","Un algorithme"}, CorrectIndex=1, Explanation="Un réseau informatique relie plusieurs appareils entre eux." },
            new Question { Difficulty=2, Text="Qu'est-ce qu'un compilateur ?", Answers=new[]{"Un logiciel qui traduit du code source en code exécutable","Un antivirus","Un type de mémoire","Un protocole réseau"}, CorrectIndex=0, Explanation="Le compilateur transforme le code source en programme exécutable." },
            new Question { Difficulty=2, Text="Qu'est-ce qu'une machine virtuelle ?", Answers=new[]{"Un ordinateur physique supplémentaire","Un environnement simulé qui exécute un système comme un ordinateur réel","Un type de virus","Un langage de programmation"}, CorrectIndex=1, Explanation="Une VM simule un ordinateur complet dans un logiciel." },
            new Question { Difficulty=2, Text="Qu'est-ce qu'un terminal en informatique ?", Answers=new[]{"Une interface en ligne de commande","Un écran tactile","Un type de câble","Un antivirus"}, CorrectIndex=0, Explanation="Le terminal permet d'exécuter des commandes textuelles." },
            new Question { Difficulty=2, Text="Que représente l'extension .py d'un fichier ?", Answers=new[]{"Un fichier Python","Un fichier Java","Un fichier texte","Un fichier image"}, CorrectIndex=0, Explanation=".py est l'extension des scripts Python." },
            new Question { Difficulty=2, Text="Qu'est-ce que la programmation ?", Answers=new[]{"Écrire des instructions pour qu'un ordinateur les exécute","Naviguer sur Internet","Installer un antivirus","Formater un disque"}, CorrectIndex=0, Explanation="Programmer, c'est écrire des instructions exécutées par une machine." },
            new Question { Difficulty=2, Text="Que signifie 'réseau local' (LAN) ?", Answers=new[]{"Un réseau mondial","Un réseau limité à une zone géographique restreinte","Un type de virus","Un protocole de chiffrement"}, CorrectIndex=1, Explanation="Un LAN relie des appareils proches, comme dans un bâtiment." },
            new Question { Difficulty=2, Text="Qu'est-ce qu'un mot de passe haché (hash) ?", Answers=new[]{"Un mot de passe en clair","Une version transformée et irréversible d'un mot de passe","Un mot de passe partagé","Un type de virus"}, CorrectIndex=1, Explanation="Le hachage transforme un mot de passe pour éviter de le stocker en clair." },

            // Difficile
            new Question { Difficulty=3, Text="Quelle structure de données fonctionne en LIFO ?", Answers=new[]{"File (Queue)","Pile (Stack)","Liste chaînée","Arbre binaire"}, CorrectIndex=1, Explanation="La pile (Stack) suit le principe dernier entré, premier sorti." },
            new Question { Difficulty=3, Text="Que mesure la complexité algorithmique O(n) ?", Answers=new[]{"Un temps constant","Un temps proportionnel à la taille des données","Un temps exponentiel","Un temps logarithmique"}, CorrectIndex=1, Explanation="O(n) signifie que le temps croît linéairement avec la taille des données." },
            new Question { Difficulty=3, Text="Que signifie POO (programmation orientée objet) ?", Answers=new[]{"Un paradigme basé sur les objets et les classes","Un langage de bas niveau","Un protocole réseau","Un type de base de données"}, CorrectIndex=0, Explanation="La POO organise le code autour de classes et d'objets." },
            new Question { Difficulty=3, Text="Qu'est-ce que la récursivité ?", Answers=new[]{"Une boucle infinie","Une fonction qui s'appelle elle-même","Un type de variable","Un protocole réseau"}, CorrectIndex=1, Explanation="Une fonction récursive s'appelle elle-même pour résoudre un sous-problème." },
            new Question { Difficulty=3, Text="A quoi sert un pointeur en programmation ?", Answers=new[]{"A stocker une valeur directement","A référencer une adresse mémoire","A chiffrer des données","A afficher du texte"}, CorrectIndex=1, Explanation="Un pointeur contient l'adresse mémoire d'une donnée." },
            new Question { Difficulty=3, Text="Quelle est la différence entre un processus et un thread ?", Answers=new[]{"Ils sont identiques","Un processus est indépendant, un thread partage la mémoire de son processus","Un thread est plus lent qu'un processus","Un processus ne peut avoir qu'un seul thread"}, CorrectIndex=1, Explanation="Les threads d'un même processus partagent sa mémoire." },
            new Question { Difficulty=3, Text="Quelle est la différence principale entre TCP et UDP ?", Answers=new[]{"TCP est fiable et orienté connexion, UDP est plus rapide mais non fiable","UDP est toujours plus lent que TCP","Ils sont identiques","TCP ne fonctionne pas sur Internet"}, CorrectIndex=0, Explanation="TCP garantit la livraison, UDP privilégie la vitesse." },
            new Question { Difficulty=3, Text="Combien de couches possède le modèle OSI ?", Answers=new[]{"5","7","4","9"}, CorrectIndex=1, Explanation="Le modèle OSI comporte 7 couches, de physique à application." },
            new Question { Difficulty=3, Text="Qu'est-ce qu'un sous-réseau (subnet) ?", Answers=new[]{"Une division logique d'un réseau IP","Un type de câble","Un protocole de chiffrement","Un antivirus réseau"}, CorrectIndex=0, Explanation="Un subnet divise un réseau IP en sous-parties plus petites." },
            new Question { Difficulty=3, Text="Qu'est-ce que le chiffrement asymétrique ?", Answers=new[]{"Utilise la même clé pour chiffrer et déchiffrer","Utilise une paire de clés publique/privée","N'utilise aucune clé","Est réservé aux mots de passe"}, CorrectIndex=1, Explanation="Le chiffrement asymétrique utilise une clé publique et une clé privée." },
            new Question { Difficulty=3, Text="Qu'est-ce qu'une injection SQL ?", Answers=new[]{"Une optimisation de base de données","Une attaque exploitant une requête SQL mal filtrée","Un type de sauvegarde","Un protocole réseau"}, CorrectIndex=1, Explanation="Une injection SQL exploite un mauvais filtrage des entrées utilisateur." },
            new Question { Difficulty=3, Text="Qu'est-ce qu'une API REST ?", Answers=new[]{"Un protocole de chiffrement","Une architecture pour échanger des données via HTTP","Un système d'exploitation","Un langage de programmation"}, CorrectIndex=1, Explanation="REST est un style d'architecture pour des API web basées sur HTTP." },
            new Question { Difficulty=3, Text="Que permet la commande 'git merge' ?", Answers=new[]{"Supprimer une branche","Fusionner deux branches","Créer un nouveau dépôt","Compresser un fichier"}, CorrectIndex=1, Explanation="git merge fusionne l'historique de deux branches." },
            new Question { Difficulty=3, Text="Sous Linux, que fait la commande 'chmod' ?", Answers=new[]{"Change le nom d'un fichier","Modifie les permissions d'un fichier","Compresse un fichier","Affiche le contenu d'un fichier"}, CorrectIndex=1, Explanation="chmod modifie les droits d'accès (lecture/écriture/exécution) d'un fichier." },
            new Question { Difficulty=3, Text="Qu'est-ce que la virtualisation ?", Answers=new[]{"Exécuter plusieurs systèmes sur un même matériel physique","Un type de virus","Un protocole réseau","Une technique de chiffrement"}, CorrectIndex=0, Explanation="La virtualisation permet de faire tourner plusieurs OS sur une même machine physique." },
        };
    }
}
