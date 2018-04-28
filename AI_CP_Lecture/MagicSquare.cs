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
            
            IntVar[,] square = solver.MakeIntVarMatrix(sideLength, sideLength,  1, sideLength ^ 2);
            IntVar sum = solver.MakeIntVar(1, sideLength ^ 4);
            // Add constraints
            // All different numbers
            solver.Add(square.Flatten().AllDifferent());
            foreach (var i in range) {
                // Rows should all have the same sum
                solver.Add((from j in range select square[i , j]).ToArray().Sum() == sum);
                // Columns should also all have the same sum
                solver.Add((from j in range select square[j , i]).ToArray().Sum() == sum);
            }

            DecisionBuilder decisionBuilder =
                solver.MakePhase(square.Flatten(), Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);
            
            solver.NewSearch(decisionBuilder);
            while (solver.NextSolution()) {
                Console.Write(square);
                Console.ReadLine();
            }
        }
    }
}
