# Time-scripts
Scripts for Time Unity game jam project

This was my submission to the GameDev.tv Community Jam, that I made in the last two weeks of May 2020. All code and art was done by me, implemented in the Unity game engine, with sounds made using LeshySF (https://www.leshylabs.com/apps/sfMaker/).
The game is an arcade-style shooter, with a flow of time that slows down as enemy leeches reach the clocks center.

A brief summary of the key components:
The response of physics objects to the flow of time is controlled by a custom component, found in TimeFlow.cs, paired with a built-in Rigidbody2D component. These components receive changes to the flow of time from TimeController.cs.

The enemies and player have shared components governing their health (Health.cs) and shooting (Gun.cs). 

Enemies have individual AI that implements the interface IAIBehaviour. This allows all enemies to be spawned and controlled by the same EnemySpawner.cs and EnemyController.cs.
