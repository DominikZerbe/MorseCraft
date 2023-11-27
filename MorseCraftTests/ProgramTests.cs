using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorseCraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorseCraft.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void GetSubCmdParamTest()
        {
            string[] testArray1 = { "-m", "-text", "\"Hallo Welt\"", "-a", "-v" };
            string testParam1 = MorseCraft.Program.GetSubCmdParam(testArray1, "-text");
            Assert.AreEqual(testParam1, "Hallo Welt");

            string[] testArray2 = { "-m", "-text" };
            string testParam2 = MorseCraft.Program.GetSubCmdParam(testArray2, "-text");
            Assert.AreEqual(testParam2, String.Empty);

        }

        [TestMethod()]
        public void CmdParamExistsTest()
        {
            string[] testArray1 = { "-m", "-text", "\"Hallo Welt\"", "-a", "-v" };
            bool testExists1 = Program.CmdParamExists(testArray1, "-text");
            Assert.IsTrue(testExists1);

            string[] testArray2 = { "-m", "-file", "\"C:\\Test.txt\"", "-a", "-v" };
            bool testExists2 = Program.CmdParamExists(testArray2, "-text");
            Assert.IsFalse(testExists2);

        }

        [TestMethod()]
        public void TextToMorseTest()
        {
            List<Morse> testExpectedMorseCode1 = new List<Morse>
            {
                Program.TranslationTable.First(item => item.Text == "H"),
                Program.TranslationTable.First(item => item.Text == "A"),
                Program.TranslationTable.First(item => item.Text == "L"),
                Program.TranslationTable.First(item => item.Text == "L"),
                Program.TranslationTable.First(item => item.Text == "O"),
                Program.TranslationTable.First(item => item.Text == " "),
                Program.TranslationTable.First(item => item.Text == "W"),
                Program.TranslationTable.First(item => item.Text == "E"),
                Program.TranslationTable.First(item => item.Text == "L"),
                Program.TranslationTable.First(item => item.Text == "T"),
            };
            List<Morse> testResultMorseCode1 = Program.TextToMorse("Hallo Welt");
            CollectionAssert.AreEqual(testExpectedMorseCode1,testResultMorseCode1);



            List<Morse> testExpectedMorseCode2 = new List<Morse>
            {
                Program.TranslationTable.First(item => item.Text == "H"),
                Program.TranslationTable.First(item => item.Text == "A"),
                Program.TranslationTable.First(item => item.Text == "L"),
                Program.TranslationTable.First(item => item.Text == "L"),
                Program.TranslationTable.First(item => item.Text == "O"),
                Program.TranslationTable.First(item => item.Text == " "),
                Program.TranslationTable.First(item => item.Text == "W"),
                Program.TranslationTable.First(item => item.Text == "E"),
                Program.TranslationTable.First(item => item.Text == "L"),
                Program.TranslationTable.First(item => item.Text == "T"),
            };
            List<Morse> testResultMorseCode2 = Program.TextToMorse("Hallo Test");
            CollectionAssert.AreNotEqual(testExpectedMorseCode2, testResultMorseCode2);


        }

        [TestMethod()]
        public void TestTranslationList()
        {
            foreach (Morse morse in Program.TranslationTable)
            {
                string Code = morse.Code;
                int expectedElements=1;

                List<Morse> allElements = Program.TranslationTable.FindAll(item1 => item1.Code == Code);
                int countedElements = allElements.Count;
                if (countedElements > 1)
                {

                    switch (morse.Text)
                    {
                        case "À":
                            expectedElements = 2;
                            break;
                        case "Å":
                            expectedElements = 2; 
                            break;
                        case "=":
                            expectedElements = 2;
                            break;
                        case "[Pause]":
                            expectedElements = 2;
                            break;
                        case "+":
                            expectedElements = 2;
                            break;
                        case "[Spruchende]":
                            expectedElements = 2;
                            break;
                        default:
                            expectedElements = 1;
                            break;
                    }

                }
                Assert.IsTrue(expectedElements== countedElements,$"Fehlerfall: {morse.Text}");
            }
        }

    }

}