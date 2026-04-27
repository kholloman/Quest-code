# Team QuestCode – Puzzle Adventure Game

## Overview
This project is a text-based puzzle adventure game built in C#. The game allows players to explore rooms, collect items, interact with NPCs, and solve puzzles.

## Features
- 5 interconnected rooms
- 10+ collectible items
- Inventory system using collections
- NPC dialogue system
- Item combination (puzzle mechanic)
- Save and load functionality using JSON
- Command-based gameplay system

## Technologies Used
- C#
- Object-Oriented Programming
- Collections (List, Dictionary, HashSet)
- LINQ
- JSON Serialization
- File I/O

## How to Run
1. Open the project in VS Code
2. Navigate to the project folder
3. Run: dotnet run


## Commands
- look → view room
- go [direction] → move
- take [item] → pick up item
- inventory → view inventory
- talk → interact with NPC
- combine [item1] [item2] → combine items
- save → save game
- load → load game
- quit → exit game

## Testing
Basic unit tests were implemented to verify inventory functionality, including adding and removing items.

## Team Roles
- Member 1 | Nia Robinson: Inventory system and item logic
- Member 2 | Kenyce Holloman: Rooms, world, NPC dialogue
- Member 3 | Terrance Holloway II: Command system, save/load, testing, documentation
