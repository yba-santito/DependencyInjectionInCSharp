# Dependency Injection in C# / ASP.NET Core

Learning Dependency Injection for ASP.NET Core – based on the [ASP.NET Core Full Course For Beginners (.NET 8)](https://www.youtube.com/watch?v=AhAxLiGC7Pc).

## Overview

This repository contains my learning journey through Dependency Injection (DI) in C# and ASP.NET Core. Dependency Injection is a design pattern where an object receives its dependencies from external sources rather than creating them internally . It helps decouple classes from their dependencies, making applications more modular, maintainable, and testable.

## Project Structure

DependencyInjectionInCSharp/
├── GameStore.Api/ # Main ASP.NET Core Web API project
├── .vscode/ # VS Code workspace settings
├── GameStore.slnx # Visual Studio solution file
└── dotnet-tools.json # .NET tools configuration


## Tech Stack

- **Language:** C#
- **Framework:** ASP.NET Core (.NET 8)
- **Dependency Injection:** Built-in .NET DI container

## Key Concepts Covered

- **What is Dependency Injection?** – A technique for achieving Inversion of Control (IoC) between classes and their dependencies
- **Why use DI?** – Reduces tight coupling, makes code easier to test, and improves maintainability
- **DI Implementation Types:**
  - **Constructor Injection** – Dependencies are provided via the class constructor
  - **Property Injection** – Dependencies are set via public properties
  - **Method Injection** – Dependencies are passed as method parameters
- **Service Lifetimes:** Transient, Scoped, and Singleton
- **DI Containers** – Frameworks that create dependencies and inject them automatically

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or VS Code with C# extensions

### Clone the Repository

```bash
cd GameStore.Api
dotnet restore
dotnet run
```

The API will be available at https://localhost:5001 (or the port configured in your launch settings).

What's Inside
The GameStore.Api project demonstrates DI concepts in a practical ASP.NET Core Web API context, including:

Registering services with the built-in DI container

Injecting dependencies into controllers

Configuring service lifetimes

Learning Resource
This project follows the ASP.NET Core Full Course For Beginners (.NET 8) by Julio Casal . You can watch the full course here:

🔗 https://youtu.be/AhAxLiGC7Pc 

Status
🚧 Work in progress – actively learning and building!

License
This project is for educational purposes. Feel free to use it as a reference for learning Dependency Injection in C# and ASP.NET Core.

```bash
git clone https://github.com/yba-santito/DependencyInjectionInCSharp.git
cd DependencyInjectionInCSharp
```
