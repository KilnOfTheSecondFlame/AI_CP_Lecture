using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using static AI_CP_Lecture.Tsp;
using static AI_CP_Lecture.Tsp_with_Capacities;

namespace AI_CP_Lecture
{
    public class Starter
    {

        /*
         * Start Examples: 
         */

        static void Main(string[] args)
        {

            // Grocery Store Example from Slides
            //Grocery.Solve();

            // Cryptogram Example from Slides
            //Cryptogram.Solve();

            // Sudoku Example from Slides - try different input
            //Sudoku.Solve(Sudoku1);

            // Graph Coloring Example from Slides as Constraint Problem (Lecture 1)
            //Coloring.SolveAsConstraintProblem(3, 5, Graph);

            // Graph Coloring Example from Slides as Optimization Problem (Lecture 3)
            //Coloring.SolveAsOptimizationProblem(5, Graph);

            // Nurse Scheduling from Slides
            //Nurses.Solve();

            /*
             * Routing Problems: 
             */

            // Knight Tour Example from Slides - try different board sizes
            KnightTour.Solve(5);

            // Travelling Salesman: 1 vehicle, random distances
            //Tsp.Solve(1, GenerateRandomDistanceMatrix(10));

            // Travelling Salesman: 3 vehicles, random distances
            //Tsp.Solve(3, GenerateRandomDistanceMatrix(10));

            // Travelling Salesman: a very small example
            //Tsp.Solve(3, Small);

            // Travelling Salesman: American cities distance table
            //Tsp.Solve(1, AmericanCityDistances);

            // Travelling Salesman: Burma
            //Tsp.Solve(1, Parse("../../data/burma14.xml"));

            // Travelling Salesman: US capitals
            //Tsp.Solve(3, Parse("../../data/att48.xml"));

            // Travelling Salesman: American cities GPS coordinates
            //Tsp.Solve(1, AmericanCityCoordinates);

            // Travelling Salesman: Drilling coordinates
            //Tsp.Solve(1, Drilling);

            // Travelling Salesman: City block example with Manhattan distance
            //Tsp.Solve(3, CityBlockDistances);

            // Travelling Salesman: City block example with Manhattan distance, 4 vehiocles, each having capacity 15 and demands per location
            //Tsp_with_Capacities.Solve(4, 15, CityBlockDistances, CityBlockDemands);

            /*
             * Other Optimization Problems
             */ 

            // Subset Sum Examples from slides
            //Xkcd.Solve();

            // Knapsack Problem from slides
            //Knapsack.Solve(AI_CP_Lecture.Starter.Knapsack1);

        }

        /*
         * Binoxxo Puzzles:
         * Source: https://krazydad.com/binox/sfiles/BINOX_10x10_TF_v2_4pp_b1.pdf
         */

        public static readonly string[,] Binoxxo1 =
        {
            {"", "X", "", "", "", "", "", "", "", ""},
            {"", "", "", "O", "", "", "", "", "", ""},
            {"", "X", "X", "", "", "", "", "", "", ""},
            {"", "", "", "", "O", "O", "", "", "", "O"},
            {"X", "", "", "", "", "", "X", "X", "", ""},
            {"", "X", "", "", "X", "", "", "", "", ""},
            {"", "", "", "O", "", "", "X", "", "", ""},
            {"", "O", "", "", "", "", "", "O", "", "O"},
            {"", "", "", "", "O", "", "", "", "", ""},
            {"O", "", "", "", "", "", "", "", "", "O"}
        };

        public static readonly string[,] Binoxxo2 =
        {
            {"", "", "O", "O", "", "", "", "", "", ""},
            {"", "", "", "O", "", "", "", "", "", ""},
            {"O", "", "", "", "X", "", "", "X", "", ""},
            {"", "", "", "", "", "O", "", "", "", ""},
            {"O", "", "", "O", "", "", "", "", "", ""},
            {"", "", "", "O", "", "", "", "", "X", ""},
            {"", "", "", "", "", "", "", "X", "X", ""},
            {"X", "", "", "", "", "", "", "X", "", ""},
            {"", "", "", "", "", "O", "", "", "", "O"},
            {"", "", "", "", "X", "", "O", "", "", ""}
        };

