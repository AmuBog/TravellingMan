using System;
using System.Text;

namespace TravellingMan{
    class Program {
        static void Main(string[] args) {
            int cities = 5;
            int[,] matrix = MatrixFill(cities);

            Console.WriteLine("\n\nTotal cost Random: " + Random(cities, matrix) + " km");
            Console.WriteLine("Total cost Iterative Random: " + IterativeRandom(cities, matrix, 5) + " km");
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
            for (int i = 0; i < nodes; i++) {
                for (int j = 0; j < nodes; j++) {
                    if (j == 0 && i != 0)
                        Console.WriteLine();
                    Console.Write(cities[i,j] + " ");
                }
            }
            return cities;
        }
        static int Random(int numbOfCities, int[,] cities) {
            int i = 0;
            int cost = 0;
            Random rnd = new Random();
            int[] route = new int[numbOfCities];
            int[] exists = new int[numbOfCities];

            //Console.WriteLine("\n");

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
            /*for (i = 0; i < route.Length; i++) {
                Console.WriteLine("City " + (route[i] + 1));
            }*/
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
                /*for (i = 0; i < route.Length; i++) {
                    Console.WriteLine("City " + (route[i] + 1));
                }*/
                
                for (j = 0; j < route.Length - 1; j++) {
                    cost += cities[route[j], route[j + 1]];                    
                }
                if (cost < minCost) {
                    minCost = cost;
                }
            }
            return minCost;
        }
        static int Greedy(int numbOfCities) {
            int cost = 0;
            Random rnd = new Random();

            int start = rnd.Next(numbOfCities);
            int[] exists = new int[numbOfCities];
            int[] route = new int[numbOfCities];

            return cost;
        }
    }
}