using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RechnerConsole;
using RechnerContracts;
using RechnerHelper;

namespace RechnerConsoleTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestParsingUndBerechnung()
        {
            //Test-Driven-Development
            //Der selbe Unit-Test muss auch für alle abgeleiteten Klassen von Parser oder Rechner funktionieren (Liskovsches Substitutionsprinzip)
            IParser parser = new Parser();
            IFormel formel = parser.Parse("5/1");
            IRechner rechner = new FlexRechner();
            Assert.AreEqual(5, rechner.Rechne(formel));
        }
    }
}
