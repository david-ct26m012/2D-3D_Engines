# Dirty Dancing – Dokumentation

**Modern 2D/3D Engines – WS 2026/27**
**Team:** David Scheucher, Eric Drößiger

---

## Wer hat welche Teile umgesetzt?

| Feature | Punkte | Umgesetzt von |
|---|---|---|
| Platform animation | 3 | Eric Drößiger |
| Animation event | 2 | David Scheucher |
| Character animations | 3 | Eric Drößiger |
| Spaceship interaction | 2 | Eric Drößiger |
| More Energy! | 2 | David Scheucher |

---

## Was wurde umgesetzt?

### Platform animation – Eric Drößiger
- Hierarchie: `PlatformAnchor` (statisch) → `PlatformMover` (animiert) → `SM_Veh_Barge_01` + `Dancers`. So speichern die Clips nur kleine lokale Offsets statt absoluter Weltpositionen, und die Tänzer bewegen sich automatisch mit.
- Zwei Clips im Animator Controller `PlatformMover.controller`, jeweils auf einem eigenen Layer (`Float` und `Spin`), damit sie gleichzeitig laufen und unabhängig lange sein können:
  - `Platform_Float`: Position.y 0 → 5 → 0 über 10 Sekunden, Loop. Die Extrempunkte liegen genau auf den Keyframes (0 s und 5 s), dort sitzen die Animation Events.
  - `Platform_Spin`: Rotation.y 0° → 360° über 30 Sekunden, Euler-Interpolation und lineare Tangenten, damit die Drehung am Loop-Punkt nicht abbremst.


### Animation event – David Scheucher
- Script `PlatformMaterialSwitcher.cs` auf dem Objekt `PlatformMover`, also auf demselben Objekt wie der Animator der Plattform. Nur so findet Unity die Funktionen der Events.
- Zwei Animation Events im Auf-und-Ab-Clip der Plattform:
  - tiefster Punkt → `SetLowestMaterial()` → **PolygonScifiWorlds_01_A** (blaue Lichter beim Hochfahren)
  - höchster Punkt → `SetHighestMaterial()` → **PolygonScifiWorlds_04_C** (rote Lichter beim Runterfahren)
- Das Script sammelt alle Renderer unterhalb von `SM_Veh_Barge_01` und tauscht über `sharedMaterials` den Material-Slot 0 aus.
- Das Event am Tiefpunkt sitzt nur auf Frame 0 und nicht zusätzlich auf dem letzten Frame, damit es beim Loopen nicht doppelt ausgelöst wird.

### Character animations – Eric Drößiger
- Vier Charaktere und vier Tanzanimationen von Mixamo, dazu eine Wave-Animation für die Raumschiff-Interaktion.
- Import: Rig auf **Humanoid**, damit jede Animation auf jedem Charakter läuft. Texturen und Materialien aus den FBX-Dateien extrahiert. Bei den Clips **Loop Time** und **Bake Into Pose** für Root-Rotation und Root-Position, damit die Tänzer nicht von der Plattform driften.
- Ein gemeinsamer Animator Controller `Dancer.controller` mit den States `Dance` (Default) und `Wave`. Pro Charakter ein **Animator Override Controller**, der nur den Tanz-Clip austauscht:

  | Charakter | Override Controller | Tanz |
  |---|---|---|
  | Ch03 | `Dancer_Ch03` | Wave Hip Hop Dance |
  | Ch20 | `Dancer_Ch20` | Northern Soul Spin Combo |
  | Kachujin | `Dancer_Kachujin` | Macarena |
  | Marker Man | `Dancer_MarkerMan` | Belly Dance |

- Alle Tänzer liegen unter `PlatformMover/Dancers`, Apply Root Motion ist deaktiviert.


### Spaceship interaction – Eric Drößiger
- Script `DancerShipDetection.cs` auf dem Objekt `Dancers`. Es holt sich automatisch die Animatoren aller Tänzer.
- Eine Coroutine prüft alle 0,2 Sekunden, ob das Raumschiff näher als `detectionRadius` (20 Einheiten) ist. Nur wenn sich dieser Zustand ändert, wird bei allen Tänzern der Bool-Parameter `isWaving` gesetzt.
- Animator Controller: Transitions zwischen Tanz und Winken über `isWaving`, *Has Exit Time* ist deaktiviert, damit die Gäste sofort reagieren.
- Beim Winken drehen sich die Tänzer weich zum Raumschiff (nur horizontal, `Quaternion.Slerp`). Danach drehen sie sich wieder in ihre ursprüngliche Richtung relativ zur rotierenden Plattform.

### More Energy! – David Scheucher
- Zwei Duplikate von `SM_Prop_SolarPanel_03` als Kinder von `SpaceShip` an den Flügeln befestigt und händisch an die leicht schrägen Flügel angepasst.
- Animation-Clip `SolarPanel_Swing` (Animator auf dem Panel-Root, von beiden Panels gemeinsam verwendet):
  - **Scharnier:** `Panel_01` Rotation.x: −30° → +30° → −30° (Frames 0 / 60 / 120, in Schleife).
  - **Emission:** kopiertes Material `SolarPanel_Emission_Animated` auf beiden Panels. Animiert wird `Material._Emission_Color`. Die Kanten blenden also von keiner Emission auf volle Intensität und wieder zurück.
- Die Emission Map des Materials sorgt dafür, dass nur die Kanten leuchten.



