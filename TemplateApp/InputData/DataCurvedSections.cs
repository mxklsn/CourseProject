using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenGlTemplateApp
{
    [DataContract]
    internal class DataCurvedSections
    {
        /// <summary>
        /// Криволинейные участки
        /// </summary>
        /// <param name="count">кол-во участков</param>
        /// <param name="array">массив с данными</param>
        public DataCurvedSections(int count, int[] array)
        {
            var curves = new List<CurveSection>();
            for (int i = 0; i < count * 7; i += 7)
            {
                curves.Add(new CurveSection(
                    array[i],
                    array[i + 1], array[i + 2], array[i + 3],
                    new Point(array[i + 4], array[i + 5], array[i + 6])));              
            }
            Curves = curves;
        }

        public struct CurveSection
        {
            public int Property, Nz, Nx, Ny;
            public Point CenterPoint;

            public CurveSection(int property, int nZo, int nPolygon, int nInterval, Point point)
            {
                Property = property;
                Nz = nZo;  // плоскость по оси Z
                Nx = nPolygon;
                Ny = nInterval;
                CenterPoint = point;
            }
        }

        public List<CurveSection> Curves { get; set; }    // кривоинейные участки
    }
}
