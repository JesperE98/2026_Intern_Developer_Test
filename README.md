# 2026 Intern Developer Test - Matching Game
A Unity-based matching game inspired by Toon Blast mechanics, developed as part of the internship application for MAG Interactive.

## Features
* **Grid System:** Dynamic 9x9 grid generation.
* **Match Logic:** Efficient recursive searching for adjacent gems of the same color.
* **Cascading Refill:** Gems fall down to fill gaps, and new gems spawn at the top with a smooth Lerp animation.
* **UI Feedback:** Animated score system that reacts to matches.

## Technical Details
* **Code Structure:** Built with a focus on decoupling logic. Used Action-based events for gem selection to keep the `Board` and `Gem` classes separated.
* **Architecture Philosophy:** Developed with an event-driven approach, avoiding the `Update` loop for core gameplay, ensuring better performance and cleaner code. State changes CAN be triggered as well by user input (1 = Main Menu, 2 = Level Started), but was mostly used for early debugging.
* **Performance:** Uses a 2D array matrix (`allGems[x, y]`) for O(1) lookups of neighbors. 
* **Animations:** All movements are handled via Coroutines to ensure smooth gameplay and efficient resource management.

## How to Play
1. Click on a gem to select it.
2. Click on an adjacent gem to swap them.
3. If a match of 3 or more is formed, they pop, and the board refills!

---

## Development Documents
I used **Obsidian** to document my thoughts and architecture during developemnt.

[<img src="https://img.shields.io/badge/Download-Obsidian-purple?style=for-the-badge&logo=obsidian" />](https://obsidian.md/download)

[<img src="https://img.shields.io/badge/Download-My_Notes_(ZIP)-blue?style=for-the-badge" />](https://github.com/user-attachments/files/25977061/Obisidan.Document.zip)

*Note: The ZIP contains my development journal in Markdown format (best viewed in Obsidian).*

## References
This series was a key reference for researching grid-based logic and match-3 patterns:
* [Match-3 Tutorial Series by DaikonCode](https://www.youtube.com/watch?v=xFLZiRXJMTk&list=PL4vbr3u7UKWrfjyVhtaZAPR0o0RJq_XO3)
