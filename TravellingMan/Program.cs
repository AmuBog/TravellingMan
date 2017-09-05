using System;
using System.Text;

namespace TravellingMan{
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("Total cost random: " + Random() + "$");
            Console.WriteLine("Total cost iterative random: " + IterativeRandom() + "$");
            Console.Read();
        }
        static int[,] MatrixFill(int nodes) {
            Random rnd = new Random();
            int edge;
            int[,] cities = new int[nodes, nodes];

            for(int i = 0; i < nodes; i++) {
                for(int j = 0; j < nodes; j++) {
                    if (cities[i, j] != 0)// If space is filled.
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
        static int Random() {
            int i = 0;
            int cost = 0;
            int numbOfCities = 5;
            Random rnd = new Random();
            int[] route = new int[numbOfCities];
            int[] exists = new int[numbOfCities];
            int[,] cities;

            cities = MatrixFill(numbOfCities);
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
        static int IterativeRandom() {
            int cost = Random();
            int temp;
            for(int i = 0;i < 5; i++) {
                temp = Random();
                if (temp < cost)
                    cost = temp;
            }
            return cost;
        }
    }
}