using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCraft
{
    internal class MorseCodes
    {

        private string _letter;
        private string _code;
        private List<MorseCodes> _knownCodes;
        public string Letter
        {
            get { return _letter; }
            set { _letter = value; }
        }

        public string Code
        {
            get {
                return _code;
            }
            set {
                _code = value;
            }
        }

        public List<MorseCodes> KnownCodes
        {
            get
            {
                return _knownCodes;
            }
            set
            {
                _knownCodes = value;
            }    
        }

        private MorseCodes(string letter, string code)
        {
            this.Letter = letter;
            this.Code = code;  
        }

        private void InitializeCodes()
        {

            List<MorseCodes> knownCodes = new List<MorseCodes>();

            // Lateinische Buchstaben
            MorseCodes A = new MorseCodes("A", ".-");
            MorseCodes B = new MorseCodes("B", "-...");
            MorseCodes C = new MorseCodes("C", "-.-.");
            MorseCodes D = new MorseCodes("D", "-..");
            MorseCodes E = new MorseCodes("E", ".");
            MorseCodes F = new MorseCodes("F", "..-.");
            MorseCodes G = new MorseCodes("G", "--.");
            MorseCodes H = new MorseCodes("H", "....");
            MorseCodes I = new MorseCodes("I", "..");
            MorseCodes J = new MorseCodes("J", ".---");
            MorseCodes K = new MorseCodes("K", "-.-");
            MorseCodes L = new MorseCodes("L", ".-..");
            MorseCodes M = new MorseCodes("M", "--");
            MorseCodes N = new MorseCodes("N", "-.");
            MorseCodes O = new MorseCodes("O", "---");
            MorseCodes P = new MorseCodes("P", ".--.");
            MorseCodes Q = new MorseCodes("Q", "--.-");
            MorseCodes R = new MorseCodes("R", ".-.");
            MorseCodes S = new MorseCodes("S", "...");
            MorseCodes T = new MorseCodes("T", "-");
            MorseCodes U = new MorseCodes("U", "..-");
            MorseCodes V = new MorseCodes("V", "...-");
            MorseCodes W = new MorseCodes("W", ".--");
            MorseCodes X = new MorseCodes("X", "-..-");
            MorseCodes Y = new MorseCodes("Y", "-.--");
            MorseCodes Z = new MorseCodes("Z", "--..");
            knownCodes.Add(A);
            knownCodes.Add(B);
            knownCodes.Add(C);
            knownCodes.Add(D);

            // Zahlen
            MorseCodes Eins = new MorseCodes("1", ".----");
            MorseCodes Zwei = new MorseCodes("2", "..---");
            MorseCodes Drei = new MorseCodes("3", "...--");
            MorseCodes Vier = new MorseCodes("4", "....-");
            MorseCodes Fuenf = new MorseCodes("5", ".....");
            MorseCodes Sechs = new MorseCodes("6", "-....");
            MorseCodes Sieben = new MorseCodes("7", "--...");
            MorseCodes Acht = new MorseCodes("8", "---..");
            MorseCodes Neun = new MorseCodes("9", "----.");
            MorseCodes Null = new MorseCodes("0", "-----");

            // Umlaute + Diakritisches
            MorseCodes A1 = new MorseCodes("À", ".--.-");
            MorseCodes A2 = new MorseCodes("Å", ".--.-");
            MorseCodes Ae = new MorseCodes("Ä", ".-..-");
            MorseCodes E1 = new MorseCodes("È", ".-..-");
            MorseCodes E2 = new MorseCodes("É", "..-..");
            MorseCodes Oe = new MorseCodes("Ö", "---.");
            MorseCodes Ue = new MorseCodes("Ü", "..--");
            MorseCodes Sz = new MorseCodes("ß", "...--..");
            MorseCodes Ch = new MorseCodes("CH", "----"); // Muss in späteren Auswertungen besonders berücksichtigt werden!
            MorseCodes N1 = new MorseCodes("Ñ", "--.--");


            // Zeichen
            MorseCodes Punkt = new MorseCodes(".", ".-.-.-"); 
            MorseCodes Komma = new MorseCodes(",", "--..--"); 
            MorseCodes Doppelpunkt = new MorseCodes(":", "---...");
            MorseCodes Semikolon = new MorseCodes(";", "-.-.-.");
            MorseCodes Fragezeichen = new MorseCodes("?", "..--..");
            MorseCodes Ausrufezeichen = new MorseCodes("!", "-.-.--");
            MorseCodes Bindestrich = new MorseCodes("-", "-....-");
            MorseCodes Unterstrich = new MorseCodes("_", "..--.-");
            MorseCodes KlammerAuf = new MorseCodes("(", "-.--.");
            MorseCodes KlammerZu = new MorseCodes(")", "-.--.-");
            MorseCodes Hochkomma = new MorseCodes("'", ".----.");
            MorseCodes IstGleich = new MorseCodes("=", "-...-");
            MorseCodes Plus = new MorseCodes("+", ".-.-.");
            MorseCodes GeteiltDurch = new MorseCodes("/", "-..-.");
            MorseCodes AT = new MorseCodes("@", ".--.-.");
            MorseCodes KlaAnfuehrungszeichen = new MorseCodes("\"", ".-..-.");

            // Signale
            MorseCodes SOS = new MorseCodes("[SOS]", "...---...");
            MorseCodes Spruchanfang = new MorseCodes("[Spruchanfang]", "-.-.-");
            MorseCodes Pause = new MorseCodes("[Pause]", "-...-");
            MorseCodes Spruchende = new MorseCodes("[Spruchende]", ".-.-.");
            MorseCodes Verstanden = new MorseCodes("[Verstanden]", "...-.");
            MorseCodes Verkehrsende = new MorseCodes("[Verkehrsende]", "...-.-");
            MorseCodes FtNotruf = new MorseCodes("[CQD]", "-.-. --.- -..");
            MorseCodes FehlerIrrung = new MorseCodes("[EEEEEEEE]", "..... . . .");




        }

    }
}
