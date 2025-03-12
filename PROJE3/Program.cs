using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace PROJE3
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            // DOSYANIN AÇILMASI
            string path = "bilgi.txt";
            StreamReader sr = new StreamReader(path);

            // 1) EgeDeniziB_Ağacı
            // -----------------------------------------------
            string ad = sr.ReadLine();
            List<EgeDeniziB> baliklar_liste = new List<EgeDeniziB>();    // işlemleri kolaylaştırmak için balıkları depolayan bir generic list
            BST<EgeDeniziB> Baliklar = new BST<EgeDeniziB>();            // EgeDeniziB biçiminde nesneleri depolayan BinarySearchTree nesnesi
            List<BST<string>> Dengeli_Agaclar = new List<BST<string>>(); //dengeli ağaçları depolayacak liste
            while (ad != null)
            {
                string bilgi = sr.ReadLine();
                BST<string> Kelimeler = new BST<string>(); // her balık için bilgi kelimelerini depolayan BST
                Kelimeler = TextToBST(bilgi);              // bilgi satırının kelimeler BST'sine dönüştürülmesi
                EgeDeniziB balik = new EgeDeniziB(ad, Kelimeler);
                Baliklar.Insert(balik);                    // balık objesinin ada göre sıralanarak Balıklar BST'sine eklenmesi
                baliklar_liste.Add(balik);

                string[] kelime_sirali_dizi = Kelimeler.ToSortedArray(Kelimeler.GetRoot(), new List<string>()).ToArray(); // DENGELİ KELİME AĞACI
                BST<string> Dengeli_Kelimeler = new BST<string>();                                                        //-
                Dengeli_Kelimeler.ConstructBalanced(kelime_sirali_dizi, 0, kelime_sirali_dizi.Length - 1);                //-
                Dengeli_Agaclar.Add(Dengeli_Kelimeler);                                                                   //---------------------

                ad = sr.ReadLine();
            }

            // 1.a EgeDeniziB Sıralı Olarak Ekrana Listelenmesi

            Console.WriteLine("1.a Sıralı olarak listeleniyor");
            Console.WriteLine("------------------------------");
            Baliklar.InOrder(Baliklar.GetRoot());

            // 1.b Derinlik Bulma

            Console.WriteLine("\n\n1.b Derinlik ve düğüm sayısı bulma (DERİNLİK:DÜĞÜMSAYISI)");
            int total_depth = 0;
            foreach (EgeDeniziB item in baliklar_liste)
            {
                int depth = item.Kelimeler.FindDepth(item.Kelimeler.GetRoot());
                Console.Write($"{depth}:{item.Kelimeler.node_count} ");
                total_depth += depth;
            }
            Console.WriteLine("\nOrtalama derinlik: " + total_depth / baliklar_liste.Count); // ortalama derinliği hesaplamak

            
            // Dengeli kelimeler ağacı derinliği bulma

            Console.WriteLine("\nDengeli ağaçların derinliği yazdırılıyor: ");
            foreach (BST<string> dengeli in Dengeli_Agaclar)
            {
                int depth = dengeli.FindDepth(dengeli.GetRoot());
                Console.Write(depth + ", ");
            }

            // 1.c Arama Listeleme

            Console.WriteLine("\n\n1.c Balık isimleri arasında aramak istediğiniz baş harfleri girin");

            Console.Write("ilk harf: ");
            string first = Console.ReadLine();
            Console.Write("son harf: ");
            string last = Console.ReadLine();

            Console.WriteLine($"{first} ve {last} arasındaki balık adları yazdırılıyor: ");
            Baliklar.InBetween(Baliklar.GetRoot(), first, last);

            // 1.d Özyineli olarak Dengeli Bir Ağaç Oluşturulması

            EgeDeniziB[] balik_sirali_dizi = new EgeDeniziB[Baliklar.node_count];
            balik_sirali_dizi = Baliklar.ToSortedArray(Baliklar.GetRoot(), new List<EgeDeniziB>()).ToArray();

            Console.WriteLine("\n1.d dengeli ağaç yazdırma");
            Console.WriteLine("-------------------------");
            BST<EgeDeniziB> Balanced = new BST<EgeDeniziB>();
            Balanced.ConstructBalanced(balik_sirali_dizi, 0, balik_sirali_dizi.Length - 1);
            int balanced_depth = Balanced.FindDepth(Balanced.GetRoot());
            Console.WriteLine("Dengeli derinlik: " + balanced_depth);

            // 2) HASHTABLE OLUŞTURULMASI
            // -----------------------------------------------
            Hashtable myTable = new Hashtable();

            foreach (EgeDeniziB item in baliklar_liste)
            {
                myTable.Add(item.Balik_Adi, item.Kelimeler); // 2.a HashTable'a balık elemanlarını eklemek
            }

            Console.WriteLine("\nHashTable Yazdırılıyor:");
            Console.WriteLine("-----------------------");
            foreach (string key in myTable.Keys)
            {
                Console.WriteLine(string.Format("{0}: {1}", key, myTable[key]));
            }

            // yeni bilgi girdilerinin alınması

            Console.WriteLine("Balık adı giriniz: ");
            string yeni_ad = Console.ReadLine();
            Console.WriteLine("Yeni paragrafı giriniz: ");
            string yeni_bilgi = Console.ReadLine();
            myTable[yeni_ad] = TextToBST(yeni_bilgi); // 2.b bilginin güncellenmesi

            BST<string> yeni_kelimeler = myTable[yeni_ad] as BST<string>;
            Console.WriteLine(yeni_ad + ":");
            yeni_kelimeler.InOrder(yeni_kelimeler.GetRoot());

            // 3) HEAP OBJESİNİN OLUŞTURULMASI
            // -----------------------------------------------
            Heap balik_heap = new Heap(baliklar_liste.Count);
            // 3.b heap'e eleman eklemek
            foreach (EgeDeniziB item in baliklar_liste)
            {   
                balik_heap.Insert(item);
            }


            // 3.c 3 elemanın çıkarılması

            Console.WriteLine("\n3.c 3 eleman çıkarılıyor");
            Console.WriteLine("------------------------");
            for (int i = 0; i < 3; i++)
            {
                EgeDeniziB balik = balik_heap.Remove();
                Console.WriteLine(balik.Balik_Adi);
                //balik.Kelimeler.InOrder(balik.Kelimeler.GetRoot());
                Console.WriteLine();
            }

            // 4) SORTING KARŞILAŞTIRMA
            // -----------------------------------------------
            // 4.c

            Random rn = new Random();
            int[] sayilar = new int[100];

            for (int i = 0; i < sayilar.Length; i++)
            {
                sayilar[i] = rn.Next();
            }

            var watch = Stopwatch.StartNew(); // zamanın başlatılması

            for (int i = 0; i < 1000000; i++)
            {
                QuickSort((int[])sayilar.Clone(), 0, sayilar.Length - 1);
            }

            watch.Stop();
            var elapsedMs = watch.Elapsed.TotalMinutes;
            Console.WriteLine("\nGeçen zaman QUICK: " + elapsedMs);

            watch = Stopwatch.StartNew();

            for (int i = 0; i < 1000000; i++)
            {
                BubbleSort((int[])sayilar.Clone(), sayilar.Length);
            }

            watch.Stop();
            elapsedMs = watch.Elapsed.TotalMinutes;
            Console.WriteLine("\nGeçen zaman BUBBLE: " + elapsedMs);





            Console.ReadLine();
        }

        public static BST<string> TextToBST(string line)
        {
            BST<string> Kelimeler = new BST<string>();
            string[] bilgi = line.Split(' ');
            foreach (string kelime in bilgi) { Kelimeler.Insert(kelime); }
            return Kelimeler;
        }

        public static void QuickSort(int[] arr, int left, int right)
        {
            int index = Partition(arr, left, right);
            if (left < index - 1)
                QuickSort(arr, left, index - 1);
            if (index < right)
                QuickSort(arr, index, right);
        }
        static int Partition(int[] arr, int left, int right)
        {
            int i = left, j = right;
            int tmp;
            int pivot = arr[(left + right) / 2];
            while (i <= j)
            {
                while (arr[i] < pivot) i++;
                while (arr[j] > pivot) j--;
                if (i <= j)
                {
                    tmp = arr[i];
                    arr[i] = arr[j]; //switch
                    arr[j] = tmp;
                    i++; j--;
                }
            };
            return i;
        }


        static void BubbleSort(int[] arr, int n)
        {
            int i, j, temp;
            bool swapped;
            for (i = 0; i < n - 1; i++)
            {
                swapped = false;
                for (j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (swapped == false)
                    break;
            }
        }
    }

    class EgeDeniziB
    {
        public string Balik_Adi;
        public BST<string> Kelimeler;
        public EgeDeniziB(string Balik_Adi, BST<string> Kelimeler)
        {
            this.Balik_Adi = Balik_Adi;
            this.Kelimeler = Kelimeler;
        }

    }

    class TreeNode<T>
    {
        public T data;
        public TreeNode<T> leftChild;
        public TreeNode<T> rightChild;

        public TreeNode() {}
        public TreeNode(T key) { data = key; }


        public void DisplayNode()
        {
            if (data is string) Console.Write(" " + data + " ");
            else if (data is EgeDeniziB data_balik)
            {
                Console.WriteLine(data_balik.Balik_Adi);
                data_balik.Kelimeler.InOrder(data_balik.Kelimeler.GetRoot());
            }
        }
    }

    class BST<T>
    {
        TreeNode<T> root;
        int depth;
        public int node_count;

        public BST() { root = null; depth = -1; node_count = 0; }

        public TreeNode<T> GetRoot() {  return root; }

        public void Insert(T item)
        {
            TreeNode<T> newNode = new TreeNode<T>();
            newNode.data = item;
            if (root == null) root = newNode;

            else
            {
                TreeNode<T> current = root;
                TreeNode<T> parent;
                while (true)
                {
                    parent = current;
                    int comparison = 0;
                    if (item is string)
                    {
                        comparison = String.Compare(item as string, current.data as string);
                    }
                    else if (item is EgeDeniziB item_balik)
                    {
                        EgeDeniziB data_balik = current.data as EgeDeniziB;
                        comparison = String.Compare(item_balik.Balik_Adi, data_balik.Balik_Adi);
                    }
                    else if (item is int item_int) { item_int.CompareTo(current.data); }
                    

                    if (comparison < 0)
                    {
                        current = current.leftChild;
                        if (current == null)
                        {
                            parent.leftChild = newNode;
                            node_count++;
                            return;
                        }
                    }
                    else if (comparison > 0)
                    {
                        current = current.rightChild;
                        if (current == null)
                        {
                            parent.rightChild = newNode;
                            node_count++;
                            return;
                        }
                    }
                    else return;
                }
            }
        }

        public void InOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                InOrder(node.leftChild);
                node.DisplayNode();
                InOrder(node.rightChild);
            }
        }

        public void PreOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                node.DisplayNode();
                PreOrder(node.leftChild);
                PreOrder(node.rightChild);
            }
        }

        public void PostOrder(TreeNode<T> node)
        {
            if (node != null)
            {
                PostOrder(node.leftChild);
                PostOrder(node.rightChild);
                node.DisplayNode();
            }
        }

        public List<T> ToSortedArray(TreeNode<T> node, List<T> arr)
        {
            if (node != null)
            {
                ToSortedArray(node.leftChild, arr);
                arr.Add(node.data);
                ToSortedArray(node.rightChild, arr);
            }
            return arr;
        }

        public int FindDepth(TreeNode<T> root)
        {
            if (root == null) return -1;
            int leftDepth = FindDepth(root.leftChild);
            int RightDepth = FindDepth(root.rightChild);
            depth = Math.Max(leftDepth, RightDepth) + 1;
            return depth;
        }

        public void ConstructBalanced(T[] SortedArr, int left, int right)
        {
            if (left < right)
            {
                int i = left, j = right;
                int pivot = i + (j - i) / 2;
                Insert(SortedArr[pivot]);
                i = left; j = pivot - 1;
                ConstructBalanced(SortedArr, i, j);
                i = pivot + 1; j = right;
                ConstructBalanced(SortedArr, i, j);
            }
        }

        public void InBetween(TreeNode<T> node, string first, string last) 
        {
            if (node != null && node.data is EgeDeniziB data )
            {
                InBetween(node.leftChild, first, last);
                if (String.Compare(first, data.Balik_Adi.Substring(0,1), true) <= 0 &&
                    String.Compare(last, data.Balik_Adi.Substring(0,1), true) >= 0) Console.WriteLine(data.Balik_Adi);
                InBetween(node.rightChild, first, last);
            }
        }

    }


    class Heap
    {
        private List<EgeDeniziB> heap;

        public Heap(int mx)
        {
            heap = new List<EgeDeniziB>();
        }

        // Returns the number of elements in the heap
        public int Count => heap.Count;

        // Inserts a new element into the heap
        public void Insert(EgeDeniziB value)
        {
            heap.Add(value);
            TrickleUp(heap.Count - 1);
        }

        // Removes and returns the maximum element from the heap
        public EgeDeniziB Remove()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            EgeDeniziB max = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
                TrickleDown(0);

            return max;
        }


        // Maintains the max-heap property after insertion
        private void TrickleUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (String.Compare(heap[index].Balik_Adi, heap[parentIndex].Balik_Adi) <= 0)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        // Maintains the max-heap property after extraction
        private void TrickleDown(int index)
        {
            while (true)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int largest = index;

                if (leftChildIndex < heap.Count && String.Compare(heap[leftChildIndex].Balik_Adi, heap[largest].Balik_Adi) > 0)
                    largest = leftChildIndex;

                if (rightChildIndex < heap.Count && String.Compare(heap[rightChildIndex].Balik_Adi, heap[largest].Balik_Adi) > 0)
                    largest = rightChildIndex;

                if (largest == index)
                    break;

                Swap(index, largest);
                index = largest;
            }
        }

        // Swaps two elements in the heap
        private void Swap(int i, int j)
        {
            EgeDeniziB temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }

        // Displays the heap structure
        public void Display()
        {
            Console.WriteLine(string.Join(", ", heap));
        }
    }
    }






