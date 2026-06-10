# WPF Traffic Light – Test Automation MVP

## Overview

This repository contains a small but maintainable Test Automation MVP for a WPF Traffic Light application.

The goal of the project is to demonstrate different testing approaches:

* Behavior Driven Development (BDD) using Reqnroll
* Component Testing against the ViewModel
* UI Smoke Testing using FlaUI
* Automated execution using GitHub Actions

---

## Technologies

* C# (.NET 10)
* WPF
* NUnit
* Reqnroll
* FluentAssertions
* FlaUI (UI Automation)
* GitHub Actions


## Project Structure

WpfAmpelExample
│
├── WpfAmpelExample
│   ├── Controls
│   ├── ViewModels
│   ├── MainWindow.xaml
│   └── WpfAmpelExample.csproj
│
├── Ampel.Tests
│   ├── Features
│   │   └── TrafficLight.feature
│   │
│   ├── Steps
│   │   └── TrafficLightSteps.cs
│   │
│   ├── UiTests
│   │   └── TrafficLightUiSmokeTests.cs
│   │
│   └── Ampel.Tests.csproj
│
└── .github
    └── workflows
        └── dotnet-tests.yml


## Implemented Test Types

### 1. Component Tests

These tests validate the application logic directly against the ViewModel without launching the WPF UI.

Covered scenarios:

* Switch to Red
* Switch to Yellow
* Switch to Green
* Start Automatic Mode
* Stop Automatic Mode

Advantages:

* Fast execution
* Stable
* Suitable for CI/CD


### 2. UI Smoke Tests

UI Smoke Tests are implemented using FlaUI.

Covered functionality:

* Application starts successfully
* Main window is displayed
* Green button can be clicked
* Screenshot capturing support

The UI tests are tagged using:

```csharp
[Category("UI")]
```

This allows selective execution.

---

## Gherkin Specification

The application behavior is described using Gherkin syntax.

Example:

```gherkin
Feature: Traffic Light

Scenario: Switch to Red
    Given the traffic light application is running
    When I click the "Red" button
    Then the traffic light should show "Red"

Scenario: Switch to Green
    Given the traffic light application is running
    When I click the "Green" button
    Then the traffic light should show "Green"
```

---

## Setup

### Prerequisites

Install:

* Visual Studio 2022
* .NET SDK 10
* Windows 11

Verify installation:

```bash
dotnet --version
```

---

## Build the Project

```bash
dotnet restore
dotnet build
```

---

## Execute All Tests

```bash
dotnet test
```

---

## Execute Only Component Tests

```bash
dotnet test --filter "Category!=UI"
```

---

## Execute Only UI Tests

```bash
dotnet test --filter "Category=UI"
```

---

## Screenshots

The FlaUI tests support screenshot generation.

Screenshots can be created:

* During test execution
* When a test fails

Default location:

```text
Screenshots/
```

---

## Continuous Integration

The repository contains a GitHub Actions pipeline:

```text
.github/workflows/dotnet-tests.yml
```

The pipeline performs:

1. Checkout repository
2. Restore NuGet packages
3. Build solution
4. Execute automated tests
5. Publish test results
6. Upload screenshots (if available)

---

## Assumptions

The implementation focuses on a minimal but maintainable test automation setup.

For simplicity:

* Component tests interact directly with the ViewModel.
* UI tests focus on smoke testing.
* FlaUI tests are intended to run locally.
* CI primarily executes component tests.

---

## Future Improvements

Possible extensions:

* Page Object Model for FlaUI tests
* HTML reporting
* Test data management
* Additional UI validations
* Full regression suite

---

## Author

**Sidieu Tchayep**

Software Test Automation Engineer
