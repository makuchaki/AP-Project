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
    public class EquationsTests
    {
        [TestMethod()]
        public void DeterminantTest()
        {
            Equations EQ = new Equations(new TextBox(), new Label());

            double [][] matrix = new double [3][];
            for (int i = 0; i < 3; i++)
                matrix[i] = new double[] { 1, 2, 3 };

            Assert.AreEqual(0, EQ.Determinant(matrix));
        }

        [TestMethod()]
        public void DeterminanTest()
        { }

        [TestMethod()]
        public void TranahadTest()
        { }

        [TestMethod()]
        public void ParseEquationsTEst()
        { }
    }
}