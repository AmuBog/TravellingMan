using System;

namespace TravellingMan{
    class Program {
        static void Main(string[] args) {
            int cities = 50;
            int[,] matrix = MatrixFill(cities);
            int[] random, iterativeRandom, greedy;

            //Optimal(cities, matrix);

            random = Random(cities, matrix);
            //PrintRoute(random = Random(cities, matrix));
            Console.WriteLine("Total length Random: " + GetCost(random, matrix) + " km\n");
            iterativeRandom = IterativeRandom(cities, matrix, 5);
            //PrintRoute(iterativeRandom = IterativeRandom(cities, matrix, 5));
            Console.WriteLine("Total length Iterative Random: " + GetCost(iterativeRandom, matrix) + " km\n");
            greedy = Greedy(cities, matrix);
            //PrintRoute(greedy = Greedy(cities, matrix));
            Console.WriteLine("Total length Greedy: " + GetCost(greedy, matrix) + " km\n");

            Console.WriteLine("Total length Random Improved: " + Improved(random, matrix) + " km\n");
            Console.WriteLine("Total length Iterative Random Improved: " + Improved(iterativeRandom, matrix) + " km\n");
            Console.WriteLine("Total length Greedy Improved: " + Improved(greedy, matrix) + " km\n");

            Console.Read();
        }
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
                    if (j == 0 && i != 0)
                        Console.WriteLine();
                    Console.Write(cities[i,j] + " ");
                }
            }*/
            return cities;
        }
        static int GetCost(int[] route, int[,] cities) {
            int cost = 0;

            for (int i = 0; i < route.Length - 1; i++) {
                cost += cities[route[i], route[i + 1]];
            }
            cost += cities[route[route.Length - 1], route[0]];
            return cost;
        }
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
        static int[] Random(int numbOfCities, int[,] cities) {
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
            return route;
        }
        static int[] IterativeRandom(int numbOfCities, int[,] cities, int iterations) {
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
                for (j = 0; j < route.Length - 1; j++) {
                    cost += cities[route[j], route[j + 1]];                    
                }
                if (cost < minCost) {
                    minCost = cost;
                    Array.Copy(route, cheapest, numbOfCities);
                }

            }
            return cheapest;
        }
        static int[] Greedy(int numbOfCities, int[,] cities) {
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
            return route;
        }
        static int Improved(int[] initial, int[,] cities) {
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
            }
            //PrintRoute(cheapest);
            return cost;
        }
        static void Optimal(int numbOfCities, int[,] cities) {
            int[] exists = new int[numbOfCities];
            int[] route = new int[numbOfCities];
            int[] optimal = new int[numbOfCities];

            for (int k = 0; k < numbOfCities; k++) {
                Array.Clear(exists, 0, exists.Length);
                int city = k;
                exists[city] = 1;
                route[0] = city;
                for (int i = 1; i < numbOfCities; i++) {
                    int min = Int32.MaxValue;
                    for (int j = 0; j < numbOfCities; j++) {
                        if (cities[route[i - 1], j] < min && cities[route[i - 1], j] != 0 && exists[j] != 1) {
                            min = cities[route[i - 1], j];
                            city = j;
                        }
                    }
                    route[i] = city;
                    exists[city] = 1;
                }
                if (k == 0) {
                    Array.Copy(route, optimal, route.Length);
                }
                else {
                    if (GetCost(route, cities) < GetCost(optimal, cities)) {
                        Array.Copy(route, optimal, route.Length);
                    }
                }
            }
            Console.WriteLine("The Optimal Soution is " + GetCost(optimal, cities) + " km.\n");
        }
    }
}