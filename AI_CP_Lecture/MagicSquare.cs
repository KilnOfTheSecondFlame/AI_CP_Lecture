using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Google.OrTools.ConstraintSolver;

namespace AI_CP_Lecture {
    class MagicSquare {
        public static void Solve(int sideLength) {
            var solver = new Solver("Magic Square");
            IEnumerable<int> range = Enumerable.Range(0, sideLength);

            IntVar[,] square = solver.MakeIntVarMatrix(sideLength, sideLength, 1, sideLength * sideLength);
            IntVar sum = solver.MakeIntVar(1, sideLength * sideLength * sideLength);
            // Add constraints
            // All different numbers
            solver.Add(square.Flatten().AllDifferent());
            foreach (var i in range) {
                // Rows should all have the same sum
                solver.Add((from j in range select square[i, j]).ToArray().Sum() == sum);
                // Columns should also all have the same sum
                solver.Add((from j in range select square[j, i]).ToArray().Sum() == sum);
            }

            // Diagonals should also both have the same sum
            solver.Add((from j in range select square[j, j]).ToArray().Sum() == sum);
            solver.Add((from j in range select square[j, sideLength - 1 - j]).ToArray().Sum() == sum);

            DecisionBuilder decisionBuilder =
                solver.MakePhase(square.Flatten(), Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            solver.NewSearch(decisionBuilder);
            while (solver.NextSolution()) {
                PrintSolution(square);
                Console.WriteLine("Magic square constant: " + sum.Value());
                Console.Write("\n");
            }
            
            Console.WriteLine("Finished printing solutions");
            Console.WriteLine("Time: " + solver.WallTime());
            Console.WriteLine("Solutions: " + solver.Solutions());
            solver.EndSearch();
        }

        private static void PrintSolution(IntVar[,] magicSquare) {
            for (int i = 0; i < magicSquare.GetLength(0); i++) {
                for (int j = 0; j < magicSquare.GetLength(1); j++) {
                    Console.Write("[{00}]", magicSquare[i, j].Value());
                }
                Console.Write("\n");
            }
        }
    }
}