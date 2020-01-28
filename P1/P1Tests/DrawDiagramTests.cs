using Microsoft.VisualStudio.TestTools.UnitTesting;
using P1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace P1.Tests
{
    [TestClass()]
    public class DrawDiagramTests
    {
        [TestMethod()]
        public void ConvertorXTest()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 100;
            canvas.Height = 100;
            DrawDiagram DD = new DrawDiagram(canvas, new Canvas());

            DD.UpdateVariables("-10", "10", "-10", "10");

            Assert.AreEqual(75 ,DD.ConvertorX(5));
        }

        [TestMethod()]
        public void ConvertorYTest()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 100;
            canvas.Height = 100;
            DrawDiagram DD = new DrawDiagram(canvas, new Canvas());

            DD.UpdateVariables("-10", "10", "-10", "10");

            Assert.AreEqual(75, DD.ConvertorX(5));
        }
    }
}