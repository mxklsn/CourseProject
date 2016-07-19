using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGlTemplateApp.DataClass
{
    internal class DataIntervals
    {
        /// <summary>
        /// Содержится просто весь массив интервалов и коэф.
        /// </summary>
        /// <param name="array"></param>
        public DataIntervals(double[] array)
        {
            Values = array;
        }

        public double[] Values { get; set; }
    }
}
