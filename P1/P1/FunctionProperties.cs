using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    public delegate double FunctionDelegate(double x);
    
    public class FunctionProperties
    {
        private string FunctionNotation;
        private double TangentPointX;

        public FunctionProperties(string functionNotation)
        {
            this.FunctionNotation = functionNotation;
        }
        
        #region Parse Methods
        public FunctionDelegate Parse() => this.Function;

        public double Function(double x)
        {
            double returnValue = 0;
            List<int> signs = new List<int>();

            string[] signTokens = FunctionNotation.Split(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'x', '^', 'L', 'O', 'G', 'S', 'C', 'T', 'A', 'N', 'I', 'H', ')', '(' }, StringSplitOptions.RemoveEmptyEntries);
            string[] FunctionTokens = FunctionNotation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            #region Sign
            if (signTokens.Length < FunctionTokens.Length)
            {
                signs.Add(1);
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            else
            {
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            #endregion

            for (int i = 0; i < FunctionTokens.Length; i++)
            {
                #region Log Part
                if (FunctionTokens[i].Contains("LN"))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Log(PolynomialFunction(logTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("LOG"))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'O', 'G', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Log10(PolynomialFunction(logTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("LOG") && FunctionTokens[i].Contains(','))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'O', 'G', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Log(PolynomialFunction(logTokens.First(), x), double.Parse(logTokens.Last()));
                }
                #endregion

                #region Trigonometry Part
                else if (FunctionTokens[i].Contains("SIN") && !FunctionTokens[i].Contains("SINH"))
                {
                    string[] sinTokens = FunctionTokens[i].Split(new char[] { 'S', 'I', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Sin(PolynomialFunction(sinTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("COS") && !FunctionTokens[i].Contains("COSH"))
                {
                    string[] cosTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'S', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Cos(PolynomialFunction(cosTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("TAN") && !FunctionTokens[i].Contains("TANH"))
                {
                    string[] tanTokens = FunctionTokens[i].Split(new char[] { 'T', 'A', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Tan(PolynomialFunction(tanTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("COT") && !FunctionTokens[i].Contains("COTH"))
                {
                    string[] cotTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'T', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (1 / Math.Tan(PolynomialFunction(cotTokens.First(), x)));
                }

                else if (FunctionTokens[i].Contains("SINH"))
                {
                    string[] sinhTokens = FunctionTokens[i].Split(new char[] { 'S', 'I', 'N', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Sinh(PolynomialFunction(sinhTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("COSH"))
                {
                    string[] coshTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'S', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Cosh(PolynomialFunction(coshTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("TANH"))
                {
                    string[] tanhTokens = FunctionTokens[i].Split(new char[] { 'T', 'A', 'N', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Tanh(PolynomialFunction(tanhTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("COTH"))
                {
                    string[] cothTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'T', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (1 / Math.Tanh(PolynomialFunction(cothTokens.First(), x)));
                }
                #endregion

                #region Polynomial Part
                else if (FunctionTokens[i].Contains("^x"))
                {
                    string @base = FunctionTokens[i].Split(new char[] { '^', 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += signs[i] * Math.Pow(double.Parse(@base), x);
                }

                else if(FunctionTokens[i].Contains("x^"))
                {
                    string[] numbers = FunctionTokens[i].Split(new char[] { 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
                    if(numbers.Length == 2)
                        returnValue += signs[i] * double.Parse(numbers[0]) * Math.Pow(x, double.Parse(numbers[1]));
                    if(numbers.Length == 1)
                        returnValue += signs[i] * Math.Pow(x, double.Parse(numbers[0]));
                }

                else if (FunctionTokens[i].Contains('x'))
                {
                    string[] number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                    if (number.Length == 1)
                        returnValue += signs[i] * double.Parse(number[0]) * x;
                    else
                        returnValue += signs[i] * x;
                }

                else
                {
                    string number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += signs[i] * double.Parse(number);
                }
                #endregion

            }

            return returnValue;
        }
        
        public double PolynomialFunction(string functionNotation, double x)
        {
            double returnValue = 0;
            List<int> signs = new List<int>();

            string[] signTokens = functionNotation.Split(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
            string[] functionTokens = functionNotation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            #region Sign
            if (signTokens.Length < functionTokens.Length)
            {
                signs.Add(1);
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            else
            {
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            #endregion

            for (int i = 0; i < functionTokens.Length; i++)
            {
                if (functionTokens[i].Contains("x^"))
                {
                    string[] numbers = functionTokens[i].Split(new char[] { 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numbers.Length == 2)
                        returnValue += signs[i] * double.Parse(numbers[0]) * Math.Pow(x, double.Parse(numbers[1]));
                    if (numbers.Length == 1)
                        returnValue += signs[i] * Math.Pow(x, double.Parse(numbers[0]));
                }

                else if (functionTokens[i].Contains('x'))

                {
                    string[] number = functionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                    if (number.Length == 1)
                        returnValue += signs[i] * double.Parse(number[0]) * x;
                    else
                        returnValue += signs[i] * x;
                }

                else
                {
                    string number = functionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += signs[i] * double.Parse(number);
                }
            }

            return returnValue;
        }
        #endregion

        #region Derivative
        public FunctionDelegate Derivative() => DerivativeFunction;

        public double DerivativeFunction(double x)
        {
            double returnValue = 0;
            List<int> signs = new List<int>();

            string[] signTokens = FunctionNotation.Split(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'x', '^', 'L', 'O', 'G', 'S', 'C', 'T', 'A', 'N', 'I', 'H', ')', '(' }, StringSplitOptions.RemoveEmptyEntries);
            string[] FunctionTokens = FunctionNotation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            #region Sign
            if (signTokens.Length < FunctionTokens.Length)
            {
                signs.Add(1);
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            else
            {
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            #endregion

            for (int i = 0; i < FunctionTokens.Length; i++)
            {
                #region Log Part
                if (FunctionTokens[i].Contains("LN"))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (1 / PolynomialFunction(logTokens.First(), x)) * PolynomialDerivative(logTokens.First(), x);
                }
                
                else if (FunctionTokens[i].Contains("LOG"))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'O', 'G', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        returnValue += signs[i] * (1 / Math.Log(10) * PolynomialFunction(logTokens.First(), x)) * PolynomialDerivative(logTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("LOG") && FunctionTokens[i].Contains(','))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'O', 'G', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (1 / Math.Log(double.Parse(logTokens.Last())) * PolynomialFunction(logTokens.First(), x)) * PolynomialDerivative(logTokens.First(), x);
                }
                #endregion

                #region Trigonometry Part
                else if (FunctionTokens[i].Contains("SIN") && !FunctionTokens[i].Contains("SINH"))
                {
                    string[] sinTokens = FunctionTokens[i].Split(new char[] { 'S', 'I', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Cos(PolynomialFunction(sinTokens.First(), x)) * PolynomialDerivative(sinTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("COS") && !FunctionTokens[i].Contains("COSH"))
                {
                    string[] cosTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'S', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += -1 * signs[i] * Math.Sin(PolynomialFunction(cosTokens.First(), x)) * PolynomialDerivative(cosTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("TAN") && !FunctionTokens[i].Contains("TANH"))
                {
                    string[] tanTokens = FunctionTokens[i].Split(new char[] { 'T', 'A', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (1 + Math.Pow(Math.Tan(PolynomialFunction(tanTokens.First(), x)), 2)) * PolynomialDerivative(tanTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("COT") && !FunctionTokens[i].Contains("COTH"))
                {
                    string[] cotTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'T', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += -1 * signs[i] * (1 + Math.Pow(1 / Math.Tan(PolynomialFunction(cotTokens.First(), x)), 2)) * PolynomialDerivative(cotTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("SINH"))
                {
                    string[] sinhTokens = FunctionTokens[i].Split(new char[] { 'S', 'I', 'N', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Cosh(PolynomialFunction(sinhTokens.First(), x)) * PolynomialDerivative(sinhTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("COSH"))
                {
                    string[] coshTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'S', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Sinh(PolynomialFunction(coshTokens.First(), x)) * PolynomialDerivative (coshTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("TANH"))
                {
                    string[] tanhTokens = FunctionTokens[i].Split(new char[] { 'T', 'A', 'N', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Pow(1 / Math.Cosh(PolynomialFunction(tanhTokens.First(), x)), 2) * PolynomialDerivative(tanhTokens.First(), x);
                }

                else if (FunctionTokens[i].Contains("COTH"))
                {
                    string[] cothTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'T', 'H', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += -1 * signs[i] * Math.Pow(1 / Math.Sinh(PolynomialFunction(cothTokens.First(), x)), 2) * PolynomialDerivative(cothTokens.First(), x);
                }
                #endregion

                #region Polynomial Part
                else if (FunctionTokens[i].Contains("^x"))
                {
                    string @base = FunctionTokens[i].Split(new char[] { '^', 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += signs[i] * Math.Pow(double.Parse(@base), x) * Math.Log(double.Parse(@base));
                }

                else if (FunctionTokens[i].Contains("x^"))
                {
                    string[] numbers = FunctionTokens[i].Split(new char[] { 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numbers.Length == 2)
                        returnValue += signs[i] * double.Parse(numbers[0]) * double.Parse(numbers[1]) * Math.Pow(x, double.Parse(numbers[1]) - 1);
                    if (numbers.Length == 1)
                        returnValue += signs[i] * double.Parse(numbers[0]) * Math.Pow(x, double.Parse(numbers[0]) - 1);
                }

                else if (FunctionTokens[i].Contains('x'))
                {
                    string[] number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                    if (number.Length == 1)
                        returnValue += signs[i] * double.Parse(number[0]);
                    else
                        returnValue += signs[i];
                }

                else
                {
                    string number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += 0;
                }
                #endregion

            }

            return returnValue;
        }

        public double PolynomialDerivative(string functionNotation, double x)
        {
            double returnValue = 0;
            List<int> signs = new List<int>();

            string[] signTokens = functionNotation.Split(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'x', '^', 'L', 'O', 'G', 'S', 'C', 'T', 'A', 'N', 'I', 'H', ')', '(' }, StringSplitOptions.RemoveEmptyEntries);
            string[] FunctionTokens = functionNotation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            #region Sign
            if (signTokens.Length < FunctionTokens.Length)
            {
                signs.Add(1);
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            else
            {
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            #endregion

            for (int i = 0; i < FunctionTokens.Length; i++)
            {
                if (FunctionTokens[i].Contains("^x"))
                {
                    string @base = FunctionTokens[i].Split(new char[] { '^', 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += signs[i] * Math.Pow(double.Parse(@base), x) * Math.Log(double.Parse(@base));
                }

                else if (FunctionTokens[i].Contains("x^"))
                {
                    string[] numbers = FunctionTokens[i].Split(new char[] { 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numbers.Length == 2)
                        returnValue += signs[i] * double.Parse(numbers[0]) * double.Parse(numbers[1]) * Math.Pow(x, double.Parse(numbers[1]) - 1);
                    if (numbers.Length == 1)
                        returnValue += signs[i] * double.Parse(numbers[0]) * Math.Pow(x, double.Parse(numbers[0]) - 1);
                }

                else if (FunctionTokens[i].Contains('x'))
                {
                    string[] number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                    if (number.Length == 1)
                        returnValue += signs[i] * double.Parse(number[0]);
                    else
                        returnValue += signs[i];
                }

                else
                {
                    string number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += 0;
                }
            }
            return returnValue;
        }
        #endregion

        #region Integrate
        public FunctionDelegate Integrate() => IntegrateFunction;

        private double IntegrateFunction(double x)
        {
            double returnValue = 0;
            List<int> signs = new List<int>();

            string[] signTokens = FunctionNotation.Split(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'x', '^', 'L', 'O', 'G', 'S', 'C', 'T', 'A', 'N', 'I', 'H', ')', '(' }, StringSplitOptions.RemoveEmptyEntries);
            string[] FunctionTokens = FunctionNotation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            #region Sign
            if (signTokens.Length < FunctionTokens.Length)
            {
                signs.Add(1);
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            else
            {
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            #endregion

            for (int i = 0; i < FunctionTokens.Length; i++)
            {
                #region Log Part
                if (FunctionTokens[i].Contains("LN"))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (x * Math.Log(PolynomialFunction(logTokens.First(), x)) - x);
                }

                else if (FunctionTokens[i].Contains("LOG"))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'O', 'G', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (x * Math.Log10(PolynomialFunction(logTokens.First(), x)) - x);
                }

                else if (FunctionTokens[i].Contains("LOG") && FunctionTokens[i].Contains(','))
                {
                    string[] logTokens = FunctionTokens[i].Split(new char[] { 'L', 'O', 'G', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * (x * Math.Log(PolynomialFunction(logTokens.First(), x), double.Parse(logTokens.Last())) - x);
                }
                #endregion

                #region Trigonometry Part
                else if (FunctionTokens[i].Contains("SIN") && !FunctionTokens[i].Contains("SINH"))
                {
                    string[] sinTokens = FunctionTokens[i].Split(new char[] { 'S', 'I', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += -1 * signs[i] * Math.Cos(PolynomialFunction(sinTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("COS") && !FunctionTokens[i].Contains("COSH"))
                {
                    string[] cosTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'S', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Sin(PolynomialFunction(cosTokens.First(), x));
                }

                else if (FunctionTokens[i].Contains("TAN") && !FunctionTokens[i].Contains("TANH"))
                {
                    string[] tanTokens = FunctionTokens[i].Split(new char[] { 'T', 'A', 'N', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += -1 * signs[i] * Math.Log(Math.Cos(PolynomialFunction(tanTokens.First(), x)));
                }

                else if (FunctionTokens[i].Contains("COT") && !FunctionTokens[i].Contains("COTH"))
                {
                    string[] cotTokens = FunctionTokens[i].Split(new char[] { 'C', 'O', 'T', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    returnValue += signs[i] * Math.Log(Math.Sin(PolynomialFunction(cotTokens.First(), x)));
                }
                
                else if (FunctionTokens[i].Contains("x^"))
                {
                    string[] numbers = FunctionTokens[i].Split(new char[] { 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numbers.Length == 2)
                        returnValue += (signs[i] * double.Parse(numbers[0]) * Math.Pow(x, double.Parse(numbers[1]) + 1.0)) / (double.Parse(numbers[1]) + 1.0);
                    if (numbers.Length == 1)
                        returnValue += signs[i] * Math.Pow(x, double.Parse(numbers[0]) + 1) / (double.Parse(numbers[0]) + 1);
                }

                else if (FunctionTokens[i].Contains('x'))
                {
                    string[] number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                    if (number.Length == 1)
                        returnValue += signs[i] * double.Parse(number[0]) * Math.Pow(x, 2) / 2;
                    else
                        returnValue += signs[i] * Math.Pow(x, 2) / 2;
                }

                else
                {
                    string number = FunctionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += signs[i] * double.Parse(number) * x;
                }
                #endregion

            }

            return returnValue;
        }

        public double PolynomialIntegrate(string functionNotation, double x)
        {
            double returnValue = 0;
            List<double> signs = new List<double>();

            string[] signTokens = functionNotation.Split(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
            string[] functionTokens = functionNotation.Split(new char[] { '+', '-' }, StringSplitOptions.RemoveEmptyEntries);

            #region Sign
            if (signTokens.Length < functionTokens.Length)
            {
                signs.Add(1);
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            else
            {
                for (int i = 0; i < signTokens.Length; i++)
                {
                    if (signTokens[i][0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                }
            }
            #endregion
            
            for (int i = 0; i < functionTokens.Length; i++)
            {
                if (functionTokens[i].Contains("x^"))
                {
                    string[] numbers = functionTokens[i].Split(new char[] { 'x', '^' }, StringSplitOptions.RemoveEmptyEntries);
                    if (numbers.Length == 2)
                        returnValue += (signs[i] * double.Parse(numbers[0]) * Math.Pow(x, double.Parse(numbers[1]) + 1.0)) / (double.Parse(numbers[1]) + 1.0);
                    if (numbers.Length == 1)
                        returnValue += signs[i] * Math.Pow(x, double.Parse(numbers[0]) + 1) / (double.Parse(numbers[0]) + 1);
                }

                else if (functionTokens[i].Contains('x'))
                {
                    string[] number = functionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries);
                    if (number.Length == 1)
                        returnValue += signs[i] * double.Parse(number[0]) * Math.Pow(x , 2) / 2;
                    else
                        returnValue += signs[i] * Math.Pow(x, 2) / 2;
                }

                else
                {
                    string number = functionTokens[i].Split(new char[] { 'x' }, StringSplitOptions.RemoveEmptyEntries).First();
                    returnValue += signs[i] * double.Parse(number) * x;
                }
            }
            return returnValue;
        }
        #endregion

        #region TangentLine
        public FunctionDelegate TangentLine(double point)
        {
            this.TangentPointX = point;
            return TangentLineFunction;
        }

        public double TangentLineFunction(double x) => DerivativeFunction(TangentPointX) * (x - TangentPointX) + Function(TangentPointX);
        #endregion
    }
}
