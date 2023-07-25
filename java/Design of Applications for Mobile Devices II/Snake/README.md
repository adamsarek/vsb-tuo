# Snake (Android game, written in Java)
The main objectives of the game are to try to feed the snake as much as possible and at the same time stay alive as long as possible. When the snake hits itself the game is over.

The game has a few basic rules:
- the snake lengthens by 1 block when it eats an apple
- the snake dies when it hits itself
- the snake does not die when it reaches the end of the screen but it appears on the other side

Features:
- Canvas - used for rendering game scene
- SharedPreferences - used for saving each attempt on device when finished and player has access to it over highscores section
- Gesture - the snake is maneuvered by swipe gesture across the screen
- Sound effects (may be disabled by the mute button)
- GUI - the user interface is intuitive and easy to use, there are 3 activity windows - Main, Game & Highscores
- GPS - used for sentimental purposes as for example when the player wants to know where he/she was at the time he/she achieved that score
- Threads - used for smooth gameplay while looping over game cycles (5 game updates per second)