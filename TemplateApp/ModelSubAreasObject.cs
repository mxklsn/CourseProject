using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class ModelSubAreasObject
    {
        /* 
         *  Формирование трехмерных подобластей из файла с точками
         *  Для дальнейшего создание сетки
         */
        public List<Element> GetSubAreas(InputData inputData)
        {
            var faces = GetBottomFaceSubAreas(inputData);  // массив граней (см. class FaceSubArea)
            var count = inputData.ModelSubAreaPoints;  // кол-во опорных точек на осях
            var arrayElements = new List<Element>();  // массив всех трехмерных осблатей в фигуре
            var shift = (count.X - 1) * (count.Y - 1);

            for (int iz = 0; iz < shift * (count.Z - 1); iz += shift)
            {
                for (int iy = 0; iy < shift; iy += count.X - 1)
                {
                    for (int ix = 0; ix < count.X - 1; ix++)
                    {
                        var bottom = faces[iz + iy + ix];
                        var top = faces[iz + iy + ix + shift];
                        var element = new Element(
                            bottom.Points[0], bottom.Points[1],
                            bottom.Points[2], bottom.Points[3],
                            top.Points[0], top.Points[1],
                            top.Points[2], top.Points[3]);
                        arrayElements.Add(element);
                    }
                }
            }
            return arrayElements;
        }


        /*
         * Получаем массив площадок всех подобластей по Z
         * Структрура: одна площадка - 4 трехмерных точки
         */
        public List<FaceSubArea> GetBottomFaceSubAreas(InputData inputData)
        {
            var points = inputData.ModelPoints;  // все опорные точки описанной модели
            var count = inputData.ModelSubAreaPoints;  // кол-во опорных точек на осях
            var bottomFaceSubAreas = new List<FaceSubArea>();  // 4 соседнии точки образующие нижнюю грань

            for (int iz = 0; iz < count.X * count.Y * count.Z; iz += count.X * count.Y)
            {
                for (int iy = 0; iy < (count.Y - 1) * count.X; iy += count.X)
                {
                    for (int ix = 0; ix < count.X - 1; ix++)
                    {
                        var face = new FaceSubArea(
                            points.Point[iz + iy + ix],
                            points.Point[iz + iy + ix + 1],
                            points.Point[iz + iy + ix + count.X],
                            points.Point[iz + iy + ix + count.X + 1]);
                        bottomFaceSubAreas.Add(face);
                    }
                }
            }
            return bottomFaceSubAreas;
        }
    }
}
