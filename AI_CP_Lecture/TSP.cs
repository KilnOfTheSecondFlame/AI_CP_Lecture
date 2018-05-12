using System;
using System.Linq;
using Google.OrTools.ConstraintSolver;

/*
 * This model solves the travelling salesman problem presented in CP part 4.
 *
 * Given a list of cities (points of interest) and the distances (costs) between each pair of cities. 
 * What is the shortest possible route that visits each city exactly once and returns to the origin city ?
 * There are two ways of getting input to this model: adjacency matrices or XML benchmark files.
 * 
 * Lecture: Artificial Intelligence: Search & Optimization
 * Author: Marc Pouly
 */

namespace AI_CP_Lecture
{
    /*
     * Implementation of the Travelling Salesman Problem (TSP) using the OR-Tools routing library.
     */

    public class Tsp
    {

        public static void Solve(int vehicles, Distance distances)
        {

            /*
             * Generate constraint model
             */

            // Third argument defines depot, i.e. start-end node for round trip.
            var model = new RoutingModel(distances.MapSize(), vehicles, 0);

            // Node costs vs. Arc Costs
            model.SetArcCostEvaluatorOfAllVehicles(distances);

            /*
             * A vehicle must not visit more than 7 cities
             */

            //model.AddConstantDimension(1, 7, true, "count");

            /*
             * A vehicle must visit at least 3 cities
             */

             /*model.AddConstantDimension(1, Int32.MaxValue, true, "count");
             var count = model.GetDimensionOrDie("count");
             for (int i = 0; i < vehicles; i++) {
                 count.CumulVar(model.End(i)).SetMin(3);
             }*/

            /*
             * City 3 and 5 must NOT be visited by the same vehicle
             */

            //model.solver().Add(model.VehicleVar(3) != model.VehicleVar(5));

            /*
             * City 3 must be visited before city 5 (not necessarily by the same vehicle)
             */

            /*model.AddConstantDimension(1, Int32.MaxValue, true, "time");
            var time = model.GetDimensionOrDie("time");
            model.solver().Add(time.CumulVar(3) < time.CumulVar(5));*/

            /*
             * City 3 must be visited right after city 5 (not necessarily by the same vhicle)
             */

            /*model.AddConstantDimension(1, Int32.MaxValue, true, "time");
            var time = model.GetDimensionOrDie("time");
            model.solver().Add(time.CumulVar(5) + 1 == time.CumulVar(3));*/

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
                        var target = model.Next(assignment, source);

                        var from = model.IndexToNode(source);
                        var to   = model.IndexToNode(target);

                        total += distances.Run(from, to);
                        Console.WriteLine(
                            $" - From {distances.ToString(@from),-3} travel to {distances.ToString(to),-3} with distance: {distances.Run(@from, to),-3}");
                        source = target;
                    }

