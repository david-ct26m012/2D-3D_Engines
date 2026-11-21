# Funky Town – Dokumentation

**Modern 2D/3D Engines – WS 2026/27**
**Team:** David Scheucher, Eric Drößiger

---

## Wer hat welche Teile umgesetzt?
 
| Feature | Punkte | Umgesetzt von |
|---|---|---|
| Distance | 3 | Eric Drößiger |
| Atmosphere | 1 | Eric Drößiger |
| Lights | 2 | David Scheucher |
| Music | 1 | Eric Drößiger |
| Magazine | 3 | David Scheucher |
| Performance | 1 | David Scheucher |
| Build | 1 | Eric Drößiger |

---

## Was wurde umgesetzt?

### Distance – Eric Drößiger
- World Space Canvas `PartyRadar` als Kind von `SpaceShip`, schwebt als kleines Hologramm über dem Cockpit. Inhalt: Radar-Ring (`radar_ring`), Richtungspfeil (`radar_arrow`), Distanz in Metern und ein Höhenhinweis.
- Script `PartyCompass.cs` auf `PartyRadar`:
  - `spaceship.InverseTransformPoint(party.position)` rechnet die Position der Party in den lokalen Raum des Schiffs um (`z` = vorne, `x` = rechts, `y` = oben).
  - `Mathf.Atan2(local.x, local.z)` ergibt den Winkel, um den der Pfeil in der Canvas-Ebene gedreht wird. Pfeil nach oben bedeutet „geradeaus“.
  - `local.magnitude` liefert die Distanz, die als Text (`123 m`) angezeigt wird. Liegt die Party deutlich über oder unter dem Schiff, erscheint „▲ oben“ bzw. „▼ unten“.
  - Pfeil, Ring und Text wechseln mit der Distanz von Cyan zu Pink: `Mathf.InverseLerp(maxDistance, partyRadius, distance)` ergibt einen Wert von 0 bis 1, mit dem `Color.Lerp` die beiden Farben mischt.
- Alle Referenzen und Werte (Farben, `maxDistance`, `partyRadius`) sind über `[SerializeField]` im Inspector einstellbar.

### Atmosphere – Eric Drößiger
- **Party:** Partikel-Prefabs aus `Particle Effects/Prefabs` als Kinder von `PlatformMover` an der Plattform platziert, damit sie mit der Plattform schweben und drehen. Simulation Space auf **Local** umgestellt, weil die Partikel bei *World* im Raum stehen bleiben, während die Plattform weiterzieht. Farben, Emission und Form an den Party-Look angepasst.
- **Triebwerke (Extra):** je ein Partikelsystem `Thruster` hinter den beiden Triebwerken des Raumschiffs.
  - Eigenes Material `M_Thruster` mit Shader *Universal Render Pipeline/Particles/Unlit*, Surface *Transparent*, Blending *Additive*. Damit funktioniert Color over Lifetime, und es gibt keine grauen Ränder.
  - Script `EngineThrusters.cs` auf `SpaceShip` liest den Schub über die neue Property `PlayerController.Throttle` (W-Taste) und regelt damit Emission, Partikelgeschwindigkeit (längerer Schweif bei Vollgas) und die Intensität der Engine Lights.
  - `Mathf.Lerp(currentThrottle, player.Throttle, response * Time.deltaTime)` lässt den Wert weich nachziehen, damit die Flamme nicht flackert.

### Lights – David Scheucher
- [Beschreibung durch David]

### Music – Eric Drößiger
- Party-Musik aus `FlyingDutchman/Music` über eine `AudioSource` auf `PartyMusic`, einem Kind von `PlatformMover`. Die Musikquelle bewegt sich dadurch mit der Plattform.
- Einstellungen: Loop, Play On Awake, **Spatial Blend 3D**, Volume Rolloff mit Min Distance (volle Lautstärke) und Max Distance (Stille). Dazwischen blendet Unity die Lautstärke weich ein und aus.
- **Doppler Level 0**, damit sich die Tonhöhe beim schnellen Vorbeifliegen nicht verzieht.
- Der `AudioListener` sitzt an der Kamera des Raumschiffs, es gibt nur einen Listener in der Szene.
- Import der MP3: Load Type *Streaming*, Compression *Vorbis*.

### Magazine – David Scheucher
- [Beschreibung durch David]

### Performance – David Scheucher
- [Beschreibung durch David: Static Batching, Occlusion Culling, Messwerte vorher/nachher]

### Build – Eric Drößiger
- Product Name **FunkyTown**, eigenes App-Icon (`logo_icon.png`).
- Eigener Splash Screen: Unity-Logo deaktiviert, eigenes Logo `logo_splash.png` (als Sprite importiert) in der Logos-Liste der Splash-Image-Einstellungen, dunkelvioletter Hintergrund.
- Logo, Icon und Radar-Grafiken selbst erstellt, liegen in `Assets/FlyingDutchman/UI/Branding`.
- Windows-Build über *File > Build Profiles* mit `FlyingDutchmanScene` als einziger Szene, außerhalb des Editors getestet.