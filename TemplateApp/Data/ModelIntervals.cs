using System.Collections.Generic;
using System.Runtime.Serialization;

namespace OpenGlTemplateApp
{
    [DataContract]
    internal class ModelIntervals
    {
        public ModelIntervals(int count, double[] intervals)
        {
            Count = count;

            List<AreaSettings> settings = new List<AreaSettings>();
            for (int i = 0; i < intervals.Length; i += 2)
            {
                var intInterval = System.Convert.ToInt32(intervals[i]);
                settings.Add(new AreaSettings(intInterval, intervals[i + 1]));
            }
            AreaSettings = settings;
        }

        public List<AreaSettings> AreaSettings { get; set; } // метод

        public int Count { get; set; }
    }
}
