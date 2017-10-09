using System;

namespace TravellingMan{
    class Program {
        static void Main(string[] args) {
            
            // Number of cities
            int cities = 10000;            
            // Matrix containing cities and distance
            int[,] matrix = MatrixFill(cities);
            // Tables to store initial solutions
            int[] random, iterativeRandom, greedy;
            
            // Executing the random method
            random = Random(cities, matrix);
            Console.WriteLine("Total length Random: " + GetCost(random, matrix) + " km\n");

            //Executing the iterative random method
            iterativeRandom = IterativeRandom(cities, matrix, 5);
            Console.WriteLine("Total length Iterative Random: " + GetCost(iterativeRandom, matrix) + " km\n");

            // Executing the greedy method
            greedy = Greedy(cities, matrix);
            Console.WriteLine("Total length Greedy: " + GetCost(greedy, matrix) + " km\n");

            // Improving the initial solutions
            Console.WriteLine("Total length Random Improved: " + Improved(random, matrix) + " km\n");
            Console.WriteLine("Total length Iterative Random Improved: " + Improved(iterativeRandom, matrix) + " km\n");
            Console.WriteLine("Total length Greedy Improved: " + Improved(greedy, matrix) + " km\n");

            Console.Read();
        }
        // Method filling the the matrix with cities and distances
        static int[,] MatrixFill(int nodes) {
            Random rnd = new Random();
            int edge;
            int[,] cities = new int[nodes, nodes];

            for(int i = 0; i < nodes; i++) {
                for(int j = 0; j < nodes; j++) {
                    if (cities[i, j] != 0)// If space allready is filled.
                        continue;
                    else if (j == i) { // Distance to the node itself is 0.
                        cities[i,j] = 0;
                    }
                    else {
                        edge = rnd.Next(1,10);
                        cities[i, j] = edge;
                        cities[j, i] = edge;
                    }
                }
            }
            /*for (int i = 0; i < nodes; i++) {
                for (int j = 0; j < nodes; j++) {
                    Console.Write(cities[i,j] + " ");
                    if (j == nodes-1)
                        Console.WriteLine();
                }
            }*/
            Console.WriteLine();
            return cities;
        }
        // Method calculating total cost of a route
        static int GetCost(int[] route, int[,] cities) {
            int cost = 0;

            for (int i = 0; i < route.Length - 1; i++) {
                cost += cities[route[i], route[i + 1]];
            }
            cost += cities[route[route.Length - 1], route[0]];
            return cost;
        }
        // Used to print the route
        static void PrintRoute(int[] route) {
            for (int i = 0; i < route.Length; i++) {
                if (i == (route.Length - 1)) {
                    Console.Write("City " + (route[i] + 1) + ".");
                }
                else {
                    Console.Write("City " + (route[i] + 1) + "->");
                }
            }
        }
        // Method making a random solution
        static int[] Random(int numbOfCities, int[,] cities) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int i = 0;
            Random rnd = new Random();
            int[] route = new int[numbOfCities];
            int[] exists = new int[numbOfCities];

            while (i < numbOfCities) {
                int rando = rnd.Next(numbOfCities);
                if (exists[rando] == 1)
                    continue;
                else {
                    route[i] = rando;
                    exists[rando] = 1;
                    i++;
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Execution Time Radndom = " + elapsedMs + " mS.");
            return route;
        }
        // Making an iterative random solution
        static int[] IterativeRandom(int numbOfCities, int[,] cities, int iterations) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int j, cost;
            int minCost = Int32.MaxValue;
            Random rnd = new Random();
            int[] route, exists;
            int[] cheapest = new int[numbOfCities];

            for (int i = 0; i < iterations; i++) {
                cost = 0;
                j = 0;
                exists = new int[numbOfCities];
                route = new int[numbOfCities];
                while (j < numbOfCities) {
                    int rando = rnd.Next(numbOfCities);
                    if (exists[rando] == 1)
                        continue;
                    else {
                        route[j] = rando;
                        exists[rando] = 1;
                        j++;
                    }
                }
                cost = GetCost(route, cities);

                if (cost < minCost) {
                    minCost = cost;
                    Array.Copy(route, cheapest, numbOfCities);
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Execution Time Iterative Radndom = " + elapsedMs + " mS.");
            return cheapest;
        }
        // Making a greedy solution
        static int[] Greedy(int numbOfCities, int[,] cities) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int[] exists = new int[numbOfCities];
            int[] route = new int[numbOfCities];
            Random rnd = new Random();

            int city = rnd.Next(numbOfCities);
            exists[city] = 1;
            route[0] = city;
            for (int i = 1;i < numbOfCities;i++) {
                int min = Int32.MaxValue;
                for(int j = 0;j < numbOfCities; j++) {
                    if(cities[route[i-1],j] < min && cities[route[i-1], j] != 0 && exists[j] != 1) {
                        min = cities[route[i-1], j];
                        city = j;
                    }
                }
                route[i] = city;
                exists[city] = 1;
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Execution Time Greedy = " + elapsedMs + " mS.");
            return route;
        }
        // Method improving the initial solutions
        static int Improved(int[] initial, int[,] cities) {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            int cost = GetCost(initial, cities);
            int temp, newCost;
            int stagnation = 0;
            int[] cheapest = new int[initial.Length];
            bool stop = false;
            Random rnd = new Random();

            Array.Copy(initial, cheapest, initial.Length);
            while (!stop) {
                int city1 = rnd.Next(initial.Length);
                int city2 = rnd.Next(initial.Length);

                while(city1 == city2) {
                    city2 = rnd.Next(initial.Length);
                }

                temp = initial[city1];
                initial[city1] = initial[city2];
                initial[city2] = temp;

                newCost = GetCost(initial, cities);
                if (newCost < cost) {
                    Array.Copy(initial, cheapest, initial.Length);
                    cost = newCost;
                    stagnation = 0;
                }
                else {
                    stagnation++;
                }
                if(stagnation > 1000) {
                    stop = true;
                }
                Array.Copy(cheapest, initial, cheapest.Length);
            }
            //PrintRoute(cheapest);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Execution Time Improvement = " + elapsedMs + " mS.");
            return cost;
        }
    }
}