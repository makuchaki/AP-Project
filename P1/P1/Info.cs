using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1
{
    public class Info
    {
        public static List<string> Form = new List<string>();
        public string Name;
        public string Family;
        public float Age;
        public string City;
        public double X;
        public double Y;
        public string Equation1;
        public string Equation2;
        public Info(string name, string family, float age, string city, double x,double y,string equation1,string equation2)
        {
            this.Name = name;
            this.Family = family;
            this.Age = age;
            this.City = city;
            this.X = x;
            this.Y = y;
            this.Equation1 = equation1;
            this.Equation2 = equation2;
        }
        
        public static bool IsNumber(string Temp)
        {
            bool result = true;
            for (int i = 0; i < Temp.Length; i++)
            {
                if (Temp[i] == '0' || Temp[i] == '1' || Temp[i] == '2' || Temp[i] == '3' || Temp[i] == '4' ||
                    Temp[i] == '5' || Temp[i] == '6' || Temp[i] == '7' || Temp[i] == '8' || Temp[i] == '9')
                {
                    result = true;
                }
                else
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
