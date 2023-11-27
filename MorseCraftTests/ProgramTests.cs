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

            string[] testArray2 = { "-m","-file", "\"C:\\Test.txt\"", "-a", "-v" };
            bool testExists2 = Program.CmdParamExists(testArray2, "-text");
            Assert.IsFalse(testExists2);

        }
    }

}