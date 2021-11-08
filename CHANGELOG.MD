# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased


## [0.1.2] - 2021-11-8

### Added

Features:
* Added Interaction / Crafting speed toggle

Configuration values:
* TimeMultDefault - Enabling configuration of the default value of timeMult, allowing players to switch between TimeMult and TimeMultDefault (instead of it just being 1)
* CraftingSpeedKey - Key to toggle CraftingSpeed between configured value and default value (1)
* ToggleCraftAndInteraction - choose whether CraftingSpeedKey also toggles InteractionSpeed to (1)

### Changed
* Tweaked TimeScale toggle, allowing players more freedom in usage
* Moved some settings around in NKN-Configurator, grouping all hotkeys

### Fixed

- NKN-Configurator will now also parse decimal points properly in numerical configuration file values
- Technical: Fixed compiler warnings in NKN-Configurator

## [0.1.1] - 2021-11-1
### Fixed

* Refactored the calculation of bonus orbs according to the `OrbsMult`, `RoundDown`, `OrbsConstAddIfZero` and `OrbsConstant` values because of unexpected behaviour. Does now better match with the described functionality.
* Refactored part of the `TimeScale` feature so that it becomes a toggle between 1 and the value of `TimeMult`, so that the player can always switch back to the default TimeScale. Before it was behaving inconsistently between different values of `timeMult`

## [0.1] - 2021-10-31
### Fixed
* Added an assembly reference to `UnityEngine.InputLegacyModule` that prevented compilation using more recent .DLL's of the game.
   * This also fixed some broken features that were using the (former) `UnityEngine.Input` class (used to handle keypresses):
     * Sprint
     * Save Anywhere
     * Timescale Switching
     * Reload configuration
     * Reset prayer
     * AddMoney

