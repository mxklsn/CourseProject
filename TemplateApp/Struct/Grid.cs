using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class Grid
    {
        /// <summary>
        /// Сетка
        /// </summary>
        /// <param name="element">Восемь трехмерных точек</param>
        public Grid(Element element)
        {
            var points = new List<Element>();
            points.Add(element);
            Elements = points;
        }

        public List<Element> Elements { get; private set; }
    }
}
