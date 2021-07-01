- The starting money amount can be changed in the inspector on the MoneyController.
The starting amount is set to 5000 which I believe would be a sensible amount to start with.
It can be changed in the inspector before running the game when there is no save data yet so that testing is easier.

- The plots  are spawned in an adjusting Scroll View so that more that can be seen on the screeen can be spawn.

- The game data is saved on Unity's persistentDataPath on a .dat file (small-giant-garden.dat).
The Company is named as "Small Giant Garden Company" so that it can be tracked more easily

- The Time tracking between sessions is done using PlayerPrefs.

- Both the save file and the PlayerPrefs can be deleted for testing purposes via the Editor Tools.