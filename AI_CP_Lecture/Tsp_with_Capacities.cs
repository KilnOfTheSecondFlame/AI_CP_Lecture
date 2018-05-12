using System;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the travelling salesman problem with demands and capacities presented in CP part 4.
 * 
 * Lecture: Artificial Intelligence: Search & Optimization
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    /*
     * Implementation of the Travelling Salesman Problem (TSP) using the OR-Tools routing library.
     */

    public class Tsp_with_Capacities
    {

        public static void Solve(int vehicles, int capacity, Distance distances, Demands demands)
        {

            /*
             * Generate constraint model
             */

            // Third argument defines depot, i.e. start-end node for round trip.
            var model = new RoutingModel(distances.MapSize(), vehicles, 0);

            // Node costs vs. Arc Costs
            model.SetArcCostEvaluatorOfAllVehicles(distances);

            /*
             * Capacity Constraints
             */

            if(distances.MapSize() != demands.MapSize()) {
                throw new ArgumentException("Define demand for each city.");
            }

            model.AddDimension(demands, 0, capacity, true, "capacity");

            /*
             * Solve problem and display solution
             */

            Assignment assignment = model.Solve();

            if (assignment != null)
            {
                Console.WriteLine("Depot: " + model.GetDepot());
                Console.WriteLine("Total Distance: " + assignment.ObjectiveValue() + "\n");

                for (int i = 0; i < vehicles; i++)
                {
                    /*
                     * Display Trips:
                     */

                    Console.WriteLine("Round Trip for Vehicle " + i + ":");                  

                    long total = 0;
                    var source = model.Start(i);

                    while(!model.IsEnd(source))
                    {
                        var s = model.IndexToNode(source);
                        var t = model.IndexToNode(model.Next(assignment, source));
          
                        total += distances.Run(s, t);

                        Console.WriteLine(String.Format(" - From {0,-2} (demand: {1,-1}) travel to {2,-2} (demand: {3,-1}) with distance: {4,-2}", 
                            distances.ToString(s), 
                            demands.Demand(s),
                            distances.ToString(t),
                            demands.Demand(t),
                            distances.Run(s, t)));
                        source = model.Next(assignment, source);
                    }

                    Console.WriteLine("Total Distance for Vehicle " + i + ": " + total + "\n");
                }
            }

            Console.ReadKey();
        }

        /*
         * Demands 
         */

        public class Demands : NodeEvaluator2
        {

            private int[] demands;

            /*
             * Constructor: 
             */

            public Demands(int[] demands)
            {
                this.demands = demands;
            }

            /*
             * Demand Function:
             */

            public override long Run(int i, int j)
            {
                return Demand(i);
            }

            public int Demand(int node)
            {
                return demands[node];
            }

            /*
             * Number of Cities: 
             */

            public int MapSize()
            {
                return demands.Length;
            }
        }

    }
}