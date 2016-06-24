using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenGlTemplateApp
{
    [DataContract]
    internal class ModelPoints
    {
        public ModelPoints(double[] array)
        {
            List<Point> points = new List<Point>();  // один массив с трехмерными точками
            double[] x = new double[array.Length / 3];  // массивы для быстрого нахождения Min() Max()
            double[] y = new double[array.Length / 3];
            double[] z = new double[array.Length / 3];

            for (int i = 0, k = 0; i < array.Length; i += 3, k++)
            {
                points.Add(new Point(array[i], array[i + 1], array[i + 2]));
                x[k] = array[i];
                y[k] = array[i + 1];
                z[k] = array[i + 2];
            }
            Point = points;
            ArrayX = x;
            ArrayY = y;
            ArrayZ = z;
        }

        public List<Point> Point { get; set; }  // метод

        public double[] ArrayX { get; set; }

        public double[] ArrayY { get; set; }

        public double[] ArrayZ { get; set; }

    }
}
