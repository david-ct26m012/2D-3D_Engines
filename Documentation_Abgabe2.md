# Dirty Dancing – Dokumentation

**Modern 2D/3D Engines – WS 2026/27**
**Team:** David Scheucher, Eric Drössiger

---

## Wer hat welche Teile umgesetzt?

| Feature | Punkte | Umgesetzt von |
|---|---|---|
| Platform animation | 3 | [Name Teammitglied] |
| Animation event | 2 | David Scheucher |
| Character animations | 3 | [Name Teammitglied] |
| Spaceship interaction | 2 | David Scheucher |
| More Energy! | 2 | David Scheucher |

---

## Was wurde umgesetzt?

### Platform animation – [Name Teammitglied]
[Kurze Beschreibung durch Teammitglied]

### Animation event – David Scheucher
- Script `PlatformMaterialSwitcher.cs` auf dem Objekt `PlatformMover`, also auf demselben Objekt wie der Animator der Plattform. Nur so findet Unity die Funktionen der Events.
- Zwei Animation Events im Auf-und-Ab-Clip der Plattform:
  - tiefster Punkt → `SetLowestMaterial()` → **PolygonScifiWorlds_01_A** (blaue Lichter beim Hochfahren)
  - höchster Punkt → `SetHighestMaterial()` → **PolygonScifiWorlds_04_C** (rote Lichter beim Runterfahren)
- Das Script sammelt alle Renderer unterhalb von `SM_Veh_Barge_01` und tauscht über `sharedMaterials` den Material-Slot 0 aus.
- Das Event am Tiefpunkt sitzt nur auf Frame 0 und nicht zusätzlich auf dem letzten Frame, damit es beim Loopen nicht doppelt ausgelöst wird.

### Character animations – [Name Teammitglied]
[Kurze Beschreibung durch Teammitglied]

### Spaceship interaction – Eric Drössiger
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
