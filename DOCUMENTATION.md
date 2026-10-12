# Implementation Documentation

## Camera Movement
- Attached the camera to the spaceship prefab and positioned it for a third-person view.
- Locked and hid the cursor at startup. Pressing Escape unlocks and shows it again.
- Added Logging to keep track of Vector

**Implemented by:** Eric Drößiger

## Spaceship Controls
- Used Unity's Input System callbacks for movement, mouse-look, roll, and Escape.
- Applied movement as relative Rigidbody acceleration.
- Converted mouse-look and keyboard roll input to quaternion rotations and applied them with `Rigidbody.MoveRotation`. They are currently issued as separate rotation updates in `FixedUpdate`.

**Implemented by:** Eric Drößiger

## Spaceship Physics and Collisions
- Setup Player's Spaceship Rigidbody and Collision.
- Custom collision behavior for other ships: TBD.

**Implemented by:** Eric Drößiger





