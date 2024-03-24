# Asteroid Crashers
## Team Bracket

### Project Objective
Our goal for this project was to design a video game in the Unity game engine with secure software engineering principles for our Secure Software Engineering course project.

This involves architecture and design, code review, risk analysis, penetration testing, and feedback.

Our main objective was to secure the high scores aspect of the game to make it harder for attackers to modify their scores. This involved all of the processes listed above.
<p align="center"><img src="https://github.com/Dunnatello/AsteroidCrashers/blob/main/GitHub%20Readme%20Data/High%20Scores%20Scene.png" width="750"></img></p>
<p align="center"><b>Figure 1:</b> Top scores will appear in the high scores scene. Scores are encrypted and saved to file.</p>

## Asteroid Crashers
The game, <i>Asteroid Crashers</i>, features an astronaut that has to avoid being hit by asteroids by either moving out of the way or shooting asteroids to gain points. With only three lives, the astronaut must try to survive as long as possible. Once the astronaut has been hit by an asteroid or falls off the map three times, the game will end and the player will be able to see their score and a list of high scores.
<p align="center"><img src="https://github.com/Dunnatello/AsteroidCrashers/blob/main/GitHub%20Readme%20Data/Asteroid%20Crashers%201.0.1.png" width="750"></img></p>
<p align="center"><b>Figure 2:</b> Players use a rocket launcher to destroy falling asteroids.</p>

## Conclusions And Lessons Learned
### This is an excerpt from the final report. See the full report [here.](https://github.com/Dunnatello/AsteroidCrashers/blob/main/GitHub%20Readme%20Data/Asteroid%20Crashers%20Final%20Report.pdf)
### Advanced Encryption Standard (AES)
In this project, we learned how to encrypt and decrypt files using AES (Advanced Encryption Standard) encryption which utilizes the same key to encrypt and decrypt. Since it uses the same key, it's a symmetric encryption, the key is called the “secret” and is kept secret from third parties. We are using this encryption algorithm to encrypt the high scores, but we are unable to hide the private cryptographic key without the use of dedicated servers. Due to this, the key is hardcoded, but most users would be discouraged from modifying the scores file considering the effort required.
### Intermediate Language to C++ Compiler
Using IL2CPP in this project has helped with obscuring the code from potential attackers. Initially during development, we thought that the IL2CPP compiled build was 700 MB compared to the Mono build’s size of 100 MB. However, we discovered that the IL2CPP build included a folder titled: “Asteroid Crashers_BackUpThisFolder_ButDontShipItWithYourGame” that included all of the converted C# to C++ code. 

After removing this folder, the game size was reduced to a more reasonable size of 130 MB. If we had included this in the compressed game folder, hackers would have had access to the C++ source code directly. Therefore, it is important for developers to understand the file structure that comes with switching compilers and only include files in release builds that are absolutely necessary for the game to function properly.
### Anti-Cheat
Creating an anti-cheat for the game required a lot of contemplation in order to understand potential attack vectors that attackers could use. By focusing on securing the high scores aspect of the game, we were able to design an anti-cheat system that would protect that section of the game while other sections of the game were left unprotected due to time constraints.

Although we worked to make sure that the number of asteroids destroyed was not modified, we overlooked securing the “time survived” variable. After discovering this exploit, we promptly patched the issue. Therefore, the anti-cheat system created for the game can be expanded in future versions to include other aspects of the game such as the lives, weapon, and movement systems. In the development of this project, we learned how to create a viable anti-cheat that discourages a majority of potential attackers.

