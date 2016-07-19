using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Policy;

namespace OpenGlTemplateApp
{
    [DataContract]
    internal class DataElements
    {   
        /// <summary>
        /// Элементарные подобласти
        /// </summary>
        /// <param name="count">кол-во элмен. подобластей</param>
        /// <param name="array">массив с данными</param>
        public DataElements(int count, int[] array)
        {
            var subdomain = new List<ElementarySubdomain>();

            for (int i = 0; i < count * 7; i += 7)
            {
                var paramX = new int[2];
                var paramY = new int[2];
                var paramZ = new int[2];

                paramX[0] = array[i + 1];
                paramX[1] = array[i + 2];
                paramY[0] = array[i + 3];
                paramY[1] = array[i + 4];
                paramZ[0] = array[i + 5];
                paramZ[1] = array[i + 6];

                subdomain.Add(new ElementarySubdomain(
                    array[i],
                    paramX, paramY, paramZ));                
            }

            Count = count;
            Subdomain = subdomain;
        }

        public struct ElementarySubdomain
        {
            public int Number;

            public int[] X, Y, Z;

            public ElementarySubdomain(int number, int[] x, int[] y, int[] z)
            {
                Number = number;
                X = x;
                Y = y;
                Z = z;
            }
        }

        public List<ElementarySubdomain> Subdomain { get; set; }    // элементарные подобласти 

        public int Count { get; set; }  // кол-во элемент подобластей
    }
}
