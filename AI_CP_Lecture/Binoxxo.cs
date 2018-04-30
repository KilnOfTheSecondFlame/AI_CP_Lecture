using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Google.OrTools.ConstraintSolver;

namespace AI_CP_Lecture {
    class Binoxxo {
        public static void Solve(String[,] inputBinoxxo) {
            var solver = new Solver("Binoxxo");
            int sideLength = inputBinoxxo.GetLength(0);

            IntVar[,] binoxxo = solver.MakeIntVarMatrix(sideLength, sideLength, 0, 1);

            // Preassignments
            
            for (int row = 0; row < sideLength; row++) {
                for (int column = 0; column < sideLength; column++) {
                    
                }
            }
            
            /*
            Binoxxo Anleitung:
            Es dürfen nicht mehr als zwei auf­ein­ander­fol­gende X oder O in einer Zeile oder Spalte vorkommen
            In jeder Zeile und jeder Spalte stehen gleich viele X und O
            Alle Zeilen und alle Spalten sind einzigartig
            */
            
            // Constraints

            DecisionBuilder decisionBuilder = null;

            solver.NewSearch(decisionBuilder);
            while (solver.NextSolution()) {
                PrintSolution();
                Console.WriteLine("Magic square constant: " + sum.Value());
                Console.Write("\n");
            }

            Console.WriteLine("Finished printing solutions");
            Console.WriteLine("Time: " + solver.WallTime());
            Console.WriteLine("Solutions: " + solver.Solutions());
            solver.EndSearch();
        }

        private static void PrintSolution() {
//            for (int i = 0; i < magicSquare.GetLength(0); i++) {
//                for (int j = 0; j < magicSquare.GetLength(1); j++) {
//                    Console.Write("[{00}]", magicSquare[i, j].Value());
//                }
//                Console.Write("\n");
//            }
        }
    }
}