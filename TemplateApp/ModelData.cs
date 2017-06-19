using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace OpenGlTemplateApp
{
    public class JsonResult
    {
        public int layerId { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
        public string material { get; set; }
        public string topSection { get; set; }
        public string bottomSection { get; set; }
    }

    internal class ModelData
    {
        /// <summary>
        /// Чтение и сохранение данных
        /// </summary>
        /// <param name="filePath">Путь до файла .Json</param>
        public ModelData(string filePath, string dataGridJson)
        {
            _filePath = filePath;
            _dataGridJson = dataGridJson;
        }

        public List<double> PointsDll { get; set; }

        private readonly string _filePath;

        private readonly string _dataGridJson;

        public InputData LoadJson(DataGridView dataGV)
        {
            using (var reader = new StreamReader(_dataGridJson))
            {
                var json = reader.ReadToEnd();
                var dataSource = JsonConvert.DeserializeObject<List<JsonResult>>(json);
                dataGV.DataSource = dataSource;
            }

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
