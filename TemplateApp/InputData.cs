using System.Runtime.Serialization;

namespace OpenGlTemplateApp
{
    [DataContract]
    internal class InputData
    {
        [DataMember(Name = "mainPoints")]
        public ModelSubAreaPoints ModelSubAreaPoints { get; private set; }

        [DataMember(Name = "dataPoints")]
        public ModelPoints ModelPoints { get; set; }

        [DataMember(Name = "dataAreaSettings")]
        public ModelIntervals ModelIntervals { get; private set; }
    }
}
