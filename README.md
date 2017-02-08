Name: Victor Yu
Programming Assignment 1, Part 1

A. Required elements
- Use either the arrow keys or "WASD" to tilt the board
- The goal of the game is to move the ball into the green circle to win
- There are what I call "wind forces" that will push the ball away from the green circle. Occasionally, they can be strong enough to push the ball off the platform.
- I allow a maximum tilt of 30 degrees.
- Hit 'Q' or ESC to quit
- Hit 'R' to restart

B. Additional elements
- There are what I call "wind forces" that will push the ball away from the green circle. Occasionally, they can be strong enough to push the ball off the platform.

C. Known issues
- The "wind forces" I made don't look the way I want them to. I'd like to make them look more like "streaks", but instead I settled on a particle system that looks like it's "shooting out wind."
- Trying to quit the game by using 'Q' or ESC will not work in the Unity editor mode. According to some posts on Unity Answers, this will only work in an executable of the game.
- Similarly, the lighting dims after restarting the game in editor mode, but works correctly in an executable.

D. External resources
- Other than using the public Unity Answers forum and Unity tutorials, I used two youtube tutorials to help implement the plane tilt and "wind", respectively:
- https://www.youtube.com/watch?v=5fZnwmcdAR8
- https://www.youtube.com/watch?v=LEhOiLW_API
