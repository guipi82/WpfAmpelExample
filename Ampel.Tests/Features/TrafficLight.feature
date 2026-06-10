Feature: Traffic Light

As a user
I want to control the traffic light
So that I can switch between red, yellow and green

Scenario: Switch to yellow
    Given the traffic light application is running
    When I click the "Yellow" button
    Then the traffic light should show "Yellow"

Scenario: Switch to green
    Given the traffic light application is running
    When I click the "Green" button
    Then the traffic light should show "Green"

Scenario: Switch back to red
    Given the traffic light application is running
    When I click the "Red" button
    Then the traffic light should show "Red"

Scenario: Switch to Automatic modus
    Given the traffic light application is running
    When I click the Automatic button
    Then the traffic light should show different colors in a loop

Scenario: Stop Automatic modus
    Given the traffic light application is running
    When I click the Automatic button
    When I click the Automatic button again
    Then the traffic light should show stop changing colors


Scenario: UI smoke test application starts
    Given the WPF application is started
    Then the main window should be visible

Scenario: UI smoke test application starts and click the Green Button
    Given the WPF application is started
    Then the main window should be visible
    When I click the "Green" button
    Then the color green will be shown on the the traffic