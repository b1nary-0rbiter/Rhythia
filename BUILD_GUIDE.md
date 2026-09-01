# Build Guide for Hallway of the Dead

## Setup

1. Open Unity Hub, create a new project (Unity 2022 LTS or later).
2. Add to Packages/manifest.json the required packages (OpenXR, XR Interaction Toolkit).
3. Import via Package Manager: Window > Package Manager > + (from disk).

## Scene Setup

1. Create MainHallway.unity in Assets/Scenes/.
2. Add MainHallway scene to Build Settings.
3. Configure XR settings for target platform.

## Build

### Desktop VR (PC)
- Platform: PC, Mac & Linux Standalone
- Add OpenVR or Oculus PC SDK
- Build & Run

### Mobile VR (Quest)
- Platform: Android
- Set Build to IL2CPP
- Enable OpenXR Plugin
- Build & Run

## Debugging

- Test in Play mode
- Use Unity Console for runtime logging
- Verify BeatMap timing with audio waveform