        public static readonly string[,] Binoxxo3 =
        {
            {"X", "", "", "", "", "", "O", "O", "", ""},
            {"", "", "", "O", "", "", "", "", "X", ""},
            {"", "", "O", "", "", "", "", "", "", ""},
            {"", "", "", "O", "", "X", "", "", "", "O"},
            {"X", "", "X", "", "", "X", "", "", "X", ""},
            {"", "", "X", "", "X", "", "", "", "", ""},
            {"", "", "", "", "X", "X", "", "", "X", ""},
            {"", "", "", "", "", "", "", "", "", "O"},
            {"", "X", "", "X", "", "", "", "", "", ""},
            {"", "X", "", "", "X", "", "", "X", "X", ""}
        };

        /*
         * Sudoku Puzzle:
         * Source: http://www.extremesudoku.info/sudoku.html
         */

        public static readonly string[,] Sudoku1 =
        {
            {"1", "", "", "", "3", "", "", "8", ""},
            {"", "6", "", "4", "", "", "", "", ""},
            {"", "", "4", "", "", "9", "3", "", ""},
            {"", "4", "5", "", "", "6", "", "", "7"},
            {"9", "", "", "", "", "5", "", "", ""},
            {"", "", "8", "", "", "3", "", "2", ""},
            {"", "", "", "", "", "", "9", "5", "6"},
            {"", "2", "", "", "", "", "", "", ""},
            {"", "", "7", "", "", "8", "", "1", ""}
        };

        public static readonly string[,] Sudoku2 =
        {
            {"4", "", "8", "", "", "", "", "", ""},
            {"", "", "", "1", "7", "", "", "", ""},
            {"", "", "", "", "8", "", "", "3", "2"},
            {"", "", "6", "", "", "8", "2", "5", ""},
            {"", "9", "", "", "", "", "", "8", ""},
            {"", "3", "7", "6", "", "", "9", "", ""},
            {"2", "7", "", "", "5", "", "", "", ""},
            {"", "", "", "", "1", "4", "", "", ""},
            {"", "", "", "", "", "", "6", "", "4"}
        };

        public static readonly string[,] Sudoku3 =
        {
            {"8", "", "", "3", "", "2", "", "", "7"},
            {"", "4", "", "", "6", "", "", "9", ""},
            {"", "", "5", "", "", "", "6", "", ""},
            {"1", "", "", "6", "", "8", "", "", "5"},
            {"", "3", "", "", "2", "", "", "1", ""},
            {"4", "", "", "7", "", "3", "", "", "6"},
            {"", "", "6", "", "", "", "8", "", ""},
            {"", "2", "", "", "3", "", "", "6", ""},
            {"5", "", "", "2", "", "6", "", "", "1"}
        };

        public static readonly string[,] Sudoku4 =
        {
            {"", "", "6", "1", "", "", "", "", "8"},
            {"", "7", "", "", "9", "", "", "2", ""},
            {"3", "", "", "", "", "6", "9", "", ""},
            {"6", "", "", "", "", "2", "3", "", ""},
            {"", "8", "", "", "4", "", "", "1", ""},
            {"", "", "4", "3", "", "", "", "", "9"},
            {"", "", "9", "2", "", "", "", "", "4"},
            {"", "5", "", "", "7", "", "", "8", ""},
            {"8", "", "", "", "", "5", "1", "", ""}
        };

        /*
         * Xmas Puzzle:
         */

        public static readonly string[,] Xmas1 =
        {
            {"2", " ", " ", " ", "1"},
            {"2", " ", "4", "3", " "},
            {" ", "2", " ", "1", " "},
            {" ", "1", " ", "3", " "},
            {"1", " ", " ", " ", " "}
        };

        public static readonly string[,] Xmas2 =
        {
            {"1", " ", " ", " ", "2", " ", " ", "1"},
            {" ", "1", "2", " ", "3", " ", " ", "1"},
            {" ", "2", " ", "1", " ", " ", " ", "0"},
            {" ", "2", "1", " ", " ", "2", "3", "1"},
            {" ", " ", " ", "2", " ", " ", " ", " "},
            {"1", " ", " ", " ", "4", "3", " ", " "},
            {" ", "1", " ", " ", "4", " ", "3", " "},
            {"1", " ", " ", "2", " ", "2", " ", "1"}
        };

