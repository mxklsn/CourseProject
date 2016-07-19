using System.Runtime.Serialization;
using OpenGlTemplateApp.DataClass;

namespace OpenGlTemplateApp
{
    /// <summary>
    /// Структура считываемых файлов.
    /// </summary>
    [DataContract]
    internal class InputData
    {
        [DataMember(Name = "dataPoints")]       // описание опорных точек
        public DataPoints DataPoints { get; set; }

        [DataMember(Name = "dataElements")]       // элементарные подобласти
        public DataElements DataElements { get; set; }

        [DataMember(Name = "dataCurvedSections")]  // криволинейные участки
        public DataCurvedSections DataCurvedSections { get; private set; }

        [DataMember(Name = "dataIntervals")]  // коэф. и интервалы 
        public DataIntervals DataIntervals { get; private set; } 
    }
}
