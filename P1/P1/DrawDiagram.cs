using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace P1
{
    public class DrawDiagram
    {
        private Canvas DrawingCanvas;
        private Canvas InformationCanvas;

        #region Drawing variables
        //dragging variables
        public Point FirstPoint;
        public Point SecondPoint;

        //canvas variables
        private double CanvasCenterX;
        private double CanvasCenterY;

        //diagram variables
        private int DiagramParts;
        private double DiagramMinX;
        private double DiagramMaxX;
        private double DiagramMinY;
        private double DiagramMaxY;

        private double DiagramCenterX;
        private double DiagramCenterY;
        private double DiagramDistanseX;
        private double DiagramDistanceY;

        //converting variables
        public double ConvertingNumberX;
        public double ConvertingNumberY;
        #endregion

        public DrawDiagram(Canvas drawingCanvas, Canvas informationCanvas)
        {
            this.DrawingCanvas = drawingCanvas;
            this.InformationCanvas = informationCanvas;
        }

        public void UpdateVariables(string minX, string maxX, string minY, string maxY)
        {
            // canvas Variables
            CanvasCenterX = DrawingCanvas.Width / 2;
            CanvasCenterY = DrawingCanvas.Height / 2;

            // diagram Variables
            DiagramParts = 1000;
            DiagramMinX = double.Parse(minX);
            DiagramMaxX = double.Parse(maxX);
            DiagramMinY = double.Parse(minY);
            DiagramMaxY = double.Parse(maxY);

            DiagramCenterX = (DiagramMinX + DiagramMaxX) / 2;
            DiagramCenterY = (DiagramMinY + DiagramMaxY) / 2;
            DiagramDistanseX = Math.Abs(DiagramMaxX - DiagramMinX);
            DiagramDistanceY = Math.Abs(DiagramMaxY - DiagramMinY);

            //converting Variables
            ConvertingNumberX = DrawingCanvas.Width / DiagramDistanseX;
            ConvertingNumberY = DrawingCanvas.Height / DiagramDistanceY;
        }

        public void DrawChartLines()
        {
            Line lineX = new Line();
            lineX.Stroke = Brushes.Black;
            lineX.StrokeThickness = 1;

            lineX.X1 = 0;
            lineX.X2 = DrawingCanvas.Width;

            lineX.Y1 = 0 + CanvasCenterY;
            lineX.Y2 = 0 + CanvasCenterY;

            DrawingCanvas.Children.Add(lineX);

            Line lineY = new Line();
            lineY.Stroke = Brushes.Black;
            lineY.StrokeThickness = 1;

            lineY.X1 = 0 + CanvasCenterX;
            lineY.X2 = 0 + CanvasCenterX;

            lineY.Y1 = 0;
            lineY.Y2 = DrawingCanvas.Height;

            DrawingCanvas.Children.Add(lineY);

            for (int i = -16; i <= 16; i++)
            {
                //hirozentical line
                Line line = new Line();
                line.Stroke = Brushes.Black;

                line.StrokeThickness = 0.1;
                if (i % 4 == 0)
                    line.StrokeThickness = 0.5;

                line.X1 = CanvasCenterX + i * (DrawingCanvas.Width / 32);
                line.X2 = CanvasCenterX + i * (DrawingCanvas.Width / 32);

                line.Y1 = 0;
                line.Y2 = DrawingCanvas.Height;

                DrawingCanvas.Children.Add(line);

                // vertical lines
                line = new Line();
                line.Stroke = Brushes.Black;

                line.StrokeThickness = 0.1;
                if (i % 4 == 0)
                    line.StrokeThickness = 0.5;

                line.Y1 = CanvasCenterY + i * (DrawingCanvas.Height / 32);
                line.Y2 = CanvasCenterY + i * (DrawingCanvas.Height / 32);

                line.X1 = 0;
                line.X2 = DrawingCanvas.Width;

                DrawingCanvas.Children.Add(line);
            }
        }

        public void DrawChartLabels()
        {
            double marginFactor = 1.0;

            Label centerLabel = new Label();
            centerLabel.Content = $"({DiagramCenterX},{DiagramCenterY})";
            centerLabel.FontWeight = FontWeights.Bold;
            centerLabel.Margin = new Thickness(marginFactor * DrawingCanvas.Width / 2, marginFactor * DrawingCanvas.Height / 2, 0, 0);
            DrawingCanvas.Children.Add(centerLabel);

            Label minX = new Label();
            minX.Content = DiagramMinX.ToString();
            minX.FontWeight = FontWeights.Bold;
            minX.Margin = new Thickness(0, marginFactor * DrawingCanvas.Height / 2, 0, 0);
            DrawingCanvas.Children.Add(minX);

            Label maxX = new Label();
            maxX.Content = DiagramMaxX.ToString();
            maxX.FontWeight = FontWeights.Bold;
            maxX.Margin = new Thickness(DrawingCanvas.Width - 25, marginFactor * DrawingCanvas.Height / 2, 0, 0);
            DrawingCanvas.Children.Add(maxX);

            Label minY = new Label();
            minY.Content = DiagramMinY.ToString();
            minY.FontWeight = FontWeights.Black;
            minY.Margin = new Thickness(marginFactor * DrawingCanvas.Width / 2, DrawingCanvas.Height - 25, 0, 0);
            DrawingCanvas.Children.Add(minY);

            Label maxY = new Label();
            maxY.Content = DiagramMaxY.ToString();
            maxY.FontWeight = FontWeights.Bold;
            maxY.Margin = new Thickness(marginFactor * DrawingCanvas.Width / 2, 0, 0, 0);
            DrawingCanvas.Children.Add(maxY);
        }

        public void DrawInformationLabels (List<string> functions, List<Brush> color)
        {
            InformationCanvas.Width = 0;

            double leftMargin = 0;
            double labelWidth = 0;

            for (int i=0; i<functions.Count; i++)
            {
                if (functions[i].Length == 4)
                    labelWidth = 20;
                else if (functions[i].Length == 14)
                    labelWidth = 55;
                else if (functions[i].Length == 15)
                    labelWidth = 60;
                else
                    labelWidth = 82;

                InformationCanvas.Width += labelWidth + 20;

                Label label = new Label();
                label.Content = functions[i];
                label.HorizontalContentAlignment = HorizontalAlignment.Left;
                label.FontSize = 8;
                label.Margin = new Thickness(leftMargin + 2.5, -3, 0, 0);
                InformationCanvas.Children.Add(label);

                Line line = new Line();
                line.Stroke = color[i];
                line.StrokeThickness = 3;

                line.X1 = (leftMargin + 2.5) + labelWidth ;
                line.X2 = line.X1 + 15;

                line.Y1 = 7.5;
                line.Y2 = 7.5;

                InformationCanvas.Children.Add(line);

                leftMargin = InformationCanvas.Width; 
            }
        }

        public void DrawFunction(FunctionDelegate function, Brush color)
        {
            for (int i = -1 * DiagramParts; i < DiagramParts; i++)
            {
                double diagramElementX1 = i * (DiagramDistanseX / (2 * DiagramParts)) + DiagramCenterX;
                double diagramElementX2 = (i + 1) * (DiagramDistanseX / (2 * DiagramParts)) + DiagramCenterX;

                double diagramElementY1 = function(diagramElementX1);
                double diagramElementY2 = function(diagramElementX2);

                if (double.IsNaN(diagramElementY1) || double.IsNaN(diagramElementY2) || ConvertorY(diagramElementY1) < 0 || DrawingCanvas.Height < ConvertorY(diagramElementY1))
                    continue;

                Line line = new Line();
                line.Stroke = color;
                line.StrokeThickness = 2;

                line.X1 = ConvertorX(diagramElementX1);
                line.X2 = ConvertorX(diagramElementX2);

                line.Y1 = ConvertorY(diagramElementY1);
                line.Y2 = ConvertorY(diagramElementY2);

                DrawingCanvas.Children.Add(line);
            }
        }

        public double ConvertorX(double diagramElementX) => CanvasCenterX - (DiagramCenterX - diagramElementX) * ConvertingNumberX;

        public double ConvertorY(double diagramElementY) => CanvasCenterY + (DiagramCenterY - diagramElementY) * ConvertingNumberY;
    }
}
