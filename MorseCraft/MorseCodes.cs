using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MorseCraft
{
    /// <summary>
    /// Hier werden alle bekannten Morsecodes beim Programmstart reingeladen.
    /// Alle Übersetzungen greifen hierauf zu.
    /// </summary>
    public static class CodeList
    {

        private static List<MorseCodes> _codes = new List<MorseCodes>();
        public static List<MorseCodes> Codes
        {
            get { return _codes; }
            set { _codes = value; }
        }

        public static void Initialize()
        {

            List<MorseCodes> Uebersetzungstabelle = new List<MorseCodes>()
            {
                // Lateinische Buchstaben
                new MorseCodes("A", ".-"),
                new MorseCodes("B", "-..."),
                new MorseCodes("C", "-.-."),
                new MorseCodes("D", "-.."),
                new MorseCodes("E", "."),
                new MorseCodes("F", "..-."),
                new MorseCodes("G", "--."),
                new MorseCodes("H", "...."),
                new MorseCodes("I", ".."),
                new MorseCodes("J", ".---"),
                new MorseCodes("K", "-.-"),
                new MorseCodes("L", ".-.."),
                new MorseCodes("M", "--"),
                new MorseCodes("N", "-."),
                new MorseCodes("O", "---"),
                new MorseCodes("P", ".--."),
                new MorseCodes("Q", "--.-"),
                new MorseCodes("R", ".-."),
                new MorseCodes("S", "..."),
                new MorseCodes("T", "-"),
                new MorseCodes("U", "..-"),
                new MorseCodes("V", "...-"),
                new MorseCodes("W", ".--"),
                new MorseCodes("X", "-..-"),
                new MorseCodes("Y", "-.--"),
                new MorseCodes("Z", "--.."),

                // Zahlen
                new MorseCodes("1", ".----"),
                new MorseCodes("2", "..---"),
                new MorseCodes("3", "...--"),
                new MorseCodes("4", "....-"),
                new MorseCodes("5", "....."),
                new MorseCodes("6", "-...."),
                new MorseCodes("7", "--..."),
                new MorseCodes("8", "---.."),
                new MorseCodes("9", "----."),
                new MorseCodes("0", "-----"),

                // Umlaute + Diakritisches
                new MorseCodes("À", ".--.-"),
                new MorseCodes("Å", ".--.-"),
                new MorseCodes("Ä", ".-..-"),
                new MorseCodes("È", ".-..-"),
                new MorseCodes("É", "..-.."),
                new MorseCodes("Ö", "---."),
                new MorseCodes("Ü", "..--"),
                new MorseCodes("ß", "...--.."),
                new MorseCodes("CH", "----"), // Muss in späteren Auswertungen besonders berücksichtigt werden!
                new MorseCodes("Ñ", "--.--"),

                // Zeichen
                new MorseCodes(".", ".-.-.-"),
                new MorseCodes(",", "--..--"),
                new MorseCodes(":", "---..."),
                new MorseCodes(",", "-.-.-."),
                new MorseCodes("?", "..--.."),
                new MorseCodes("!", "-.-.--"),
                new MorseCodes("-", "-....-"),
                new MorseCodes("_", "..--.-"),
                new MorseCodes("(", "-.--."),
                new MorseCodes(")", "-.--.-"),
                new MorseCodes("'", ".----."),
                new MorseCodes("=", "-...-"),
                new MorseCodes("+", ".-.-."),
                new MorseCodes("/", "-..-."),
                new MorseCodes("@", ".--.-."),
                new MorseCodes("\"", ".-..-."),

                // Signale
                new MorseCodes("[SOS]", "...---..."),
                new MorseCodes("[Spruchanfang]", "-.-.-"),
                new MorseCodes("[Pause]", "-...-"),
                new MorseCodes("[Spruchende]", ".-.-."),
                new MorseCodes("[Verstanden]", "...-."),
                new MorseCodes("[Verkehrsende]", "...-.-"),
                new MorseCodes("[CQD]", "-.-. --.- -.."),
                new MorseCodes("[EEEEEEEE]", "..... . . ."),

                // Pause wird ebenfalls als Code verwendet
                new MorseCodes(" ", "/"),
                new MorseCodes("\n", "\n"),


            };

            Codes = Uebersetzungstabelle;


        }



    }

    public class MorseCodes
    {

        private string _letter =string.Empty;
        private string _code=string.Empty;

        public string Letter
        {
            get { return _letter; }
            set { _letter = value; }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }


        public  MorseCodes(string letter, string code)
        {
            this.Letter = letter;
            this.Code = code;
        }


        public static string TranslateToText(string code)
        {
            return "";
        }

        public static List<MorseCodes> AnalyseText(string text)
        {

            List<MorseCodes> analysierterText = new List<MorseCodes>();

            foreach (char c in text)
            {

                string zeichen = c.ToString().ToUpper();
                // Suche das richtige Element aus der Übersetzungsliste
                MorseCodes code = CodeList.Codes.FirstOrDefault(item => item.Letter == zeichen);
                if (code != null)
                {
                     analysierterText.Add(code);
                }

            }

            ;
            return analysierterText;
        }

    }
}

