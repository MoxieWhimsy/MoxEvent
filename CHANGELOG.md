# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added 
+ package.json tells the package manager about Events.
+ Runtime Assembly Definition will speed up compilation when using as a package.

### Refactored
+ Make Runtime folder and move runtime code into it.

## [0.4.1] - 2021-12-09
### Changed
+ Global events will display a readonly list of listeners in the inspector, if Odin Inspector is installed and active.

## [0.4.0] - 2021-08-16
### Added
+ Vector Two event since Vector Two events are common enough

## [0.3.1] - 2021-06-16
### Refactored
+ Renamed Folders for namespace consistency (assuming #ignored namespaces)

## [0.3.0] - 2021-05-05

### Changed
+ A game event will pass itself to listeners when Raised.

### Refactored
+ Moved Void to Mox namespace from Mox.Events

## [0.2.0] - 2021-04-07
### Added
+ AGameState is a base class for Game States with `OnExit`, `OnEnter`, and transitions to other Game States.
+ Transition defines a path to a GameState via a specific GameEvent
### Refactored
+ Renamed `Register` and `Unregister` from ...`Listener`
  + AGameEventListener
  + AGameEvent
  + AGlobal

## [0.1.0] - 2021-04-07
### Imported
+ Code used in a personal project:
  + AGameEventListener behaviour
  + VoidListener behaviour
  + AGameEvent
  + AGlobalEvent
  + IGameEventListener
  + Void
  + VoidEvent