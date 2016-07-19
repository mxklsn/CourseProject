using System;
using System.Collections.Generic;

namespace OpenGlTemplateApp
{
    internal class DataIntervalsData
    {
        /// <summary>
        /// Обрабатываем массив с разрядкой "dataIntervals"
        /// Приводим его к нормальному виду
        /// </summary>
        /// <param name="array">массив с разрядкой и кол-вом интерваллов</param>
        /// <param name="countPt">массив с кол-м точек по X,Y,Z осям</param>
        public DataIntervalsData(double[] array, DataPoints.CountPoints countPt)
        {
            var arrayX = new List<Coefficient>(countPt.X);
            var arrayY = new List<Coefficient>(countPt.Y);
            var arrayZ = new List<Coefficient>(countPt.Z);
            var countX = (countPt.X - 1);
            var countY = (countPt.Y - 1);
            var countZ = (countPt.Z - 1);

            for (var i = 0; i < countX * 2; i += 2)
            {
                arrayX.Add(new Coefficient(Convert.ToInt32(array[i]), array[i + 1]));
            }

            for (var i = countX * 2; i < (countX + countY) * 2; i += 2)
            {
                arrayY.Add(new Coefficient(Convert.ToInt32(array[i]), array[i + 1]));
            }

            for (var i = (countX + countY) * 2; i < (countX + countY + countZ) * 2; i += 2)
            {
                arrayZ.Add(new Coefficient(Convert.ToInt32(array[i]), array[i + 1]));
            }

            X = arrayX;
            Y = arrayY;
            Z = arrayZ;
        }
        
        /*
         * Структура данных коэффициентов и кол-ва интервалов
         */
        public struct Coefficient
        {
            public int IntervalCount;

            public double Coef;

            public Coefficient(int interv, double coef)
            {
                IntervalCount = interv;
                Coef = coef;
            }
        }

        public List<Coefficient> X { get; set; }

        public List<Coefficient> Y { get; set; }

        public List<Coefficient> Z { get; set; }
    }
}