                    Console.WriteLine("Total Distance for Vehicle " + i + ": " + total + "\n");
                }
            }

            Console.ReadKey();
        }

        /*
         * Static Distance Matrices
         */

        public class StaticDistance : Distance
        {

            private string[] cities;
            private long[,] distances;

            /*
             * Constructor: 
             */

            public StaticDistance(long[,] distances, string[] cities = null)
            {
                if(distances.GetLength(0) != distances.GetLength(1))
                {
                    throw new ArgumentException("Distance matrix is not square.");
                }

                if(cities == null)
                {
                    cities = Array.ConvertAll(Enumerable.Range(0, distances.GetLength(0)).ToArray(), x => x.ToString());
                }

                if (cities.Length != distances.GetLength(0))
                {
                    throw new ArgumentException("Distance matrix dimensions do not match the number of cities.");
                }

                this.cities = cities;
                this.distances = distances;
            }

            /*
             * Distance Function:
             */

            public override long Run(int i, int j)
            {
                return distances[i, j];
            }

            /*
             * Number of Cities: 
             */

            public override int MapSize()
            {
                return cities.Length;
            }
            
            /*
             * City Name:
             */

            public override string ToString(int node)
            {
                return cities[node];
            }
        }

        /*
         * Distances from GPS Coordinates
         */

        public class GPS : Distance
        {

            private string[] cities;
            private double[,] gps;

            // Mean radius of Earth in miles
            private static int EARTH = 3959;

            /*
             * Constructors: 
             */

            public GPS(double[,] gps, string[] cities = null)
            {
                if (gps.GetLength(1) != 2)
                {
                    throw new ArgumentException("Wrong matrix dimensions. Array of pairs expected");
                }

                if (cities == null)
                {
                    cities = Array.ConvertAll(Enumerable.Range(0, gps.GetLength(0)).ToArray(), x => x.ToString());
                }

                if (cities.Length != gps.GetLength(0))
                {
                    throw new ArgumentException("Distance matrix dimensions do not match the number of cities.");
                }

                this.cities = cities;
                this.gps = gps;
            }

            /*
             * Distance Function:
             */

            public override long Run(int i, int j)
            {
  
                return (long)ToDistance(gps[i, 0], gps[i, 1], gps[j, 0], gps[j, 1]);

            }

            /*
             * Number of Cities: 
             */

            public override int MapSize()
            {
                return cities.Length;
            }

            /*
             * City Name:
             */

            public override string ToString(int node)
            {
                return cities[node];
            }

            /*
             * Haversine Distance used to calculate distances on sea (assumes earth as perfect sphere)
             */

            private double ToDistance(double lat1, double long1, double lat2, double long2)
            {
                var degrees_to_radians = Math.PI / 180.0;
                var phi1 = lat1 * degrees_to_radians;
                var phi2 = lat2 * degrees_to_radians;
                var lambda1 = long1 * degrees_to_radians;
                var lambda2 = long2 * degrees_to_radians;
                var dphi = phi2 - phi1;
                var dlambda = lambda2 - lambda1;

                var a = Haversine(dphi) + Math.Cos(phi1) * Math.Cos(phi2) * Haversine(dlambda);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return EARTH * c;
            }

            private double Haversine(double angle)
            {
                return Math.Pow(Math.Sin(angle / 2), 2);
            }
 
        }

        /*
         * Euclidean Distances (e.g. for drilling problems) 
         */

        public class Euclid : Distance
        {

            private string[] cities;
            private int[,] coord;

            /*
             * Constructor: 
             */

            public Euclid(int[,] coord, string[] cities = null)
            {
                if (coord.GetLength(1) != 2)
                {
                    throw new ArgumentException("Wrong matrix dimensions. Array of pairs expected");
                }

                if (cities == null)
                {
                    cities = Array.ConvertAll(Enumerable.Range(0, coord.GetLength(0)).ToArray(), x => x.ToString());
                }

                if (cities.Length != coord.GetLength(0))
                {
                    throw new ArgumentException("Distance matrix dimensions do not match the number of cities.");
                }

                this.cities = cities;
                this.coord = coord;
            }

            /*
             * Distance Function:
             */

            public override long Run(int i, int j)
            {

                return (long)Math.Sqrt(Math.Pow((coord[i, 0] - coord[j, 0]), 2) + Math.Pow((coord[i, 1] - coord[j, 1]), 2));

            }

            /*
             * Number of Cities: 
             */

            public override int MapSize()
            {
                return cities.Length;
            }

            /*
             * City Name:
             */

            public override string ToString(int node)
            {
                return cities[node];
            }
        }

        /*
         * Manhattan Distances
         */

        public class Manhattan : Distance
        {

            private string[] cities;
            private int[,] coord;

            // Block size West to East
            private static int BLOCK_WIDTH = 228 / 2;

            // Block size West to East
            private static int BLOCK_HEIGHT = 80;

            /*
             * Constructor: 
             */

            public Manhattan(int[,] coord, string[] cities = null)
            {
                if (coord.GetLength(1) != 2)
                {
                    throw new ArgumentException("Wrong matrix dimensions. Array of pairs expected");
                }

                if (cities == null)
                {
                    cities = Array.ConvertAll(Enumerable.Range(0, coord.GetLength(0)).ToArray(), x => x.ToString());
                }

                if (cities.Length != coord.GetLength(0))
                {
                    throw new ArgumentException("Distance matrix dimensions do not match the number of cities.");
                }

                this.cities = cities;
                this.coord = coord;
            }

            /*
             * Distance Function:
             */

            public override long Run(int i, int j)
            {
                return (long)Math.Abs((coord[i, 0] - coord[j, 0])) * BLOCK_WIDTH + Math.Abs((coord[i, 1] - coord[j, 1]) * BLOCK_HEIGHT);
            }

            /*
             * Number of Cities: 
             */

            public override int MapSize()
            {
                return cities.Length;
            }

            /*
             * City Name:
             */

            public override string ToString(int node)
            {
                return cities[node];
            }
        }
    }
}