using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs.Сode
{
    static class HelpClass
    {
        private static int weight;

        private static int[,] adjMatrix;
        private static int size;
        public static int Size { get=>size; set=>size=value; }

        public static int[,] AdjMatrix { get => adjMatrix; set => adjMatrix = value; }
        public static int Weight { get => weight; set => weight = value; }

        public static int[,] AdjCopy()
        {
            int[,] CopyArr = new int[Size, Size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    CopyArr[i, j] = AdjMatrix[i, j];
                }
            }
            return CopyArr;
        }
    }
}
