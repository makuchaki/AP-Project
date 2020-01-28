using Microsoft.VisualStudio.TestTools.UnitTesting;
using P1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1.Tests
{
    [TestClass()]
    public class FunctionPropertiesTests
    {
        [TestMethod()]
        public void FunctionTest()
        {
            FunctionProperties FP = new FunctionProperties("SIN(x)");
            Assert.AreEqual(0, FP.Function(0));

            FP = new FunctionProperties("COS(x)");
            Assert.AreEqual(1, FP.Function(0));

            FP = new FunctionProperties("TAN(x)");
            Assert.AreEqual(0, FP.Function(0));

            FP = new FunctionProperties("COT(x)");
            Assert.AreEqual(0,Math.Round(FP.Function(Math.PI/2)));
        }

        [TestMethod()]
        public void PolynomialFunctionTest()
        {
            //with ^
            //with factor
            FunctionProperties FP = new FunctionProperties("");
            Assert.AreEqual(-8, FP.PolynomialFunction("-2x^2", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(8, FP.PolynomialFunction("2x^2", 2));

            //without factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-4, FP.PolynomialFunction("-x^2", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(4, FP.PolynomialFunction("x^2", 2));

            // without ^
            //with x
            //with factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-4, FP.PolynomialFunction("-2x", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(4, FP.PolynomialFunction("2x", 2));

            //without factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-2, FP.PolynomialFunction("-x", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(2, FP.PolynomialFunction("x", 2));

            //without x
            FP = new FunctionProperties("");
            Assert.AreEqual(-2, FP.PolynomialFunction("-2", 2));
            FP = new FunctionProperties("");
            Assert.AreEqual(8, FP.PolynomialFunction("8", 2));
        }

        [TestMethod()]
        public void PolynomialDerivativeTest()
        {
            //with ^
            //with factor
            FunctionProperties FP = new FunctionProperties("");
            Assert.AreEqual(-8, FP.PolynomialDerivative("-2x^2", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(8, FP.PolynomialDerivative("2x^2", 2));

            //without factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-4, FP.PolynomialDerivative("-x^2", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(4, FP.PolynomialDerivative("x^2", 2));

            // without ^
            //with x
            //with factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-2, FP.PolynomialDerivative("-2x", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(2, FP.PolynomialDerivative("2x", 2));

            //without factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-1, FP.PolynomialDerivative("-x", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(1, FP.PolynomialDerivative("x", 2));

            //without x
            FP = new FunctionProperties("");
            Assert.AreEqual(0, FP.PolynomialDerivative("-2", 2));
            FP = new FunctionProperties("");
            Assert.AreEqual(0, FP.PolynomialDerivative("8", 2));
        }

        [TestMethod()]
        public void PolynomialIntegrateTest()
        {
            //with ^
            //with factor
            FunctionProperties FP = new FunctionProperties("");
            Assert.AreEqual( ((double)-16/3), FP.PolynomialIntegrate("-2x^2", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(((double)16/3), FP.PolynomialIntegrate("2x^2", 2));

            //without factor
            FP = new FunctionProperties("");
            Assert.AreEqual(((double)-8/3), FP.PolynomialIntegrate("-x^2", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(((double)8/3), FP.PolynomialIntegrate("x^2", 2));

            // without ^
            //with x
            //with factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-4, FP.PolynomialIntegrate("-2x", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(4, FP.PolynomialIntegrate("2x", 2));

            //without factor
            FP = new FunctionProperties("");
            Assert.AreEqual(-2, FP.PolynomialIntegrate("-x", 2));

            FP = new FunctionProperties("");
            Assert.AreEqual(2, FP.PolynomialIntegrate("x", 2));

            //without x
            FP = new FunctionProperties("");
            Assert.AreEqual(-4, FP.PolynomialIntegrate("-2", 2));
            FP = new FunctionProperties("");
            Assert.AreEqual(16, FP.PolynomialIntegrate("8", 2));
        }
    }
}