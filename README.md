# TCS ScriptableObject Architecture

![GitHub release (latest by date)](https://img.shields.io/github/v/release/Ddemon26/TCS-ScriptableObjectArchitecture) ![GitHub license](https://img.shields.io/github/license/Ddemon26/TCS-ScriptableObjectArchitecture) ![GitHub issues](https://img.shields.io/github/issues/Ddemon26/TCS-ScriptableObjectArchitecture) ![GitHub pull requests](https://img.shields.io/github/issues-pr/Ddemon26/TCS-ScriptableObjectArchitecture) ![GitHub contributors](https://img.shields.io/github/contributors/Ddemon26/TCS-ScriptableObjectArchitecture)

## Overview
The TCS ScriptableObject Architecture is a Unity framework that leverages the power of ScriptableObjects to create modular, scalable, and maintainable game systems. It abstracts data and interactions, reducing tightly-coupled dependencies and enabling developers to manage and extend game features efficiently. This architecture focuses on improving workflow efficiency by reducing redundancy, enhancing flexibility, and promoting a structured approach to game development.

By utilizing ScriptableObjects, developers can create reusable assets shared across multiple scenes, simplifying debugging, testing, and extending features. This approach facilitates a clean, modular architecture that reduces interdependencies and improves the efficiency of developing sophisticated projects.

## Features
- **Game Events System**: A versatile event-driven system (`GameEvent.cs`, `UnityEventGameEventListener.cs`) for managing in-game actions and their corresponding responses. Game events enable decoupled communication, making it easy to have interactions like enemy defeats trigger UI or audio responses independently.

- **ScriptableObject Variables**: Custom variable types, such as `IntVariable.cs`, serve as encapsulated data containers. These ScriptableObject instances manage state data shared across objects, such as player health or score, making them persistent and easy to modify. This reduces reliance on singleton objects and enhances system modularity.

- **Runtime Collections**: `RuntimeCollection.cs` manages dynamic collections of in-game objects, such as enemies or items. It facilitates efficient access to entities during gameplay, useful for collective behaviors like disabling all enemies or spawning waves.

- **Singleton Pattern Support**: `SingletonScriptableObject.cs` offers a way to create globally accessible ScriptableObjects, such as settings managers or configuration data, ensuring resources are unique and consistently available throughout the game.

## Installation

Repository URL:
```
https://github.com/Ddemon26/TCS-ScriptableObjectArchitecture.git
```
1. Clone or download the repository.
2. Import the project into Unity via the Unity Editor.
3. Fulfill any project dependencies. Use Unity version [specify version if known] or later for compatibility.

## Getting Started
1. **Setting Up Game Events**: Create new `GameEvent` assets through the Unity Editor and link them to listeners and response scripts. This approach ensures decoupled game logic, enhancing readability and maintainability.

2. **Creating Scriptable Variables**: Use `IntVariable` or similar classes to create shared variables like player health. This externalizes data, ensuring persistence and simplifying cross-scene sharing.

3. **Using Runtime Collections**: Use `RuntimeCollection` to track instances of specific types, like NPCs or collectibles, allowing efficient management of collective behaviors during gameplay.

## Folder Structure
- **TCS ScriptableObjectArchitecture**: Contains the core components of the architecture.
  - **Editor**: Custom scripts for the Unity Editor to improve productivity, including custom inspectors and utilities.
  - **Runtime**: Core runtime scripts like `GameEvent` and `IntVariable`, forming the building blocks for gameplay systems.
  - **Tests**: Unit tests that validate the correct operation of core components, enhancing system stability and reliability.

## Contributing
Contributions are welcome! Submit issues or pull requests through GitHub. Follow the contribution guidelines to maintain a cohesive codebase. Enhancements could include new ScriptableObject types, improved editor tooling, or runtime optimizations. Feedback and suggestions help shape future project directions.

## License
This project is licensed under the terms specified in the [LICENSE](LICENSE) file in the root directory. Contributions are licensed under the same terms to keep the framework open and accessible to all users.

## Contact
For inquiries or suggestions, please reach out through GitHub issues. Your feedback helps improve the framework, and we're interested in hearing about projects using this architecture. If you have questions, encounter bugs, or have ideas for improvement, feel free to get in touch.
