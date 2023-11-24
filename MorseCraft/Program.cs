

namespace MorseCraft
{
    class Program
    {
        static void Main(string[] args)
        {

            CodeList.Initialize();

            if (args[0] == "-t")
            {

                List<MorseCodes> codes = MorseCodes.AnalyseText(args[1]);
                foreach (MorseCodes code in codes)
                {
                    Console.Write(code.Code);
                    Console.Write(" ");

                }
            }


            if (args[0] == "-b")
            {

                List<MorseCodes> codes = MorseCodes.AnalyseText(args[1]);

                ExecuteMorseCode(codes,true,true,false,350);

            }


        }

        public static void ExecuteMorseCode(List<MorseCodes> codeList,bool withTextoutput, bool withSound, bool withGraphics, int dithLength)
        {

            int ditLength = dithLength;
            int dahLength = 3 * dithLength;
            int dohLength = 7* dithLength;
            int pauseBetweenChars = dithLength;
            int ditCounter = 0;

            foreach (MorseCodes code in codeList)
            {
                foreach (char c in code.Code)
                {

                    if (withTextoutput == true)
                    {
                        Console.Write(c);

                    }


                    switch (c.ToString())
                    {
                        case ".":
                            if (withSound == true)
                            {
                                Console.Beep(1000,dithLength);
                                ditCounter++;
                            }
                            Thread.Sleep(pauseBetweenChars);
                            break;
                        case "-":
                            if (withSound == true)
                            {
                                Console.Beep(1000, dahLength);
                                ditCounter++;
                            }
                            Thread.Sleep(pauseBetweenChars);
                            break;
                        case "/":
                            Thread.Sleep(dohLength);
                            break;
                        default:
                            break;

                    }

      
                }

                if (ditCounter == 2)
                {
                    Thread.Sleep(ditLength);
                    ditCounter = 0;
                }

                if (withTextoutput == true)
                {
                    Console.Write(" ");
                }

            }
        }
    }
}
