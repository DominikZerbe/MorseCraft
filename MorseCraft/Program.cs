

using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;

namespace MorseCraft
{
    class Program        
    {

        private static List<MorseCodes> _codes = new List<MorseCodes>();
        public static List<MorseCodes> Codes
        {
            get { return _codes; }
            set { _codes = value; }
        }

        static void Main(string[] args)
        {

            // Definition der Komandozeilenparameter 

            // Lädt die Übersetzungstabelle
            CodeList.Initialize();

            // Komandozeilenparameter -> Morse zu Text
            bool t = false;

            // Komandozeilenparameter -> Text zu Morse
            bool m = false;

            // Komandozeilenparameter -> Morsecode akustisch ausgeben
            bool a = false;

            // Komandozeilenparameter -> Gibt den Morsecode in der Konsole aus
            bool v = false;

            // Komandozeilenparameter -> Der auszwertende Text / Morsecode soll aus einer Datei geladen werden
            bool file = false;

            // Komandozeilenparameter -> Der auszwertende Text / Morsecode soll aus der Kommandozeile übernommen werden
            bool text = false;

            // Komandozeilenparameter -> Die Standard Ditgeschwindigkeit wird vom Benutzer mitgeliefert
            bool dit = false;
            int ditLength = 100;

            // Komandozeilenparameter -> Gibt die Hilfe aus
            bool h = false;            

            // Komandozeilenparameter -> Gibt die Übersetzungstabelle aus
            bool dir = false;                            

            // Komandozeilenparameter -> Ob der Output in einer Datei gespeichert werden soll
            bool save = false;
            bool y = false;
            string saveFilePath = string.Empty;

            bool version = false;   

            // Einlesen der Komandozeilenparameter

            if (args.Length == 0 )
            { 
                h = true;
            }

            if (Array.IndexOf(args, "-t") != -1) {
                t = true;
            }

            if (Array.IndexOf(args, "-m") != -1)
            {
                m = true;
            }

            if (Array.IndexOf(args, "-a") != -1)
            {
                a = true;
            }

            if (Array.IndexOf(args, "-v") != -1)
            {
                v = true;
            }

            if (Array.IndexOf(args, "-y") != -1)
            {
                y = true;
            }

            if (Array.IndexOf(args, "-file") != -1)
            {
                file = true;
                text = false;
            }

            if (Array.IndexOf(args, "-text") != -1)
            {
                text = true;
                file = false;
            }

            if (Array.IndexOf(args, "-dit") != -1)
            {
                dit = true;
            }

            if (Array.IndexOf(args, "-dir") != -1)
            {
                dir = true;
            }

            if (Array.IndexOf(args, "-save") != -1)
            {
                save = true;
            }

            if (Array.IndexOf(args, "-version") != -1)
            {
                version = true;
            }

            if (Array.IndexOf(args, "-h") != -1)
            {
                h = true;
                t = false;
                m = false;
                a = false;
                v = false;
                file = false;
                text = false;
                dit = false;
                dir = false;
            }

            // Der Text / Morsecode der verarbeitet werden soll
            string textOrMorsecode = string.Empty;

            // Verarbeitung der Komandozeilenparameter         
            
            if (file)
            {
                // Der Pfad zur Datei sollte direkt nach dem File Parameter folgen
                int indexOfParam = GetParameterIndex(args, "-file");
                // Der Pfad zur Datei
                string filePath = string.Empty;

                // Prüfen ob es überhaupt einen mitgelieferten Pfad gibt.
                if (args.Length <= indexOfParam +1)
                {
                    Console.WriteLine("Please enter a valid file path.");
                    return;
                }

                filePath = args[indexOfParam + 1].Replace("\"", "");

                if (System.IO.File.Exists(filePath)== false)
                {
                    Console.WriteLine("The file doesn't exists.");
                    return;
                }

                textOrMorsecode = System.IO.File.ReadAllText(filePath);
            }

            if (save)
            {
                // Der Pfad zur Datei sollte direkt nach dem File Parameter folgen
                int indexOfParam = GetParameterIndex(args, "-save");

                // Prüfen ob es überhaupt einen mitgelieferten Pfad gibt.
                if (args.Length <= indexOfParam + 1)
                {
                    Console.WriteLine("Please enter a valid file path.");
                    return;
                }

                saveFilePath = args[indexOfParam+1].Replace("\"", "");

                if (System.IO.File.Exists(saveFilePath) && y == false)
                {
                    Console.WriteLine("The file already exists.");
                    Console.Write("Should the file be overwritten? (y/N) ");
                    string userInput = Console.ReadLine().ToLower();
                    if ((userInput != "y" && userInput !="yes"))
                    {
                        Console.WriteLine("Process was aborted by user.");
                        return;
                    }
                }

            }

            if (text) 
            {

                // Der Pfad zur Datei sollte direkt nach dem File Parameter folgen
                int indexOfParam = GetParameterIndex(args, "-text") + 1;
                // Der Pfad zur Datei

                if (args.Length < indexOfParam)
                {
                    Console.WriteLine("Please enter a valid string.");
                    return;
                }

                textOrMorsecode = args[indexOfParam].Replace("\"", "");

            }

            if(dit)
            {
                // Der Pfad zur Datei sollte direkt nach dem File Parameter folgen
                int indexOfParam = GetParameterIndex(args, "-dit");

                if (args.Length <= indexOfParam+1) 
                {
                    Console.WriteLine("Please enter a valid dit lenght.");
                    return;
                }

               string ditParam = args[indexOfParam +1];

                if (Regex.IsMatch(ditParam, @"^\d+$")==false)
                {
                    Console.WriteLine("Please enter a valid dit lenght.");
                    return;
                }

                ditLength= int.Parse(ditParam);

                if (ditLength< 0)
                {
                    Console.WriteLine("The entered ditlength was resetted to 100.");
                    ditLength = 100;
                }

                if (ditLength== 0) {
                    a = false;
                }

            }

            // Die Ditlenght ist nur relevant, wenn es auch eine akustische ausgabe gibt
            if (a == false) { ditLength = 0; }

            if (t && m)
            {
                Console.WriteLine("You cannot combine -m and -t");
                return;
            }

            // Prüft ob die Mindestkonfiguration vorliegt um das Programm auszuführen
            if (file==false && text == false && h == false && dir==false && version==false) 
            {
                Console.WriteLine("You need to specify what text you want to translate");
                return;
            }

            if (m)
            {
                List<MorseCodes> convertedTextOrMorse = new List<MorseCodes>();
                convertedTextOrMorse = TextToMorse(textOrMorsecode);
                ShowMorseCode(convertedTextOrMorse, v, a, ditLength,save,saveFilePath);
                return;
            }

            if(t) 
            {
                MorseToText(textOrMorsecode, save, saveFilePath);
                return;
            }

            if(h)
            {
                Console.WriteLine("-m (-file or -text)         | Convert text to morse");
                Console.WriteLine("-t (-file or -text)         | Convert morse to text");
                Console.WriteLine("-file (filepath)            | Read the text/morsecode from a file");
                Console.WriteLine("-text \"your text or morse\"  | Read the text/morsecode from the comandline");
                Console.WriteLine("-a                          | Outputs the Morse code acoustically");
                Console.WriteLine("-v                          | Outputs the Morse code / text in the console");
                Console.WriteLine("-dit                        | Sets the ditlength - default is 100");
                Console.WriteLine("-save (filepath)            | Saves the output o a file");
                Console.WriteLine("-y                          | does not ask whether an existing file should be overwritten");


                return;
            }

            if (dir)
            {
                ShowCodeList(v, a,  save, saveFilePath,ditLength);
                return;
            }


            if(version)
            {
                Console.WriteLine("MorseCraft by Dominik Zerbe");
                Console.WriteLine($"Version: 1.0.0");
                return;
            }

            


        }


