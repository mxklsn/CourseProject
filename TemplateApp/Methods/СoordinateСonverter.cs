using System;
using System.Linq;

namespace OpenGlTemplateApp.Methods
{
    internal class СoordinateСonverter
    {
        /// <summary>
        /// Получаем коэф. координат в система DirectX
        /// </summary>
        /// <param name="minX">мин. значение координаты X</param>
        /// <param name="maxX">макс. значение координаты X</param>
        /// <param name="minY">мин. значение координаты Y</param>
        /// <param name="maxY">макс. значение координаты Y</param>
        /// <param name="minZ">мин. значение координаты Z</param>
        /// <param name="maxZ">макс. значение координаты Z</param>
        public СoordinateСonverter(double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            var sideLength = new double[3];
            sideLength[0] = Math.Abs(maxX - minX);
            sideLength[1] = Math.Abs(maxY - minY);
            sideLength[2] = Math.Abs(maxZ - minZ);

            DxCoefficient = 1 / sideLength.Max();
        }

        public double DxCoefficient { set; get; }
    }
}
