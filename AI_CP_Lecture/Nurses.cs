using System;
using System.Collections.Generic;
using System.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the nurse scheduling problem presented in CP part 3.
 * 
 * Companies that operate 24 hours a day, seven days a week, such as factories or hospitals, need to solve a common problem: 
 * How to schedule workers in multiple daily shifts so that each shift is staffed by enough workers to maintain operations. 
 * The schedule can have various constraints — for example, that no employee works two consecutive shifts.
 * 
 * A hospital supervisor needs to create a weekly schedule for four nurses, subject to the following conditions:
 *      Each day is divided into three 8-hour shifts.
 *      On each day, all nurses are assigned to different shifts and one nurse has the day off.
 *      Each nurse works five or six days a week.
 *      No shift is staffed by more than two different nurses in a week.
 *      If a nurse works shifts 2 or 3 on a given day, he must also work the same shift either the previous day or the following day.
 *      
 * Example borrowed from: https://developers.google.com/optimization/scheduling/employee_scheduling
 * However, the model is different from the one suggested in the tutorial - simpler, but probably not equally efficient
 *
 * Lecture: Artificial Intelligence: Search & Optimization
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    public class Nurses
    {

        /*
         * Create Model:
         */

        public static void Solve()
        {

            var solver = new Solver("Nurse Scheduling");

            // The total number of nurses: 4 with values {0, ..., 3}
            int num_nurses = 4;

            // The number of shifts per day: 4 with values {0, ..., 3}
            // 3x 8h, shift = 0 means not working that day
            int num_shifts = 4;

            // The number of days: 7 with values {0 ... 6}
            int num_days = 7;

            // nurse[i, j] = k means nurse k works in shift i on day j
            IntVar[,] nurse = solver.MakeIntVarMatrix(num_shifts, num_days, 0, num_nurses-1);

            /*
             * A nurse must not work two shifts on the same day:
             */

            for (int j = 0; j < num_days; j++)
            {

                // Cells in the same nurse table column must have different values
                solver.Add(solver.MakeAllDifferent((from k in Enumerable.Range(0, num_shifts) select nurse[k, j]).ToArray()));

            }

            /*
             * Auxiliary decision variables: How many days does nurse k work in shift i
             */

            IntVar[,] days = new IntVar[num_nurses, num_shifts];

            for (int k = 0; k < num_nurses; k++)
            {
                for (int i = 0; i < num_shifts; i++)
                {

                    days[k, i] = ((from j in Enumerable.Range(0, num_days) select nurse[i, j] == k).ToArray()).Sum().Var();

                }
            }

            /*
             * Each nurse has at most 2 days off
             */

            for (int k = 0; k < num_nurses; k++)
            {

                // Less than 3 days off per week
                solver.Add(days[k, 0] < 3);

                // At least one day off per week
                solver.Add(days[k, 0] > 0);

            }

            /*
             * Work and Holiday = 7 days (this is actually redundant)
             */

            /*for (int k = 0; k < num_nurses; k++)
            {

                solver.Add(((from i in Enumerable.Range(0, num_shifts) select days[k, i]).ToArray()).Sum() == 7);

            }

            /*
             * Shifts are staffed by at most two nurses per week: 
             * Excluse shif 0 (i.e. days off)
             */

            for (int i = 1; i < num_shifts; i++)
            {

                solver.Add(((from k in Enumerable.Range(0, num_nurses) select days[k, i] > 0).ToArray()).Sum() <= 2);

            }

            /*
             * Nurses work shift 2 or 3 on consecutive days 
             * 
             * Examples:
             *  nurses[2, 1] == nurses[2, 2] is true if the same nurse works shift 2 on Monday and Tuesday
             *  nurses[2, 2] == nurses[2, 3] is True if the same nurse works shift 2 on Tuesday or Wednesday
             *  => at least one of the two cases must hold
             */

            solver.Add(solver.MakeMax(nurse[2, 0] == nurse[2, 1], nurse[2, 1] == nurse[2, 2]) == 1);
            solver.Add(solver.MakeMax(nurse[2, 1] == nurse[2, 2], nurse[2, 2] == nurse[2, 3]) == 1);
            solver.Add(solver.MakeMax(nurse[2, 2] == nurse[2, 3], nurse[2, 3] == nurse[2, 4]) == 1);
            solver.Add(solver.MakeMax(nurse[2, 3] == nurse[2, 4], nurse[2, 4] == nurse[2, 5]) == 1);
            solver.Add(solver.MakeMax(nurse[2, 4] == nurse[2, 5], nurse[2, 5] == nurse[2, 6]) == 1);
            solver.Add(solver.MakeMax(nurse[2, 5] == nurse[2, 6], nurse[2, 6] == nurse[2, 0]) == 1);
            solver.Add(solver.MakeMax(nurse[2, 6] == nurse[2, 0], nurse[2, 0] == nurse[2, 1]) == 1);

            solver.Add(solver.MakeMax(nurse[3, 0] == nurse[3, 1], nurse[3, 1] == nurse[3, 2]) == 1);
            solver.Add(solver.MakeMax(nurse[3, 1] == nurse[3, 2], nurse[3, 2] == nurse[3, 3]) == 1);
            solver.Add(solver.MakeMax(nurse[3, 2] == nurse[3, 3], nurse[3, 3] == nurse[3, 4]) == 1);
            solver.Add(solver.MakeMax(nurse[3, 3] == nurse[3, 4], nurse[3, 4] == nurse[3, 5]) == 1);
            solver.Add(solver.MakeMax(nurse[3, 4] == nurse[3, 5], nurse[3, 5] == nurse[3, 6]) == 1);
            solver.Add(solver.MakeMax(nurse[3, 5] == nurse[3, 6], nurse[3, 6] == nurse[3, 0]) == 1);
            solver.Add(solver.MakeMax(nurse[3, 6] == nurse[3, 0], nurse[3, 0] == nurse[3, 1]) == 1);

            var all = nurse.Flatten().Concat(days.Flatten()).ToArray();

            /*
             * Start solver 
             */

            DecisionBuilder db = solver.MakePhase(all, Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            Console.WriteLine("Nurse Scheduling:\n");
            int counter = 0;
            solver.NewSearch(db);

            while (solver.NextSolution() && counter < 2)
            {
                /*Console.WriteLine("Shift x Day -> Nurse\n");
                PrintSolution(nurse, (from i in Enumerable.Range(0, num_shifts) select "Shift "+i).ToArray(), new String[] {"M", "D", "M", "D", "F", "S", "S"});
                Console.WriteLine("\nNurse x Shift -> Days\n");
                PrintSolution(days, (from i in Enumerable.Range(0, num_nurses) select "Nurse " + i).ToArray(), (from i in Enumerable.Range(0, num_shifts) select i+"").ToArray());
                Console.WriteLine();
                counter++;*/
            }

            Console.WriteLine("Solutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

            Console.ReadKey();
        }

        /*
         * Print Game Board:
         */

        private static void PrintSolution(IntVar[,] board, String[] rows, String[] cols)
        {

            if(rows.GetLength(0) != board.GetLength(0) || cols.GetLength(0) != board.GetLength(1))
            {
                throw new ArgumentException("Dimensions do not match !");
            }

            Console.Write("".PadRight(rows[0].Length + 2, ' '));

            for (int j = 0; j < board.GetLength(1); j++)
            {
                Console.Write("[{0}] ", cols[j]);
            }

            Console.WriteLine();

            for (int i = 0; i < board.GetLength(0); i++)
            {

                Console.Write("{0}: ", rows[i]);

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write("[{0}] ", board[i, j].Value());
                }
                Console.WriteLine();
            }
        }
    }
}