        private static async void ShowCodeList(bool printText, bool playSound,bool saveToFile, string ?filePath, int ditLength) 
        {


            // Das sind die Geschwindigkeitseinheiten beim Morsen.
            int dit = ditLength;
            int dah = 3 * dit;
            int doh = 7 * dit;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (MorseCodes code in Codes)
            {


                if (printText)
                {
                    Console.Write($"{code.Letter}   -->   ");
                }
                stringBuilder.Append($"{code.Letter}   -->   ");

                ;
                foreach (char c in code.Code)
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
                                if (playSound) { await PlayBeep(dit, 800); }
                                else { Thread.Sleep(dit); }
                                break;
                            case '-':
                                if (playSound) { await PlayBeep(dah, 800); }
                                else { Thread.Sleep(dah); }
                                break;
                            case '/':
                                Thread.Sleep(doh);
                                break;
                            default:
                                break;
                        }

                    }

                }

                if (playSound) {Thread.Sleep(doh); }
                if (printText) {Console.WriteLine();}
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

            StringBuilder klartextBuilder = new StringBuilder();

            foreach (string code in codeText.Split(" "))
            {

               if (string.IsNullOrEmpty(code) == false)
                {
                    MorseCodes morseCode = Program.Codes.FirstOrDefault(item => item.Code == code);
                    if (morseCode != null)
                    {
                        klartextBuilder.Append(morseCode.Letter);
                    }
                    else
                    {
                        klartextBuilder.Append($"[unbekannter code ({code})]");
                    }
                }
            }

