

using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.Text;
using System.Text.RegularExpressions;

namespace MorseCraft
{
    public class Program
    {

        public static readonly List<Morse> TranslationTable = CodeList.Initialize();

        static void Main(string[] args)
        {
            // Loads the translation table
            CodeList.Initialize();

            /*
                The command line parameters are defined here. The variables are usually named like
                the command line parameters themselves. It is best to refer to the operating instructions.
            */
            bool t = CmdParamExists(args, "-t");
            bool m = CmdParamExists(args, "-m");
            bool a = CmdParamExists(args, "-a");
            bool v = CmdParamExists(args, "-v");
            bool file = CmdParamExists(args, "-file");
            bool text = CmdParamExists(args, "-text");
            bool dit = CmdParamExists(args, "-dit");
            int ditLength = 100;
            bool h = CmdParamExists(args, "-h");
            bool dir = CmdParamExists(args, "-dir");
            bool save = CmdParamExists(args, "-save");
            bool y = CmdParamExists(args, "-y");
            string saveFilePath = string.Empty;
            bool version = CmdParamExists(args, "-version");


            // The text or Morse code to be processed.
            string textOrMorsecode = string.Empty;

            // Einlesen der Komandozeilenparameter
            if (text && file)
            {
                Console.WriteLine("You can read in either a file or a string. But not both at the same time.");
                return;
            }

            if (h)
            {
                t = false;
                m = false;
                a = false;
                v = false;
                file = false;
                text = false;
                dit = false;
                dir = false;
                version = false;
            }

            // Verarbeitung der Komandozeilenparameter         

            if (file)
            {

                string filePath = GetSubCmdParam(args, "-file");
                if (filePath == String.Empty)
                {
                    Console.WriteLine("Please enter a valid file path.");
                    return;
                }

                if (System.IO.File.Exists(filePath) == false)
                {
                    Console.WriteLine("The file doesn't exists.");
                    return;
                }

                textOrMorsecode = System.IO.File.ReadAllText(filePath);
            }

            if (save)
            {
                saveFilePath = GetSubCmdParam(args, "-save");
                if (saveFilePath == String.Empty)
                {
                    Console.WriteLine("Please enter a valid file path.");
                    return;
                }

                if (System.IO.File.Exists(saveFilePath) && y == false)
                {
                    Console.WriteLine("The file already exists.");
                    Console.Write("Should the file be overwritten? (y/N) ");
                    string? userInput = Console.ReadLine()?.ToLower();
                    if ((userInput != "y" && userInput != "yes"))
                    {
                        Console.WriteLine("Process was aborted by user.");
                        return;
                    }
                }
            }

            if (text)
            {

                textOrMorsecode = GetSubCmdParam(args, "-text");
                if (textOrMorsecode == String.Empty)
                {
                    Console.WriteLine("You need to specify the text you want to translate.");
                    return;
                }

            }

            if (dit)
            {

                string ditLengthUnconverted = GetSubCmdParam(args, "-dit");
                if (ditLengthUnconverted == String.Empty)
                {
                    Console.WriteLine("Please enter a valid dit lenght.");
                    return;
                }

                if (Regex.IsMatch(ditLengthUnconverted, @"^\d+$") == false)
                {
                    Console.WriteLine("The lenght of a dit should be a number.");
                    return;
                }

                ditLength = int.Parse(ditLengthUnconverted);

                if (ditLength < 0)
                {
                    Console.WriteLine("The entered ditlength was resetted to 100.");
                    ditLength = 100;
                }

                if (ditLength == 0)
                {
                    a = false;
                }

            }

            // Die Ditlenght ist nur relevant, wenn es auch eine akustische ausgabe gibt
            if (a == false)
            {
                ditLength = 0;
            }

            if (t && m)
            {
                Console.WriteLine("You cannot combine -m and -t");
                return;
            }

            // Prüft ob die Mindestkonfiguration vorliegt um das Programm auszuführen
            if (file == false && text == false && h == false && dir == false && version == false)
            {
                Console.WriteLine("You need to specify what text you want to translate");
                return;
            }

            if (m)
            {
                List<Morse> convertedTextOrMorse = TextToMorse(textOrMorsecode);
                ShowMorseCode(convertedTextOrMorse, v, a, ditLength, save, saveFilePath);
                return;
            }

            if (t)
            {
                MorseToText(textOrMorsecode, save, saveFilePath);
                return;
            }

            if (h)
            {
                Console.WriteLine("-m (-file or -text)         | Convert text to morse");
                Console.WriteLine("-t (-file or -text)         | Convert morse to text");
                Console.WriteLine("-file (filepath)            | Read the text/morsecode from a file");
                Console.WriteLine("-text \"your text or morse\"  | Read the text/morsecode from the comandline");
                Console.WriteLine("-a                          | Outputs the Morse code acoustically");
                Console.WriteLine("-v                          | Outputs the Morse code / text in the console");
                Console.WriteLine("-dit                        | Sets the ditlength - default is 100");
                Console.WriteLine("-save (filepath)            | Saves the output o a file");
                Console.WriteLine(
                    "-y                          | does not ask whether an existing file should be overwritten");
                return;
            }

            if (dir)
            {
                ShowCodeList(v, a, save, saveFilePath, ditLength);
                return;
            }


            if (version)
            {
                Console.WriteLine("MorseCraft by Dominik Zerbe");
                Console.WriteLine($"Version: 1.0.1");
                return;
            }


        }

