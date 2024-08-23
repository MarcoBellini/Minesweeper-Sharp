# Minesweeper Sharp

![Screenshot 2024-08-23 163817](https://github.com/user-attachments/assets/b0dde179-2777-4176-9431-1d7b8f2b4544)


**Minesweeper Sharp** is a Windows Forms application written in C#. It is a custom implementation of the classic Minesweeper game, developed to strengthen understanding of C# and advanced design patterns, specifically the **Mediator** and **Composite** patterns. This project offers an engaging way to explore these concepts while creating a functional and enjoyable game.

## Features

- **Three Difficulty Levels**: Play Minesweeper at Easy, Intermediate, or Expert levels.
- **Advanced Design Patterns**: Implements the Mediator and Composite design patterns, providing a real-world application of these powerful software design techniques.
- **Intuitive Interface**: A clean and responsive user interface built with Windows Forms.

## Technical Overview

### Mediator Pattern

The **Mediator pattern** is central to the architecture of Minesweeper Sharp. It is implemented using the `IMediator` interface, which defines a `Notify` method for passing notifications between components. Here's how it's structured:

- **`GameCell` Class**: Represents a single cell on the Minesweeper grid. Inherits from the `BaseComponent` class and implements the `IGameObject` interface. The `GameCell` class uses the `SetMediator` method inherited from `BaseComponent` to set its mediator, which is responsible for managing interactions between cells.
  
- **`GameCellsManager` Class**: Acts as the mediator for the `GameCell` objects. It implements the `IMediator` interface and inherits from `BaseComponent`. The `GameCellsManager` handles communication between the cells and the `GameManager`.

- **`GameManager` Class**: Serves as the mediator for the entire game. It manages communication between the `GameCellsManager`, the `GameCounter` classes (which track the timer and the number of flagged mines), and the `GameBanner` class (which displays the win or lose banner).

### Composite Pattern

The **Composite pattern** is used to manage the hierarchical structure of the game components. The `IGameObject` interface is implemented by all major classes (`GameCell`, `GameCellsManager`, `GameCounter`, `GameBanner`, and `GameManager`), ensuring that each component can be drawn and can handle mouse click events. This allows individual cells, as well as groups of cells and other game elements, to be treated uniformly.

## Getting Started

### Prerequisites

- **.NET 6**: Ensure you have .NET 6 installed on your machine.
- **Visual Studio 2022**: It is recommended to use Visual Studio 2022 or later for compiling and running the project.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/Minesweeper-Sharp.git
   ```
2. Open the solution file (`Minesweeper Sharp.sln`) in Visual Studio 2022.
3. Build the solution.
4. Run the application.

### How to Play

1. Choose a difficulty level: Beginner, Intermediate, or Expert.
2. Click on the cells to reveal either a number, an empty space, or a mine.
3. Use the numbers to deduce where the mines are located and flag them.
4. Clear all non-mined cells to win the game!

## Icon Attribution

This project uses the following icons from [FlatIcon](https://www.flaticon.com):

- **Flag icon**: [Flag icons created by Smashicons - Flaticon](https://www.flaticon.com/free-icons/Flag_Image)
- **Timer icon**: [Time icons created by Freepik - Flaticon](https://www.flaticon.com/free-icons/time)
- **Application icon**: [Bomb icons created by surang - Flaticon](https://www.flaticon.com/free-icons/bomb)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
