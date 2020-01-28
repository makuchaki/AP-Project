using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace P1
{
    public class TaylorSeries
    {
        DrawDiagram DD;
        Canvas DrawingCanvas;

        public int N;
        public int X0;

        public TaylorSeries(Canvas drawingCanvas)
        {
            this.DrawingCanvas = drawingCanvas;
            DD = new DrawDiagram(DrawingCanvas, new Canvas() );
        }

        public void UpdateVariables(string n, string x0)
        {
            this.N = int.Parse(n);
            this.X0 = int.Parse(x0);

            double minX = double.MaxValue;
            for (double i = 0; ; i -= 0.01)
                if (1 < Math.Abs(TaylorSeriesSin(i) - Math.Sin(i)))
                {
                    minX = i;
                    break;
                }
            minX = Math.Floor(minX * 2);

            double maxX = -1 * minX;

            double minY = double.MaxValue;
            for (double i = 0; i <= Math.PI * 2; i += 0.01)
                if (Math.Sin(i) < minY)
                    minY = Math.Sin(i);
            minY = Math.Floor(minY * 2);

            double maxY = -1 * minY;

            DD.UpdateVariables(minX.ToString(), maxX.ToString(), minY.ToString(), maxY.ToString());
        }

        public void Draw()
        {
            DD.DrawChartLines();
            DD.DrawChartLabels();

            DD.DrawFunction(Math.Sin, Brushes.Red);
            DD.DrawFunction(TaylorSeriesSin, Brushes.Blue);
        }
        
        public double TaylorSeriesSin(double x)
        {
            double returnValue = 0;

            for (int i = 0; i < N; i++)
                returnValue += Math.Pow(x - X0, i) * Math.Sin(i * Math.PI / 2 + X0) / Factorial(i);

            return returnValue;
        }

        public int Factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            return n * Factorial(n - 1);
        }
    }
}
