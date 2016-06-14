using System;
using System.Linq;

namespace OpenGlTemplateApp
{
    /*
     * Создание модели отображения объекта
     */
    internal class ModelCreator
    {
        private readonly IModelData _curveData;
        private readonly ModelGrid _modelGrid = new ModelGrid();
        private readonly ModelSubAreasObject _modelSubAreas = new ModelSubAreasObject();

        public ModelCreator(IModelData curveFile)
        {
            _curveData = curveFile;
        }

        public void Create()
        {
            var inputData = _curveData.LoadJson();
            var modelSubAreas = _modelSubAreas.GetSubAreas(inputData);
            //var drawerData = _modelGrid.GetGrid(inputData);
            //var mnkEstimator = MnkFactory.CreateEstimator(inputData.Type);
            //var curve = mnkEstimator.GetCurve(inputData);
            //_drawer.Draw(inputData.CurvePoints.X.Min(), inputData.CurvePoints.X.Max());
            //_drawer.Draw(inputData);
            //Console.WriteLine(inputData.ModelPoints.ArrayX.Max());
        }
    }
}
