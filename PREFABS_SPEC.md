# Prefab Specifications

## Zombie Prefab (Assets/Prefabs/Zombie.prefab)
- SpriteRenderer: Grey blocky zombie sprite
- Scripts: Zombie.cs, RingPulse.cs
- Children:
  - RingHolder (empty)
    - RingSprite (SpriteRenderer with ring sprite)
- Layer: Zombie

## Sword Prefab (Assets/Prefabs/Sword.prefab)
- SpriteRenderer: Sword sprite
- Collider2D: Trigger for hit detection
- Script: SwordSwing.cs (manages animation & damage)
- Layer: Player

## Main Scene (Assets/Scenes/MainHallway.unity)
- Player (Capsule + PlayerController + VRInputHandler)
- Main Camera (XR Rig setup)
- Canvas (HUD showing hearts, score, multiplier)
- Empty GameObject: BeatMapHolder (Config script)
- Zombie Spawn Points (empty GameObjects along hallway path)
- Lighting: Ambient grey hallway
- AudioSource (music loop)

## Materials
- Ring Material: Radial gradient (transparent center, opaque ring)
- Player Material: Basic grey
- Zombie Material: Slightly varied greys

## Settings Assets (ScriptableObjects)
- BeatMapData.asset (per level/music track)
- GameConfig.asset (global settings: health, spawn rate, difficulty)