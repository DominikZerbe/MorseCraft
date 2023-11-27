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
        public static List<Morse> Initialize()
        {

            List<Morse> translationTable = new List<Morse>()
            {
                // Lateinische Buchstaben
                new Morse("A", ".-"),
                new Morse("B", "-..."),
                new Morse("C", "-.-."),
                new Morse("D", "-.."),
                new Morse("E", "."),
                new Morse("F", "..-."),
                new Morse("G", "--."),
                new Morse("H", "...."),
                new Morse("I", ".."),
                new Morse("J", ".---"),
                new Morse("K", "-.-"),
                new Morse("L", ".-.."),
                new Morse("M", "--"),
                new Morse("N", "-."),
                new Morse("O", "---"),
                new Morse("P", ".--."),
                new Morse("Q", "--.-"),
                new Morse("R", ".-."),
                new Morse("S", "..."),
                new Morse("T", "-"),
                new Morse("U", "..-"),
                new Morse("V", "...-"),
                new Morse("W", ".--"),
                new Morse("X", "-..-"),
                new Morse("Y", "-.--"),
                new Morse("Z", "--.."),

                // Zahlen
                new Morse("1", ".----"),
                new Morse("2", "..---"),
                new Morse("3", "...--"),
                new Morse("4", "....-"),
                new Morse("5", "....."),
                new Morse("6", "-...."),
                new Morse("7", "--..."),
                new Morse("8", "---.."),
                new Morse("9", "----."),
                new Morse("0", "-----"),

                // Umlaute + Diakritisches
                new Morse("À", ".--.-"),
                new Morse("Å", ".--.-"),
                new Morse("Ä", ".-..-"),
                new Morse("È", ".-..-"),
                new Morse("É", "..-.."),
                new Morse("Ö", "---."),
                new Morse("Ü", "..--"),
                new Morse("ß", "...--.."),
                new Morse("CH", "----"), // Muss in späteren Auswertungen besonders berücksichtigt werden!
                new Morse("Ñ", "--.--"),

                // Zeichen
                new Morse(".", ".-.-.-"),
                new Morse(",", "--..--"),
                new Morse(":", "---..."),
                new Morse(",", "-.-.-."),
                new Morse("?", "..--.."),
                new Morse("!", "-.-.--"),
                new Morse("-", "-....-"),
                new Morse("_", "..--.-"),
                new Morse("(", "-.--."),
                new Morse(")", "-.--.-"),
                new Morse("'", ".----."),
                new Morse("=", "-...-"),
                new Morse("+", ".-.-."),
                new Morse("/", "-..-."),
                new Morse("@", ".--.-."),
                new Morse("\"", ".-..-."),

                // Signale
                new Morse("[SOS]", "...---..."),
                new Morse("[Spruchanfang]", "-.-.-"),
                new Morse("[Pause]", "-...-"),
                new Morse("[Spruchende]", ".-.-."),
                new Morse("[Verstanden]", "...-."),
                new Morse("[Verkehrsende]", "...-.-"),
                new Morse("[CQD]", "-.-. --.- -.."),
                new Morse("[EEEEEEEE]", "..... . . ."),

                // Pause wird ebenfalls als Code verwendet
                new Morse(" ", "/"),
                new Morse("\n", "\n"),

            };

            return translationTable;

        }

    }

    public class Morse
    {
        public string Text { get; set; }
        public string Code { get; set; }

        public  Morse(string text, string code)
        {
            Text = text;
            Code = code;
        }                     

    }
}

