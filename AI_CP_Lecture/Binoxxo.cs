using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Channels;
using Google.OrTools.ConstraintSolver;

namespace AI_CP_Lecture {
    class Binoxxo {
        public static void Solve(String[,] inputBinoxxo) {
            var solver = new Solver("Binoxxo");
            int sideLength = inputBinoxxo.GetLength(0);
            
            if (inputBinoxxo.Length != sideLength * sideLength)
            {
                throw new ArgumentException("This is not a valid square Binoxxo puzzle.");
            }
            
            if (inputBinoxxo.Length % 2 != 0)
            {
                throw new ArgumentException("This Binoxxo puzzle does not have an even sidelength!.");
            }

            IntVar[,] binoxxo = solver.MakeIntVarMatrix(sideLength, sideLength, 0, 1);
            IEnumerable<int> sideRange = Enumerable.Range(0, sideLength);

            // Preassignments

            for (int row = 0; row < sideLength; row++) {
                for (int column = 0; column < sideLength; column++) {
                    String preassignedValue = inputBinoxxo[row, column];
                    if (!preassignedValue.Equals("")) {
                        int value = (preassignedValue.Equals("X") ? 1 : 0);
                        solver.Add(binoxxo[row, column] == value);
                    }
                }
            }

            /*
            Binoxxo Anleitung:
            Es dürfen nicht mehr als zwei auf­ein­ander­fol­gende X oder O in einer Zeile oder Spalte vorkommen
            In jeder Zeile und jeder Spalte stehen gleich viele X und O
            Alle Zeilen und alle Spalten sind einzigartig
            */

            // Constraints
            // In jeder Zeile gleich viele X wie O's
            for (int row = 0; row < sideLength; row++) {
                solver.Add((from column in sideRange select binoxxo[row, column]).ToArray().Sum() == sideLength/2);
            }
            // In jeder Spalte gleich viele X wie O's
            for (int column = 0; column < sideLength; column++) {
                solver.Add((from row in sideRange select binoxxo[row, column]).ToArray().Sum() == sideLength/2);
            }
            DecisionBuilder decisionBuilder = null;

            solver.NewSearch(decisionBuilder);
            while (solver.NextSolution()) {
                PrintSolution();
                Console.WriteLine("SideLength: " + sideLength);
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