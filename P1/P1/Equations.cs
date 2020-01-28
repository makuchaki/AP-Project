using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace P1
{
    public class Equations
    {
        TextBox EquationsTextBox;
        Label AnswerLabel;
        DrawDiagram DD;
        double Telorance;

        public List<char> Variables;
        List<char>[] VariablesInEquationI;

        public double[][] FactorMatrix;
        public double[] ConstantMatrix;
        
        public Equations(TextBox equationsTextBox, Label answerLabel)
        {
            this.EquationsTextBox = equationsTextBox;
            this.AnswerLabel = answerLabel;
            Telorance = 0.025;
        }

        public void ParseEquations()
        {
            string[] equations = EquationsTextBox.Text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            #region Variables
            //find all variables
            Variables = new List<char>();
            VariablesInEquationI = new List<char>[equations.Length];

            for (int i = 0; i < equations.Length; i++)
            {
                VariablesInEquationI[i] = equations[i].Split(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '=', '+', '-' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    .Select(token => token[0])
                    .ToList();

                VariablesInEquationI[i].ForEach(variable => { if (!Variables.Contains(variable)) Variables.Add(variable); });
            }
            #endregion

            #region Init Class Variables
            FactorMatrix = new double[equations.Length][];
            for (int i = 0; i < equations.Length; i++)
                FactorMatrix[i] = new double[equations.Length];

            ConstantMatrix = new double[equations.Length];
            #endregion

            List<int> signs;
            for (int i = 0; i < equations.Length; i++)
            {
                #region Signs
                // find removable chars to find signs in equation i
                List<char> removableChars = new List<char>();
                Variables.ForEach(Variable => removableChars.Add(Variable));
                removableChars.Add('=');
                removableChars.Add('0');
                removableChars.Add('1');
                removableChars.Add('2');
                removableChars.Add('3');
                removableChars.Add('4');
                removableChars.Add('5');
                removableChars.Add('6');
                removableChars.Add('7');
                removableChars.Add('8');
                removableChars.Add('9');

                //find signs in equation i
                string[] signTokens = equations[i].Split(removableChars.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                signs = new List<int>();

                //first and last number hasnt sign part 1 (before adding other signs)
                if (signTokens.Length == VariablesInEquationI[i].Count() - 1)
                    signs.Add(1);

                //first number hasnt sign 
                if (signTokens.Length == VariablesInEquationI[i].Count() && equations[i][0] != '-' && equations[i][0] != '+')
                    signs.Add(1);

                signTokens.ToList().ForEach(token =>
                {
                    if (token[0] == '-')
                        signs.Add(-1);
                    else
                        signs.Add(1);
                });

                //first and last number hasnt sign part 2 (after adding other signs)
                if (signTokens.Length == VariablesInEquationI[i].Count() - 1)
                    signs.Add(1);

                //last number hasnt sign 
                if (signTokens.Length == VariablesInEquationI[i].Count() && (equations[i][0] == '-' || equations[i][0] == '+'))
                    signs.Add(1);
                #endregion

                #region Factor And Conatant
                //find factors and constant in equation i
                string[] numberVariableTokens = equations[i].Split(new char[] { '=', '+', '-' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < numberVariableTokens.Length; j++)
                {
                    string[] number = numberVariableTokens[j].Split(Variables.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    string[] variable = numberVariableTokens[j].Split(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' }, StringSplitOptions.RemoveEmptyEntries);

                    if (variable.Length != 0 && number.Length != 0)
                    {
                        for (int k = 0; k < Variables.Count(); k++)
                            if (Variables[k] == variable[0][0])
                                FactorMatrix[i][k] = signs[j] * double.Parse(number[0]);
                    }
                    else if (variable.Length != 0 && number.Length == 0)
                    {
                        for (int k = 0; k < Variables.Count(); k++)
                            if (Variables[k] == variable[0][0])
                                FactorMatrix[i][k] = signs[j];
                    }
                    else if (number.Length != 0 && variable.Length == 0)
                    {
                        ConstantMatrix[i] = signs[j] * double.Parse(number[0]);
                    }
                }
                #endregion
            }
        }

        private double FirstEquationFunction(double x)
        {
            if (Variables.Contains('y') && Variables.Contains('x'))
            {
                if (Variables[0] == 'x')
                {
                    return (ConstantMatrix[0] - FactorMatrix[0][0] * x) / FactorMatrix[0][1] + Telorance;
                }
                else
                    return (ConstantMatrix[0] - FactorMatrix[0][1] * x) / FactorMatrix[0][0] + Telorance;
            }

            else
                return (ConstantMatrix[0] - FactorMatrix[0][0] * x) / FactorMatrix[0][1] + Telorance;
        }

        private double SecondEquationFunction(double x)
        {
            if (Variables.Contains('y') && Variables.Contains('x'))
            {
                if (Variables[0] == 'x')
                {
                    return (ConstantMatrix[1] - FactorMatrix[1][0] * x) / FactorMatrix[1][1] - Telorance;
                }
                else
                    return (ConstantMatrix[1] - FactorMatrix[1][1] * x) / FactorMatrix[1][0] - Telorance;
            }

            else
                return (ConstantMatrix[1] - FactorMatrix[1][0] * x) / FactorMatrix[1][1] - Telorance;
        }

        private double FirstEquationInverseFunction(double y)
        {
            if (Variables.Contains('y') && Variables.Contains('x'))
            {
                if (Variables[0] == 'x')
                {
                    return (ConstantMatrix[0] - FactorMatrix[0][1] * y) / FactorMatrix[0][0] ;
                }
                else
                    return (ConstantMatrix[0] - FactorMatrix[0][0] * y) / FactorMatrix[0][1];
            }

            else
                return (ConstantMatrix[0] - FactorMatrix[0][1] * y) / FactorMatrix[0][0] ;
        }

        private double SecondEquationInverseFunction(double y)
        {
            if (Variables.Contains('y') && Variables.Contains('x'))
            {
                if (Variables[0] == 'x')
                {
                    return (ConstantMatrix[1] - FactorMatrix[1][1] * y) / FactorMatrix[1][0] ;
                }
                else
                    return (ConstantMatrix[1] - FactorMatrix[1][0] * y) / FactorMatrix[1][1] ;
            }

            else
                return (ConstantMatrix[1] - FactorMatrix[1][1] * y) / FactorMatrix[1][0] ;
        }

        public void Draw (Canvas drawingCanvas, Canvas informationCanvas)
        {
            ParseEquations();
            if (Variables.Count != 2)
            {
                MessageBox.Show("Can not draw Equations Diagram");
                return;
            }

            DD = new DrawDiagram(drawingCanvas, informationCanvas);

            informationCanvas.Children.Clear();
            drawingCanvas.Children.Clear();

            double minY = Math.Min( Math.Floor(-1.5 * Math.Abs(FirstEquationFunction(0))), Math.Floor(-1.5 * Math.Abs(SecondEquationFunction(0))));
            double maxY = -1 * minY;

            double minX = Math.Min(Math.Min(Math.Floor(-1.5 * Math.Abs(FirstEquationInverseFunction(minY))), -1.5 * Math.Abs(SecondEquationInverseFunction(minY))),
                Math.Min(Math.Floor(-1.5 * Math.Abs(FirstEquationInverseFunction(maxY))), -1.5 * Math.Abs(SecondEquationInverseFunction(maxY))));
            double maxX = -1 * minX;

            if (answer() == true)
            {
                List<double> answers = new List<double>();
                double determinant = Determinant(FactorMatrix);

                for (int i = 0; i < FactorMatrix.Length; i++)
                    answers.Add(Math.Round(Determinant(KeramerMatrix(FactorMatrix, ConstantMatrix, i)) / determinant));

                if (Variables.Contains('y') && Variables.Contains('x'))
                {
                    if (Variables[0] == 'x')
                    {
                        minX = answers[0] - 4;
                        maxX = answers[0] + 4;
                        minY = answers[1] - 4;
                        maxY = answers[1] + 4;
                    }
                    else
                    {
                        minX = answers[1] - 4;
                        maxX = answers[1] + 4;
                        minY = answers[0] - 4;
                        maxY = answers[0] + 4;
                    }
                }

                else
                {
                    minX = answers[0] - 4;
                    maxX = answers[0] + 4;
                    minY = answers[1] - 4;
                    maxY = answers[1] + 4;
                }
            }

            DD.UpdateVariables(minX.ToString(), maxX.ToString(), minY.ToString(), maxY.ToString());

            DD.DrawChartLines();
            DD.DrawChartLabels();

            List<string> functions = new List<string>();
            List<Brush> colors = new List<Brush>();

            DD.DrawFunction(FirstEquationFunction, Brushes.Red);
            functions.Add("Line 1");
            colors.Add(Brushes.Red);

            DD.DrawFunction(SecondEquationFunction, Brushes.Blue);
            functions.Add("Line 2");
            colors.Add(Brushes.Blue);

            DD.DrawInformationLabels(functions, colors);
        }

        public double Determinant(double[][] matrix)
        {
            if (matrix.Length == 1)
                return matrix[0][0];

            double returnValue = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                if (i % 2 == 0)
                    returnValue += matrix[0][i] * Determinant(Tranahad(matrix, 0, i));
                else
                    returnValue -= matrix[0][i] * Determinant(Tranahad(matrix, 0, i));
            }

            return returnValue;
        }

        public double[][] Tranahad(double[][] matrix, int column, int row)
        {
            double[][] returnMatrix = new double[matrix.Length - 1][];

            for (int i = 0, k = 0; i < matrix.Length; i++)
                if (i != column)
                {
                    List<double> columnList = new List<double>();
                    for (int j = 0; j < matrix.Length; j++)
                        if (j != row)
                            columnList.Add(matrix[i][j]);
                    returnMatrix[k] = columnList.ToArray();
                    k++;
                }

            return returnMatrix;
        }

        public bool answer()
        {
            StringWriter SW = new StringWriter();

            ParseEquations();

            double determinant = Determinant(FactorMatrix);

            if (determinant == 0)
            {
                for (int i = 0; i < FactorMatrix.Length; i++)
                    if (Determinant(KeramerMatrix(FactorMatrix, ConstantMatrix, i)) != 0)
                    {
                        AnswerLabel.Content = "NO Solution";
                        return false;
                    }

                AnswerLabel.Content = "NO Unique Solution";
                return false;
            }

            for (int i = 0; i < FactorMatrix.Length; i++)
            {
                double answer = Math.Round(Determinant(KeramerMatrix(FactorMatrix, ConstantMatrix, i)) / determinant, 4);
                SW.Write($"{Variables[i]} = {answer}, ");
            }
            AnswerLabel.Content = SW.ToString();
            return true;
        }

        public double[][] KeramerMatrix(double[][] matrix, double[] constant, int row)
        {
            double[][] returnMatrix = new double[matrix.Length][];

            for (int i = 0; i < matrix.Length; i++)
                returnMatrix[i] = new double[matrix.Length];

            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (j == row)
                        returnMatrix[i][j] = constant[i];
                    else
                        returnMatrix[i][j] = matrix[i][j];
                }

            return returnMatrix;
        }
    }
}
