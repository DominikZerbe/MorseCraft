

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
            int ditLength = 280;

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
                   codes = MorseCodes.AnalyseText(fileContent);

                }
                else{Console.WriteLine($"The file doesnt exists: {filePath}");}
            }
            else { }

            int indexTextParameter = Array.IndexOf(args, "-s");
            if(indexTextParameter!=-1) { 
            showCodeAsText = true;
            }

            int indexMakeBeep = Array.IndexOf(args, "-b");
            if (indexMakeBeep != -1)
            {
                makeBeepSound = true;
            }

            int indexConvertMode = Array.IndexOf(args, "-m");
            if (indexConvertMode != -1)
            {
                textToMorse = true;
            }

            if (Array.IndexOf(args, "-t") != -1)
            {
                morseToText = true;
            }

            int indexDitParameter = Array.IndexOf(args, "-dit");
            if(indexDitParameter != -1)
            {
              indexDitParameter++;
              ditLength =  int.Parse(args[indexDitParameter]);                    
            }




            if (textToMorse)
            {
                 ExecuteMorseCode(codes, showCodeAsText, makeBeepSound, false, ditLength);
            }

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

        public static async Task ExecuteMorseCode(List<MorseCodes> codeList,bool withTextoutput, bool withSound, bool withGraphics, int dithLength)
        {

            int ditLength = dithLength;
            int dahLength = 3 * dithLength;
            int dohLength = 7* dithLength;

            foreach (MorseCodes code in codeList)
            {
                foreach (char c in code.Code)
                {                

                    if (withTextoutput == true) { Console.Write($"{c}"); }

                    switch (c)
                    {
                        case '.':
                            if (withSound){await PlayBeep(ditLength,800); }
                            else{Thread.Sleep(ditLength);}
                            break;
                        case '-':
                            if (withSound){ await PlayBeep(dahLength, 800); }
                            else{Thread.Sleep(dahLength);}
                            break;
                        case '/':
                            Thread.Sleep(dohLength);
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


    }
}
