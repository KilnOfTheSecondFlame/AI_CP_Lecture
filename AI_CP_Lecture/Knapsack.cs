using System;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the grocery store puzzle presented in CP part 3.
 *
 * A smuggler has a knapsack with a capacity of 9 units. She wants to maximize her profit. Which of the following items does she smuggle ?
 * 
 * Lecture: Artificial Intelligence: Search & Optimization
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    public class Knapsack
    {
        /*
         * Main Method:
         */

        public static void Solve(Tuple<int, int[], int[], string[]> input)
        {
            
            var solver = new Solver("Knapsack");

            // Total capacity:
            int capacity = input.Item1;

            // Weights and prices for each item:
            int[] weights = input.Item2;
            int[] prices  = input.Item3;

            // Item names for pretty printing:
            string[] names = input.Item4;

            IntVar[] items = solver.MakeIntVarArray(names.Length, 0, capacity);

            /*
             * Constraints:
             */

            solver.Add(solver.MakeScalProd(items, weights) <= capacity);

            /*
             * Objective Function:
             */

            IntVar obj = solver.MakeScalProd(items, prices).Var();

            /*
             * Start Solver:
             */

            DecisionBuilder db = solver.MakePhase(items, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            SolutionCollector col = solver.MakeBestValueSolutionCollector(true);
            col.AddObjective(obj);
            col.Add(items);

            Console.WriteLine("Knapsack Problem:\n");

            if (solver.Solve(db, col))
            {
                Assignment sol = col.Solution(0);
                Console.WriteLine("Maximum value found: " + sol.ObjectiveValue() + "\n");

                for (int i = 0; i < items.Length; i++)
                {
                    Console.WriteLine("Item " + names[i] + " is smuggled " + sol.Value(items[i]) + " time(s).");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Solutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            Console.ReadKey();
        }
    }
}