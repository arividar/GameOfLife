# AGENTS.md

This file provides instructions for AI coding agents working on this project.

## Project Overview

This is a C# implementation of Conway's Game of Life. It includes a console application (`GameOfLife`) and a corresponding test project (`GameOfLifeTests`).

## Development Workflow: Test-Driven Development (TDD)

This project follows a strict Test-Driven Development (TDD) workflow. All changes must be driven by tests.

### TDD Cycle

1.  **Add a Test:** Before writing any new implementation code, add a new test that fails. The test should define a new function or an improvement of an existing function.
2.  **Run All Tests:** Run all tests and see the new test fail. This is to ensure that the test is not passing by mistake. The command to run tests is `dotnet test`.
3.  **Write the Code:** Write the minimum amount of code required to make the failing test pass.
4.  **Run All Tests Again:** Run all tests again to ensure that the new code passes the new test and does not break any existing tests.
5.  **Refactor:** Refactor the code to improve its structure and readability without changing its behavior. Run tests again after refactoring to ensure that everything still works.

### Rules for Agents

-   **No new feature without a test:** Any pull request that adds a new feature must include a corresponding test.
-   **Fix bugs with tests:** When fixing a bug, first write a test that reproduces the bug, then write the code to fix it.
-   **Keep tests fast:** Tests should be fast to run to encourage frequent execution.
-   **All tests must pass:** Before submitting any changes, ensure that all tests are passing.

## Setup and Testing Commands

-   **Build the project:** `dotnet build`
-   **Run tests:** `dotnet test`
