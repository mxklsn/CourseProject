

using System;
using System.Linq;
using OpenGlTemplateApp.Methods;

namespace OpenGlTemplateApp
{
    /*
     * Создание модели отображения объекта
     */
    internal class ModelCreator
    {
        private readonly ModelData _curveData;

        public double[] Points { get; set; }

        public int[] CountFe { get; set; }

        public int CountArray { get; set; }

        public ModelCreator(ModelData curveFile)
        {
            _curveData = curveFile;
        }

        public void Create()
        {
            // Получаем данные
            var inputData = _curveData.LoadJson();

            // Преобразовываем данные
            var intervalsData = new DataIntervalsData(inputData.DataIntervals.Values, inputData.DataPoints.Count);
            var sections = new GetSections(inputData, intervalsData);

            // Получаем коненые елементы (сетку)
            var mesh = new Mesh(inputData, sections.FinitArea, sections.CurvesPoints);
            mesh.GetGrid();

            var values = inputData.DataPoints.Values;
            var dxCoefficient = new СoordinateСonverter(
                values.X.Min(), values.X.Max(),
                values.Y.Min(), values.Y.Max(),
                values.Z.Min(), values.Z.Max());

            // массив всех чисел (точек) для DLL
            _curveData.SaveArray(mesh.ElementsGrid, dxCoefficient.DxCoefficient);
            Points = _curveData.PointsDll.ToArray();

            // кол-во ке
            //CountFe = mesh.ElementsGrid.Count;
            CountFe = mesh.SubAreasCounts;
            CountArray = CountFe.Length;
        }
    }
}
