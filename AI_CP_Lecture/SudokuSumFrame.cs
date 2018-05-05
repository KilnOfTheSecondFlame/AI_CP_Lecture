using Google.OrTools.ConstraintSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_CP_Lecture
{
    class SudokuSumFrame
    {
        /*
         * Create Model and Solve Sudoku:
         */

        public static void Solve(int[] frame)
        {
            if (frame.Length != 3 *12)
            {
                throw new ArgumentException("This is not a valid 9x9 Sudoku Puzzle.");
            }

            const int cellSize = 3;
            const int boardSize = cellSize * cellSize;

            var solver = new Solver("Sudoku");

            IEnumerable<int> cell = Enumerable.Range(0, cellSize);
            IEnumerable<int> range = Enumerable.Range(0, boardSize);

            // Sudoku Board as 9x9 Matrix of Decision Variables in {1..9}:
            IntVar[,] board = solver.MakeIntVarMatrix(boardSize, boardSize, 1, boardSize);
            IntVar[] sums = solver.MakeIntVarArray(frame.Length, 1, 24);


            // Each Row / Column contains only different values: 
            foreach (int i in range)
            {
                // Rows:
                solver.Add((from j in range select board[i, j]).ToArray().AllDifferent());
                //first three cells
                solver.Add((from j in range select board[i, j]).ToArray().Take(3).ToArray().Sum() == frame[(9*1)+i] );
                //last three cells
                solver.Add( (from j in range select board[i, j]).ToArray().Skip(6).Take(3).ToArray().Sum() == frame[(2*9)+i]);
                // Columns:
                solver.Add((from j in range select board[j, i]).ToArray().AllDifferent());
                //first three cells
                solver.Add((from j in range select board[j, i]).ToArray().Take(3).ToArray().Sum() == frame[i]);
                //last three cells
                solver.Add((from j in range select board[j, i]).ToArray().Skip(6).Take(3).ToArray().Sum() == frame[(3 * 9)+i]);
            }


            // Each Sub-Matrix contains only different values:
            foreach (int i in cell)
            {
                foreach (int j in cell)
                {
                    solver.Add(
                        (from di in cell from dj in cell select board[i * cellSize + di, j * cellSize + dj]).ToArray()
                            .AllDifferent());
                }
            }

            // Start Solver:

            DecisionBuilder db = solver.MakePhase(board.Flatten(), Solver.INT_VAR_SIMPLE, Solver.INT_VALUE_SIMPLE);

            Console.WriteLine("Sudoku:\n\n");

            solver.NewSearch(db);

            while (solver.NextSolution())
            {
                PrintSolution(board, frame);
                Console.WriteLine();
                Console.ReadKey();
            }

            Console.WriteLine("\nSolutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

            Console.ReadKey();
        }

        /*
         * Print Game Board:
         */

        private static void PrintSolution(IntVar[,] board, int[] frameSums)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.Write("  ");
                Console.Write(frameSums[i].ToString("D2"));
            }
            Console.WriteLine();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write(frameSums[1* 9 + i].ToString("D2"));

                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write("[{0}] ", board[i, j].Value());
                }

                Console.Write(frameSums[2*9+i].ToString("D2"));
                Console.WriteLine();
            }
            
            for (int i = 3 * 9; i < 3 * 9+9; i++)
            {
                Console.Write("  ");
                Console.Write(frameSums[i].ToString("D2"));
            }
        }
    }
}