        /// <summary>
        /// Evaluates whether a command line parameter exists
        /// </summary>
        /// <param name="suppliedParams">The string[] that contains all command line parameters.</param>
        /// <param name="searchedParam">The cmd line param that what searched for</param>
        /// <returns>true if it Exists, false if it doesnt exists</returns>
        public static bool CmdParamExists(string[] suppliedParams, string searchedParam)
        {
            bool paramExists;
            if (Array.IndexOf(suppliedParams, searchedParam) == -1)
            {
                paramExists = false;
            }
            else
            {
                paramExists = true;
            }

            return paramExists;
        }

        private static async void ShowCodeList(bool printText, bool playSound, bool saveToFile, string filePath,
            int ditLength)
        {


            // Das sind die Geschwindigkeitseinheiten beim Morsen.
            int dit = ditLength;
            int dah = 3 * dit;
            int doh = 7 * dit;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (Morse morse in TranslationTable)
            {

                if (morse.Visible == false)
                {
                    continue;
                }

                if (printText)
                {
                    Console.Write($"{morse.Text}   -->   ");
                }

                stringBuilder.Append($"{morse.Text}   -->   ");

                ;
                foreach (char c in morse.Code)
                {

                    if (printText)
                    {
                        Console.Write(c);
                        stringBuilder.Append(c);
                    }

                    if (playSound)
                    {
                        switch (c)
                        {
                            case '.':
                                if (playSound)
                                {
                                    await PlayBeep(dit, 800);
                                }
                                else
                                {
                                    Thread.Sleep(dit);
                                }

                                break;
                            case '-':
                                if (playSound)
                                {
                                    await PlayBeep(dah, 800);
                                }
                                else
                                {
                                    Thread.Sleep(dah);
                                }

                                break;
                            case '/':
                                Thread.Sleep(doh);
                                break;
                            default:
                                break;
                        }

                    }

                }

                if (playSound)
                {
                    Thread.Sleep(doh);
                }

                if (printText)
                {
                    Console.WriteLine();
                }

                stringBuilder.AppendLine();
            }

            if (saveToFile)
            {
                try
                {
                    System.IO.File.WriteAllText(filePath, stringBuilder.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }

        public static void MorseToText(string codeText, bool saveToFile, string filePath)
        {

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string code in codeText.Split(" "))
            {

                if (string.IsNullOrEmpty(code) == false)
                {
                    Morse? morseCode = TranslationTable.FirstOrDefault(item => item.Code == code);
                    if (morseCode != null)
                    {
                        stringBuilder.Append(morseCode.Text);
                    }
                    else
                    {
                        stringBuilder.Append($"[unknown code ({code})]");
                    }
                }
            }

            if (saveToFile)
            {
                try
                {
                    System.IO.File.WriteAllText(filePath, stringBuilder.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public static async Task ShowMorseCode(List<Morse> codeList, bool printToConsole, bool playSound, int ditLength,
            bool saveToFile, string filePath)
        {

            // Das sind die Geschwindigkeitseinheiten beim Morsen.
            int dit = ditLength;
            int dah = 3 * dit;
            int doh = 7 * dit;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (Morse code in codeList)
            {
                foreach (char c in code.Code)
                {

                    if (printToConsole)
                    {
                        Console.Write($"{c}");
                    }

                    stringBuilder.Append(c);

                    switch (c)
                    {
                        case '.':
                            if (playSound)
                            {
                                await PlayBeep(dit, 800);
                            }
                            else
                            {
                                Thread.Sleep(dit);
                            }

                            break;
                        case '-':
                            if (playSound)
                            {
                                await PlayBeep(dah, 800);
                            }
                            else
                            {
                                Thread.Sleep(dah);
                            }

                            break;
                        case '/':
                            Thread.Sleep(doh);
                            break;
                        default:
                            break;
                    }
                }

                Thread.Sleep(dah);
                Console.Write(" ");
                stringBuilder.Append(" ");
            }

            if (saveToFile)
                try
                {
                    File.WriteAllText(filePath, stringBuilder.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }


        private static async Task PlayBeep(int duration, int frequence)
        {
            using (var player = new WaveOutEvent())
            {
                var sineWaveProvider = new SignalGenerator() { Frequency = frequence, Type = SignalGeneratorType.Sin };
                player.Init(sineWaveProvider);
                player.Play();

                Thread.Sleep(duration);
                player.Stop();
            }

        }

        public static List<Morse> TextToMorse(string plainText)
        {

            List<Morse> textAsCodeList = new List<Morse>();

            foreach (char c in plainText.ToUpper())
            {

                // Suche das richtige Element aus der Übersetzungsliste
                Morse? code = Program.TranslationTable.FirstOrDefault(item => item.Text == c.ToString());
                if (code != null)
                {
                    textAsCodeList.Add(code);
                }

            }

            return textAsCodeList;
        }

        /// <summary>
        /// Gets the sub commandline parameter like a path or something.
        /// example: -text "hello world" would return hello world.
        /// </summary>
        /// <param name="allParams">all command line parameters</param>
        /// <param name="mainParam">the param where we would like to get the sub param</param>
        /// <returns>the sub param or string.empty if it not exists</returns>
        public static string GetSubCmdParam(string[] allParams, string mainParam)
        {
            int indexOfMainParam = Array.IndexOf(allParams, mainParam);

            if (allParams.Length <= indexOfMainParam + 1)
            {
                return string.Empty;
            }
            
            return allParams[indexOfMainParam + 1].Replace("\"", String.Empty);
        }
    }
}