        /*
         * Sum Frame Sudoku Puzzle:
         */

        public static readonly Tuple<int[], int[], int[], int[]> SumFrameSudoku1 =
            new Tuple<int[], int[], int[], int[]>(
                // Top:
                new[] { 21, 12, 12, 13, 14, 18, 10, 19, 16 },
                // Right:
                new[] { 20, 15, 10, 22, 8, 15, 17, 15, 13 },
                // Bottom:
                new[] { 17, 9, 19, 18, 13, 14, 23, 15, 7 },
                // Left:
                new[] { 12, 12, 21, 14, 14, 17, 14, 9, 22 });

        public static readonly Tuple<int[], int[], int[], int[]> SumFrameSudoku2 =
            new Tuple<int[], int[], int[], int[]>(
                // Top:
                new[] { 12, 18, 15, 21, 6, 18, 13, 15, 17 },
                // Right:
                new[] { 18, 20, 7, 16, 15, 14, 16, 18, 11 },
                // Bottom:
                new[] { 18, 6, 21, 16, 20, 9, 12, 20, 13 },
                // Left:
                new[] { 13, 9, 23, 11, 21, 13, 16, 15, 14 });

        /*
         * Knapsack:
         */

        public static readonly Tuple<int, int[], int[], string[]> Knapsack1 =
            new Tuple<int, int[], int[], string[]>(
                // Total knapsack capacity:
                9,
                // Item weights:
                new[] { 4, 3, 2 },
                // Item values:
                new[] { 15, 10, 7 },
                // Items names:
                new[] { "Whiskey", "Perfume", "Cigarettes" });


        public static readonly Tuple<int, int[], int[], string[]> Knapsack2 =
            new Tuple<int, int[], int[], string[]>(
                // Total knapsack capacity:
                29,
                // Item weights:
                new[] { 4, 3, 2, 6 },
                // Item values:
                new[] { 12, 8, 2, 15 },
                // Items names:
                new[] { "Whiskey", "Perfume", "Corned Beef", "Riffle" });

        /*
         * Graph:
         */

        public static readonly Tuple<int, int>[] Graph =
        {
            new Tuple<int, int>(4, 1),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(4, 0),
            new Tuple<int, int>(1, 3),
            new Tuple<int, int>(3, 0),
            new Tuple<int, int>(3, 2)
        };

        /*
         * Random Distance Matrices: 
         */

        public static StaticDistance GenerateRandomDistanceMatrix(int dim)
        {
            var costs = new long[dim + 1, dim + 1];
            var rand = new Random();
            for (int i = 0; i < dim + 1; i++)
            {
                for (int j = 0; j < dim + 1; j++)
                {
                    costs[i, j] = (i == j) ? 0 : rand.Next(1000);
                }
            }

            return new StaticDistance(FloydWarshall(costs));
        }

        /*
         * Floyd-Warshall Algorithm for shortest distances:
         */

        private static long[,] FloydWarshall(long[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int v = 0; v < matrix.GetLength(0); v++)
                {
                    for (int w = 0; w < matrix.GetLength(0); w++)
                    {
                        if (matrix[v, w] > matrix[v, i] + matrix[i, w])
                        {
                            matrix[v, w] = matrix[v, i] + matrix[i, w];
                        }
                    }
                }
            }

            return matrix;
        }

        /*
         * Distance Matrices
         */

        public static readonly StaticDistance Small = new StaticDistance(
            
            new long[,] {
                {0, 1, 2, 4},
                {3, 0, 1, 4},
                {8, 2, 0, 6},
                {9, 4, 3, 0}
            },

            new string[] { "A", "B", "C", "D" });

        /*
         * American Cities with absolut Distances
         */