Note: The anti-cheat consists of validation checks that are present in [Assets/Game/Scripts/Game/GameManager.cs](https://github.com/Dunnatello/AsteroidCrashers/blob/main/Assets/Game/Scripts/Game/GameManager.cs)  

The validation checks are done in realtime and after the player finishes the game. Realtime checks are done in the <b>Update()</b> method (checked every frame) while final checks are done in the <b>ScoreValidation()</b> method (checked once the game is completed).  

More information about the anti-cheat as well as information on the penetration testing can be found in the final report referenced above.  
<p align="center"><img src="https://github.com/Dunnatello/AsteroidCrashers/blob/main/GitHub%20Readme%20Data/Modification%20Detected.png" width="750"></img></p>
<p align="center"><b>Figure 3:</b> When the game is improperly modified using third party software, this screen will appear.</p>

### Final Words
Secure software engineering is a continuous process that requires all developers to consider how their code can be used. Although we were able to add anti-cheat measures to our game to prevent attackers from influencing the high score directly, a determined and knowledgeable person could eventually find a way to influence the score determination process as shown in our penetration testing. We were able to mostly secure the high score process to ensure that high scores are validated and that memory manipulation of the high score variables causes the game to end.
However, attackers could influence other aspects of the game if they were determined enough as demonstrated in our penetration testing. Without server validation, securing a local game is difficult since the attacker already has all of the resources that they need by downloading the game. Applying secure software principles requires dedication from developers in order to continuously improve their software to compete against committed attackers. Although it is not possible to completely thwart attackers, developers can design and develop their programs to be written as securely as possible using similar techniques that were used in this project.

## Team Members
| Team Member  | GitHub Profile  | Roles |
| ------------- | ------------- | ------------- |
| Eric Diep | <p align="center"><img src="https://avatars.githubusercontent.com/u/109764701" width="40"></img><br>[@eric-diep](https://github.com/eric-diep)</p> | Penetration Testing, Team Contact, Documentation |
| Michael Dunn | <p align="center"><img src="https://avatars.githubusercontent.com/u/11823777" width="40"></img><br>[@Dunnatello](https://www.github.com/Dunnatello)</p> | Project Lead, Game Development, Documentation |
| Darius Haynes | <p align="center"><img src="https://avatars.githubusercontent.com/u/143749525" width="40"></img><br>[@SSGHOKAGE](https://github.com/SSGHOKAGE)</p> | Penetration Testing, Risk Analysis |
| Talha Khan | <p align="center"><img src="https://avatars.githubusercontent.com/u/90275404" width="40"></img><br>[@NotTK](https://www.github.com/NotTK)</p> | Game Development, Risk Analysis   |
| Zachary Neal | <p align="center"><img src="https://avatars.githubusercontent.com/u/144372211" width="40"></img><br>[@zneal2002](https://github.com/zneal2002)</p> | Penetration Testing, Risk Analysis |

## Known Issues
- Asteroids will regularly stop and float above the platform for a second before impact. We tried fixing this during development, but were unable due to time constraints.

## Unity Information
Universal Render Pipeline  
Originally developed in Unity 2023.1.11f1 but upgraded to 2023.2.11f1.

## Credits
### Assets
Free:  
9t5: [9t5 PBR Textures Freebies](https://assetstore.unity.com/packages/2d/textures-materials/9t5-pbr-textures-freebies-171062)  
Tatermand: [Space Game Art Pack (Extended)](https://opengameart.org/content/space-game-art-pack-extended)  
EVPO Games: [2D Pixel Asteroids](https://assetstore.unity.com/packages/2d/environments/2d-pixel-asteroids-136477)  
Sean Dulley: [Diverse Space Skybox](https://assetstore.unity.com/packages/2d/textures-materials/diverse-space-skybox-11044)  
Jean Moreno: [Cartoon FX Remastered Free](https://assetstore.unity.com/packages/vfx/particles/cartoon-fx-remaster-free-109565)  
3d.rina: [2D Sci-Fi Weapons Pack](https://assetstore.unity.com/packages/2d/textures-materials/2d-sci-fi-weapons-pack-22679)  
COPYSPRIGHT: [2D Character - Astronaut](https://assetstore.unity.com/packages/2d/characters/2d-character-astronaut-182650)  

### Sounds
Paid:  
Gamemaster Audio: [Fun Casual Sounds](https://assetstore.unity.com/packages/audio/sound-fx/fun-casual-sounds-64048)  
(Two sounds used)  

Free:  
MATRIXXX_: [Retro, Explosion 05.wav](https://freesound.org/people/MATRIXXX_/sounds/441497/)  
Jarusca: [Rocket Launch](https://freesound.org/people/Jarusca/sounds/521377/)  
Sophia_C: [Pistol Dry Fire](https://freesound.org/people/Sophia_C/sounds/467183/)  

### Music
"The Lift" Kevin MacLeod (incompetech.com)  
Licensed under Creative Commons: By Attribution 4.0 License  
http://creativecommons.org/licenses/by/4.0/  

"Canon In D (Interstellar Mix)" Kevin MacLeod (incompetech.com)  
Licensed under Creative Commons: By Attribution 4.0 License  
http://creativecommons.org/licenses/by/4.0/  

"Bleeping Demo" Kevin MacLeod (incompetech.com)  
Licensed under Creative Commons: By Attribution 4.0 License  
http://creativecommons.org/licenses/by/4.0/  
