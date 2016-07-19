using System;
using System.Collections.Generic;

namespace OpenGlTemplateApp.Methods
{
    internal class Mesh
    {
        private InputData Data { get; set; }

        private List<GetSections.AreaProperty> Areas { get; set; }

        private List<GetSections.CurvePoints> CurvePoints { get; set; }

        public List<Element> ElementsGrid { get; set; }

        public int[] SubAreasCounts { get; set; }

        public struct Curve
        {
            public List<Point> P1 { get; set; }

            public List<Point> P2 { get; set; }

            public List<Point> Pcenter { get; set; }

            public Curve(List<Point> p1, List<Point> p2, List<Point> pCenter)
                : this()
            {
                P1 = p1;
                P2 = p2;
                Pcenter = pCenter;
            }
        }

        public Mesh(
            InputData data,
            List<GetSections.AreaProperty> areas, 
            List<GetSections.CurvePoints> curvePoints)
        {
            Data = data;
            Areas = areas;
            CurvePoints = curvePoints;
        }

        public void GetGrid()
        {
            var list = new List<Element>();
            var subAreasCounts = new int[Areas.Count];
            for (int i = 0; i < Areas.Count; i++)
            {
                var area = Areas[i];
                var subAreasCount = 0;
                var gridPoints = _getPointsByArea(area);  // все линии по Х с точками в подобласти 

                var countX = area.ElemIntervals[0].IntervalCount + 1;
                var countY = area.ElemIntervals[1].IntervalCount + 1;
                var countZ = area.ElemIntervals[2].IntervalCount + 1;
                for (int z = 0; z < countZ - 1; z++)
                {
                    for (int y = 0; y < countY - 1; y++)
                    {
                        for (int x = 0; x < countX - 1; x++)
                        {
                            var listPoint = new List<Point>();
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z]);
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z + 1]);
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z + countX]);
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z + countX + 1]);
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z + countX * countY]);
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z + countX * countY + 1]);
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z + countX * countY + countX]);
                            listPoint.Add(gridPoints[x + countX * y + countX * countY * z + countX * countY + countX + 1]);
                            var element = new Element(listPoint);
                            list.Add(element);
                            subAreasCount++;
                        }
                    }
                }
                subAreasCounts[i] = subAreasCount;
            }
            ElementsGrid = list;
            SubAreasCounts = subAreasCounts;
        }

        /* Все точки в области
         */
        private List<Point> _getPointsByArea(GetSections.AreaProperty area)
        {
            var x1 = _getPointsOnFace(
                area.PointsNumbers[0],
                area.PointsNumbers[1],
                area.PointsNumbers[4],
                area.PointsNumbers[5],
                area.ElemIntervals[0],
                area.ElemIntervals[2]);
            var y1 = _getPointsOnFace(
                area.PointsNumbers[1],
                area.PointsNumbers[3],
                area.PointsNumbers[5],
                area.PointsNumbers[7],
                area.ElemIntervals[1],
                area.ElemIntervals[2]);
            var x2 = _getPointsOnFace(
                area.PointsNumbers[2],
                area.PointsNumbers[3],
                area.PointsNumbers[6],
                area.PointsNumbers[7],
                area.ElemIntervals[0],
                area.ElemIntervals[2]);
            var y2 = _getPointsOnFace(
                area.PointsNumbers[0],
                area.PointsNumbers[2],
                area.PointsNumbers[4],
                area.PointsNumbers[6],
                area.ElemIntervals[1],
                area.ElemIntervals[2]);

            var pointsLines = new List<List<Point>>();
            for (int i = 0; i < x1.Count; i++)
            {
                pointsLines.Add(x1[i]);
                for (int j = 1; j < y2[i].Count - 1; j++)
                {
                    pointsLines.Add(_getPointsByLine(y2[i][j], y1[i][j],
                        area.ElemIntervals[0].IntervalCount, area.ElemIntervals[0].Coef));
                }
                pointsLines.Add(x2[i]);
            }

            var points = new List<Point>();
            for (int i = 0; i < pointsLines.Count; i++)
            {
                for (int j = 0; j < pointsLines[i].Count; j++)
                {
                    points.Add(pointsLines[i][j]);
                }
            }
            return points;
        }


        /* Получение всех точек на грани
         */
        private List<List<Point>> _getPointsOnFace(int p11, int p12, int p21, int p22, 
            DataIntervalsData.Coefficient coef1, DataIntervalsData.Coefficient coef2)
        {
            var listPoint = new List<List<Point>>();
            var isCurve = _isCurve(p11, p12);
            if (isCurve != "false")  // если в грани кривая по Y или X
            {
                var z1 = _getPointsByInterval(p11, p21, coef2);
                var z2 = _getPointsByInterval(p12, p22, coef2);

                var isCurveUp = _isCurve(p21, p22);
                var centersPoints = _getPointsByLine(
                    Data.DataCurvedSections.Curves[Convert.ToInt32(isCurve)].CenterPoint,
                    Data.DataCurvedSections.Curves[Convert.ToInt32(isCurveUp)].CenterPoint,
                    coef2.IntervalCount,
                    coef2.Coef);

                listPoint.Add(_getPointsByCurve(  // first
                    _getPointByNumber(p11),
                    _getPointByNumber(p12),
                    coef1.IntervalCount,
                    coef1.Coef,
                    Data.DataCurvedSections.Curves[Convert.ToInt32(isCurve)].CenterPoint));
                for (int i = 1; i < centersPoints.Count - 1; i++)
                {
                    listPoint.Add(_getPointsByCurve(
                        z1[i],
                        z2[i],
                        coef1.IntervalCount,
                        coef1.Coef,
                        centersPoints[i]
                        ));
                }
                listPoint.Add(_getPointsByCurve(  // last
                    _getPointByNumber(p21),
                    _getPointByNumber(p22),
                    coef1.IntervalCount,
                    coef1.Coef,
                    Data.DataCurvedSections.Curves[Convert.ToInt32(isCurveUp)].CenterPoint));
            }
            else
            {
                var z1 = _getPointsByInterval(p11, p21, coef2);
                var z2 = _getPointsByInterval(p12, p22, coef2);

                listPoint.Add(_getPointsByLine(
                    _getPointByNumber(p11),
                    _getPointByNumber(p12),
                    coef1.IntervalCount,
                    coef1.Coef));

                for (int i = 1; i < z1.Count - 1; i++)
                {
                    listPoint.Add(_getPointsByLine(
                        z1[i],
                        z2[i],
                        coef1.IntervalCount,
                        coef1.Coef));
                }
                listPoint.Add(_getPointsByLine(
                    _getPointByNumber(p21),
                    _getPointByNumber(p22),
                    coef1.IntervalCount,
                    coef1.Coef));

            }
            return listPoint;
        }

        /* Точки на интервале
         */
        private List<Point> _getPointsByInterval(int p1, int p2,
            DataIntervalsData.Coefficient coef)
        {
            var listPoint = new List<Point>();
            var point1 = _getPointByNumber(p1);
            var point2 = _getPointByNumber(p2);
            var count = coef.IntervalCount;
            var coefficient = coef.Coef;

            var isCurve = _isCurve(p1, p2);
            if (isCurve != "false")  // интервал кривая
            {
                listPoint = _getPointsByCurve(point1, point2, count, coefficient, 
                    Data.DataCurvedSections.Curves[Convert.ToInt32(isCurve)].CenterPoint);
            }
            else
            {
                listPoint = _getPointsByLine(point1, point2, count, coefficient);
            }
            return listPoint;
        }

        /* Получить точку по номеру
         */
        private Point _getPointByNumber(int number)
        {
            return Data.DataPoints.Point[number];
        }

        /* Является ли интервал кривой
         */
        private string _isCurve(int p1, int p2 )
        {
            for (int i = 0; i < CurvePoints.Count; i++)
            {
                if (CurvePoints[i].Point1 == p1 && CurvePoints[i].Point2 == p2)
                {
                    return CurvePoints[i].IndexCurvedSections.ToString();
                }
            }
            return "false";
        }


        ////////////// Функции для кривой
        /* Возвращает массив трехмерных точек на кривой
         */
        private List<Point> _getPointsByCurve(Point p1, Point p2, int count, double coef, Point centerPoint)
        {
            var points = new List<Point>();
            var radiusLength = _getLengthLine(p1, centerPoint); 
            var hordaLength = _getLengthLine(p1, p2);
            var fi = _getAngelFi(radiusLength, hordaLength); // угол характеризующий дугу кривой
            var angles = _getIntevalsByCurve(fi, count, coef); // все углы кривой

            points.Add(p1);
            for (int i = 0; i < angles.Length - 1; i++)
            {
                var hordaInterval = _getHordaLengthByAngel(radiusLength, hordaLength, angles[i]);
                points.Add(
                    _getPointByTwoPointsAndTwoRadius(
                        points[points.Count - 1],
                        centerPoint, 
                        radiusLength, 
                        hordaInterval,
                        p2));
            }
            points.Add(p2);
            return points;
        }

        /* Возвращает длину линии в трехмерном пространстве
         */
        private double _getLengthLine(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X), 2) + Math.Pow((p1.Y - p2.Y), 2) + Math.Pow((p1.Z - p2.Z), 2));
        }

        /* Возращает угол противолежащий хорде в радианах 
         */
        private double _getAngelFi(double radiusLength, double hordaLength)
        {
            return Math.Acos((Math.Pow(radiusLength, 2) + Math.Pow(radiusLength, 2) - Math.Pow(hordaLength, 2))/
                   (2*Math.Pow(radiusLength, 2)));
        }

        /* Возвращает длину всех интервалов числу
         */
        private double[] _getIntevalsByCurve(double length, int count, double coef)
        {
            var h = -1.0;
            var lengthIntervals = new double[count];
            for (int i = 0; i < count; i++)
            {
                if (coef == 1.0) // если коэф. раст¤жени¤ = 1
                {
                    h = length / count;
                }
                else if (h != -1.0 && coef != 1.0)
                {
                    if (coef < 0)
                    {
                        double coefExp1 = 1 / Math.Abs(coef);
                        h = h * coefExp1;
                    }
                    else h = h * coef;
                }
                else
                {
                    if (coef < 0)
                    {
                        double coefExp1 = 1 / Math.Abs(coef);
                        h = length * (1 - coefExp1) / (1 - Math.Pow(coefExp1, count));  //считаем первый шаг
                    }
                    else
                        h = length * (1 - coef) / (1 - Math.Pow(coef, count));  //считаем первый шаг
                }

                lengthIntervals[i] = h;
            }
            return lengthIntervals;
        }

        /* Возвращает длину противолежащей стороный в треугольнике по углу и двум сторонам
         */
        private double _getHordaLengthByAngel(double a, double b, double fi)
        {
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) - 2*a*b*Math.Cos(fi));
        }

        /* Возвращает координату третей точки в треугольнике по двум точкам и радиусам описывающие окружность в этих точках
         * работает только в плоскасти (x, y) 
         * возвращает трехмерную точку только если p1.Z == p2.Z 
         * finalAngelPoint - краевая точка конца дуги
         */
        private Point _getPointByTwoPointsAndTwoRadius(Point p1, Point p2, double radius1, double radius2, Point finalAngelPoint)
        {
            var eps = 0.0001;
            if (Math.Abs(p1.Z - p2.Z) < eps)
            {
                var a = radius1;
                var c = radius2;
                var x1 = p1.X;
                var y1 = p1.Y;
                var x2 = p2.X;
                var y2 = p2.Y;
                double x, y;

                var xRes1 = (1.0 / 2) * ((y1 - y2) * Math.Sqrt(-(-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (-c + a - y1 + y2) * (-c + a + y1 - y2)) * (-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (c + a - y1 + y2) * (c + a + y1 - y2)) * Math.Pow(x1 - x2, 2)) + (Math.Pow(x1, 3) - Math.Pow(x1, 2) * x2 + (Math.Pow(y2, 2) - 2 * y1 * y2 - Math.Pow(c, 2) + Math.Pow(y1, 2) + Math.Pow(a, 2) - Math.Pow(x2, 2)) * x1 - x2 * (Math.Pow(a, 2) - Math.Pow(c, 2) - Math.Pow(x2, 2) - Math.Pow(y2, 2) + 2 * y1 * y2 - Math.Pow(y1, 2))) * (x1 - x2)) / ((x1 - x2) * (Math.Pow(x1, 2) - 2 * x2 * x1 + Math.Pow(x2, 2) + Math.Pow(y1 - y2, 2)));
                var yRes1 = (-Math.Sqrt(-(-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (-c + a - y1 + y2) * (-c + a + y1 - y2)) * (-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (c + a - y1 + y2) * (c + a + y1 - y2)) * Math.Pow(x1 - x2, 2)) + Math.Pow(y1, 3) - Math.Pow(y1, 2) * y2 + (Math.Pow(a, 2) + Math.Pow(x1, 2) - Math.Pow(c, 2) + Math.Pow(x2, 2) - 2 * x2 * x1 - Math.Pow(y2, 2)) * y1 + Math.Pow(y2, 3) + (Math.Pow(x2, 2) - 2 * x2 * x1 + Math.Pow(c, 2) - Math.Pow(a, 2) + Math.Pow(x1, 2)) * y2) / (2 * Math.Pow(y1, 2) - 4 * y1 * y2 + 2 * Math.Pow(y2, 2) + 2 * Math.Pow(x1 - x2, 2));

                if (  // ниже соединяющей линии
                    ((p1.X < xRes1 && xRes1 < finalAngelPoint.X) && (p1.Y > yRes1 && yRes1 > finalAngelPoint.Y)) || // p1.X < x_res1 < finalAngelPoint.X and p1.Y > y_res1 > finalAngelPoint.Y  TODO
                    ((p1.X > xRes1 && xRes1 < finalAngelPoint.X) && (p1.Y > yRes1 && yRes1 > finalAngelPoint.Y))    // p1.X > x_res1 < finalAngelPoint.X and p1.Y > y_res1 > finalAngelPoint.Y
                    )
                {
                    x = xRes1;
                    y = yRes1;
                }
                else  // выше соединяющей линии
                {
                    x = (1.0 / 2) * ((-y1 + y2) * Math.Sqrt(-(-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (-c + a - y1 + y2) * (-c + a + y1 - y2)) * Math.Pow(x1 - x2, 2) * (-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (c + a - y1 + y2) * (c + a + y1 - y2))) + (x1 - x2) * (Math.Pow(x1, 3) - Math.Pow(x1, 2) * x2 + (Math.Pow(y1, 2) - 2 * y1 * y2 + Math.Pow(y2, 2) + Math.Pow(a, 2) - Math.Pow(c, 2) - Math.Pow(x2, 2)) * x1 - x2 * (-Math.Pow(c, 2) - Math.Pow(x2, 2) + Math.Pow(a, 2) - Math.Pow(y1, 2) + 2 * y1 * y2 - Math.Pow(y2, 2)))) / ((Math.Pow(x1, 2) - 2 * x2 * x1 + Math.Pow(x2, 2) + Math.Pow(y1 - y2, 2)) * (x1 - x2));
                    y = (Math.Sqrt(-Math.Pow(x1 - x2, 2) * (-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (c + a + y1 - y2) * (c + a - y1 + y2)) * (-Math.Pow(x1, 2) + 2 * x2 * x1 - Math.Pow(x2, 2) + (-c + a + y1 - y2) * (-c + a - y1 + y2))) + Math.Pow(y1, 3) - Math.Pow(y1, 2) * y2 + (Math.Pow(a, 2) + Math.Pow(x1, 2) - Math.Pow(c, 2) + Math.Pow(x2, 2) - 2 * x2 * x1 - Math.Pow(y2, 2)) * y1 + Math.Pow(y2, 3) + (Math.Pow(x2, 2) - 2 * x2 * x1 + Math.Pow(c, 2) - Math.Pow(a, 2) + Math.Pow(x1, 2)) * y2) / (2 * Math.Pow(y1, 2) - 4 * y1 * y2 + 2 * Math.Pow(y2, 2) + 2 * Math.Pow(x1 - x2, 2));
                
                }
                return new Point(x, y, p1.Z);
            }
            else
            {
                return new Point(-999, -999, -999); // условие выхода "кастыль" TODO
            }
        }


        /////////// Функции для линии
        /* Возвращает массив точек на линии
         */
        private List<Point> _getPointsByLine(Point p1, Point p2, int count, double coef)
        {
            var result = new List<Point>();
            var changeX = _getIntevalsByCurve(Math.Abs(p2.X - p1.X), count, coef);
            var changeY = _getIntevalsByCurve(Math.Abs(p2.Y - p1.Y), count, coef);
            var changeZ = _getIntevalsByCurve(Math.Abs(p2.Z - p1.Z), count, coef);

            result.Add(p1);
            for (int i = 0; i < changeX.Length; i++)
            {
                if (i == 0)
                {
                    var chngX = changeX[i];
                    var chngY = changeY[i];
                    var chngZ = changeZ[i];

                    if (p1.X > p2.X)
                    {
                        chngX = -1 * chngX;
                    }
                    if (p1.Y > p2.Y)
                    {
                        chngY = -1 * chngY;
                    }
                    if (p1.Z > p2.Z)
                    {
                        chngZ = -1 * chngZ;
                    }
                    result.Add(new Point(
                        p1.X + chngX,
                        p1.Y + chngY,
                        p1.Z + chngZ));
                }
                else
                {
                    var chngX = changeX[i];
                    var chngY = changeY[i];
                    var chngZ = changeZ[i];

                    if (p1.X > p2.X)
                    {
                        chngX = -1 * chngX;
                    }
                    if (p1.Y > p2.Y)
                    {
                        chngY = -1 * chngY;
                    }
                    if (p1.Z > p2.Z)
                    {
                        chngZ = -1 * chngZ;
                    }
                    var previosPoint = result[result.Count - 1];
                    result.Add(new Point(
                        previosPoint.X + chngX,
                        previosPoint.Y + chngY,
                        previosPoint.Z + chngZ));
                }
            }
            return result;
        }
    }
}
