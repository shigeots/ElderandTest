# ElderandTest

### In the development of the game for the technical test, the following boss requirements have been completed.

- The aerial patrol of the gargoyle. Requirement to move the gargoyle slightly beyond the action area and back before performing an action has been met.
- The actions of the gargoyle (fly, land, take off, fireball from the ground, fireball from the air, air dive, claw attack).
- Implementation of the logic for the behavior and decision of the gargoyle. All the aspects requested were taken to make a decision of the action. Actions by state (in the air or on the ground), the percentages of actions and not to repeat the last action performed.
- Left order variables visible and sorted to help ease in the editor. The variables are located in the BossCoreController script.
- Fireball explosion effect with particle system has been implemented.

### Additions
- The boss will die when he runs out of health. You will also be able to kill the boss with the button in the editor found in the BossCoreController script.
- The boss can cause the player with each of his attacks with their respective points of damage.
- Implemented the effects with the received sprites. Back dust, Heavy dust, and Fire cast.
- Added flicker effect when the gargoyle takes damage.

### Extra assets used.

- The CineMachine was used to give the effect of shaking when the gargoyle falls to the ground on the air dive.
- DotWeen was used to animate the player health bar when take damage from boss attack. Also to animate the game over screen.
- TextMeshPro used for interface texts.
- The TilePalette was used to create the game's scenery.

### Controls:
- A and D Key for movement.
- J Key to attack.
