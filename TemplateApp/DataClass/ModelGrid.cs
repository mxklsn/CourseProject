using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class ModelGrid
    {
        public Grid GetGrid(InputData inputData, List<Element> modelSubAreas)
        {
            //Console.WriteLine(inputData.ModelPoints);
            Point p1, p2, p3, p4, p5, p6, p7, p8;
            p1 = p2 = p3 = p4 = p5 = p6 = p7 = p8 = new Point(1, 1, 1);
            var element = new Element(p1, p2, p3, p4, p5, p6, p7, p8);
            var grid = new Grid(element);



            return grid;
        }
    }
}