        public static readonly StaticDistance AmericanCityDistances = new StaticDistance(

            new long[,] {
                { 0, 2451, 713, 1018, 1631, 1374, 2408, 213, 2571, 875, 1420, 2145, 1972 },     // New York
                {2451, 0, 1745, 1524, 831, 1240, 959, 2596, 403, 1589, 1374, 357, 579 },        // Los Angeles
                {713, 1745, 0, 355, 920, 803, 1737, 851, 1858, 262, 940, 1453, 1260 },          // Chicago
                {1018, 1524, 355, 0, 700, 862, 1395, 1123, 1584, 466, 1056, 1280, 987 },        // Minneapolis
                {1631, 831, 920, 700, 0, 663, 1021, 1769, 949, 796, 879, 586, 371 },            // Denver
                {1374, 1240, 803, 862, 663, 0, 1681, 1551, 1765, 547, 225, 887, 999 },          // Dallas
                {2408, 959, 1737, 1395, 1021, 1681, 0, 2493, 678, 1724, 1891, 1114, 701 },      // Seattle
                {213, 2596, 851, 1123, 1769, 1551, 2493, 0, 2699, 1038, 1605, 2300, 2099 },     // Boston
                {2571, 403, 1858, 1584, 949, 1765, 678, 2699, 0, 1744, 1645, 653, 600 },        // San Francisco
                {875, 1589, 262, 466, 796, 547, 1724, 1038, 1744, 0, 679, 1272, 1162 },         // St. Louis
                {1420, 1374, 940, 1056, 879, 225, 1891, 1605, 1645, 679, 0, 1017, 1200 },       // Houston
                {2145, 357, 1453, 1280, 586, 887, 1114, 2300, 653, 1272, 1017, 0, 504 },        // Phoenix
                {1972, 579, 1260, 987, 371, 999, 701, 2099, 600, 1162, 1200, 504, 0 }           // Salt Lake City
            },

            new string[] {"New York", "Los Angeles", "Chicago", "Minneapolis", "Denver", "Dallas", "Seattle", "Boston", "San Francisco", "St. Louis", "Houston", "Phoenix", "Salt Lake City"});

        /*
         * American Cities with GPS Coordinates 
         */

        public static readonly GPS AmericanCityCoordinates = new GPS(

            new double[,] {
                {40.71,  -74.01},     // New York
                {34.05, -118.24},     // Los Angeles
                {41.88,  -87.63},     // Chicago
                {44.98,  -93.27},     // Minneapolis
                {39.74, -104.99},     // Denver
                {32.78,  -96.89},     // Dallas
                {47.61, -122.33},     // Seattle
                {42.36,  -71.06},     // Boston
                {37.77, -122.42},     // San Francisco
                {38.63,  -90.20},     // St. Louis
                {29.76,  -95.37},     // Houston
                {33.45, -112.07},     // Phoenix
                {40.76, -111.89}      // Salt Lake City
        },

        new string[] { "New York", "Los Angeles", "Chicago", "Minneapolis", "Denver", "Dallas", "Seattle", "Boston", "San Francisco", "St. Louis", "Houston", "Phoenix", "Salt Lake City" });

        /*
         * Drilling Coordinates with Euclideal Distance 
         */

