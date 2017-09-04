using System;
using System.Text;

namespace TravellingMan{
    class Program {
        static void Main(string[] args) {

            int i = 0;
            int cost = 0;
            Random rnd = new Random();
            int[] route = new int[4];
            int[] exists = new int[4];
            int[,] cities = new int[,]
            {
                {0, 3, 6, 7},
                {3, 0, 9, 5},
                {6, 9, 0, 4},
                {7, 5, 4, 0}
            };

            while (i < 4) {
                int rando = rnd.Next(4);
                if (exists[rando] == 1)
                    continue;
                else {
                    route[i] = rando;
                    exists[rando] = 1;
                    i++;
                }
            }
            for (i = 0; i < route.Length; i++) {
                Console.WriteLine(route[i]+1);
            }
            for (i = 0; i < route.Length - 1; i++) {
                cost += cities[route[i], route[i + 1]];
            }
            Console.WriteLine("Total cost: " + cost + "$");


            Console.Read();
        }
    }
}