Usage:
Press PageUp + Num1 -> Counts counter 1 up and writes it's value into counter1.txt.
Use that file in obs.
Profit.

Count up key is PageUp, Count down key is PageDown, Reset key is Delete.
All NumKeys are mapped to counters.
The console can be minimized.

Some stuff can be changed via args, the defaults are:
...\CountOnKeypress.exe "True" "0" "0" "0" "0" "0" "0" "0" "0" "0" "0"
The first defines if console-output is used, set it to False if you don't want to use that.
The others set the default value for every counter. That is the starting value and the value the reset key resets the counter to.
