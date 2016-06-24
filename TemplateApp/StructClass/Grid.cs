using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class Grid
    {
        public List<Element> Elements { get; private set; }

        public Grid(Element element)
        {
            var points = new List<Element>();
            points.Add(element);
            Elements = points;
        }
    }


}
