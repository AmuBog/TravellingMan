using System;
using System.Text;

namespace TravellingMan{
    class Program{
        static void Main(string[] args){

            Random rnd = new Random();
            StringBuilder builder = new StringBuilder();
            int[] route = new int[4];
            bool chosen = true;
            bool[] visited = new bool[4];
            int[,] array = new int[,]
            {
                {0, 3, 6, 7},
                {3, 0, 9, 5},
                {6, 9, 0, 4},
                {7, 5, 4, 0}
            };

            int start = rnd.Next(4);
            builder.Append(start + "->");
            route[0] = start;
            visited[start] = true;
            for(int i=1;i<4;i++) {
                int next = rnd.Next(4);
                while (chosen) {
                    if (visited[next] == true)
                        next = rnd.Next(4);
                    else
                        chosen = false;
                }
                Console.WriteLine("City" + (i+1) + " = " + next);
                route[i] = next;
                visited[next] = true;

                if (i < 3)
                    builder.Append(next + "->");
                else {
                    builder.Append(next + ".");
                }
            }
            
            Console.Read();
        }
    }
}