using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PROJE
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //////////SECTION FOR ALL CITIES//////////
            //Initialise arrays needed for storing calculation data (1.a)
            int[][] distance = new int[81][]; //holds the distances between cities that are gathered from the data set
            int[][] neighbours = new int[81][]; //holds the plate codes for neighbouring cities
            int[,] distance_clone = new int[81, 81]; //clone of initial distance array for easier calculation
            int[,] distance_calculated = new int[81, 81]; //calculated values using dijkstra that are different from data set


            //Read data from txt files
            string[] dist_lines = File.ReadAllLines("ilmesafe.txt");
            string[] nbour_lines = File.ReadAllLines("komsuiller.txt");

            //Converts txt file data into needed data structures such as jagged array
            for (int i = 0; i < 81; i++)
            {
                int[] dist_line = Array.ConvertAll(dist_lines[i].Split(null), int.Parse);
                ArraySegment<int> dist_numbers = new ArraySegment<int>(dist_line, 0, i + 1);
                int[] nbour_line = Array.ConvertAll(nbour_lines[i].Split(','), int.Parse);
                distance[i] = dist_numbers.ToArray();
                neighbours[i] = nbour_line;
            }

                PrintRandomCities(distance,10);

            for (int i = 0; i < 81; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    distance_clone[i, j] = distance[i][j]; //cloning the distance before making differences in the original one
                }
            }

            distance = InitiateDistanceMatrix(distance, neighbours); //set non neighburing city distances to infinity (1.b)

            PrintRandomCities(distance, 10);

            //(1.c)
            List<int[][]> differences = new List<int[][]>();//stores the difference_arr, representing the entire jagged array
            for (int i = 1; i < 81; i++)
            {
                int[][] difference_arr = new int[81][];//stores the "difference" arrays, representing each row in jagged array
                for (int j = 0; j < i; j++)
                {
                    int[] difference = new int[3];//stores indeces and the difference value of calculated cities
                    int shortestpath = FindShortestPath(distance, i + 1, j + 1, neighbours);//checks if the chosen cities are neighbouring
                    if (shortestpath != -1)
                    {
                        distance_calculated[i, j] = shortestpath;
                        Console.WriteLine(cities[i] + " ve " + cities[j] + " arasındaki cetvel değeri, hesaplanan değer, fark:");
                        Console.Write(distance_clone[i, j] + ", ");//print table value
                        Console.Write(distance_calculated[i, j] + ", ");//print calculated value
                        difference[0] = Math.Abs(distance_calculated[i, j] - distance_clone[i, j]);
                        difference[1] = i; difference[2] = j;
                        Console.WriteLine(+difference[0] + " "); //print difference value
                        difference_arr[j] = difference;
                    }
                    


                }
                if (difference_arr != null) differences.Add(difference_arr);
            }

            Console.WriteLine("Cities with maximum difference found: ");
            PrintDiff(FindMax(differences));
            Console.WriteLine("Cities with minimum difference found: ");
            PrintDiff(FindMin(differences));



            //////////SECTION FOR IZMIR DISTRICTS//////////
            double[][] distance_izmir = new double[30][];//holds the distances between districts that are gathered from the data set
            int[][] district_neighbours = new int[30][];//holds the alphabetcal order number (codes) for neighbouring districts ex:(aliağa=1, bayındır=3)
            double[,] distance_izmir_clone = new double[30, 30];//clone of the distance_izmir array to be used to compare
            double[,] distance_izmir_calculated = new double[30, 30];//calculated distance values using dijkstra

            //Read data from txt files
            string[] district_lines = File.ReadAllLines("izmirilce.txt");
            string[] district_nbour_lines = File.ReadAllLines("komsuilceler.txt");

            //Converts txt file data into needed data structures such as jagged array
            for (int i = 0; i < distance_izmir.GetLength(0); i++)
            {
                int[] district_nbour_line = Array.ConvertAll(district_nbour_lines[i].Split(','), int.Parse);
                district_neighbours[i] = district_nbour_line;
                double[] izmir_line = Array.ConvertAll(district_lines[i].Split(null), double.Parse);
                ArraySegment<double> dist_numbers = new ArraySegment<double>(izmir_line, 0, i + 1);
                distance_izmir[i] = dist_numbers.ToArray();
            }

            for (int i = 0; i < distance_izmir.GetLength(0); i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    distance_izmir_clone[i, j] = distance_izmir[i][j]; //cloning
                }
            }

            PrintRandomDistricts(distance_izmir, 10);

            distance_izmir = InitiateDistanceMatrix(distance_izmir, district_neighbours);//set to infinity


            List<double[][]> differences_izmir = new List<double[][]>();
            for (int i = 1; i < 30; i++)
            {
                double[][] difference_arr = new double[30][];
                for (int j = 0; j < i; j++)
                {
                    double[] difference = new double[3]; //stores indeces and the difference value of calculated cities
                    double shortestpath = FindShortestPath(distance_izmir, i + 1, j + 1, district_neighbours);//checks if the chosen districts are neighbouring
                    if (shortestpath != -1)
                    {
                        distance_izmir_calculated[i, j] = shortestpath;
                        Console.WriteLine(districts[i] + " ve " + districts[j] + " arasındaki cetvel değeri, hesaplanan değer, fark:");
                        Console.Write(distance_izmir_clone[i, j] + ", "); //print table value
                        Console.Write(distance_izmir_calculated[i, j] + ", "); //print calculated value
                        difference[0] = Math.Abs(distance_izmir_calculated[i, j] - distance_izmir_clone[i, j]);
                        string diff = String.Format("{0:0.0000}", difference[0]);//round to 4 decimals
                        difference[1] = i; difference[2] = j;
                        Console.WriteLine(diff + " "); //print difference value
                        difference_arr[j] = difference;
                    }

                }
                if (difference_arr != null) differences_izmir.Add(difference_arr);
            }

            Console.WriteLine("Districts with maximum difference found: ");
            PrintDiff(FindMax(differences_izmir));
            Console.WriteLine("Districts with minimum difference found: ");
            PrintDiff(FindMin(differences_izmir));



            Console.ReadLine(); 
        }
        static void PrintRandomCities(int[][] distance, int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
            Start:
                int firstCity = rnd.Next(0, 81);
                int secondCity = rnd.Next(0, 81);
                if (firstCity > secondCity)
                {
                    Console.WriteLine($"{cities[firstCity]} {firstCity + 1} - {cities[secondCity]} {secondCity+1} Distance: {distance[firstCity][secondCity]}");
                }
                else if (secondCity > firstCity) { 
                    Console.WriteLine($"{cities[secondCity]} {secondCity+1} - {cities[firstCity]} {firstCity+1} Distance: {distance[secondCity][firstCity]}");
                }
                else { Console.WriteLine("Randomly selected cities appear to be the same. Re-running the method."); goto Start; }
            }
        }

        static void PrintRandomDistricts(double[][] distance, int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
            Start:
                int firstDistrict = rnd.Next(0, 30);
                int secondDistrict = rnd.Next(0, 30);
                if (firstDistrict > secondDistrict)
                {
                    Console.WriteLine($"{districts[firstDistrict]} {firstDistrict + 1} - {districts[secondDistrict]} {secondDistrict + 1} Distance: {distance[firstDistrict][secondDistrict]}");
                }
                else if (secondDistrict > firstDistrict)
                {
                    Console.WriteLine($"{districts[secondDistrict]} {secondDistrict + 1} - {districts[firstDistrict]} {firstDistrict + 1} Distance: {distance[secondDistrict][firstDistrict]}");
                }
                else { Console.WriteLine("Randomly selected cities appear to be the same. Re-running the method."); goto Start; }
            }
        }

        //method that sets distance matrix values to infinity
        static int[][] InitiateDistanceMatrix(int[][] distance, int[][] neighbours)
        {
            for (int i = 0; i < distance.GetLength(0); i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (Array.IndexOf(neighbours[i], j + 1) == -1) distance[i][j] = int.MaxValue;
                }
                for (int j = 0; j > i; j++)
                {
                    distance[i][j] = int.MaxValue;
                }
            }
            return distance;
        }
        static double[][] InitiateDistanceMatrix(double[][] distance, int[][] neighbours)
        {
            for (int i = 0; i < distance.GetLength(0); i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    if (Array.IndexOf(neighbours[i], j + 1) == -1) distance[i][j] = int.MaxValue;
                }
                for (int j = 0; j > i; j++)
                {
                    distance[i][j] = int.MaxValue;
                }
            }
            return distance;
        }

        //dijkstra's algorithm
        static int FindShortestPath(int[][] distance, int startCity, int endCity, int[][] neighbours)
        {
            if (Array.IndexOf(neighbours[startCity - 1], endCity) == -1)
            {
                int cityCount = distance.Length;
                int startIdx = startCity - 1;
                int endIdx = endCity - 1;

                int[] distances = new int[cityCount];
                bool[] visited = new bool[cityCount];
                int[] previous = new int[cityCount];

                for (int i = 0; i < cityCount; i++)
                {
                    distances[i] = int.MaxValue;
                    previous[i] = -1;
                }
                distances[startIdx] = 0;

                for (int count = 0; count < cityCount; count++)
                {
                    int u = -1;
                    int minDistance = int.MaxValue;

                    for (int i = 0; i < cityCount; i++)
                    {
                        if (!visited[i] && distances[i] < minDistance)
                        {
                            minDistance = distances[i];
                            u = i;
                        }
                    }

                    if (u == -1 || u == endIdx) break;
                    visited[u] = true;

                    for (int v = 0; v < cityCount; v++)
                    {
                        int dist;
                        if (u > v)
                            dist = distance[u][v];
                        else if (u < v && v < distance[v].Length)
                            dist = distance[v][u];
                        else
                            continue;

                        if (dist != int.MaxValue && distances[u] + dist < distances[v])
                        {
                            distances[v] = distances[u] + dist;
                            previous[v] = u;
                        }
                    }
                }

                if (distances[endIdx] == int.MaxValue)
                {
                    Console.WriteLine($"No path found from city {startCity} to city {endCity}.");
                    return int.MaxValue;
                }

                //Console.WriteLine($"Shortest path from city {startCity} to city {endCity}:");
                //PrintPath(previous, endIdx);
                //Console.WriteLine($"\nTotal distance: {distances[endIdx]}");
                return distances[endIdx];
            }
            else return -1;
        }
        static double FindShortestPath(double[][] distance, int startCity, int endCity, int[][] neighbours)
        {
            if (Array.IndexOf(neighbours[startCity - 1], endCity) == -1)
            {
                int cityCount = distance.Length;
                int startIdx = startCity - 1;
                int endIdx = endCity - 1;

                double[] distances = new double[cityCount];
                bool[] visited = new bool[cityCount];
                int[] previous = new int[cityCount];

                for (int i = 0; i < cityCount; i++)
                {
                    distances[i] = int.MaxValue;
                    previous[i] = -1;
                }
                distances[startIdx] = 0;

                for (int count = 0; count < cityCount; count++)
                {
                    int u = -1;
                    double minDistance = int.MaxValue;

                    for (int i = 0; i < cityCount; i++)
                    {
                        if (!visited[i] && distances[i] < minDistance)
                        {
                            minDistance = distances[i];
                            u = i;
                        }
                    }

                    if (u == -1 || u == endIdx) break;
                    visited[u] = true;

                    for (int v = 0; v < cityCount; v++)
                    {
                        double dist;
                        if (u > v)
                            dist = distance[u][v];
                        else if (u < v && v < distance[v].Length)
                            dist = distance[v][u];
                        else
                            continue;

                        if (dist != int.MaxValue && distances[u] + dist < distances[v])
                        {
                            distances[v] = distances[u] + dist;
                            previous[v] = u;
                        }
                    }
                }

                if (distances[endIdx] == int.MaxValue)
                {
                    Console.WriteLine($"No path found from city {startCity} to city {endCity}.");
                    return int.MaxValue;
                }

                //Console.WriteLine($"Shortest path from city {startCity} to city {endCity}:");
                //PrintPath(previous, endIdx);
                //Console.WriteLine($"\nTotal distance: {distances[endIdx]}");
                return distances[endIdx];
            }
            else return -1;
        }

        static void PrintPath(int[] previous, int current)
        {
            if (current == -1) return;
            PrintPath(previous, previous[current]);
            Console.Write((current + 1) + " ");
        }

        //stores values in a list in case of there are multiple ones
        static (List<int[]>, int) FindMax(List<int[][]> arr)
        {
            int maxNumber = int.MinValue;
            List<int[]> maxIndices = new List<int[]>();

            for (int i = 0; i < arr.Count; i++)
            {

                for (int j = 0; j < arr[i].Length; j++)
                {if (arr[i][j] != null)
                    {
                        int[] indices = { arr[i][j][1], arr[i][j][2] };

                        if (arr[i][j][0] > maxNumber)
                        {
                            maxNumber = arr[i][j][0];
                            maxIndices.Clear(); // Clear previous indices
                            maxIndices.Add(indices); // Add current index
                        }
                        else if (arr[i][j][0] == maxNumber)
                        {
                            maxIndices.Add(indices); // Add index of another occurrence
                        }
                    }
                }
            }
            return (maxIndices, maxNumber);

        }
        static (List<double[]>, double) FindMax(List<double[][]> arr)
        {
            double maxNumber = double.MinValue;
            List<double[]> maxIndices = new List<double[]>();

            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {if (arr[i][j] != null)
                    {
                        double[] indices = { arr[i][j][1], arr[i][j][2] };


                        if (arr[i][j][0] > maxNumber)
                        {
                            maxNumber = arr[i][j][0];
                            maxIndices.Clear(); // Clear previous indices
                            maxIndices.Add(indices); // Add current index
                        }
                        else if (arr[i][j][0] == maxNumber)
                        {
                            maxIndices.Add(indices); // Add index of another occurrence
                        }
                    }
                }
            }
            return (maxIndices, maxNumber);

        }

        static (List<int[]>, int) FindMin(List<int[][]> arr)
        {
            int minNumber = int.MaxValue;
            List<int[]> minIndices = new List<int[]>();

            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != null)
                    {
                        int[] indices = { arr[i][j][1], arr[i][j][2] };

                        if (arr[i][j][0] < minNumber)
                        {
                            minNumber = arr[i][j][0];
                            minIndices.Clear(); // Clear previous indices
                            minIndices.Add(indices); // Add current index
                        }
                        else if (arr[i][j][0] == minNumber)
                        {
                            minIndices.Add(indices); // Add index of another occurrence
                        }
                    }
                }
            }
            return (minIndices, minNumber);

        }
        static (List<double[]>, double) FindMin(List<double[][]> arr)
        {
            double minNumber = int.MaxValue;
            List<double[]> minIndices = new List<double[]>();

            for (int i = 0; i < arr.Count; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {if (arr[i][j] != null)
                    {
                        double[] indices = { arr[i][j][1], arr[i][j][2] };

                        if (arr[i][j][0] < minNumber)
                        {
                            minNumber = arr[i][j][0];
                            minIndices.Clear(); // Clear previous indices
                            minIndices.Add(indices); // Add current index
                        }
                        else if (arr[i][j][0] == minNumber)
                        {
                            minIndices.Add(indices); // Add index of another occurrence
                        }
                    }
                }
            }
            return (minIndices, minNumber);

        }
        static void PrintDiff((List<int[]>, int) maxmin)
        {
            (List<int[]>, int) maxmin_diff = maxmin;
            foreach (int[] indices in maxmin_diff.Item1)
            {
                foreach (int index in indices)
                {
                    Console.Write(cities[index] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Difference is: " + maxmin_diff.Item2);
        }
        static void PrintDiff((List<double[]>, double) maxmin)
        {
            (List<double[]>, double) maxmin_diff = maxmin;
            foreach (double[] indices in maxmin_diff.Item1)
            {
                foreach (int index in indices)
                {
                    Console.Write(districts[index] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Difference is: " + maxmin_diff.Item2);
        }


        static string[] cities = {
                "Adana", "Adıyaman", "Afyon", "Ağrı", "Amasya", "Ankara", "Antalya", "Artvin", "Aydın",
                "Balıkesir", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı",
                "Çorum", "Denizli", "Diyarbakır", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep",
                "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Isparta", "İçel (Mersin)", "İstanbul", "İzmir", "Kars",
                "Kastamonu", "Kayseri", "Kırklareli", "Kırşehir", "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa",
                "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Rize", "Sakarya", "Samsun",
                "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Uşak", "Van",
                "Yozgat", "Zonguldak", "Aksaray", "Bayburt", "Karaman", "Kırıkkale", "Batman", "Şırnak", "Bartın",
                "Ardahan", "Iğdır", "Yalova", "Karabük", "Kilis", "Osmaniye", "Düzce" };

        static string[] districts = {
                "ALİAĞA", "BALÇOVA", "BAYINDIR", "BAYRAKLI", "BERGAMA",
                "BEYDAĞ", "BORNOVA", "BUCA", "ÇEŞME", "ÇİĞLİ", "DİKİLİ",
                "FOÇA", "GAZİEMİR", "GÜZELBAHÇE", "KARABAĞLAR",
                "KARABURUN", "KARŞIYAKA", "KEMALPAŞA", "KINIK", "KİRAZ",
                "KONAK", "MENDERES", "MENEMEN", "NARLIDERE", "ÖDEMİŞ",
                "SEFERİHİSAR", "SELÇUK", "TİRE", "TORBALI", "URLA"
                };


    }
}