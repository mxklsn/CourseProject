using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenGlTemplateApp
{
    public static class Em
    {
        public static int[] FindAllIndexOf<T>(this IEnumerable<T> values, T val)
        {
            return values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
        }
    }

    internal class GetSections
    {
        /// <summary>
        /// Формируем подобласти в виде КЭ
        /// </summary>
        public GetSections(InputData input, DataIntervalsData intervals)
        {
            Data = input;
            Intervals = intervals;
            _initPointsByDataCurvedSections();
            _initSections();
        }


        /***** Methods *****/
        private InputData Data { get; set; }  // точки, области, кривые (наследован)

        private DataIntervalsData Intervals { get; set; }  // интервалы (наследован)

        private int[] CurvedSectionsPoint { get; set; } // !временный метод (нужен только внутри класса)

        public List<CurvePoints> CurvesPoints { get; set; }  // метод свойств для всех точек кривых (output)

        public List<AreaProperty> FinitArea { get; set; } // сформированные подобласти с свойствами (output)

        /***** Structs *****/
        /* Подобласть с свойствами для дробления
         */
        public struct AreaProperty
        {
            public List<int> PointsNumbers { get; set; }

            public List<DataIntervalsData.Coefficient> ElemIntervals { get; set; }

            public AreaProperty(List<int> pointsNumbers, List<DataIntervalsData.Coefficient> elemIntervals)
                : this()
            {
                PointsNumbers = pointsNumbers;
                ElemIntervals = elemIntervals;
            }
        }

        /* Номера точек начала и конца кривой 
         * и индекс в методе DataCurvedSections
         */
        public struct CurvePoints
        {
            public int Point1 { get; set; }

            public int Point2 { get; set; }

            public int IndexCurvedSections { get; set; }

            public CurvePoints(int p1, int p2, int index)
                : this()
            {
                Point1 = p1;
                Point2 = p2;
                IndexCurvedSections = index;
            }
        }


        /***** Main init *****/
        /* Инициализируем метод с точками начала кривых
         */
        private void _initPointsByDataCurvedSections()
        {
            var dataCurves = Data.DataCurvedSections.Curves;
            var array = new int[dataCurves.Count];  // точки из которых начинаются кривые
            for (int i = 0; i < dataCurves.Count; i++)
            {
                array[i] = _getNumberPoint(dataCurves[i].Nx, dataCurves[i].Ny, dataCurves[i].Nz);
            }
            CurvedSectionsPoint = array;
        }

        /* Форируем секции с их кривыми и свойствами
         */
        private void _initSections()
        {
            var result = new List<AreaProperty>();
            var curves = new List<CurvePoints>();
            // Получаем номера элемнтарных подобластей в которых описана кривая
            for (int i = 0; i < Data.DataElements.Subdomain.Count; i++)
            {
                // строю линии точек
                // тут есть ошибка не будет работать если в элемент подобл есть изменения по Y TODO
                var intervals = Data.DataElements.Subdomain[i];
                var numbers = _getElementalPoint(intervals);
                var countX = Math.Abs(intervals.X[0] - intervals.X[1]) + 1;
                var oneLine = new List<List<int>>();
                for (int k = 0; k < numbers.Count - 1; k += countX)
                {
                    var lineX = new List<int>();
                    for (int j = 0; j <= countX - 1; j++)
                    {
                        lineX.Add(numbers[k + j]);
                    }
                    oneLine.Add(lineX);
                }

                // свойства кривых для текущей эл. под.
                var curvePoints = _initCurvePoints(oneLine);
                if (curvePoints.Count != 0)
                {
                    for (int j = 0; j < curvePoints.Count; j++)
                    {
                        curves.Add(curvePoints[j]);
                    }
                }



                // формируем области для дробления
                countX = countX - 1;
                var countY = Math.Abs(intervals.Y[0] - intervals.Y[1]);
                var countZ = Math.Abs(intervals.Z[0] - intervals.Z[1]);

                for (int z = 0; z < countZ * 2; z += 2)
                {
                    for (int y = 0; y < countY; y++)
                    {
                        for (int x = 0; x < countX; x++)
                        {
                            var p = new List<int>();
                            var e = new List<DataIntervalsData.Coefficient>();

                            p.Add(oneLine[z][x]);
                            p.Add(oneLine[z][x + 1]);
                            p.Add(oneLine[z + 1][x]);
                            p.Add(oneLine[z + 1][x + 1]);
                            p.Add(oneLine[z + 2][x]);
                            p.Add(oneLine[z + 2][x + 1]);
                            p.Add(oneLine[z + 3][x]);
                            p.Add(oneLine[z + 3][x + 1]);

                            e.Add(Intervals.X[intervals.X[0] - 1 + x]);
                            e.Add(Intervals.Y[intervals.Y[0] - 1 + y]);
                            e.Add(Intervals.Z[intervals.Z[0] - 1 + z / 2]);

                            result.Add(new AreaProperty(p, e));
                        }
                    }
                }
            }
            FinitArea = result;
            CurvesPoints = curves;
        }

        /***** Private functions *****/
        /* Получаем номер точки в глобальном массиве
         */
        private int _getNumberPoint(int ix, int iy, int iz)
        {
            var countPt = Data.DataPoints.Count;
            var i = ix + (iy - 1) * countPt.X + (iz - 1) * countPt.X * countPt.Y;  // МКЭ стр. 485
            return i - 1;
        }

        /* Возвращает номера всех точек элементарной подобласти
         * в глобальном массиве
         * 
         * Хождения точек по горизонтальным прямым X
         */
        private List<int> _getElementalPoint(DataElements.ElementarySubdomain elem)
        {
            var countX = Math.Abs(elem.X[0] - elem.X[1]);
            var countY = Math.Abs(elem.Y[0] - elem.Y[1]);
            var countZ = Math.Abs(elem.Z[0] - elem.Z[1]);

            var list = new List<int>();
            for (int z1 = 0; z1 < 2; z1++)  // z-плоскость
            {
                for (int y1 = 0; y1 < 2; y1++)  // y-плоскость
                {
                    for (int x1 = 0; x1 < 2; x1++)  // x-плоскость
                    {
                        for (int z = 0; z < countZ; z++)  // z-плоскость
                        {
                            for (int y = 0; y < countY; y++)  // y-плоскость
                            {
                                for (int x = 0; x < countX; x++)  // x-плоскость
                                {
                                    var xt = elem.X[x1] + x;
                                    var yt = elem.Y[y1] + y;
                                    var zt = elem.Z[z1] + z;

                                    if (xt <= elem.X[1] && yt <= elem.Y[1] && zt <= elem.Z[1])
                                    {
                                        list.Add(
                                            _getNumberPoint(xt, yt, zt));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // сортировка
            var result = new List<int>();
            var orderedNumbers = from k in list
                        orderby k
                        select k;

            foreach (int k in orderedNumbers)
                result.Add(k);

            return result;
        }

        /* Получаем массив точек начала и конца кривых
         * и индекс в массиве кривых
         */
        private List<CurvePoints> _initCurvePoints(List<List<int>> pointsNumbers)
        {
            // соотвествие точек кривых и глобальных точек в виде элем подобл.
            var curvePoints = new List<CurvePoints>();
            for (int i = 0; i < pointsNumbers.Count; i++)  // одна линия точек по X
            {
                for (int j = 0; j < pointsNumbers[i].Count; j++)  // один элемент в линии точек по X
                {
                    var index = CurvedSectionsPoint.FindAllIndexOf(pointsNumbers[i][j]);
                    if (index.Length != 0)
                    {
                        if (j + 1 != pointsNumbers[i].Count)  // кривая идет по X
                        {
                            // index from dataCurvedSections
                            var indexCs = _findIndexFromDataCurvedSectionsByProperty(1, index);
                            curvePoints.Add(
                                new CurvePoints(
                                    pointsNumbers[i][j],
                                    pointsNumbers[i][j + 1],
                                    indexCs));
                        }
                        else  // кривая идет по Y
                        {
                            // index from dataCurvedSections
                            var indexCs = _findIndexFromDataCurvedSectionsByProperty(-1, index);
                            curvePoints.Add(
                                new CurvePoints(
                                    pointsNumbers[i][j],
                                    pointsNumbers[i + 1][j],
                                    indexCs));
                        }
                    }
                }
            }
            return curvePoints;
        }

        /* Поиск по свойству и индексам массива
         */
        private int _findIndexFromDataCurvedSectionsByProperty(int property, int[] markers)
        {
            foreach (var index in markers)
            {
                if (Data.DataCurvedSections.Curves[index].Property == property)
                {
                    return index;
                }
            }
            return 03072016; // кастыль код ошибки TODO
        }
    }
}
