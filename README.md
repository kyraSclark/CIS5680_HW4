# *Whack-A-Platypus!*

### Saksham Nagpal & Kyra CLark

### Game Features
Our game is a variation of the class arcade game "Whack-a-Mole." There is a time limit, and when both players declare that they are ready, the timer begins to count down. In that time, the players must attempt to shoot the "Perry" from the assemblage of platypi that appear every few seconds. Unlike the normal platypi (which decreases your points if you hit them), Perry the Platypus is wearing a small hat and will award players with points and one extra bullet if shot.
#### A win/loss condition
When the timer ends, the player with the most points wins! A normal platypus will subtract 1 point from the player's score, Perry the platypus will award the player with 5 points and 1 extra bullet, and hitting Doofenshmirtz (the evil scientist) will not award the player with any additional points but will give the player 5 extra bullets.
#### At least 3 new prefabs
There are several prefabs in our game:
* Perry the Platypus Prefab
* Platypus Prefab
* Doofenshmirtz Prefab
* Mask Prefab (The mask prefab is what is used to make the sleek transparent up-and-down animation)
* Game Over Prefab (The screen that appears with the winner and final winning score. It is also what holds the 'Play Again' button that appears on the player client screens and restarts the game.)
* Global State Prefab (This prefab keeps track of the game, if the client is ready, and what state the game is in (start, playing, or game over.))
#### 2 goals
* The main goal is to score more points than your opponents. This is achieved by trying to hit the Perry and avoiding the Platypi.
* The secondary goal is to not run out of bullets. If you run out of bullets, you cannot get more points or bullets, so you will be stuck until the game ends. In order to get more bullets, you can try to hit a Perry, which will award you with one additional bullet, or a Doofenshmirtz which will award you 5 bullets.
* Another secondary goal is to hit the Doofenshmirtz. The Doofenshmirtz will award players with a helpful power-up, as described below.
#### 2 resources
* The primary resource is the bullets. Players start with a limited number of bullets (15). If you run out of bullets, you will be stuck until the end of the game. Players should try to be aware of how many bullets they have left and hit a Perry or (especially) a Doofenshmirtz to get more bullets.
* The second resource is the power-up that comes from hitting the Doofenshmirtz. If players successfully collect a Doofenshmirtz by hitting it, their chance of getting a Perry increases. There is not a guaranteed chance that a Perry will pop up every round, making the chances of accumulating points slim. Using a Doofenshirmtz will increase the chance that there will be a Perry every single round, giving you more chances to collect points.
#### The game supports at least 2 mobile devices (the game is networked)
The game successfully supports multiplayer, using the Photon Network. It plays best with 1 - 3 players.
#### Ability to restart the game (resetting the game for all the players/devices)
We implemented the ability to restart the game after the game ends. Once the timer reaches 0, the winner and their final score are displayed on screen. Below it is the 'Play Again' button. Hitting that button will reset the timer, points, and bullets. Then, the 'Ready' button will be back on screen. Once all players have hit the 'Ready' button, the game will start again and the timer will begin counting down again.


### Additional features beyond the base framework
We implemented a few extra features in our game that are worthy of consideration.
* While we started with based models found online, we had to hand alter them to fit our game, and even model some elements of Doofenshmirtz and Perry the platypus ourselves.
* We implemented a Mask prefab that created a smooth transparent animation that the targets move up and down through. Instead of a simple spawn and despawn, this transparent animation took a significant amount of work.
* Our game also has sound effects! Hitting a platypus will trigger the "Platypus growl" sound, hitting a Doofenshmirtz will trigger the "Doofenshmirtz Evil Incorporated Jingle" sound, and hitting a Perry will trigger the "*PERRY* the platypus" sound effect.


_________
This project is based on https://github.com/Unity-Technologies/arfoundation-samples#interaction

