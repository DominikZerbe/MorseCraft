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

        /// <summary>
        /// Initializes the translation list. This function is called once when the program is started
        /// </summary>
        /// <returns>The translation list of morse codes</returns>
        public static List<Morse> Initialize()
        {

            List<Morse> translationTable = new List<Morse>()
            {
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
                new Morse("À", ".--.-"),
                new Morse("Å", ".--.-"),
                new Morse("Ä", ".-..-"),
                new Morse("È", ".-..-"),
                new Morse("É", "..-.."),
                new Morse("Ö", "---."),
                new Morse("Ü", "..--"),
                new Morse("ß", "...--.."),
                new Morse("CH", "----"),
                new Morse("Ñ", "--.--"),
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
                new Morse("[SOS]", "...---..."),
                new Morse("[Spruchanfang]", "-.-.-"),
                new Morse("[Pause]", "-...-"),
                new Morse("[Spruchende]", ".-.-."),
                new Morse("[Verstanden]", "...-."),
                new Morse("[Verkehrsende]", "...-.-"),
                new Morse("[CQD]", "-.-. --.- -.."),
                new Morse("[EEEEEEEE]", "..... . . ."),
                new Morse(" ", "/",false),
                new Morse("\n", "\n",false),

            };

            return translationTable;

        }

    }
    /// <summary>
    /// The class contains the Morse code in comparison with human-readable characters.
    /// </summary>
    public class Morse
    {
        /// <summary>
        /// The human readable symbol text like "h" or "3"
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The morse code of the symbol
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Determines whether the code is displayed in the "dir" function of the program.
        /// </summary>
        public bool Visible = true;

        /// <summary>
        /// Structure for the Morse class
        /// </summary>
        /// <param name="text">The human readable symbol text like "h" or "3"</param>
        /// <param name="code">The morse code of the symbol</param>
        /// <param name="visible">Should the code be visible with the dir function? default is true</param>
        public Morse(string text, string code, bool visible = true)
        {
            Text = text;
            Code = code;
            Visible = visible;
        }                     

    }
}

