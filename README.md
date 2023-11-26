MorseCraft.exe is a command line tool for translating text into Morse code or vice versa.

The execution of the program requires a few command line parameters. You can run the tool directly in the directory.


Parameters:
-m : Converts regular text into Morse code.
-t : Converts Morse code into regular readable text.
-file : Loads the text or Morse code to be processed from a file.
-text : The text or Morse code to be processed can be passed directly in the command line. The text must be enclosed in quotation marks.
-a : Outputs the result in acoustic Morse signals.
-v : Outputs the result readable in the command line.
-dit : Determines the length of a dit (default value = 100).
-save : Saves the result in a file.
-y : Overwrites the file automatically if it already exists.
-h : Displays the help.
-version : Displays the program version
-dir : Displays a translation table for Morse code

Examples:
.\MorseCraft.exe -m -text "Hello World" -a -v : Outputs the text "Hello World" as readable Morse code in the command line and outputs the corresponding acoustic signals.

.\MorseCraft.exe -t -file "C:\morsecode.txt" -v -save "C:\klartext.txt" -y : Loads the file morsecode.txt and converts the Morse code into text. Saves the text in klartext.txt and displays the value on the screen.

I am happy if you participate in this project. :-)
