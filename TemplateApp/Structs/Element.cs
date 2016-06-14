using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class Element
    {
        /*
         * Создаем элемент из 8 трехмерных точек
         */

        public List<Point> Points { get; private set; }

        public Element(Point p1, Point p2, Point p3, Point p4,
            Point p5, Point p6, Point p7, Point p8)
        {
            List<Point> points = new List<Point>();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            points.Add(p5);
            points.Add(p6);
            points.Add(p7);
            points.Add(p8);
            Points = points;
        }
    }
}
