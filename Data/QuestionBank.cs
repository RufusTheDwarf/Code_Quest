using CodeQuest.Models;

namespace CodeQuest.Data;

public static class QuestionBank
{
    public static List<Question> Build()
    {
        return new List<Question>
        {
            // ── FACILE ──────────────────────────────────────────────
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Que signifie CPU ?",
                Answers = new[] { "Central Processing Unit", "Computer Personal User", "Central Program Utility", "Computer Processing User" },
                CorrectIndex = 0,
                Explanation = "Le CPU (Central Processing Unit) est le processeur, le cerveau de l'ordinateur. Il exécute les instructions des programmes.",
                Hint = "CPU = le \"cerveau\" qui exécute les instructions."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "À quoi sert une souris ?",
                Answers = new[] { "À imprimer des documents", "À naviguer et cliquer à l'écran", "À stocker des fichiers", "À se connecter à Internet" },
                CorrectIndex = 1,
                Explanation = "La souris est un périphérique de pointage qui permet de déplacer un curseur et de cliquer sur des éléments à l'écran.",
                Hint = "Souris = pointer, cliquer, glisser."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Qu'est-ce qu'un navigateur web ?",
                Answers = new[] { "Un logiciel pour écrire du code", "Un logiciel pour naviguer sur Internet", "Un antivirus", "Un système d'exploitation" },
                CorrectIndex = 1,
                Explanation = "Un navigateur web (Chrome, Firefox, Edge) est un logiciel qui permet de consulter des pages web sur Internet.",
                Hint = "Navigateur = fenêtre sur le web."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Combien de bits contient un octet ?",
                Answers = new[] { "4", "8", "16", "10" },
                CorrectIndex = 1,
                Explanation = "Un octet (byte) est composé de 8 bits. C'est l'unité de base pour mesurer les données.",
                Hint = "1 octet = 8 bits. Pense à \"octo\" = 8."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Que signifie Wi-Fi ?",
                Answers = new[] { "Wireless Fidelity", "Wired Fidelity", "Wireless File", "Web Fidelity" },
                CorrectIndex = 0,
                Explanation = "Wi-Fi signifie \"Wireless Fidelity\". C'est une technologie de réseau local sans fil.",
                Hint = "Wi-Fi = sans fil (wireless)."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Quel composant sert à afficher une image ?",
                Answers = new[] { "Le disque dur", "La carte graphique", "Le clavier", "La RAM" },
                CorrectIndex = 1,
                Explanation = "La carte graphique (GPU) traite les données graphiques et les envoie à l'écran pour affichage.",
                Hint = "GPU = Graphics Processing Unit."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Qu'est-ce qu'un fichier ?",
                Answers = new[] { "Un dossier physique", "Un ensemble de données stocké sous un nom", "Un virus", "Un type de câble" },
                CorrectIndex = 1,
                Explanation = "Un fichier est un ensemble de données (texte, image, programme...) identifié par un nom et une extension.",
                Hint = "Fichier = données + nom."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Quel appareil permet de stocker des fichiers ?",
                Answers = new[] { "Le clavier", "La souris", "Le disque dur", "L'écran" },
                CorrectIndex = 2,
                Explanation = "Le disque dur (ou SSD) est un périphérique de stockage qui conserve les données même lorsque l'ordinateur est éteint.",
                Hint = "Disque dur = mémoire permanente."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Que signifie USB ?",
                Answers = new[] { "Universal Serial Bus", "United System Board", "Universal System Bus", "Unified Serial Board" },
                CorrectIndex = 0,
                Explanation = "USB signifie \"Universal Serial Bus\". C'est un standard de connexion pour de nombreux périphériques.",
                Hint = "USB = Universal Serial Bus."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Qu'est-ce qu'un système d'exploitation ?",
                Answers = new[] { "Un jeu vidéo", "Un logiciel qui gère le matériel et les programmes", "Un langage de programmation", "Un type de câble" },
                CorrectIndex = 1,
                Explanation = "Le système d'exploitation (Windows, macOS, Linux) fait le lien entre le matériel et les logiciels.",
                Hint = "OS = chef d'orchestre de la machine."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Que signifie RAM ?",
                Answers = new[] { "Random Access Memory", "Read Access Memory", "Rapid Access Memory", "Random Application Memory" },
                CorrectIndex = 0,
                Explanation = "RAM signifie \"Random Access Memory\". C'est la mémoire vive, rapide mais temporaire.",
                Hint = "RAM = mémoire de travail."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Quel raccourci permet de copier un texte sous Windows ?",
                Answers = new[] { "Ctrl+V", "Ctrl+C", "Ctrl+X", "Ctrl+Z" },
                CorrectIndex = 1,
                Explanation = "Ctrl+C copie, Ctrl+V colle, Ctrl+X coupe et Ctrl+Z annule.",
                Hint = "C = Copy, V = Coller (penser \"V\" comme \"Valider\")."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Qu'est-ce qu'un mot de passe fort ?",
                Answers = new[] { "Un mot simple facile à retenir", "Une combinaison longue de lettres, chiffres et symboles", "Le prénom de l'utilisateur", "Une suite de chiffres identiques" },
                CorrectIndex = 1,
                Explanation = "Un mot de passe fort est long et varié (majuscules, minuscules, chiffres, symboles). Il est difficile à deviner.",
                Hint = "Long + varié = fort."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Quel type de fichier est une image ?",
                Answers = new[] { ".exe", ".jpg", ".mp3", ".txt" },
                CorrectIndex = 1,
                Explanation = ".jpg est un format d'image très courant. .exe est un programme, .mp3 un son, .txt un texte.",
                Hint = "JPG = image."
            },
            new Question
            {
                Difficulty = Difficulty.Easy,
                Text = "Qu'est-ce que le cloud ?",
                Answers = new[] { "Un logiciel antivirus", "Un espace de stockage en ligne", "Un type de processeur", "Un langage de programmation" },
                CorrectIndex = 1,
                Explanation = "Le cloud (nuage) désigne des services de stockage et de calcul accessibles via Internet, sur des serveurs distants.",
                Hint = "Cloud = données dans les nuages (serveurs distants)."
            },

            // ── NORMAL ─────────────────────────────────────────────
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Quelle est la différence entre RAM et stockage ?",
                Answers = new[] { "La RAM est permanente, le stockage est temporaire", "La RAM est temporaire et rapide, le stockage est permanent", "Les deux sont identiques", "La RAM sert à imprimer" },
                CorrectIndex = 1,
                Explanation = "La RAM est une mémoire volatile (temporaire) et très rapide. Le stockage (disque dur/SSD) est non-volatile (permanent).",
                Hint = "RAM = rapide mais oublie tout. Disque = lent mais garde tout."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Quel est le rôle principal du processeur (CPU) ?",
                Answers = new[] { "Stocker les fichiers", "Exécuter les instructions et calculs", "Afficher l'image", "Se connecter au Wi-Fi" },
                CorrectIndex = 1,
                Explanation = "Le CPU exécute les instructions des programmes : calculs, comparaisons, déplacements de données.",
                Hint = "CPU = exécuteur d'instructions."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce qu'une adresse IP ?",
                Answers = new[] { "Un mot de passe", "Un identifiant unique d'un appareil sur un réseau", "Un type de virus", "Un nom de domaine" },
                CorrectIndex = 1,
                Explanation = "Une adresse IP identifie de façon unique un appareil sur un réseau. C'est comme une adresse postale numérique.",
                Hint = "IP = identifiant réseau."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "À quoi sert le DNS ?",
                Answers = new[] { "Chiffrer les données", "Traduire les noms de domaine en adresses IP", "Compresser les fichiers", "Gérer la mémoire" },
                CorrectIndex = 1,
                Explanation = "Le DNS (Domain Name System) traduit les noms de domaine (ex: google.com) en adresses IP compréhensibles par les machines.",
                Hint = "DNS = annuaire du web."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Quelle est la différence entre HTTP et HTTPS ?",
                Answers = new[] { "Aucune différence", "HTTPS est chiffré et sécurisé", "HTTP est plus rapide", "HTTPS est plus ancien" },
                CorrectIndex = 1,
                Explanation = "HTTPS est la version sécurisée de HTTP. Le S signifie \"Secure\" : les données sont chiffrées via TLS.",
                Hint = "S de HTTPS = Secure."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "À quoi sert HTML dans le développement web ?",
                Answers = new[] { "À styliser les pages", "À structurer le contenu des pages", "À gérer les bases de données", "À sécuriser le site" },
                CorrectIndex = 1,
                Explanation = "HTML (HyperText Markup Language) définit la structure et le contenu d'une page web : titres, paragraphes, liens, images.",
                Hint = "HTML = structure."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "À quoi sert CSS ?",
                Answers = new[] { "Structurer le contenu", "Ajouter de l'interactivité", "Mettre en forme et styliser les pages", "Gérer le serveur" },
                CorrectIndex = 2,
                Explanation = "CSS (Cascading Style Sheets) gère l'apparence visuelle : couleurs, polices, marges, disposition.",
                Hint = "CSS = style."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "À quoi sert JavaScript principalement ?",
                Answers = new[] { "Styliser les pages", "Rendre les pages web interactives", "Stocker les données sur disque", "Chiffrer les mots de passe" },
                CorrectIndex = 1,
                Explanation = "JavaScript ajoute du comportement et de l'interactivité aux pages web : clics, animations, requêtes réseau.",
                Hint = "JS = interactivité."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce que Git ?",
                Answers = new[] { "Un langage de programmation", "Un système de gestion de versions", "Un navigateur web", "Un antivirus" },
                CorrectIndex = 1,
                Explanation = "Git est un système de gestion de versions décentralisé. Il permet de suivre l'historique des modifications du code.",
                Hint = "Git = historique du code."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce qu'une base de données ?",
                Answers = new[] { "Un logiciel de dessin", "Un système organisé pour stocker et gérer des données", "Un type de processeur", "Un protocole réseau" },
                CorrectIndex = 1,
                Explanation = "Une base de données organise et stocke des informations de façon structurée, avec des mécanismes de recherche et de mise à jour.",
                Hint = "BDD = données organisées."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce qu'un algorithme ?",
                Answers = new[] { "Un virus informatique", "Une suite d'instructions pour résoudre un problème", "Un type de mémoire", "Un langage de programmation" },
                CorrectIndex = 1,
                Explanation = "Un algorithme est une suite finie et ordonnée d'opérations permettant de résoudre un problème.",
                Hint = "Algo = recette de cuisine pour un problème."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Quel est le rôle d'un système d'exploitation ?",
                Answers = new[] { "Gérer le matériel et exécuter les logiciels", "Naviguer sur Internet uniquement", "Stocker des mots de passe", "Afficher des images" },
                CorrectIndex = 0,
                Explanation = "L'OS gère les ressources matérielles (CPU, mémoire, disques) et fournit des services aux logiciels.",
                Hint = "OS = gestionnaire de ressources."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Quel terme désigne un réseau d'ordinateurs interconnectés ?",
                Answers = new[] { "Un cluster", "Un réseau informatique", "Une base de données", "Un algorithme" },
                CorrectIndex = 1,
                Explanation = "Un réseau informatique relie plusieurs appareils entre eux pour échanger des données.",
                Hint = "Réseau = plusieurs machines connectées."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce qu'un compilateur ?",
                Answers = new[] { "Un logiciel qui traduit du code source en code exécutable", "Un antivirus", "Un type de mémoire", "Un protocole réseau" },
                CorrectIndex = 0,
                Explanation = "Le compilateur transforme le code source écrit par le programmeur en code machine exécutable par le processeur.",
                Hint = "Compilateur = traducteur code → machine."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce qu'une machine virtuelle ?",
                Answers = new[] { "Un ordinateur physique supplémentaire", "Un environnement simulé qui exécute un système comme un ordinateur réel", "Un type de virus", "Un langage de programmation" },
                CorrectIndex = 1,
                Explanation = "Une machine virtuelle (VM) simule un ordinateur complet dans un logiciel. Elle peut exécuter un OS comme si c'était du matériel réel.",
                Hint = "VM = ordinateur dans un ordinateur."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce qu'un terminal en informatique ?",
                Answers = new[] { "Une interface en ligne de commande", "Un écran tactile", "Un type de câble", "Un antivirus" },
                CorrectIndex = 0,
                Explanation = "Le terminal est une interface textuelle qui permet d'exécuter des commandes et de lancer des programmes.",
                Hint = "Terminal = ligne de commande."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Que représente l'extension .py d'un fichier ?",
                Answers = new[] { "Un fichier Python", "Un fichier Java", "Un fichier texte", "Un fichier image" },
                CorrectIndex = 0,
                Explanation = ".py est l'extension des scripts Python. Python est un langage de programmation populaire.",
                Hint = ".py = Python."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce que la programmation ?",
                Answers = new[] { "Écrire des instructions pour qu'un ordinateur les exécute", "Naviguer sur Internet", "Installer un antivirus", "Formater un disque" },
                CorrectIndex = 0,
                Explanation = "Programmer, c'est écrire des instructions dans un langage compréhensible par la machine pour qu'elle accomplisse des tâches.",
                Hint = "Programmer = donner des ordres à la machine."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Que signifie 'réseau local' (LAN) ?",
                Answers = new[] { "Un réseau mondial", "Un réseau limité à une zone géographique restreinte", "Un type de virus", "Un protocole de chiffrement" },
                CorrectIndex = 1,
                Explanation = "Un LAN (Local Area Network) relie des appareils proches, comme dans un bâtiment ou un campus.",
                Hint = "LAN = Local Area Network."
            },
            new Question
            {
                Difficulty = Difficulty.Normal,
                Text = "Qu'est-ce qu'un mot de passe haché (hash) ?",
                Answers = new[] { "Un mot de passe en clair", "Une version transformée et irréversible d'un mot de passe", "Un mot de passe partagé", "Un type de virus" },
                CorrectIndex = 1,
                Explanation = "Le hachage transforme un mot de passe en une empreinte irréversible. On ne stocke jamais le mot de passe en clair.",
                Hint = "Hash = empreinte irréversible."
            },

