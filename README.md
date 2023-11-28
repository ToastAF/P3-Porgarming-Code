# P3-Porgarming-Code


This is an attempt at creating a game resembling the popular MMO League of Legends.
All 2D sprites and 3D models, that are not Unity primitives, are made by me.

All around, I spend around 40-50 hours on this project. Maybe more.

### Description
For this project I chose to make League of Legends, but worse. The point of the game is to kill the enemies, minions and turrets, earn gold to buy items, and then destroy their base to win the game. However, in my version I have only implemented earning gold and buying items, the enemies, minions and turrets, so there is no way to win the game, technically. Yet, you can still die from the enemies, so surviving is winning.

In the current version of the game, the player can move around, shoot abilities with the Q and W keys, damaging enemies and using mana, take damage from enemies, earn gold by killing enemies, and buy items in the shop with that gold, increasing the player's stats.

The project is made in Unity 2022.3.8f1, so to run it, have this version installed and download the project. Works on Windows computer.

### The game contains:
-A player that can move around using NavMeshSurface and NavMeshAgent<br/>
-Controlled by right mouse button<br/>
-Use Q and W to cast abilities<br/>
-Different types of enemies<br/>
-A turret that shoots the player at long range<br/>
-A minion that shoots the player at short range<br/>
-An enemy, which basically acts like a target dummy<br/>
-UI<br/>
-Health and mana bars dependant on player health and mana<br/>
-A toggleable shop menu using P<br/>
-Inventory able to hold 6 items<br/>
-Stats and gold count<br/>
-Icons for the abilities, which show their range, if you hover over them<br/>
-Terrain which the player walks around<br/>
-Ground<br/>

### All the scripts in the project:
-CameraControl.cs - Make the camera follow the player<br/>
-DestroySelf.cs - Destroys a gameObject after x seconds<br/>
-EnemyScript.cs - Put on enemies to control their health, stats and if things hit them<br/>
-InventoryManager.cs - Controls the 6 item inventory<br/>
-LookTowardsPlayer.cs - Makes a gameObject look towards the player<br/>
-MinionProjectileScript.cs - Controls the projectiles shot from minions<br/>
-MinionScript.cs - Controls the minions attacks and animation<br/>
-MouseHover.cs - Turns attack range rings on and off when hovering UI elements<br/>
-ParticlesQFollowProjectile.cs - Makes a particle system follow a Q projectile<br/>
-ParticlesWFollowProjectile.cs - Makes a particle system follow a W projectile<br/>
-PlayerAbilities.cs - Controls all the abilities that can be casted and their respective cooldown<br/>
-PlayerMove.cs - Controls the players movement and all the stats for the player<br/>
-QProperties.cs - Holds the stats of the Q projectile and controls its movement<br/>
-ShopScript.cs - Holds methods that are called in the shop UI to buy items<br/>
-SpriteBillboarding.cs - Turns 2D sprites toward the camera<br/>
-TurretScript.cs - Control the attacking and stats of the turrets<br/>
-WProperties.cs - Holds the stats of the W projectile and controls its movement<br/>
-WStatsCarryOver.cs - Holds stats carried from W projectile<br/>

### 3D models:
-A small red-hooded minion (made by myself)<br/>
-A turret with a big staff (made by myself)<br/>
-Unity primitives for the player, some enemies, ground, terrain, and projectiles<br/>

### Materials:
-Unity primitive materials for player, enemy, projectiles, and ground<br/>
-Particle materials using sprites made by myself for particle systems when shooting and hitting objects<br/>

### Scenes:
-There is only one Unity scene in the game


### Links used for the project:
Learning how to write in markdown:
https://docs.github.com/en/get-started/writing-on-github/getting-started-with-writing-and-formatting-on-github/basic-writing-and-formatting-syntax

Unitys scripting API:
https://docs.unity3d.com/ScriptReference/

Navmesh and moving the player with the mouse:
https://www.youtube.com/watch?v=KU2CKBlCAxQ
https://www.youtube.com/watch?v=u2EQtrdgfNs

Sprite billboarding in 3D:
https://www.youtube.com/watch?v=_LRZcmX_xw0

League of Legends’ own calculations for defense against damage in the game:
https://leagueoflegends.fandom.com/wiki/Armor
https://leagueoflegends.fandom.com/wiki/Magic_resistance

Blender shortcuts pie wheels:
https://blender.stackexchange.com/questions/153195/what-are-the-shortcuts-for-the-pie-menus

Blender hotkey cheat sheet to make the process easier:
https://www.blender.hu/tutor/kdoc/Blender_Cheat_Sheet.pdf
