# Multimodal VR Interface — Voice + Gesture Object Control

> A fully functional **Virtual Reality prototype** built in Unity that lets users interact with 3D virtual objects through a combination of **voice commands**, **controller pointing**, and **hand gestures** — powered by a probabilistic FSM-based multimodal fusion engine.

Developed as part of the **Multimodal Interfaces (MMI)** course at Julius-Maximilians-Universität Würzburg (WiSe 2023/24).

---

## Demo Video

Demo video available in the `Video/` folder (`MMI VR Prototype Video.mp4`).

---

## Project Overview

The application processes simultaneous input from three modalities:

- **Speech** — real-time voice commands via Recognissimo
- **Pointing** — right-hand controller ray direction as spatial input
- **Gestures** — trigger-held controller rotation gestures

A **Finite State Machine (FSM)** fusion engine syntactically and semantically combines these modalities to determine and execute the correct application function.

```
User Input
    │
    ├── Speech Input ──────────┐
    ├── Controller Pointing ───► Multimodal Fusion Engine (FSM)
    └── Gesture Input ─────────┘
                                        │
                            ┌───────────▼────────────┐
                            │   Syntactic Analysis   │
                            │  (Input Combination)   │
                            └───────────┬────────────┘
                                        │
                            ┌───────────▼────────────┐
                            │  Semantic Integration  │
                            │  (Validity Filtering)  │
                            └───────────┬────────────┘
                                        │
                            ┌───────────▼────────────┐
                            │  Application Function  │
                            │  Create/Select/Move/   │
                            │  Delete/Color/Shoot    │
                            └────────────────────────┘
```

---

## Supported User Interactions

### Voice + Pointing Commands

| Command | Modality | Action |
|---------|----------|--------|
| `"create"` + point | Speech + Point | Spawn new object at pointed location |
| `"create glass"` + point | Speech + Point | Spawn glass object |
| `"select"` + point | Speech + Point | Select the pointed object |
| `"move"` + point | Speech + Point | Move selected object to new location |
| `"delete"` + point | Speech + Point | Delete pointed or selected object |
| `"color [red/green/blue...]"` | Speech + Point | Change color of selected object |
| `"create [color] glass"` | Speech | Spawn colored object directly |
| `"gun"` | Speech | Equip gun on right hand |
| `"shoot"` + point | Speech + Point | Shoot the pointed object |

### Gesture Commands

| Gesture | Action |
|---------|--------|
| Hold right trigger + rotate right hand | Rotate selected object right |
| Hold left trigger + rotate left hand | Rotate selected object left |

---

## Project Structure

```
MultiModal-Interface/
├── Assets/
│   ├── MMI/
│   │   ├── fusion-method/        # FSM multimodal fusion engine
│   │   │   └── FusionMethod.cs
│   │   ├── gesture/              # Gesture recognition
│   │   │   ├── GestureRecognitionSystem.cs
│   │   │   └── GestureSimulator.cs
│   │   ├── speech/               # Speech recognition
│   │   │   ├── SpeechRecognitionSystem.cs
│   │   │   └── SpeechSimulator.cs
│   │   ├── scenes/               # Unity scene
│   │   ├── RayPicking.cs         # Controller ray-casting
│   │   └── OutlineModified.cs    # Object selection outline
│   ├── GestureRecognition/       # Gesture library
│   ├── Recognissimo/             # Speech recognition plugin
│   ├── Models/                   # Quest 2 controller models
│   └── WoodPack/                 # Environment assets
├── Graph/
│   └── Transition Graph.png      # FSM state transition diagram
├── Images/                       # Screenshots
└── Video/                        # Demo video (not tracked by git)
```

---

## Key Scripts

| Script | Description |
|--------|-------------|
| `FusionMethod.cs` | Core FSM fusion engine — processes probabilistic, unsorted multimodal input and produces probabilistic output |
| `SpeechRecognitionSystem.cs` | Real-time voice command recognition via Recognissimo |
| `SpeechSimulator.cs` | Debug tool to simulate speech input without a microphone |
| `GestureRecognitionSystem.cs` | Detects controller rotation gestures |
| `GestureSimulator.cs` | Debug tool to simulate gesture input |
| `RayPicking.cs` | Ray-casting from right controller for object selection/pointing |
| `OutlineModified.cs` | Visual outline feedback on selected objects |

---

## FSM Transition Graph

The `Graph/` folder contains the full FSM state transition diagram showing how speech, pointing, and gesture inputs are combined and filtered.


---

## Getting Started

### Prerequisites

- Meta Quest 2 headset
- PC with Unity 2021.3.11f1
- Meta Quest Link app

### Run in Unity

1. Clone this repository
2. Open **Unity Hub** → Add Project → select the cloned folder
3. Open with **Unity 2021.3.11f1**
4. Load scene: `Assets/MMI/scenes/`
5. Connect Meta Quest 2 via Quest Link
6. Put on the headset and enter **Game Mode** in Unity

### Controls

Use both controllers to interact:
- **Right controller ray** — point at objects
- **Voice** — speak commands (or use `SpeechSimulator` in the Unity inspector for testing)
- **Right/Left trigger + rotate** — rotate selected object

---

## Tech Stack

| Component | Technology |
|-----------|-----------|
| Game Engine | Unity 2021.3.11f1 |
| VR Headset | Meta Quest 2 |
| Language | C# |
| Fusion Method | FSM (Probabilistic) |
| Speech Recognition | Recognissimo |
| Environment | [Pirate Environment Pack](https://assetstore.unity.com/packages/3d/environments/pirate-environment-pack-55133) |
| Gun Model | [Revolver Gun Low Poly](https://assetstore.unity.com/packages/3d/props/guns/revolver-gun-low-poly-221659) |
| Base Template | MMI Unity Template (JMU Würzburg) |

---

## Developer

**Krunal Jesani**
M.Sc. Computer Science — Julius-Maximilians-Universität Würzburg

[![GitHub](https://img.shields.io/badge/GitHub-KrunalJesani-181717?logo=github)](https://github.com/KrunalJesani)
