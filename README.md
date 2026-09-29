<div align="center">

# 🎮 Code Quest

### Le RPG qui teste ta culture informatique

![Release](https://img.shields.io/github/v/release/RufusTheDwarf/Code_Quest?style=flat-square&label=Release&color=blueviolet&logo=github) ![Plateforme](https://img.shields.io/badge/Windows-10%20%7C%2011-0078D6?style=flat-square&logo=windows11&logoColor=white) ![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet&logoColor=white) ![Licence](https://img.shields.io/badge/Licence-MIT-3DA639?style=flat-square&logo=opensourceinitiative&logoColor=white) ![Téléchargements](https://img.shields.io/github/downloads/RufusTheDwarf/Code_Quest/latest/total?style=flat-square&label=T%C3%A9l%C3%A9chargements&color=brightgreen&logo=github)

</div>

---

<div align="center">

> **Le quiz qui transforme tes connaissances en arme.**
> Un RPG console où tu affrontes des bugs, virus et hackers en répondant à des questions d'informatique.

</div>

---

## 📖 Table des matières

- [🎯 Concept](#-concept)
- [✨ Fonctionnalités](#-fonctionnalités)
- [🕹️ Comment jouer](#️-comment-jouer)
- [📥 Installation rapide](#-installation-rapide)
- [🛠️ Installation pour les développeurs](#️-installation-pour-les-développeurs)
- [📁 Structure du projet](#-structure-du-projet)
- [🎓 Ce que tu vas apprendre](#-ce-que-tu-vas-apprendre)
- [🗺️ Feuille de route](#️-feuille-de-route)
- [🤝 Contribution](#-contribution)
- [📜 Licence](#-licence)
- [🙏 Remerciements](#-remerciements)

---

## 🎯 Concept

**Code Quest** est un jeu de rôle (RPG) en console où tu incarnes un développeur affrontant des ennemis informatiques. Chaque ennemi représente un concept ou un problème du monde du code : un **Bug**, un **Malware**, un **Hacker**, une **Segmentation Fault**… et même le boss final, **The Compiler**.

Pour vaincre tes ennemis, tu dois répondre correctement à des questions d'informatique. Chaque bonne réponse leur inflige des dégâts. Chaque mauvaise réponse te coûte des points de vie. Gagne de l'XP, monte de niveau, et deviens le maître du code !

Le projet est développé en **C# / .NET 8** et fonctionne entièrement dans le terminal. Il est conçu pour être simple à lancer, amusant à jouer, et pédagogique sans en avoir l'air.

---

## ✨ Fonctionnalités

| Fonctionnalité | Description |
|:---|:---|
| 🎮 **Interface de jeu complète** | Écran centré, barres de vie, ennemis au milieu de la console. Une vraie expérience de jeu, pas un simple quiz. |
| ⬆️ **Navigation aux flèches** | Choisis tes réponses avec ↑ ↓ et valide avec Entrée. Pas besoin d'écrire quoi que ce soit. |
| 🔀 **Réponses mélangées** | Les bonnes réponses changent de position à chaque partie. Impossible d'apprendre les réponses par cœur. |
| 🐛 **5 ennemis uniques** | Bug, Malware, Hacker, Segmentation Fault, et le boss final *The Compiler*. Chacun a sa propre barre de vie. |
| ⭐ **Système de niveaux et d'XP** | Gagne 20 XP par ennemi vaincu. Monte de niveau tous les 40 XP et augmente tes PV maximum. |
| 🎯 **3 niveaux de difficulté** | Facile, Normal, Difficile. Chaque niveau débloque de nouvelles questions et des ennemis plus coriaces. |
| 📚 **Plus de 50 questions** | Couvrant le matériel, les réseaux, la programmation, la cybersécurité et les outils de développement. |
| 🧠 **Explications pédagogiques** | Après chaque réponse, une explication t'aide à comprendre la bonne réponse. |
| 💚 **Exécutable autonome** | Le jeu est compilé en un seul fichier `.exe` qui fonctionne sans aucune installation. |
| 🪟 **Compatible Windows 10 / 11** | Testé et fonctionnel sur les versions 64 bits de Windows. |

---

## 🕹️ Comment jouer

Voici à quoi ressemble le jeu en cours de partie :

```
┌─────────────────────────────────────────────────────┐
│                                                     │
│   🐛   Bug   🐛                                     │
│   [████████░░░░░░░░]   2/3                          │
│                                                     │
│   ─────────────────────────────────────────────     │
│                                                     │
│   Que signifie CPU ?                                │
│                                                     │
│   ▶ 1. Central Processing Unit                      │
│     2. Computer Personal User                       │
│     3. Central Program Utility                      │
│                                                     │
│   ─────────────────────────────────────────────     │
│                                                     │
│   VOUS                                              │
│   [████████████████]   5/5                          │
│                                                     │
│   ↑ ↓ pour choisir  •  Entrée pour valider          │
│                                                     │
└─────────────────────────────────────────────────────┘
```

### 🎮 Commandes

| Touche | Action |
|:---:|:---|
| **↑ ↓** | Choisir une réponse |
| **Entrée** | Valider la réponse sélectionnée |

### 🧠 Règles du jeu

- **Bonne réponse** → L'ennemi perd 1 PV, tu gagnes 20 XP.
- **Mauvaise réponse** → Tu perds 1 PV.
- **Ennemi vaincu** → +20 XP. Si tu passes un palier d'XP, tu montes de niveau et tes PV max augmentent.
- **Tous les ennemis vaincus** → Victoire ! 🎉

---

## 📥 Installation rapide

**Pour les joueurs : la méthode la plus simple.**

1. Va dans la section **[Releases](https://github.com/RufusTheDwarf/Code_Quest/releases)** de ce dépôt.
2. Télécharge le fichier **`CodeQuest.exe`** (≈ 67 Mo) dans la section **Assets** de la dernière version.
3. Double-clique sur le fichier téléchargé.

C'est tout. Le jeu se lance immédiatement.

> 💡 **Aucun prérequis.** Ni .NET, ni Git, ni Visual Studio. Le fichier `.exe` est autonome et contient tout ce qu'il faut pour fonctionner.

---

## 🛠️ Installation pour les développeurs

**Pour ceux qui veulent modifier le code, compiler le projet ou contribuer.**

### Prérequis

- **.NET 8 SDK** ou version ultérieure // [Télécharger ici](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Git** // [Télécharger ici](https://git-scm.com/downloads)
- Un terminal (Git Bash, PowerShell, Windows Terminal, etc.)

### Étapes

```bash
# 1. Cloner le dépôt
git clone https://github.com/RufusTheDwarf/Code_Quest.git

# 2. Entrer dans le dossier
cd Code_Quest

# 3. Lancer le jeu en mode développement
dotnet run

# 4. OU compiler une version Release
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

L'exécutable compilé se trouve dans :
```
bin/Release/net8.0/win-x64/publish/CodeQuest.exe
```

### Lanceur automatique

Un fichier **`JOUER.bat`** est inclus à la racine du projet. Il effectue automatiquement :

1. La vérification et l'installation locale de .NET (dans `.dotnet/`).
2. La compilation du jeu.
3. La création d'un raccourci **"Code Quest"** sur le Bureau.
4. Le lancement du jeu.

Double-clique simplement sur `JOUER.bat` pour tout faire en une seule fois.

---

## 📁 Structure du projet

```
Code_Quest/
│
├── Program.cs              # Code source principal du jeu
├── CodeQuest.csproj        # Fichier de configuration du projet .NET
├── JOUER.bat               # Lanceur automatique pour développeurs
├── .gitignore              # Fichiers exclus du suivi Git
├── README.md               # Ce fichier
│
├── .dotnet/                # SDK .NET portable (généré par JOUER.bat, ignoré par Git)
│
└── bin/                    # Fichiers compilés (généré, ignoré par Git)
    └── Release/
        └── net8.0/
            └── win-x64/
                └── publish/
                    └── CodeQuest.exe   # Exécutable final
```

---

## 🎓 Ce que tu vas apprendre

En jouant à Code Quest, tu révises (ou découvres) des notions essentielles en informatique :

| Thème | Exemples de questions |
|:---|:---|
| 🖥️ **Matériel** | CPU, RAM, carte graphique, disque dur, USB, Wi-Fi… |
| 🌐 **Réseaux** | Adresse IP, DNS, HTTP/HTTPS, TCP/UDP, modèle OSI, LAN… |
| 💻 **Programmation** | Algorithmes, récursivité, POO, complexité O(n), pointeurs… |
| 🔒 **Cybersécurité** | Chiffrement asymétrique, injection SQL, hachage, mots de passe… |
| 🛠️ **Outils de développement** | Git, terminaux, API REST, machines virtuelles, compilateurs… |

---

## 🗺️ Feuille de route

Voici ce qui est prévu pour les prochaines versions :

- [x] Interface de jeu centrée avec barres de vie
- [x] Navigation aux flèches pour choisir les réponses
- [x] Mélange aléatoire des réponses
- [x] Système de niveaux et d'XP
- [x] 3 niveaux de difficulté
- [x] Plus de 50 questions
- [ ] Mode multijoueur local (2 joueurs sur le même clavier)
- [ ] Système de sauvegarde de la progression
- [ ] Nouvelles catégories de questions (bases de données, cloud, IA)
- [ ] Sprites ASCII améliorés pour les ennemis
- [ ] Effets sonores et musique de fond
- [ ] Support de Linux et macOS

> 💡 Une idée ? Ouvre une **[issue](https://github.com/RufusTheDwarf/Code_Quest/issues)** pour la proposer !

---

## 🤝 Contribution

Les contributions sont les bienvenues ! Voici comment participer :

1. **Fork** le dépôt.
2. Crée une branche pour ta fonctionnalité :
   ```bash
   git checkout -b feature/ma-super-fonctionnalite
   ```
3. Fais tes modifications et commit :
   ```bash
   git commit -m "Ajout de ma super fonctionnalité"
   ```
4. Pousse ta branche :
   ```bash
   git push origin feature/ma-super-fonctionnalite
   ```
5. Ouvre une **Pull Request** sur GitHub.

### 💡 Idées de contribution

- Ajouter de nouvelles questions au jeu.
- Améliorer l'interface console.
- Corriger des bugs.
- Traduire le jeu dans d'autres langues.
- Écrire de la documentation.

---

## 📜 Licence

Ce projet est distribué sous licence **MIT**. Tu es libre de l'utiliser, de le modifier et de le distribuer, tant que tu conserves la mention de copyright originale.

Voir le fichier [`LICENSE`](LICENSE) pour plus de détails.

---

## 🙏 Remerciements

Merci à toutes les personnes qui testeront ce jeu, qui donneront leur avis, et qui contribueront à l'améliorer.

Un merci tout particulier à la communauté **.NET** pour les outils et la documentation qui ont rendu ce projet possible.

---

<div align="center">

**Fait avec ❤️ et beaucoup de café**

*"Talk is cheap. Show me the code."* — Linus Torvalds

<br>

⭐ **Si tu aimes ce projet, mets une étoile sur le dépôt !** ⭐

<br>

![Visiteurs](https://visitor-badge.laobi.icu/badge?page_id=RufusTheDwarf.Code_Quest)

</div>
