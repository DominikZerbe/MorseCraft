

using NAudio.Wave.SampleProviders;
using NAudio.Wave;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Text;

namespace MorseCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeList.Initialize();

            // Soll eine Textdatei aus dem Dateisystem geladen werden?
            bool loadFromFile = false;

            // Der Morsecode soll als Text ausgegeben werden?
            bool showCodeAsText = false;

            // Die Konsole soll Piepgeräusche machen?
            bool makeBeepSound = false; 

            // Soll Morse in text ausgegeben werden? oder andersherum?
            bool morseToText = false;
            bool textToMorse=false;

            // Die Ditlänge in Milisekunden
            int ditLength = 100;

            List<MorseCodes> codes = new List<MorseCodes> ();

            // Sucht den Parameter -file, falls aus einer Datei ausgelesen werden muss.
            // Der Dateipfad musss direkt nach dem Parameter folgen.
            int indexFileParameter = Array.IndexOf(args, "-file");
            string filePath = "";
            string fileContent = "";
            if (indexFileParameter != -1)
            {
                loadFromFile = true;
                indexFileParameter++;
                filePath = args[indexFileParameter];
                if(System.IO.File.Exists(filePath))
                {
                   fileContent = System.IO.File.ReadAllText(filePath);
                   codes = TextToMorse(fileContent);
                }
                else{Console.WriteLine($"The file doesnt exists: {filePath}");}
            }

            if (Array.IndexOf(args, "-s") != -1){showCodeAsText = true;}
            if (Array.IndexOf(args, "-b") != -1){makeBeepSound = true;}
            if (Array.IndexOf(args, "-m") != -1){textToMorse = true;}
            if (Array.IndexOf(args, "-t") != -1){morseToText = true;}

            int indexDitParameter = Array.IndexOf(args, "-dit");
            if(indexDitParameter != -1)
            {
              indexDitParameter++;
                try { 
                    ditLength = int.Parse(args[indexDitParameter]);
                    if (ditLength < 0) { ditLength = 100; }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            if (textToMorse){ShowMorseCode(codes, showCodeAsText, makeBeepSound, false, ditLength);}

            if (morseToText)
            {
                string klartext = MorseToText(fileContent);
                Console.Write(klartext);    
            }
        }

        public static string MorseToText(string codeText)
        {

            StringBuilder klartextBuilder = new StringBuilder();

            foreach (string code in codeText.Split(" "))
            {

               if (string.IsNullOrEmpty(code) == false)
                {
                    MorseCodes morseCode = CodeList.Codes.FirstOrDefault(item => item.Code == code);
                    if (morseCode != null)
                    {
                        klartextBuilder.Append(morseCode.Letter);
                    }
                    else
                    {
                        klartextBuilder.Append("[unbekannter code]");
                    }
                }
            }

            return klartextBuilder.ToString();
        }

        public static async Task ShowMorseCode(List<MorseCodes> codeList,bool printTextOnConsole, bool playSound, bool withGraphics, int ditLength)
        {

            // Das sind die Geschwindigkeitseinheiten beim Morsen.
            int dit = ditLength;
            int dah = 3 * dit;
            int doh = 7* dit;

            foreach (MorseCodes code in codeList)
            {
                foreach (char c in code.Code)
                {                

                    if (printTextOnConsole) { Console.Write($"{c}"); }

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
                MorseCodes? code = CodeList.Codes.FirstOrDefault(item => item.Letter == c.ToString());
                if (code != null)
                {
                    textAsCodeList.Add(code);
                }

            }

            return textAsCodeList;
        }



    }
}
