Feature: Traffic Light

As a user
I want to control the traffic light
So that I can switch between red, yellow and green

@yellow
Scenario: Switch to yellow
    Given the traffic light application is running
    When I click the "Yellow" button
    Then the traffic light should show "Yellow"

@green
Scenario: Switch to green
    Given the traffic light application is running
    When I click the "Green" button
    Then the traffic light should show "Green"

@red
Scenario: Switch back to red
    Given the traffic light application is running
    When I click the "Red" button
    Then the traffic light should show "Red"

@automatic
Scenario: Switch to Automatic modus
    Given the traffic light application is running
    When I click the Automatic button
    Then the traffic light should show different colors in a loop

@stopautomatic
Scenario: Stop Automatic modus
    Given the traffic light application is running
    When I click the Automatic button
    When I click the Automatic button again
    Then the traffic light should show stop changing colors
