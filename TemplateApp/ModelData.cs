using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace OpenGlTemplateApp
{
    internal class ModelData
    {
        /// <summary>
        /// Чтение и сохранение данных
        /// </summary>
        /// <param name="filePath">Путь до файла .Json</param>
        public ModelData(string filePath)
        {
            _filePath = filePath;
        }

        public List<double> PointsDll { get; set; }

        private readonly string _filePath;

        public InputData LoadJson()
        {
            using (var reader = new StreamReader(_filePath))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<InputData>(json);
            }
        }

        public void SaveArray(List<Element> elements, double dxCoefficient)
        {
            var array = new List<double>();
            for (int element = 0; element < elements.Count; element++)
            {
                for (int point = 0; point < elements[element].Points.Count; point++)
                {
                    array.Add(dxCoefficient * elements[element].Points[point].X);
                    array.Add(dxCoefficient * elements[element].Points[point].Y);
                    array.Add(dxCoefficient * elements[element].Points[point].Z);
                }
            }
            PointsDll = array;
        }
    }
}
