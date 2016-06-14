using System.Runtime.Serialization;

namespace OpenGlTemplateApp
{
    [DataContract]
    internal class ModelSubAreaPoints
    {
        public ModelSubAreaPoints(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        [DataMember(Name = "x")]
        public int X { get; set; }

        [DataMember(Name = "y")]
        public int Y { get; set; }

        [DataMember(Name = "z")]
        public int Z { get; set; }
    }
}
