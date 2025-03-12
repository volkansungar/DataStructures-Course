using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Proje1part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] weight1 = new double[25];
            double[] weight2 = new double[25];

            // Variation 1
            int[,] one1 = {
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 0, 0, 0, 0 }
            };
            

            // Variation 2
            int[,] one2 = {
                { 0, 0, 1, 0, 0 },
                { 1, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 1, 1, 1, 1, 1 }
            };

            // Variation 3
            int[,] one3 = {
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 0, 0 }
            };

            // Variation 4
            int[,] one4 = {
                { 0, 0, 1, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 1, 1, 1, 1, 1 }
            };

            // Variation 5
            int[,] one5 = {
                { 0, 0, 0, 1, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 1, 1, 1 }
            };

            // Variation 6
            int[,] one6 = {
                { 0, 0, 0, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 1, 1, 1, 0 }
            };

            // Variation 7
            int[,] one7 = {
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 }
            };

            // Variation 8
            int[,] one8 = {
                { 0, 0, 1, 0, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 1, 1, 1, 0 }
            };

            // Variation 9
            int[,] one9 = {
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 0, 1, 0, 0 }
            };

            // Variation 10
            int[,] one10 = {
                { 0, 0, 0, 1, 0 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 0, 1, 0 }
            };

            // Variation 1
            int[,] two1 = {
                { 0, 1, 1, 1, 0 },
                { 1, 0, 0, 0, 1 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 1, 1, 1, 1, 1 }
            };

            // Variation 2
            int[,] two2 = {
                { 1, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 0, 0, 0 },
                { 1, 1, 1, 1, 1 }
};

            // Variation 3
            int[,] two3 = {
                { 0, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 1 },
                { 0, 1, 1, 1, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 1 }
            };

            // Variation 4
            int[,] two4 = {
                { 0, 1, 1, 1, 0 },
                { 1, 0, 0, 0, 1 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 1, 1, 1, 1, 0 }
            };

            // Variation 5
            int[,] two5 = {
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 1 },
                { 0, 1, 1, 1, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 1, 1, 1, 0 }
            };

            // Variation 6
            int[,] two6 = {
                { 1, 1, 1, 1, 1 },
                { 0, 0, 0, 1, 0 },
                { 0, 0, 1, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 1, 1, 1, 1, 1 }
            };

            // Variation 7
            int[,] two7 = {
                { 1, 1, 1, 1, 1 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 0 },
                { 0, 0, 0, 1, 0 },
                { 1, 1, 1, 1, 1 }
            };

            // Variation 8
            int[,] two8 = {
                { 0, 1, 1, 1, 0 },
                { 1, 0, 0, 0, 1 },
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 0 }
            };

            // Variation 9
            int[,] two9 = {
                { 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 0, 0, 0 },
                { 1, 1, 1, 1, 1 }
            };

            // Variation 10
            int[,] two10 = {
                { 0, 1, 1, 1, 0 },
                { 1, 0, 0, 0, 1 },
                { 0, 0, 1, 1, 0 },
                { 0, 1, 0, 0, 0 },
                { 1, 1, 1, 1, 0 }
            };

            int[,] test1 =
            {
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 0 },
            };
            
            int[,] test2 =
            {
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
            };
            int[,] test3 =
            {
                { 0, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 1 },
            };



            //store inputs in list
            List<int[,]> arraysOne = new List<int[,]> { one1, one2, one3, one4, one5, one6, one7, one8, one9, one10 };
            List<int[,]> arraysTwo = new List<int[,]> { two1, two2, two3, two4, two5, two6, two7, two8, two9, two10 };


            List<int[]> flattenedOne = new List<int[]>();
            List<int[]> flattenedTwo = new List<int[]>();

            //flatten inputs into 1 dimension and add to another list
            foreach (var array2D in arraysOne)
            {
                flattenedOne.Add(Flatten(array2D));
            }

            foreach (var array2D in arraysTwo)
            {
                flattenedTwo.Add(Flatten(array2D));
            }


            //initialise random weight values
            Random rnd = new Random();
            double MAX_VALUE = 1;
            double MIN_VALUE = -1;
            for (int i = 0; i < weight1.Length; i++)
            {
                weight1[i] = rnd.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE;
                weight2[i] = rnd.NextDouble() * (MAX_VALUE - MIN_VALUE) + MIN_VALUE;

            }


            NeuralNetwork network = new NeuralNetwork();
            network.Learn(weight1, weight2, flattenedOne, flattenedTwo);
            //PrintArray(weight1);
            //PrintArray(weight2);

            network.FindResult(weight1, weight2, flattenedOne, flattenedTwo);
            network.FindResult(weight1, weight2, Flatten(test1));
            network.FindResult(weight1, weight2, Flatten(test2));
            network.FindResult(weight1, weight2, Flatten(test3));



            Console.ReadLine();
        }
        static int[] Flatten(int[,] arr)
        {
            int index = 0;
            int[] result = new int[arr.Length * arr.Length];
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    result[index] = arr[i, j];
                    index++;
                }

            }
            return result;
        }

        static void PrintArray<T>(T[] array) 
        {
            Console.WriteLine("Printing 1D array:");
            foreach (T item in array)
            {
                Console.Write(item+" ");
            }
            Console.WriteLine();
        }
        static void PrintArray<T>(T[,] array)
        {
            Console.WriteLine("Printing 2D array:");
            foreach (T item in array)
            {
                Console.WriteLine();
                foreach (T item1 in array)
                {
                    Console.Write(item1+" ");
                }
            }
            Console.WriteLine();
        }

    }

    class Neuron
    {
        double[] weight;
        int[] input;

        public Neuron(double[]weightArr, int[] inputArr)
        { weight = weightArr; input = inputArr; }
        public double Addition()
        {
            double sum = 0;
            for (int i = 0; i < input.Length && i < weight.Length; i++)
            {
                sum += weight[i] * input[i];
            }
            return sum;
        }
    }

    class NeuralNetwork
    {
        Neuron neuron1;
        Neuron neuron2;

        public NeuralNetwork(){}
        public void Learn(double[] weightArr1, double[] weightArr2, List<int[]> onesList, List<int[]> twosList)
        {
            double result1;
            double result2;
            double LAMBDA = 0.03;
            int EPOCH = 40;
            for (int i = 0; i < EPOCH; i++)
            {
                foreach (int[] input in onesList)
                {
                    neuron1 = new Neuron(weightArr1, input);
                    neuron2 = new Neuron(weightArr2, input);
                    result1 = neuron1.Addition();
                    result2 = neuron2.Addition();
                    if (result1 < result2)
                    {
                        for (int j = 0; j < weightArr2.Length; j++)
                        {
                            double w2 = weightArr2[j];
                            double x = input[j];
                            w2 = w2 - (LAMBDA * x);
                            weightArr2[j] = w2;
                            double w1 = weightArr1[j];
                            w1 = w1 + (LAMBDA * x);
                            weightArr1[j] = w1;
                        }
                    }
                }
                foreach (int[] input in twosList)
                {
                    neuron1 = new Neuron(weightArr1, input);
                    neuron2 = new Neuron(weightArr2, input);
                    result1 = neuron1.Addition();
                    result2 = neuron2.Addition();
                    if (result1 > result2)
                    {
                        for (int j = 0; j < weightArr2.Length; j++)
                        {
                            double w1 = weightArr1[j];
                            double x = input[j];
                            w1 = w1 - (LAMBDA * x);
                            weightArr1[j] = w1;
                            double w2 = weightArr2[j];
                            w2 = w2 + (LAMBDA * x);
                            weightArr2[j] = w2;
                        }
                    }
                }
            }    
        }

        public void FindResult(double[] weightArr1, double[] weightArr2, List<int[]> onesList, List<int[]> twosList) 
        {
            double[] onesResult = new double[onesList.Count];
            double[] twosResult = new double[twosList.Count];
            int correct_guesses = 0;
            double success_rate;
            int index = 0;

            Console.WriteLine("Results for ones:");
            foreach (int[] input in onesList)
            {
                neuron1 = new Neuron(weightArr1, input);
                neuron2 = new Neuron(weightArr2, input);
                double result1 = neuron1.Addition();
                double result2 = neuron2.Addition();
                if (result1 > result2) { onesResult[index] = 1; correct_guesses++; }
                else { onesResult[index] = 2; }
                Console.Write(onesResult[index]+" ");
                index++;
            }
            success_rate = (double) correct_guesses / (double) onesList.Count * 100;
            Console.WriteLine("Success rate is: %"+success_rate);

            correct_guesses = 0;
            Console.WriteLine();
                index = 0;
            Console.WriteLine("Results for twos:");
            foreach (int[] input in twosList)
            {

                neuron1 = new Neuron(weightArr1, input);
                neuron2 = new Neuron(weightArr2, input);
                double result1 = neuron1.Addition();
                double result2 = neuron2.Addition();
                if (result1 < result2) { twosResult[index] = 2; correct_guesses++;  }
                else { twosResult[index] = 1; }
                Console.Write(twosResult[index]+" ");

                index++;
            }
            success_rate = (double) correct_guesses / (double) twosList.Count * 100;
            Console.WriteLine("Success rate is: %" + success_rate);

        }
        public void FindResult(double[] weightArr1, double[] weightArr2, int[] input)
        {
            int result;

            Console.WriteLine("Result:");
                neuron1 = new Neuron(weightArr1, input);
                neuron2 = new Neuron(weightArr2, input);
                double result1 = neuron1.Addition();
                double result2 = neuron2.Addition();
                if (result1 > result2) { result = 1; }
                else { result = 2; }
                Console.WriteLine(result + " ");
        }

    }

}
