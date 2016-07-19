using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class FaceSubArea
    {
        public List<Point> Points { get; private set; }

        /// <summary>
        /// Класс одной нижней(опорной) грани элемента
        /// Класс-помошник нужен на этапе формирования подобласти
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        public FaceSubArea(Point p1, Point p2, Point p3, Point p4)
        {
            var points = new List<Point>();
            points.Add(p1);
            points.Add(p2);
            points.Add(p3);
            points.Add(p4);
            Points = points;
        }
    }
}