            if (saveToFile)
            {
                try
                {
                    System.IO.File.WriteAllText(filePath, klartextBuilder.ToString());
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);   
                }
            }

            Console.WriteLine(klartextBuilder.ToString());
        }

        public static async Task ShowMorseCode(List<MorseCodes> codeList,bool printToConsole, bool playSound, int ditLength, bool saveToFile, string filePath)
        {

            // Das sind die Geschwindigkeitseinheiten beim Morsen.
            int dit = ditLength;
            int dah = 3 * dit;
            int doh = 7* dit;

            StringBuilder stringBuilder = new StringBuilder();  

            foreach (MorseCodes code in codeList)
            {
                foreach (char c in code.Code)
                {                

                    if (printToConsole) { Console.Write($"{c}"); }
                    stringBuilder.Append(c);

                    switch (c)
                    {
                        case '.':
                            if (playSound){await PlayBeep(dit,800); }
                            else{Thread.Sleep(dit);}
                            break;
                        case '-':
                            if (playSound){ await PlayBeep(dah, 800); }
                            else{Thread.Sleep(dah);}
                            break;
                        case '/':
                            Thread.Sleep(doh);
                            break;
                        default:
                            break;
                    }
                }

                Console.Write(" ");
                stringBuilder.Append(" ");
            }

            if (saveToFile)
            {
                try
                {
                    System.IO.File.WriteAllText(filePath,stringBuilder.ToString());
                }catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }


        private static async Task PlayBeep(int duration, int frequence)
        {         
            using (var player = new WaveOutEvent())
            {
                var sineWaveProvider = new SignalGenerator() { Frequency = frequence, Type = SignalGeneratorType.Sin};
                player.Init(sineWaveProvider);
                player.Play();

                // Verwende Task.Delay für die Wartezeit
                Thread.Sleep(duration); 
                player.Stop();
            }

        }
        public static List<MorseCodes> TextToMorse(string plainText)
        {

            List<MorseCodes> textAsCodeList = new List<MorseCodes>();

            foreach (char c in plainText.ToUpper())
            {

                // Suche das richtige Element aus der Übersetzungsliste
                MorseCodes? code = Program.Codes.FirstOrDefault(item => item.Letter == c.ToString());
                if (code != null)
                {
                    textAsCodeList.Add(code);
                }

            }

            return textAsCodeList;
        }

        /// <summary>
        /// Gibt den Index eines Strings aus einem Array zurück.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns>Index des Parameters / -1 wenn der Index nicht existiert</returns>
        private static int GetParameterIndex(Array array, string value) {

            return  Array.IndexOf(array, value);
       
        }



    }
}
