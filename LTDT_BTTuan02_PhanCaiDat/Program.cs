using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_BTTuan02_PhanCaiDat
{
    class Program
    {
        static void Main(string[] args)
        {
            GRAPH g = new GRAPH();
            string input_filename = @"D:\STUDY\IT\Semester3\GraphTheory\4_Projects\LTDT_BTTuan02_PhanCaiDat\input.txt";
            //string input_filename = args[0];
            inputValue(input_filename, g);
            Console.ReadLine();
        }
        private static void inputValue(string input_filename, GRAPH g)
        {
            try
            {
                using (StreamReader reader = new StreamReader(input_filename))
                {
                    string line = reader.ReadLine();
                    g.numberOfVertexes = int.Parse(line);
                    if (g.numberOfVertexes > 2)
                    {
                        //Declare variables
                        g.matrix = new int[g.numberOfVertexes, g.numberOfVertexes];
                        int rowIndex, colIndex;
                        int sIndex = 0;
                        int numberOfEdges = 0;
                        //int numberOfVertexesHaveMultiples = 0;
                        //int numberOfLoops = 0;

                        bool completeGraph = true;
                        bool cycle = true;
                        bool regularGraph = true;

                        //Process
                        line = reader.ReadToEnd().Replace("\r\n", " ");
                        string[] s = line.Split(' ');

                        while (sIndex < g.numberOfVertexes * 2)
                        {
                            for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                {
                                    g.matrix[rowIndex, colIndex] = int.Parse(s[sIndex]);
                                    numberOfEdges += g.matrix[rowIndex, colIndex];
                                    sIndex++;
                                }
                        }
                        reader.Close();

                        //Identify a complete graph
                        for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                        {
                            for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                            {
                                if (rowIndex == colIndex && g.matrix[rowIndex, colIndex] != 0)
                                {
                                    completeGraph = false;
                                    break;
                                }
                                if (rowIndex != colIndex && g.matrix[rowIndex, colIndex] != 1)
                                {
                                    completeGraph = false;
                                    break;
                                }
                            }
                        }

                        //Identify degree of each vertex
                        int[] degree = new int[g.numberOfVertexes];
                        for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                        {
                            int count = 0;
                            for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                if (g.matrix[rowIndex, colIndex] != 0)
                                {
                                    if (rowIndex == colIndex)
                                        count += g.matrix[rowIndex, colIndex] * 2;
                                    else
                                        count += g.matrix[rowIndex, colIndex];
                                }
                            degree[rowIndex] = count;
                        }

                        //Identify a cycle
                        for (int i = 0; i < degree.Length; i++)
                        {
                            if (degree[i] != 2)
                            {
                                cycle = false;
                                break;
                            }
                        }

                        //Print
                        if (completeGraph)
                            Console.WriteLine("Day la do thi day du K" + g.numberOfVertexes);
                        else if (!completeGraph)
                            Console.WriteLine("Day khong phai la do thi day du");
                        if (cycle)
                            Console.WriteLine("Day la do thi vong C" + g.numberOfVertexes);
                        else if (!cycle)
                            Console.WriteLine("Day khong phai la do thi vong");
                        
                    }
                    else
                        Console.WriteLine("The number of Vertexes must be greater than 2.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }
    }
    class GRAPH
    {
        private int _numberOfVertexes;
        private int[,] _matrix;

        public int numberOfVertexes
        {
            get { return _numberOfVertexes; }
            set
            {
                if (value > 2)
                    _numberOfVertexes = value;
            }
        }

        public int[,] matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }
    }
}
