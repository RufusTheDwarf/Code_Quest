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
    const int BarWidth = 30;

    // Mémorise la dernière taille connue de la console pour ne re-clear
    // l'écran que lorsqu'elle change réellement (évite le scintillement
    // tout en gérant proprement un redimensionnement en cours de partie).
    static int lastKnownWidth = -1;
    static int lastKnownHeight = -1;

    static void Main()
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
        }
        catch { }

        try
        {
            if (Console.WindowWidth < 100) Console.WindowWidth = 100;
            if (Console.WindowHeight < 30) Console.WindowHeight = 30;
        }
        catch { }

        ShowIntro();
        int difficulty = ChooseDifficulty();
        int enemyHpBase = difficulty == 1 ? 3 : difficulty == 2 ? 4 : 5;

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
            new Enemy { Name = "Bug", Emoji = "🐛", MaxHp = enemyHpBase, Hp = enemyHpBase },
            new Enemy { Name = "Malware", Emoji = "🦠", MaxHp = enemyHpBase, Hp = enemyHpBase },
            new Enemy { Name = "Hacker", Emoji = "💻", MaxHp = enemyHpBase, Hp = enemyHpBase },
            new Enemy { Name = "Segmentation Fault", Emoji = "💥", MaxHp = enemyHpBase, Hp = enemyHpBase },
            new Enemy { Name = "The Compiler", Emoji = "👾", MaxHp = enemyHpBase + 2, Hp = enemyHpBase + 2 },
        };

        foreach (var enemy in enemies)
        {
            ShowEnemyIntro(enemy);

            while (enemy.Hp > 0 && playerHp > 0)
            {
                if (qIndex >= bank.Count)
                {
                    Shuffle(bank);
                    qIndex = 0;
                }

                Question q = bank[qIndex++];

                // Mélange aléatoire des réponses
                string[] shuffledAnswers = (string[])q.Answers.Clone();
                ShuffleArray(shuffledAnswers);
                int newCorrectIndex = Array.IndexOf(shuffledAnswers, q.Answers[q.CorrectIndex]);

                // Boucle de sélection avec les flèches
                int selected = 0;
                Console.Clear();
                while (true)
                {
                    DrawGameScreen(enemy, q.Text, shuffledAnswers, selected, playerHp, playerMaxHp, level, xp, null, ConsoleColor.White, -1);
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                        selected = (selected - 1 + shuffledAnswers.Length) % shuffledAnswers.Length;
                    else if (key == ConsoleKey.DownArrow)
                        selected = (selected + 1) % shuffledAnswers.Length;
                    else if (key == ConsoleKey.Enter)
                        break;
                }

                bool correct = (selected == newCorrectIndex);
                string correctAnswerText = shuffledAnswers[newCorrectIndex];

                string message;
                ConsoleColor msgColor;

                if (correct)
                {
                    correctCount++;
                    enemy.Hp--;
                    message = "✅  Bonne réponse !";
                    msgColor = ConsoleColor.Green;
                }
                else
                {
                    wrongCount++;
                    playerHp--;
                    message = $"❌  Mauvaise réponse !  →  {correctAnswerText}";
                    msgColor = ConsoleColor.Red;
                }

                // Écran de feedback
                Console.Clear();
                int lastY = DrawGameScreen(enemy, q.Text, shuffledAnswers,
                    correct ? newCorrectIndex : selected,
                    playerHp, playerMaxHp, level, xp, message, msgColor, newCorrectIndex);

                // Explication, ancrée juste sous le bloc centré (pas au bas
                // de la fenêtre) afin que tout reste centré ensemble.
                int explY = Math.Min(lastY + 2, Console.WindowHeight - 2);
                int continueY = Math.Min(lastY + 4, Console.WindowHeight - 1);
                CenterWrite(q.Explanation, explY, ConsoleColor.DarkGray);
                CenterWrite("Appuyez sur une touche pour continuer...", continueY, ConsoleColor.DarkGray);
                Console.ReadKey(true);

                // Vider le buffer clavier pour éviter les pressions résiduelles
                while (Console.KeyAvailable) Console.ReadKey(true);

                if (playerHp <= 0) break;

                if (enemy.Hp <= 0)
                {
                    xp += 20;
                    int newLevel = 1 + xp / 40;

                    if (newLevel > level)
                    {
                        int oldMaxHp = playerMaxHp;
                        level = newLevel;
                        playerMaxHp++;
                        playerHp = Math.Min(playerHp + 1, playerMaxHp);
                        ShowLevelUp(level, oldMaxHp, playerMaxHp);
                    }
                    else
                    {
                        ShowEnemyDefeated(enemy, 20);
                    }
                }
            }

            if (playerHp <= 0) break;
        }

        if (playerHp <= 0)
            ShowGameOver(level, correctCount, wrongCount);
        else
            ShowVictory(level, correctCount, wrongCount);

        try { Console.CursorVisible = true; } catch { }
    }

    // ------------------------------------------------------------------
    // HELPERS D'AFFICHAGE
    // ------------------------------------------------------------------

    static void WriteAt(string text, int x, int y, ConsoleColor fg, ConsoleColor? bg = null)
    {
        if (y < 0 || y >= Console.WindowHeight) return;
        if (x < 0) x = 0;
        try { Console.SetCursorPosition(x, y); } catch { return; }
        Console.ForegroundColor = fg;
        if (bg.HasValue) Console.BackgroundColor = bg.Value;
        Console.Write(text);
        Console.ResetColor();
    }

    static void CenterWrite(string text, int y, ConsoleColor fg = ConsoleColor.Gray, ConsoleColor? bg = null)
    {
        if (text == null) text = "";
        int w = Console.WindowWidth;
        int x = Math.Max(0, (w - text.Length) / 2);
        WriteAt(text, x, y, fg, bg);
    }

    static string MakeHpBar(int current, int max, int width)
    {
        if (max <= 0) max = 1;
        if (current < 0) current = 0;
        if (current > max) current = max;
        int filled = (int)Math.Round((double)current / max * width);
        return new string('█', filled) + new string('░', width - filled);
    }

    // ------------------------------------------------------------------
    // ÉCRAN DE JEU PRINCIPAL
    // ------------------------------------------------------------------

    static int DrawGameScreen(Enemy enemy, string questionText, string[] answers,
        int selected, int playerHp, int playerMaxHp, int level, int xp,
        string message, ConsoleColor messageColor, int correctAnswerIndex)
    {
        int w = Console.WindowWidth;
        int h = Console.WindowHeight;
        int sepWidth = Math.Min(w - 10, 70);

        // Si la fenêtre a été redimensionnée depuis la dernière frame, on
        // efface tout pour ne laisser aucun résidu de l'ancien centrage.
        if (w != lastKnownWidth || h != lastKnownHeight)
        {
            Console.Clear();
            lastKnownWidth = w;
            lastKnownHeight = h;
        }

        // Hauteur totale du bloc de jeu (en-tête → aide en bas), quel que
        // soit le nombre de réponses. Sert à le centrer verticalement.
        int blockHeight = 16 + answers.Length;
        int y = Math.Max(0, (h - blockHeight) / 2);

        // En-tête : niveau + XP
        CenterWrite($"⚔   Niveau {level}      XP : {xp}", y, ConsoleColor.Cyan);
        y += 2;

        // Ennemi
        CenterWrite($"{enemy.Emoji}   {enemy.Name}   {enemy.Emoji}", y, ConsoleColor.DarkYellow);
        y += 1;
        string eBar = MakeHpBar(Math.Max(enemy.Hp, 0), enemy.MaxHp, BarWidth);
        CenterWrite($"[{eBar}]   {Math.Max(enemy.Hp, 0)}/{enemy.MaxHp}", y, ConsoleColor.Red);
        y += 2;

        // Séparateur
        CenterWrite(new string('─', sepWidth), y, ConsoleColor.DarkGray);
        y += 2;

        // Question
        CenterWrite(questionText, y, ConsoleColor.White);
        y += 2;

        // Réponses
        int answersY = y;
        for (int i = 0; i < answers.Length; i++)
        {
            string line = $"  {i + 1}. {answers[i]}  ";
            string fullLine = "▶ " + line;

            if (i == correctAnswerIndex)
                CenterWrite(fullLine, answersY + i, ConsoleColor.Black, ConsoleColor.Green);
            else if (i == selected)
                CenterWrite(fullLine, answersY + i, ConsoleColor.Black, ConsoleColor.Cyan);
            else
                CenterWrite("  " + line, answersY + i, ConsoleColor.Gray);
        }
        y = answersY + answers.Length + 1;

        // Séparateur bas
        CenterWrite(new string('─', sepWidth), y, ConsoleColor.DarkGray);
        y += 2;

        // Joueur : barre de vie
        CenterWrite("VOUS", y, ConsoleColor.Green);
        y += 1;
        string pBar = MakeHpBar(playerHp, playerMaxHp, BarWidth);
        CenterWrite($"[{pBar}]   {playerHp}/{playerMaxHp}", y, ConsoleColor.Green);
        y += 2;

        // Message / aide — fait maintenant partie du bloc centré au lieu
        // d'être figé en bas de la fenêtre (h - 3).
        try { Console.SetCursorPosition(0, Math.Min(y, h - 1)); Console.Write(new string(' ', w)); } catch { }
        if (!string.IsNullOrEmpty(message))
            CenterWrite(message, y, messageColor);
        else
            CenterWrite("↑ ↓ pour choisir   •   Entrée pour valider", y, ConsoleColor.DarkGray);

        return y;
    }

    // ------------------------------------------------------------------
    // ÉCRANS DIVERS
    // ------------------------------------------------------------------

    static void ShowIntro()
    {
        Console.Clear();
        int h = Console.WindowHeight;
        CenterWrite("========================================", h / 2 - 6, ConsoleColor.Cyan);
        CenterWrite("⚔   CODE QUEST   ⚔", h / 2 - 4, ConsoleColor.Cyan);
        CenterWrite("LE RPG QUIZ INFORMATIQUE", h / 2 - 3, ConsoleColor.Cyan);
        CenterWrite("========================================", h / 2 - 2, ConsoleColor.Cyan);
        CenterWrite("Bienvenue dans Code Quest !", h / 2, ConsoleColor.White);
        CenterWrite("Réponds correctement aux questions d'informatique", h / 2 + 2, ConsoleColor.Gray);
        CenterWrite("pour vaincre tes ennemis.", h / 2 + 3, ConsoleColor.Gray);
        CenterWrite("Utilise les flèches ↑ ↓ pour choisir, Entrée pour valider.", h / 2 + 5, ConsoleColor.DarkGray);
        CenterWrite("Appuyez sur une touche pour commencer...", h - 2, ConsoleColor.DarkGray);
        Console.ReadKey(true);
    }

    static int ChooseDifficulty()
    {
        string[] options = { "1. Facile", "2. Normal", "3. Difficile" };
        int selected = 0;

        while (true)
        {
            Console.Clear();
            int h = Console.WindowHeight;
            CenterWrite("Choisis ta difficulté", h / 2 - 4, ConsoleColor.Cyan);

            for (int i = 0; i < options.Length; i++)
            {
                string line = "  " + options[i] + "  ";
                if (i == selected)
                    CenterWrite("▶ " + line, h / 2 + i, ConsoleColor.Black, ConsoleColor.Cyan);
                else
                    CenterWrite("  " + line, h / 2 + i, ConsoleColor.Gray);
            }

            CenterWrite("↑ ↓ pour choisir   •   Entrée pour valider", h - 2, ConsoleColor.DarkGray);
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                selected = (selected - 1 + options.Length) % options.Length;
            else if (key == ConsoleKey.DownArrow)
                selected = (selected + 1) % options.Length;
            else if (key == ConsoleKey.Enter)
                return selected + 1;
        }
    }

    static void ShowEnemyIntro(Enemy enemy)
    {
        Console.Clear();
        int h = Console.WindowHeight;
        CenterWrite(new string('─', 44), h / 2 - 3, ConsoleColor.DarkYellow);
        CenterWrite("Un ennemi apparaît !", h / 2 - 1, ConsoleColor.DarkYellow);
        CenterWrite($"{enemy.Emoji}   {enemy.Name}   {enemy.Emoji}", h / 2 + 1, ConsoleColor.DarkYellow);
        CenterWrite(new string('─', 44), h / 2 + 3, ConsoleColor.DarkYellow);
        CenterWrite("Appuyez sur une touche pour continuer...", h - 2, ConsoleColor.DarkGray);
        Console.ReadKey(true);
    }

    static void ShowLevelUp(int level, int oldMaxHp, int newMaxHp)
    {
        Console.Clear();
        int h = Console.WindowHeight;
        CenterWrite("⭐   LEVEL UP !   ⭐", h / 2 - 2, ConsoleColor.Magenta);
        CenterWrite($"Vous êtes maintenant niveau {level} !", h / 2, ConsoleColor.Magenta);
        CenterWrite($"PV maximum : {oldMaxHp} → {newMaxHp}", h / 2 + 2, ConsoleColor.Magenta);
        CenterWrite("Appuyez sur une touche pour continuer...", h - 2, ConsoleColor.DarkGray);
        Console.ReadKey(true);
        while (Console.KeyAvailable) Console.ReadKey(true);
    }

    static void ShowEnemyDefeated(Enemy enemy, int gainedXp)
    {
        Console.Clear();
        int h = Console.WindowHeight;
        CenterWrite($"{enemy.Name} VAINCU !", h / 2 - 2, ConsoleColor.Cyan);
        CenterWrite($"+{gainedXp} XP", h / 2, ConsoleColor.Cyan);
        CenterWrite("Appuyez sur une touche pour continuer...", h - 2, ConsoleColor.DarkGray);
        Console.ReadKey(true);
        while (Console.KeyAvailable) Console.ReadKey(true);
    }

    static void ShowVictory(int level, int correct, int wrong)
    {
        Console.Clear();
        int h = Console.WindowHeight;
        int total = correct + wrong;
        int score = total == 0 ? 0 : (int)Math.Round(100.0 * correct / total);

        CenterWrite("========================================", h / 2 - 6, ConsoleColor.Green);
        CenterWrite("VICTOIRE !", h / 2 - 4, ConsoleColor.Green);
        CenterWrite("========================================", h / 2 - 3, ConsoleColor.Green);
        CenterWrite("Vous avez vaincu le Boss !", h / 2 - 1, ConsoleColor.Green);
        CenterWrite($"Niveau : {level}", h / 2 + 1, ConsoleColor.White);
        CenterWrite($"Bonnes réponses : {correct}", h / 2 + 2, ConsoleColor.White);
        CenterWrite($"Mauvaises réponses : {wrong}", h / 2 + 3, ConsoleColor.White);
        CenterWrite($"Score : {score}%", h / 2 + 5, ConsoleColor.Yellow);

        string finalMsg = score < 50 ? "Il reste encore quelques bugs à corriger ! 🐛"
            : score < 80 ? "Pas mal ! Ton code compile presque ! 💻"
            : "Excellent ! Tu as clairement le niveau pour coder ! 🚀";
        CenterWrite(finalMsg, h - 3, ConsoleColor.Cyan);
        CenterWrite("Appuyez sur une touche pour quitter...", h - 1, ConsoleColor.DarkGray);
        Console.ReadKey(true);
    }

    static void ShowGameOver(int level, int correct, int wrong)
    {
        Console.Clear();
        int h = Console.WindowHeight;
        int total = correct + wrong;
        int score = total == 0 ? 0 : (int)Math.Round(100.0 * correct / total);

        CenterWrite("========================================", h / 2 - 6, ConsoleColor.Red);
        CenterWrite("GAME OVER", h / 2 - 4, ConsoleColor.Red);
        CenterWrite("========================================", h / 2 - 3, ConsoleColor.Red);
        CenterWrite("Vous avez été vaincu...", h / 2 - 1, ConsoleColor.Red);
        CenterWrite($"Niveau : {level}", h / 2 + 1, ConsoleColor.White);
        CenterWrite($"Bonnes réponses : {correct}", h / 2 + 2, ConsoleColor.White);
        CenterWrite($"Mauvaises réponses : {wrong}", h / 2 + 3, ConsoleColor.White);
        CenterWrite($"Score : {score}%", h / 2 + 5, ConsoleColor.Yellow);
        CenterWrite("Appuyez sur une touche pour quitter...", h - 1, ConsoleColor.DarkGray);
        Console.ReadKey(true);
    }

    // ------------------------------------------------------------------
    // LOGIQUE
    // ------------------------------------------------------------------

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

    static void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (array[k], array[n]) = (array[n], array[k]);
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
            new Question { Difficulty=3, Text="Qu'est-ce que la virtualisation ?", Answers=new[]{"Exécuter plusieurs systèmes sur un même matériel physique","Un type de virus","Un protocole réseau","Une technique de chiffrement"}, CorrectIndex=0, Explanation="La virtualisation permet de faire tourner plusieurs OS sur une même machine." },
        };
    }
}