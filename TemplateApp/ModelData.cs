using System;
using System.IO;
//using System.Runtime.Serialization.Json;
using Newtonsoft.Json;


namespace OpenGlTemplateApp
{
    internal class ModelData : IModelData  // Явная реализация элемента интерфейса
    {
        private readonly string _filePath;

        public ModelData(string filePath)
        {
            _filePath = filePath;
        }

        public InputData LoadJson() // чтение файла .json
        {
            using (StreamReader reader = new StreamReader(_filePath))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<InputData>(json);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