            // ── DIFFICILE ──────────────────────────────────────────
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Quelle structure de données fonctionne en LIFO ?",
                Answers = new[] { "File (Queue)", "Pile (Stack)", "Liste chaînée", "Arbre binaire" },
                CorrectIndex = 1,
                Explanation = "La pile (Stack) suit le principe LIFO (Last In, First Out) : le dernier élément ajouté est le premier retiré.",
                Hint = "Stack = pile d'assiettes : la dernière posée est la première enlevée."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Que mesure la complexité algorithmique O(n) ?",
                Answers = new[] { "Un temps constant", "Un temps proportionnel à la taille des données", "Un temps exponentiel", "Un temps logarithmique" },
                CorrectIndex = 1,
                Explanation = "O(n) signifie que le temps d'exécution croît linéairement avec la taille des données d'entrée.",
                Hint = "O(n) = temps linéaire."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Que signifie POO (programmation orientée objet) ?",
                Answers = new[] { "Un paradigme basé sur les objets et les classes", "Un langage de bas niveau", "Un protocole réseau", "Un type de base de données" },
                CorrectIndex = 0,
                Explanation = "La POO organise le code autour de classes et d'objets qui encapsulent données et comportements.",
                Hint = "POO = objets + classes."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Qu'est-ce que la récursivité ?",
                Answers = new[] { "Une boucle infinie", "Une fonction qui s'appelle elle-même", "Un type de variable", "Un protocole réseau" },
                CorrectIndex = 1,
                Explanation = "Une fonction récursive s'appelle elle-même pour résoudre un sous-problème, avec une condition d'arrêt.",
                Hint = "Récursif = se mordre la queue."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "À quoi sert un pointeur en programmation ?",
                Answers = new[] { "À stocker une valeur directement", "À référencer une adresse mémoire", "À chiffrer des données", "À afficher du texte" },
                CorrectIndex = 1,
                Explanation = "Un pointeur contient l'adresse mémoire d'une donnée. Il permet d'accéder indirectement à cette donnée.",
                Hint = "Pointeur = adresse mémoire."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Quelle est la différence entre un processus et un thread ?",
                Answers = new[] { "Ils sont identiques", "Un processus est indépendant, un thread partage la mémoire de son processus", "Un thread est plus lent qu'un processus", "Un processus ne peut avoir qu'un seul thread" },
                CorrectIndex = 1,
                Explanation = "Un processus a son propre espace mémoire. Les threads d'un même processus partagent cet espace, ce qui les rend plus légers.",
                Hint = "Thread = fil d'exécution dans un processus."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Quelle est la différence principale entre TCP et UDP ?",
                Answers = new[] { "TCP est fiable et orienté connexion, UDP est plus rapide mais non fiable", "UDP est toujours plus lent que TCP", "Ils sont identiques", "TCP ne fonctionne pas sur Internet" },
                CorrectIndex = 0,
                Explanation = "TCP garantit la livraison des paquets (accusés de réception). UDP privilégie la vitesse, sans garantie de livraison.",
                Hint = "TCP = fiable. UDP = rapide."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Combien de couches possède le modèle OSI ?",
                Answers = new[] { "5", "7", "4", "9" },
                CorrectIndex = 1,
                Explanation = "Le modèle OSI comporte 7 couches : physique, liaison, réseau, transport, session, présentation, application.",
                Hint = "OSI = 7 couches."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Qu'est-ce qu'un sous-réseau (subnet) ?",
                Answers = new[] { "Une division logique d'un réseau IP", "Un type de câble", "Un protocole de chiffrement", "Un antivirus réseau" },
                CorrectIndex = 0,
                Explanation = "Un subnet divise un réseau IP en sous-parties plus petites pour organiser et sécuriser les communications.",
                Hint = "Subnet = sous-réseau."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Qu'est-ce que le chiffrement asymétrique ?",
                Answers = new[] { "Utilise la même clé pour chiffrer et déchiffrer", "Utilise une paire de clés publique/privée", "N'utilise aucune clé", "Est réservé aux mots de passe" },
                CorrectIndex = 1,
                Explanation = "Le chiffrement asymétrique utilise une clé publique pour chiffrer et une clé privée pour déchiffrer (ou inversement).",
                Hint = "Asymétrique = 2 clés."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Qu'est-ce qu'une injection SQL ?",
                Answers = new[] { "Une optimisation de base de données", "Une attaque exploitant une requête SQL mal filtrée", "Un type de sauvegarde", "Un protocole réseau" },
                CorrectIndex = 1,
                Explanation = "Une injection SQL exploite un mauvais filtrage des entrées utilisateur pour exécuter des requêtes malveillantes.",
                Hint = "Injection SQL = entrée non filtrée."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Qu'est-ce qu'une API REST ?",
                Answers = new[] { "Un protocole de chiffrement", "Une architecture pour échanger des données via HTTP", "Un système d'exploitation", "Un langage de programmation" },
                CorrectIndex = 1,
                Explanation = "REST est un style d'architecture pour des API web basées sur HTTP, utilisant des ressources et des verbes standard.",
                Hint = "REST = API via HTTP."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Que permet la commande 'git merge' ?",
                Answers = new[] { "Supprimer une branche", "Fusionner deux branches", "Créer un nouveau dépôt", "Compresser un fichier" },
                CorrectIndex = 1,
                Explanation = "git merge fusionne l'historique de deux branches en une seule, combinant les modifications.",
                Hint = "merge = fusionner."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Sous Linux, que fait la commande 'chmod' ?",
                Answers = new[] { "Change le nom d'un fichier", "Modifie les permissions d'un fichier", "Compresse un fichier", "Affiche le contenu d'un fichier" },
                CorrectIndex = 1,
                Explanation = "chmod (change mode) modifie les droits d'accès (lecture/écriture/exécution) d'un fichier ou dossier.",
                Hint = "chmod = change mode."
            },
            new Question
            {
                Difficulty = Difficulty.Hard,
                Text = "Qu'est-ce que la virtualisation ?",
                Answers = new[] { "Exécuter plusieurs systèmes sur un même matériel physique", "Un type de virus", "Un protocole réseau", "Une technique de chiffrement" },
                CorrectIndex = 0,
                Explanation = "La virtualisation permet de faire tourner plusieurs OS ou environnements sur une même machine physique.",
                Hint = "Virtualisation = plusieurs OS sur une machine."
            }
        };
    }
}