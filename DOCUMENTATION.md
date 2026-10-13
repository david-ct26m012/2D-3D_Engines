# Implementation Documentation

## Camera Movement
- Attached the camera to the spaceship prefab and positioned it for a third-person view.
- Locked and hid the cursor at startup. Pressing Escape unlocks and shows it again.
- Kept Logic to display input vector on the bottom of the screen.

**Implemented by:** Eric Drößiger

## Spaceship Controls
- Used Unity's Input System callbacks for movement, mouse-look, roll, and Escape.
- Applied movement as relative Rigidbody acceleration.
- Converted mouse-look and keyboard roll input to quaternion rotations and applied them with `Rigidbody.MoveRotation`. They are currently issued as separate rotation updates in `FixedUpdate`.

**Implemented by:** Eric Drößiger

## Spaceship Physics and Collisions
- Setup Player's Spaceship Rigidbody and Collision.
- Added Rigidbody to Spaceship Prefabs and buoys, defined mass and dampening.
- Added Collision where necessary.

**Implemented by:** Eric Drößiger





