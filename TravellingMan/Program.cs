using System;
using System.Text;

namespace TravellingMan{
    class Program {
        static void Main(string[] args) {
            int cities = 10;
            int[,] matrix = MatrixFill(cities);

            Console.WriteLine("Total length Random: " + Random(cities, matrix) + " km");
            Console.WriteLine("Total length Iterative Random: " + IterativeRandom(cities, matrix, 5) + " km");
            Console.WriteLine("Total length Greedy: " + Greedy(cities, matrix) + " km");
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
        static int Random(int numbOfCities, int[,] cities) {
            int i = 0;
            int cost = 0;
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
            for (i = 0; i < route.Length; i++) {
                if (i == (route.Length - 1)) {
                    Console.Write("City " + (route[i] + 1) + ".\n");
                }
                else {
                    Console.Write("City " + (route[i] + 1) + "->");
                }
            }
            for (i = 0; i < route.Length - 1; i++) {
                cost += cities[route[i], route[i + 1]];
            }        
            return cost;
        }
        static int IterativeRandom(int numbOfCities, int[,] cities, int iterations) {
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
            for (j = 0; j < cheapest.Length; j++) {
                if (j == (cheapest.Length - 1)) {
                    Console.Write("City " + (cheapest[j] + 1) + ".\n");
                }
                else {
                    Console.Write("City " + (cheapest[j] + 1) + "->");
                }
            }
            return minCost;
        }
        static int Greedy(int numbOfCities, int[,] cities) {
            int cost = 0;
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
            for (int i = 0; i < route.Length - 1; i++) {
                cost += cities[route[i], route[i + 1]];
            }
            return cost;
        }
    }
}