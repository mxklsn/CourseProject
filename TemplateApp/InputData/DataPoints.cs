using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenGlTemplateApp
{
    [DataContract]
    internal class DataPoints
    {
        /// <summary>
        /// Формирование методов Count, Point, Values
        /// </summary>
        /// <param name="count"></param>
        /// <param name="array"></param>
        public DataPoints(int[] count, double[] array)  
        {            
            var points = new List<Point>();  // один массив с трехмерными точками
            var x = new double[array.Length / 3];  // массивы со всеми занч.(XYZ) для быстрого нахождения Min() Max()
            var y = new double[array.Length / 3];
            var z = new double[array.Length / 3];

            for (int i = 0, k = 0; i < array.Length; i += 3, k++)
            {
                points.Add(new Point(array[i], array[i + 1], array[i + 2]));
                x[k] = array[i];
                y[k] = array[i + 1];
                z[k] = array[i + 2];
            }

            Point = points;
            Count = new CountPoints(count[0], count[1], count[2]);
            Values = new ValuesPoints(x, y, z);
        }

        public struct CountPoints
        {
            public int X, Y, Z;

            public CountPoints(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public struct ValuesPoints
        {
            public double[] X, Y, Z;

            public ValuesPoints(double[] x, double[] y, double[] z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }

        public CountPoints Count { get; set; }  // кол-во опорных точек для подобластей на X-Y-Z-осях 

        public List<Point> Point { get; set; }  // массив трехмерных точек (метод)

        public ValuesPoints Values { get; set; }  // отдельные массивы с точками по X,Y,Z для поиска Min(), Max()
    }
}
