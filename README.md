# Ryhthia (working title)

VR rhythm-sword game. Slash on the beat.

See `CONCEPT.md` for the core loop.

## Project layout
```
Assets/
  Scripts/        Core, Audio, Combat, Enemies, UI, Player
  Prefabs/        Zombies, sword, rings
  Scenes/         MainHallway.unity
  Audio/          Music + SFX
  Materials/      Ring pulse materials
  Settings/       ScriptableObjects (music track, difficulty)
Packages/
  manifest.json   Required packages (OpenXR, XR Interaction Toolkit, etc.)
ProjectSettings/
  LayerDefinitions.txt
  InputManager.asset
  XRSettings.asset
```

## Build pipeline

### 1. Install dependencies
```
cd ~/Projects/vrGame
# Unity Package Manager
Window > Package Manager > +
  Import from disk: Packages/manifest.json
```
Required packages: OpenXR Plugin, XR Interaction Toolkit, Unity Input System, Cinemachine.

### 2. Configure project
```
Edit > Project Settings
  XR Plug-in Management > Enable OpenXR
  Input System > Enable Input System
  Tags & Layers > Add tags: Zombie, Ring, Player, Hand
```

### 3. Build
```
File > Build Settings
  Platform: Android (Quest/Android VR) or PC (Desktop VR)
  Add OpenVR / Oculus / PICO as needed
  Build & Run
```

### 4. Iterate
- Swap `Assets/Audio/Music/` tracks and adjust `BeatMap` ScriptableObject.
- Tune `ZombieStats` ScriptableObjects per zombie type.
