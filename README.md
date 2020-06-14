Provides up to 10 counters that can be incremented, decremented and reset using keyboard shortcuts.  
They are written in text files that can be used by programs that read from text files, like OBS.
The console window can be minimized.

**Usage**  
Press the modifier-key (or keys) + the key of the counter you want to manipulate  
Example: PageUp + NumKey 1 => Increments Counter 1

**Default keys**  
Counters = NumPad-Keys  
Increment = PageUp  
Decrement = PageDown  
Reset = Delete  

**Config**  
Run the app once to get a config.json.  
ShowConsoleOutput > Set this to false if you don't use the console to save performance  
Keys > You can set custom keys for everything. Check https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes for key-codes. You can use hex-values like 0x11 in the json. Leave the second modifier at 0 if you don't want to use it.  
CounterDefaultValue > The default value is the value that counter will start at, and reset to via the reset key  

You can also use arguments via batch-files or shortcut, if you need different sets of default values:  
...\CountOnKeypress.exe "True" "0" "0" "0" "0" "0" "0" "0" "0" "0" "0"  
The first argument sets ShowConsoleOutput, the remaining set the default values of the counters.  
You don't have to provide a value for every counter.  
Arguments override values of the config.json, but keys are always read from config.json.
