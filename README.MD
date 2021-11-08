# Not Keeper's Needs - Fix #
Fix for the [Not Keeper's Needs](https://github.com/Hy60koshk/GraveKeeper-NotKeepersNeeds) Mod so that it functions properly in newer releases.

## [ Compatibility ] ##
* Graveyard Keeper 1.402
* All DLC Installed
  * Also tested without any DLC

## [ Original Features ] ##
* Game timescale (speed up and slow down how fast time passes)
  * General and sleep time seperately configurable
  * Default value for TimeScale also configurable
* Price inflation control when buying
* Default character speed
* Energy drain and regen
* Health damage and regen
* Tech orbs drop amount
  * Multiplier
    * Rounding up or down
  * Constant bonus amount 
    * Configurable per color
    * Extra setting to manage mostly extra blue drops
* Crafting speed
  * Toggleable
* All interactions speed (probably unstable)
* Sprint
  * Configurable speed when sprinting
  * The key to use sprint (LeftShift by default, but you can set any value that Unity can parse as a KeyCode)
  * Toggle/hold sprint key
  * Energy cost for sprinting
* Hotkeys to
  * Add gold coins
  * Remove weekly church prayer flag (to pray once more, and then once more, and then more, until the Pride day ends)
  * Toggle 10x timescale

## [ Compilation ] ##

### [ Instructions ] ###

There are 2 seperate projects in this solution:
* The actual mod - regarding the file that patches the game
* The Configurator - to assist in configuring the mod

#### [ Actual Mod ] ####

`%project_path%` = `%repo_path%/notkeepersneeds/`

To compile this project, it requires several references. Because these are linked to a specific location on the system where the compilation is done. 

So to make it work on your system you must most likely alter these references. Or being more precise: alter the location of where the referenced DDL-files are stored, being `%game_path%\Graveyard Keeper_Data\Managed`.

The game specific DLL's that are referenced in this mod are:

* Assembly-CSharp.dll
* Assembly-CSharp-firstpass.dll
* SmartBearAssembly.dll
* UnityEngine.dll
* UnityEngine.CoreModule.dll
* UnityEngine.InputLegacyModule.dll
* UnityEngine.UI.dll
* 0Harmony.dll 
  
The last one will only be present **after** QModManager is installed, the rest are included with the game itself.

Assuming that you are using Visual Studio to compile this project, you can just:
* Go to the `Solution Explorer`
* Right click the `References` item (located under the `NotKeepersNeeds` project)
* Click `Add Reference`
* Click `Browse`
* Navigate to the `%game_path%\Graveyard Keeper_Data\Managed\` folder
* Add all the DLL's specified above. (you can use `ctrl` to select multiple files at once)
* Click `Ok` to confirm adding the files
* This will in fact not re-add the same references, but rather update the paths of the ones that were already listed
* Once this is done, you should be able to build the project
* This will result in a new compiled .DLL file, called `NotKeepersNeeds.dll`, located in:
  - `%project_path%/bin/Debug` - when running a Debug build 
  - `%project_path%/bin/Release` - when running a Release build 

#### [ Configurator ] ####

`%project_path%` = `%repo_path%/NKN-Configurator/`

This should compile without issues, since it is not dependent on the game specific DLL's. Just run build and normally it *should* work. 

### [ Output ] ###

This solution consists of 2 seperate projects:

* Not Keepers Needs - the actual mod
  * Compiles to `%repo_path%/notkeepersneeds/bin/debug/NotKeepersNeeds.dll`
* NKN-Configurator - a wizard to assist in the configuration (optional)
  * Compiles to `%repo_path%/NKN-Configurator/bin/debug/NKN-Configurator.exe`

For the proper functioning of the mod, two additional files are required, which can be found in `notkeepersneeds/files/`:

### [ Additional files ] ###

* `mod.json`, a file required for QModManager, containing version info among others.
* `config.txt`, can also be automatically generated (see **`Installation`**) 

## [ Installation ] ##

1. Make sure QModManager is [installed and configured](https://www.nexusmods.com/graveyardkeeper/mods/1) properly.
2. Create a new directory named `NotKeepersNeeds` in the `Qmods` directory in your Graveyard Keepers.
3. Copy all (either 3 or 4) files mentioned in the previous section to the that directory (`%game_path%\QMods\NotKeepersNeeds\`).
4. If you decided to copy the config.txt file manually, the mod will now work with the default values.
5. If not, run NKN-Configurator.exe in your mod folder at least once to generate the `config.txt` file.

## [ Configuration ] ##
Configuration of the mod can be done by manually changing the configuration values in the `config.txt` file. 

There's also a configuration tool present - the `NKN-Configurator` part of the solution - that can assist you to set the available configuration options if you do not feel comfortable editing the configuration file manually.

If the configuration tool is ran from the `%game_path%\QMods\NotKeepersNeeds\` folder, it will load the existing settings, allowing you to edit them.

If you rather have it run pre-seeded with the default configuration values just delete the existing `config.txt` before running `NKN-Configurator.exe` 

And here's an [Article](https://www.nexusmods.com/graveyardkeeper/articles/2) that contains information and instructions regarding the available configuration options and values, written by the original [mod author](https://www.nexusmods.com/graveyardkeeper/users/3662701).


## [ Credits ] ##
[Hy60koshk](https://github.com/Hy60koshk) / [megarunner](https://www.nexusmods.com/graveyardkeeper/users/3662701) for the  [original mod](https://github.com/Hy60koshk/GraveKeeper-NotKeepersNeeds)

