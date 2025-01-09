## Minigolfer VR

### Overview
This project was created by Elise Kidroske as her capstone project for the Unity Learn VR Course. This project is a Minigolf VR game where players can play an 18-hole minigolf course in VR. This implementation is single player only although multiplayer may be considered during a future release or as a separate project.
This game is not play ready, see version 1.0 features for intended features and the roadmap section for where the project currently stands. This README file will be updated as progress is made. If you are a 3D artist or animator and would like to collaborate on this project, please feel free to reach out to me.

### Intended Features for Version 1.0
- Players can interact with objects in VR.
- Players can spawn a golf club in their hands using the grab button.
- Players can select a golf ball to play with using a golf ball vending machine with pushable buttons.
- When players hit their golf ball, their score is tallied for the current hole.
- Hitting the ball into the hole concludes a hole.
- Holes are played sequentially from 1 to 18 until the player finishes the game.
- The player cannot start a game without a golf ball.
- The player can respawn their golf ball should it yeet out of existence.
- Players will have a hand menu they can access at any time to change game settings, restart the game, view their score, and quit the game.
- Players will have the ability to switch from snap and continuous rotation.
- Players will have the ability to switch from continuous and teleport movement.
- Players collision handling.
- Mini Golf Physics and Collision Handling.
- Haptic feedback.
- In-Game Sounds.
- UI Sounds.
- VR UI Interaction.
- A fully modeled New England Boating Themed Mini Golf Course.
- Main Menu and Settings Menu.

### Core Gameplay Loop
- 1: Players start in the reception room of a mini golf course after clicking play from the main menu.
- 2: Players select one of six colored golf balls from a golf ball vending machine.
- 3: Once players have selected their ball, they can start the game by pressing a button and passing through a gate to the course.
- 4: Players play holes 1 through 18 sequentially.
- 5: Upon completion, players can return to the reception room, check their score, and play again.

### Project Contents
- **Main Project Scripts:** /Assets/Scripts/.
- **XR Related Scripts:** /Assets/Scripts/XR/.
- **UI Scripts:** /Assets/Scripts/UI/.
- **Main Game Event Handling:** /Assets/Scripts/Putting/Events/.
- **Main Game Logic:** /Assets/Scripts/Putting/Game/.
- **Main Game Interactables:** /Assets/Scripts/Putting/Interactable/.
- **Mini Golf Hole Scripts:** /Assets/Scripts/Putting/Hole/.
- **Main Project Prefabs:** /Assets/Prefabs/.
- **3D Models, Textures, and Prefabs:** /Assets/Models/.
- **Materials:** /Assets/Materials/.
- **Physics Materials:** /Assets/Physics_Materials/.
- **Main Testing Scene:** /Assets/Scenes/Main Test Room.unity.
- **Hand Menu Test Scene:** /Assets/Scenes/Hand Menu Test Room.unity.
- **Additional Scenes:** /Assets/Scenes/.

### Roadmap for Version 1.0
#### Current Progress
- Putting Player with ability to spawn golf club in hands.
- Ability for player to hit golf balls.
- Ability for player to complete golf holes.
- Core gameplay loop completed.
- Poke, Direct, and Ray VR Interactions.
- Teleport, Continuous, and Headtracking Locomotion.
- Player Hand Menu Interface.
- Score tracking and Scoreboard.
- Ability to View Game Progress, Teleport to Current Hole, and Restart Game from Hand Menu.
- Ball selection mechanic.
- Mini Golf Ball Vending Machine.
- Pushable VR buttons.
- Golf ball mesh and seven unique colors.
- Character collision. 

#### Remaining Work 
- 1: Player body sockets.
- 2: Game start sequence.
- 3: Improved putting physics.
- 4: Rework the start hole sequence.
- 5: Ability to restart current hole.
- 6: Ability to change the golf putter size through the hand menu interface.
- 7: Make hand menu open with buttons instead of by gaze (looking at hand menu opens it as of now).
- 8: Ability to choose between continuous rotation or snap rotation in the hand menu's options menu.
- 9: Ability to choose between teleport and continuous locomotion in the hand menu's options menu.
- 10: Ability to terminate application.
- 11: Haptic feedback for mini golf events.
- 12: Audio feedback for player interactions (hitting ball, selecting a ball from vending machine, etc).
- 13: Audio for UI interaction.
- 14: Audio for ambient noise.
- 15: Design work for all holes in the game.
- 16: Level design and production of 3D assets for the mini golf course.
- 17: Additional 3D objects for golf interactables.
- 18: Creation of Main Menu.
- 19: Release of V1.0.