        public static readonly Euclid Drilling = new Euclid(

            new int[,]  {{288, 149}, {288, 129}, {270, 133}, {256, 141}, {256, 157}, {246, 157}, {236, 169},
                        {228, 169}, {228, 161}, {220, 169}, {212, 169}, {204, 169}, {196, 169}, {188, 169}, {196, 161},
                        {188, 145}, {172, 145}, {164, 145}, {156, 145}, {148, 145}, {140, 145}, {148, 169}, {164, 169},
                        {172, 169}, {156, 169}, {140, 169}, {132, 169}, {124, 169}, {116, 161}, {104, 153}, {104, 161},
                        {104, 169}, {90, 165}, {80, 157}, {64, 157}, {64, 165}, {56, 169}, {56, 161}, {56, 153}, {56, 145},
                        {56, 137}, {56, 129}, {56, 121}, {40, 121}, {40, 129}, {40, 137}, {40, 145}, {40, 153}, {40, 161},
                        {40, 169}, {32, 169}, {32, 161}, {32, 153}, {32, 145}, {32, 137}, {32, 129}, {32, 121}, {32, 113},
                        {40, 113}, {56, 113}, {56, 105}, {48, 99}, {40, 99}, {32, 97}, {32, 89}, {24, 89}, {16, 97},
                        {16, 109}, {8, 109}, {8, 97}, {8, 89}, {8, 81}, {8, 73}, {8, 65}, {8, 57}, {16, 57}, {8, 49},
                        {8, 41}, {24, 45}, {32, 41}, {32, 49}, {32, 57}, {32, 65}, {32, 73}, {32, 81}, {40, 83}, {40, 73},
                        {40, 63}, {40, 51}, {44, 43}, {44, 35}, {44, 27}, {32, 25}, {24, 25}, {16, 25}, {16, 17}, {24, 17},
                        {32, 17}, {44, 11}, {56, 9}, {56, 17}, {56, 25}, {56, 33}, {56, 41}, {64, 41}, {72, 41}, {72, 49},
                        {56, 49}, {48, 51}, {56, 57}, {56, 65}, {48, 63}, {48, 73}, {56, 73}, {56, 81}, {48, 83}, {56, 89},
                        {56, 97}, {104, 97}, {104, 105}, {104, 113}, {104, 121}, {104, 129}, {104, 137}, {104, 145},
                        {116, 145}, {124, 145}, {132, 145}, {132, 137}, {140, 137}, {148, 137}, {156, 137}, {164, 137},
                        {172, 125}, {172, 117}, {172, 109}, {172, 101}, {172, 93}, {172, 85}, {180, 85}, {180, 77},
                        {180, 69}, {180, 61}, {180, 53}, {172, 53}, {172, 61}, {172, 69}, {172, 77}, {164, 81}, {148, 85},
                        {124, 85}, {124, 93}, {124, 109}, {124, 125}, {124, 117}, {124, 101}, {104, 89}, {104, 81},
                        {104, 73}, {104, 65}, {104, 49}, {104, 41}, {104, 33}, {104, 25}, {104, 17}, {92, 9}, {80, 9},
                        {72, 9}, {64, 21}, {72, 25}, {80, 25}, {80, 25}, {80, 41}, {88, 49}, {104, 57}, {124, 69},
                        {124, 77}, {132, 81}, {140, 65}, {132, 61}, {124, 61}, {124, 53}, {124, 45}, {124, 37}, {124, 29},
                        {132, 21}, {124, 21}, {120, 9}, {128, 9}, {136, 9}, {148, 9}, {162, 9}, {156, 25}, {172, 21},
                        {180, 21}, {180, 29}, {172, 29}, {172, 37}, {172, 45}, {180, 45}, {180, 37}, {188, 41}, {196, 49},
                        {204, 57}, {212, 65}, {220, 73}, {228, 69}, {228, 77}, {236, 77}, {236, 69}, {236, 61}, {228, 61},
                        {228, 53}, {236, 53}, {236, 45}, {228, 45}, {228, 37}, {236, 37}, {236, 29}, {228, 29}, {228, 21},
                        {236, 21}, {252, 21}, {260, 29}, {260, 37}, {260, 45}, {260, 53}, {260, 61}, {260, 69}, {260, 77},
                        {276, 77}, {276, 69}, {276, 61}, {276, 53}, {284, 53}, {284, 61}, {284, 69}, {284, 77}, {284, 85},
                        {284, 93}, {284, 101}, {288, 109}, {280, 109}, {276, 101}, {276, 93}, {276, 85}, {268, 97},
                        {260, 109}, {252, 101}, {260, 93}, {260, 85}, {236, 85}, {228, 85}, {228, 93}, {236, 93},
                        {236, 101}, {228, 101}, {228, 109}, {228, 117}, {228, 125}, {220, 125}, {212, 117}, {204, 109},
                        {196, 101}, {188, 93}, {180, 93}, {180, 101}, {180, 109}, {180, 117}, {180, 125}, {196, 145},
                        {204, 145}, {212, 145}, {220, 145}, {228, 145}, {236, 145}, {246, 141}, {252, 125}, {260, 129},{280, 133}});

        /*
         * Manhattan Distance 
         */

        public static readonly Manhattan CityBlockDistances = new Manhattan(

            new int[,] {{4, 4}, {2, 0}, {8, 0}, {0, 1}, {1, 1}, {5, 2}, {7, 2}, {3, 3}, {6, 3}, {5, 5}, {8, 5}, {1, 6}, {2, 6}, {3, 7}, {6, 7}, {0, 8}, {7, 8}});

