In-Game Mod Configuration Menu and Custom UI Rendering Utility

> source: https://github.com/dk-chrono-ark-plugins/ChronoArk.MCM


### For Users:

This mod introduces an intuitive in-game configuration menu, allowing you to modify the settings of other mods within your game session. 


### For Modders:

> API Version: V1

This utility comes with two primary features:

**Automatic Configuration Migration**:
If you don't require custom UI elements, you don't need to make any changes. Your mod's settings will be automatically migrated by the Mod Configuration Menu (MCM) in a non-destructive manner. You can still edit the settings from within the game's own ModUI menu.

**Custom UI Elements and Pages**:
For those who want to introduce custom UI elements or pages, you can integrate MCM as a dependency within your project. Simply reference MCM and utilize its API to tailor the settings menu to your needs. Comprehensive API documentation is available both in-game and within the source code.


Note: More features will be released as I adapt my other mods using the MCM API.
