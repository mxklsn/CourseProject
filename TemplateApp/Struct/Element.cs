using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class Element
    {
        /// <summary>
        /// Создаем элемент из 8 трехмерных точек
        /// </summary>
        /// <param name="points">Восемь трехмерных точек</param>
        public Element(List<Point> points)
        {
            Points = points;
        }

        public List<Point> Points { get; private set; }
    }
}