        public static readonly Demands CityBlockDemands = new Demands(new int[] {0, 1, 1, 2, 4, 2, 4, 8, 8, 1, 2, 1, 2, 4, 4, 8, 8});

        /*
         * Nonogram Puzzle:
         */

        public static readonly Tuple<List<int>[], List<int>[]> Nonogram1 = new Tuple<List<int>[], List<int>[]>(
            // Rows:
            new[]
            {
                new List<int> {3, 4, 2, 1},
                new List<int> {2, 1, 1, 1, 2, 1},
                new List<int> {1, 1, 1, 2, 1},
                new List<int> {1, 4, 4},
                new List<int> {1, 1, 1, 1, 2},
                new List<int> {2, 1, 1, 1, 1, 2},
                new List<int> {3, 1, 1, 1, 2},
                new List<int> {1, 1},
                new List<int> {4, 3, 4},
                new List<int> {1, 1, 1, 2, 1, 1},
                new List<int> {1, 1, 1, 1, 1, 1},
                new List<int> {4, 1, 1, 4},
                new List<int> {1, 1, 1, 1, 1, 1},
                new List<int> {1, 1, 1, 2, 1, 1},
                new List<int> {1, 1, 3, 1, 1}
            },

            // Columns:
            new[]
            {
                new List<int> {5, 7},
                new List<int> {2, 2, 1, 1},
                new List<int> {1, 1, 1, 1},
                new List<int> {2, 2, 7},
                new List<int> {1},
                new List<int> {7, 7},
                new List<int> {1, 1, 1, 1},
                new List<int> {1, 1, 2, 2},
                new List<int> {7, 5},
                new List<int> {1},
                new List<int> {7, 7},
                new List<int> {4, 1, 1},
                new List<int> {4, 1, 1},
                new List<int> {7, 7}
            });


        public static readonly Tuple<List<int>[], List<int>[]> Nonogram2 = new Tuple<List<int>[], List<int>[]>(
            // Rows:
            new[]
            {
                new List<int> {2, 2},
                new List<int> {1, 3, 1, 4},
                new List<int> {3, 3, 1, 1},
                new List<int> {2, 2, 2},
                new List<int> {1, 3, 1},
                new List<int> {1, 1, 1, 2, 2},
                new List<int> {1, 1, 2},
                new List<int> {2, 1, 5},
                new List<int> {5, 3, 3},
                new List<int> {4, 4},
                new List<int> {1, 2, 1},
                new List<int> {2, 3, 2},
                new List<int> {2, 2, 2},
                new List<int> {3, 3},
                new List<int> {6}
            },

            // Columns:
            new[]
            {
                new List<int> {1, 5},
                new List<int> {4, 2},
                new List<int> {1, 1, 1},
                new List<int> {2, 2},
                new List<int> {1, 5},
                new List<int> {2, 1, 2, 4},
                new List<int> {1, 1, 2},
                new List<int> {4, 2, 3},
                new List<int> {1, 2, 2, 2, 1},
                new List<int> {5, 1, 1},
                new List<int> {3, 1, 2, 2},
                new List<int> {3, 4, 1},
                new List<int> {1, 4, 2},
                new List<int> {1, 2, 2, 2},
                new List<int> {2, 2, 3}
            });

        /*
         * This method parses XML input documents for TSP benchmark examples.
         * http://www.iwr.uni-heidelberg.de/groups/comopt/software/TSPLIB95/XML-TSPLIB/instances/
         */

        private static StaticDistance Parse(String file)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);

            XmlNodeList vertices = xmldoc.SelectNodes("//vertex");
            var result = new long[vertices.Count, vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
            {
                foreach (XmlNode edge in vertices[i].SelectNodes("//edge"))
                {
                    int target = Convert.ToInt32(edge.InnerText);
                    decimal dec = Decimal.Parse(edge.Attributes.GetNamedItem("cost").Value, NumberStyles.Float, CultureInfo.InvariantCulture);
                    result[i, target] = (long)dec;
                }
                
            }

            return new StaticDistance(result);
        }
    }
}
