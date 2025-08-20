# Conway's Game of Life in C#

This is a simple implementation of Conway's Game of Life in C#. The project is a console application that displays the simulation over a series of generations.

## About the Game of Life

The Game of Life is a cellular automaton devised by the British mathematician John Horton Conway in 1970. It is a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input. One interacts with the Game of Life by creating an initial configuration and observing how it evolves.

### Rules

The universe of the Game of Life is a two-dimensional orthogonal grid of square *cells*, each of which is in one of two possible states, *live* or *dead*. Every cell interacts with its eight *neighbours*, which are the cells that are horizontally, vertically, or diagonally adjacent. At each step in time, the following transitions occur:

1.  Any live cell with fewer than two live neighbours dies, as if by underpopulation.
2.  Any live cell with two or three live neighbours lives on to the next generation.
3.  Any live cell with more than three live neighbours dies, as if by overpopulation.
4.  Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

## Getting Started

### Prerequisites

*   [.NET SDK](https://dotnet.microsoft.com/download)

### Building and Running the Application

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/dane-harnett/GameOfLife.git
    cd GameOfLife
    ```

2.  **Build the solution:**
    ```bash
    dotnet build
    ```

3.  **Run the application:**
    ```bash
    dotnet run --project GameOfLife/GameOfLife.csproj
    ```
    The console will display the Game of Life simulation, printing a new generation of the board. The simulation will run for 20 generations.

### Running the Tests

To run the unit tests for this project, navigate to the root directory and run the following command:

```bash
dotnet test
```

This will discover and run all the tests in the `GameOfLifeTests` project and show the results in the console.
