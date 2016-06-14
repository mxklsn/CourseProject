using System.Collections.Generic;

namespace OpenGlTemplateApp
{   
    /*
     * Класс одной нижней(опорной) грани элемента
     * Класс-помошник нужен на этапе формирования подобласти как конечного элемента
     * Используется только для более простого формирования подобласти
     */
    internal class FaceSubArea
    {
        public List<Point> Points { get; private set; }

        public FaceSubArea(Point p1, Point p2, Point p3, Point p4)
        {
            List<Point> points = new List<Point>();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            Points = points;
        }
    }
}
