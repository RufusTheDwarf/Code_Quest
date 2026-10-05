<div align="center">

# 🎮 Code Quest

**The RPG that tests your computer science knowledge.**

*A console RPG where you fight bugs, viruses, and hackers by answering computer science questions.*

[![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=csharp&logoColor=white)](#)
[![.NET 8](https://img.shields.io/badge/.NET-8-512BD4?style=flat&logo=dotnet&logoColor=white)](#)
[![Platform](https://img.shields.io/badge/Platform-Windows%2010%20%7C%2011-0078D6?style=flat&logo=windows&logoColor=white)](#)
[![License](https://img.shields.io/badge/License-MIT-lightgrey?style=flat)](#)

<br>

[![Stars](https://img.shields.io/github/stars/RufusTheDwarf/Code_Quest?style=flat&color=yellow)](https://github.com/RufusTheDwarf/Code_Quest/stargazers)
[![Forks](https://img.shields.io/github/forks/RufusTheDwarf/Code_Quest?style=flat&color=blue)](https://github.com/RufusTheDwarf/Code_Quest/forks)
[![Issues](https://img.shields.io/github/issues/RufusTheDwarf/Code_Quest?style=flat&color=red)](https://github.com/RufusTheDwarf/Code_Quest/issues)
[![Last Commit](https://img.shields.io/github/last-commit/RufusTheDwarf/Code_Quest?style=flat&color=orange)](https://github.com/RufusTheDwarf/Code_Quest/commits/main)

</div>

---

## 📖 Concept

**Code Quest** is a console role-playing game (RPG) where you play as a developer fighting computer science enemies. Each enemy represents a concept or problem from the world of code: a **Bug**, a **Malware**, a **Hacker**, a **Segmentation Fault**, and even the final boss, **The Compiler**.

To defeat your enemies, you must correctly answer computer science questions. Each correct answer deals damage. Each wrong answer costs you health points. Gain XP, level up, and become the master of code.

The project is built in **C# / .NET 8** and runs entirely in the terminal. It is designed to be easy to launch, fun to play, and educational without feeling like a quiz.

---

## ✨ Features

| Feature | Description |
|---|---|
| **Full game interface** | Centered screen, health bars, enemies in the middle of the console. A real game experience, not just a quiz. |
| **Arrow key navigation** | Choose answers with ↑ ↓ and confirm with Enter. No typing required. |
| **Shuffled answers** | Correct answers change position every game. Impossible to memorize. |
| **5 unique enemies** | Bug, Malware, Hacker, Segmentation Fault, and the final boss, The Compiler. Each has its own health bar. |
| **Level and XP system** | Gain 20 XP per defeated enemy. Level up every 40 XP and increase your max HP. |
| **3 difficulty levels** | Easy, Normal, Hard. Each level unlocks new questions and tougher enemies. |
| **50+ questions** | Covering hardware, networking, programming, cybersecurity, and developer tools. |
| **Educational explanations** | After each answer, an explanation helps you understand the correct choice. |
| **Standalone executable** | The game is compiled into a single `.exe` file that runs without any installation. |
| **Windows 10 / 11 compatible** | Tested and working on 64-bit versions of Windows. |

---

## 🎮 How to Play

Here is what the game looks like during a session:

```
┌─────────────────────────────────────────────────────┐
│                                                     │
│  Bug                                                │
│  [████████░░░░░░░░] 2/3                             │
│                                                     │
│  ─────────────────────────────────────────────      │
│                                                     │
│  What does CPU stand for?                           │
│                                                     │
│  ▶ 1. Central Processing Unit                       │
│    2. Computer Personal User                        │
│    3. Central Program Utility                       │
│                                                     │
│  ─────────────────────────────────────────────      │
│                                                     │
│  YOU                                                │
│  [████████████████] 5/5                             │
│                                                     │
│  ↑ ↓ to choose • Enter to confirm                   │
│                                                     │
└─────────────────────────────────────────────────────┘
```

### Controls

| Key | Action |
|---|---|
| **↑ ↓** | Choose an answer |
| **Enter** | Confirm the selected answer |

### Rules

- **Correct answer** → The enemy loses 1 HP, you gain 20 XP.
- **Wrong answer** → You lose 1 HP.
- **Enemy defeated** → +20 XP. If you cross an XP threshold, you level up and your max HP increases.
- **All enemies defeated** → Victory!

---

## 🚀 Quick Start

**For players: the simplest method.**

1. Go to the **[Releases](https://github.com/RufusTheDwarf/Code_Quest/releases)** section of this repository.
2. Download the **`CodeQuest.exe`** file (≈ 67 MB) from the **Assets** section of the latest release.
3. Double-click the downloaded file.

That's it. The game launches immediately.

> **No prerequisites.** No .NET, no Git, no Visual Studio. The `.exe` file is standalone and contains everything it needs to run.

---

## 🛠️ Developer Setup

**For those who want to modify the code, compile the project, or contribute.**

### Prerequisites

- **.NET 8 SDK** or later : [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Git** : [Download here](https://git-scm.com/downloads)
- A terminal (Git Bash, PowerShell, Windows Terminal, etc.)

### Steps

```bash
# 1. Clone the repository
git clone https://github.com/RufusTheDwarf/Code_Quest.git

# 2. Enter the folder
cd Code_Quest

# 3. Run the game in development mode
dotnet run

# 4. OR compile a Release version
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

The compiled executable will be in:

```
bin/Release/net8.0/win-x64/publish/CodeQuest.exe
```

### Automatic Launcher

A **`JOUER.bat`** file is included at the root of the project. It automatically performs the following:

1. Checks and installs .NET locally (in `.dotnet/`).
2. Compiles the game.
3. Creates a **"Code Quest"** shortcut on your Desktop.
4. Launches the game.

Simply double-click `JOUER.bat` to do everything in one go.

---

## 📁 Project Structure

```text
Code_Quest/
│
├── Program.cs              # Main game source code
├── CodeQuest.csproj        # .NET project configuration file
├── JOUER.bat               # Automatic launcher for developers
├── .gitignore              # Files excluded from Git tracking
├── README.md               # This file
│
├── .dotnet/                # Portable .NET SDK (generated by JOUER.bat, ignored by Git)
└── bin/                    # Compiled files (generated, ignored by Git)
    └── Release/
        └── net8.0/
            └── win-x64/
                └── publish/
                    └── CodeQuest.exe   # Final executable
```

---

## 📚 What You Will Learn

By playing Code Quest, you review (or discover) essential computer science concepts:

| Theme | Example questions |
|---|---|
| **Hardware** | CPU, RAM, graphics card, hard drive, USB, Wi-Fi… |
| **Networking** | IP address, DNS, HTTP/HTTPS, TCP/UDP, OSI model, LAN… |
| **Programming** | Algorithms, recursion, OOP, O(n) complexity, pointers… |
| **Cybersecurity** | Asymmetric encryption, SQL injection, hashing, passwords… |
| **Developer tools** | Git, terminals, REST APIs, virtual machines, compilers… |

---

## 🗺️ Roadmap

Here is what is planned for future versions:

- [x] Centered game interface with health bars
- [x] Arrow key navigation to choose answers
- [x] Random shuffling of answers
- [x] Level and XP system
- [x] 3 difficulty levels
- [x] 50+ questions
- [ ] Local multiplayer mode (2 players on the same keyboard)
- [ ] Progress save system
- [ ] New question categories (databases, cloud, AI)
- [ ] Improved ASCII sprites for enemies
- [ ] Sound effects and background music
- [ ] Linux and macOS support

> Have an idea? Open an **[issue](https://github.com/RufusTheDwarf/Code_Quest/issues)** to suggest it!

---

## 🤝 Contributing

Contributions are welcome! Here is how to participate:

1. **Fork** the repository.
2. Create a branch for your feature:
   ```bash
   git checkout -b feature/my-awesome-feature
   ```
3. Make your changes and commit:
   ```bash
   git commit -m "Add my awesome feature"
   ```
4. Push your branch:
   ```bash
   git push origin feature/my-awesome-feature
   ```
5. Open a **Pull Request** on GitHub.

### Contribution Ideas

- Add new questions to the game.
- Improve the console interface.
- Fix bugs.
- Translate the game into other languages.
- Write documentation.

---

## 📄 License

This project is distributed under the **MIT License**. You are free to use, modify, and distribute it, as long as you keep the original copyright notice. See the [`LICENSE`](https://github.com/RufusTheDwarf/Code_Quest/blob/main/LICENSE) file for details.

---

<div align="center">

### 👤 Author

**RufusTheDwarf**

[![GitHub](https://img.shields.io/badge/GitHub-RufusTheDwarf-181717?style=flat&logo=github&logoColor=white)](https://github.com/RufusTheDwarf)
[![Repository](https://img.shields.io/badge/Repo-Code__Quest-2ea44f?style=flat&logo=git&logoColor=white)](https://github.com/RufusTheDwarf/Code_Quest)

<br>

### 💖 Acknowledgements

Thanks to everyone who will test this game, give feedback, and help improve it.

A special thanks to the **.NET community** for the tools and documentation that made this project possible.

<br>

### ⭐ Show Your Support

If you like this project, consider giving it a star. It means a lot.

[![Star this repo](https://img.shields.io/badge/⭐_Star_this_repo-yellow?style=flat)](https://github.com/RufusTheDwarf/Code_Quest/stargazers)
[![Report an issue](https://img.shields.io/badge/🐛_Report_an_issue-red?style=flat)](https://github.com/RufusTheDwarf/Code_Quest/issues)
[![Fork this repo](https://img.shields.io/badge/🍴_Fork_this_repo-blue?style=flat)](https://github.com/RufusTheDwarf/Code_Quest/fork)

<br>

---

<sub>Made with ❤️ and a lot of coffee by **RufusTheDwarf** · Licensed under MIT · © 2026</sub>

<br>

*"Talk is cheap. Show me the code."* Linus Torvalds

</div>
