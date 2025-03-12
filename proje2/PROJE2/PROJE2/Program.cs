using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;


namespace PROJE2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*PROJE VISUAL STUDIO 2022 KULLANILARAK AÇILMALIDIR. BAZI COMPILER'LARIN TXT DOSYASINI
             OKUMADA HATA VERDİĞİ GÖRÜLMÜŞTÜR; 1024 KARAKTERİ AŞAN SATIRLARDA AYNI SATIRI 2 SATIR OLARAK BÖLMEKTEDİR*/ 

            //KONSOL RENKLERİ AYARLANDI
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            CultureInfo turkce = new CultureInfo("tr-TR");

            //GENERIC LIST VE ARRAY OLUŞTURULMASI
            int BALIK_SAYISI = 38;
            int GRUP_SAYISI = 4;
            int GRUPTA_BALIK_SAYISI = 10;
            List<EgeDeniziB> Baliklar = new List<EgeDeniziB>(BALIK_SAYISI); //1.b
            List<EgeDeniziB>[] Baliklar_Dizi = new List<EgeDeniziB>[GRUP_SAYISI]; //1.c

            //DOSYANIN AÇILMASI
            string path = "bilgi.txt";
            StreamReader sr = new StreamReader(path);

            //1.b
            for (int i = 0; i < GRUP_SAYISI; i++)
            {

                for (int j = 0; j < GRUPTA_BALIK_SAYISI; j++)
                {
                    string ad = sr.ReadLine();
                    if (ad == null) { Baliklar.Add(null); continue; }
                    string diger_ad = sr.ReadLine();
                    string boyut = sr.ReadLine();
                    string bilgi = sr.ReadLine();
                    string ortam = sr.ReadLine();
                    string[] ortam_arr = ortam.Split(',');
                    EgeDeniziB balik = new EgeDeniziB(ad, diger_ad, boyut, bilgi, ortam_arr); //1.b balık nesnesi oluşturmak

                    Baliklar.Add(balik); //1.b balıkları generic list'e eklemek
                }

                Baliklar_Dizi[i] = Baliklar.GetRange(i*GRUPTA_BALIK_SAYISI, GRUPTA_BALIK_SAYISI); //1.c
            }
            sr.Close();


            //2) STACK VE QUEUE NESNELERİNİN OLUŞTURULMASI
            Stack balik_stack = new Stack(Baliklar.Count);
            Queue balik_queue = new Queue(Baliklar.Count);
            //3) PRIORITY QUEUE NESNESİNİN OLUŞTURULMASI
            PriorityQueue balik_pq = new PriorityQueue();

            //1.d DİZİDEKİ ELEMANLARIN BİLGİLERİNİN İNDİSLERİYLE BERABER YAZDIRILMASI
            int index = 0; //grup indisi
            foreach (var item in Baliklar_Dizi)
            {
                int c = 0; //her grup içindeki diğer adı olan balık sayısını tutacak
                Console.WriteLine("Group index:" + index); //grup indisinin yazdırımak
                foreach (var balik in item)
                {
                    if (balik != null)
                    {
                        Console.WriteLine("ad");
                        Console.WriteLine(balik.Balik_Adi);
                        Console.WriteLine("diger ad");
                        Console.WriteLine(balik.Diger_Adi);
                        Console.WriteLine("boyut");
                        Console.WriteLine(balik.Boyut);
                        Console.WriteLine("bilgi");
                        Console.WriteLine(balik.Bilgi);
                        Console.WriteLine("ortam");
                        balik.PrintOrtam();

                        //2) balık nesenelerinin stack ve queue nesnelerine konması
                        balik_stack.Push(balik);
                        balik_queue.Enque(balik);
                        balik_pq.Enque(balik);
                        if (!balik.Diger_Adi.Equals("?")) c++;
                    }
                }
                Console.WriteLine(index + ". GRUPTAKİ DİĞER ADI OLAN BALIK SAYISI: " + c);
                index++;
            }

            //2.a STACK YAPISINDAN BALIK NESNELERİNİ ÇIKARMA
            Console.WriteLine("Stack:");
            while (!balik_stack.isEmpty())
            {
                EgeDeniziB balik = balik_stack.Pop();
                if (balik != null)
                {
                    Console.WriteLine("ad");
                    Console.WriteLine(balik.Balik_Adi);
                    Console.WriteLine("diger ad");
                    Console.WriteLine(balik.Diger_Adi);
                    Console.WriteLine("boyut");
                    Console.WriteLine(balik.Boyut);
                    Console.WriteLine("bilgi");
                    Console.WriteLine(balik.Bilgi);
                    Console.WriteLine("ortam");
                    balik.PrintOrtam();
                }
            }

            //2.b QUEUE YAPISINDAN BALIK NESNELERİNİ ÇIKARMA
            Console.WriteLine("Queue:");
            while (!balik_queue.IsEmpty()) 
            {
                EgeDeniziB balik = balik_queue.Deque();
                if (balik != null)
                {
                    Console.WriteLine("ad");
                    Console.WriteLine(balik.Balik_Adi);
                    Console.WriteLine("diger ad");
                    Console.WriteLine(balik.Diger_Adi);
                    Console.WriteLine("boyut");
                    Console.WriteLine(balik.Boyut);
                    Console.WriteLine("bilgi");
                    Console.WriteLine(balik.Bilgi);
                    Console.WriteLine("ortam");
                    balik.PrintOrtam();
                }
            }

            //3) PRIORITY QUEUE YAPISINDAN BALIK NESNELERİNİ ÇIKARMAK
            Console.WriteLine("Priority Queue:");
            while (!balik_pq.IsEmpty())
            {
                EgeDeniziB balik = balik_pq.Deque();
                if (balik != null)
                {
                    Console.WriteLine("ad");
                    Console.WriteLine(balik.Balik_Adi);
                    Console.WriteLine("diger ad");
                    Console.WriteLine(balik.Diger_Adi);
                    Console.WriteLine("boyut");
                    Console.WriteLine(balik.Boyut);
                    Console.WriteLine("bilgi");
                    Console.WriteLine(balik.Bilgi);
                    Console.WriteLine("ortam");
                    balik.PrintOrtam();
                }
            }

            //4.a) MARKET İÇİN KUYRUK YAPISININ OLUŞTURULMASI
            MarketQueue market = new MarketQueue(13);
            // market.Deque(); //underflow test
            market.Enque(15, 1, 12, 8, 7, 4, 21, 3, 2, 6, 5, 9, 11);

            double sum = 0; // bütün müşterilerin bekleme sürelerinin toplamını tutacak
            double customer_time; // bireysel müşterinin bekleme süresini tutacak (ondan önce geleni beklemesi hesaba katılarak)
            double queue_avg; // tüm kuyruk boyunca ortalama işlem tamamlama süresi
            double pq_avg;

            Console.WriteLine("QUEUE SONUÇLAR(MARKET):");
            for (int i = 1; i <= market.elementCount; i++)
            {
                customer_time = market.FindTime(i); //FindTime metodu queue yapısındaki herhangi bir sıradaki elemanın bekleme süresini hesaplar
                Console.WriteLine(customer_time);
                //sonraki iki kod satırı FindTime metodu işlem yaparken eleman çıkarma yaptığından dolayı sırayı orijinal haline döndürür
                market.Clear();
                market.Enque(15, 1, 12, 8, 7, 4, 21, 3, 2, 6, 5, 9, 11);
                sum += customer_time;
            }
            queue_avg = sum / market.elementCount;
            Console.WriteLine("AVG:" + queue_avg); //ortalamayı yazdırmak

            //4.b MARKET İÇİN PRIORITY QUEUE YAPISININ OLUŞTURULMASI
            PriorityQueueMarket pq_market = new PriorityQueueMarket();
            // pq_market.Deque(); //underflow test
            pq_market.Enque(15, 1, 12, 8, 7, 4, 21, 3, 2, 6, 5, 9, 11);

            sum = 0;
            Console.WriteLine("PRIORITY QUEUE SONUÇLAR(MARKET):");
            for (int i = 1; i <= pq_market.pq_list.Count; i++)
            {
                customer_time = pq_market.FindTime(i);
                Console.WriteLine(customer_time);
                pq_market.Clear();
                pq_market.Enque(15, 1, 12, 8, 7, 4, 21, 3, 2, 6, 5, 9, 11);
                sum += customer_time;
            }
            pq_avg = sum / pq_market.elementCount;
            Console.WriteLine("AVG:" + pq_avg);

            //4.c
            if (pq_avg < queue_avg)
            {
                Console.WriteLine("Priority queue ortalama süre açısından daha verimli");
            }
            else if (pq_avg == queue_avg)
            {
                Console.WriteLine("Priority queue ve queue'dan aynı ortalama işlem süresi sonucu alındı");
            }
            else
                Console.WriteLine("Queue ortalama süre açısından daha verimli");






            Console.ReadLine();


        }
    }

    class EgeDeniziB
    {
        public string Balik_Adi;
        public string Diger_Adi;
        public string Boyut;
        public string Bilgi;
        public string[] ortam;
        public EgeDeniziB(string Balik_Adi, string Diger_Adi, string Boyut, string Bilgi, string[] Ortam)
        {
            this.Balik_Adi = Balik_Adi;
            this.Diger_Adi = Diger_Adi;
            this.Boyut = Boyut;
            this.Bilgi = Bilgi;
            this.ortam = Ortam;
        }

        public void PrintOrtam()
        {
            for (int i = 0; i < ortam.Length; i++)
            {
                Console.Write(ortam[i] + ",");
            }
            Console.WriteLine();
        }

        
    }

    class Stack
    {
        EgeDeniziB[] stack_arr;
        int top;
        int maxSize;
        public Stack(int max) 
        {
            maxSize = max;
            stack_arr = new EgeDeniziB[maxSize];
            top = -1;
        }

        public EgeDeniziB Pop() 
        {
            if (top != -1) { return stack_arr[top--]; }
            else return null;
        }

        public void Push(EgeDeniziB item) 
        {
            if (top != maxSize) { stack_arr[++top] = item; }
            else { Console.WriteLine("STACK OVERFLOW"); }
        }

        public bool isEmpty() { return top == -1; }
    }

    class Queue
    {
        public int maxSize;
        public EgeDeniziB[] queue_arr;
        public int head, tail;
        public int elementCount;
        public Queue(int max)
        {
            maxSize = max;
            queue_arr = new EgeDeniziB[maxSize];
            head = 0; tail = -1; elementCount = 0;
        }
        public void Enque(EgeDeniziB item)
        {
            if (tail == maxSize - 1 && head == 0 && !IsEmpty())
            {
                Console.WriteLine("There was an attempt to enque an item to a queue with no space left: enque failed");
                return;
            }
            else if (tail == maxSize - 1)
                tail = -1;

            queue_arr[++tail] = item; 
            elementCount++;
        }

        public EgeDeniziB Deque()
        {
            EgeDeniziB temp = queue_arr[head++];
            if (temp == null) 
            {
                Console.WriteLine("There was an attempt to deque an item that doesn't exist: returning to spot");
                head--; return null;
            }
            if (head == maxSize)
                head = 0;
            elementCount--;
            return temp;
        }
        public bool IsEmpty() 
        {
            return (elementCount == 0);
        }
    }

    class PriorityQueue
    {
        public int maxSize;
        public List<EgeDeniziB> pq_list;
        public CultureInfo turkce = new CultureInfo("tr-TR");
        public PriorityQueue() { pq_list = new List<EgeDeniziB>(); }

        public void Enque(EgeDeniziB item)
        {
            pq_list.Add(item);
        }

        public EgeDeniziB Deque()
        {
            string comparer = pq_list[0].Balik_Adi;
            int index = 0;
            for (int i = 0; i < pq_list.Count; i++)
            {
                int comparison = String.Compare(comparer, pq_list[i].Balik_Adi, true, turkce);
                if (comparison <= 0) { continue; }
                else
                {
                    comparer = pq_list[i].Balik_Adi;
                    index = i;
                }
            }

            EgeDeniziB temp = pq_list[index];
            pq_list.RemoveAt(index);
            return temp;

        }

        public bool IsEmpty()
        {
            return pq_list.Count == 0;
        }
    }

    class PriorityQueueMarket
    {
        public List<int> pq_list;
        public int sum;
        public double ITEM_TIME =3.3;
        public int elementCount;
        public PriorityQueueMarket() { pq_list = new List<int>(); elementCount = 0; }

        public void Enque(int item)
        {
            pq_list.Add(item);
            elementCount++;
        }

        public void Enque(params int[] items)
        {
            pq_list.AddRange(items);
            elementCount += items.Length;
        }

        public int Deque()
        {
            if (pq_list.Count == 0)
            {
                Console.WriteLine("There was an attempt to deque an item that doesn't exist");
                return 0;
            }
            int comparer = pq_list[0];
            int index = 0;
            for (int i = 0; i < pq_list.Count; i++)
            {
                int item = pq_list[i];
                if (comparer < item) { continue; }
                else
                {
                    comparer = pq_list[i];
                    index = i;
                }
            }

            int temp = pq_list[index];
            sum += temp;
            pq_list.RemoveAt(index);
            elementCount--;
            return temp;

        }
        public double FindTime(int customerIndex)
        {
            for (int i = 0; i < customerIndex; i++)
            {
                Deque();
            }

            return sum*ITEM_TIME;
        }



        public bool IsEmpty()
        {
            return pq_list.Count == 0;
        }

        public double Clear()
        {
            int length = pq_list.Count;
            for (int i = 0; i < length; i++)
            {
                Deque();
            }
            double temp = sum;
            sum = 0;
            return temp;
        }
    }


    class MarketQueue
    {
        public int maxSize;
        public int[] queue_arr;
        public int head, tail;
        public int elementCount;
        public int sum;
        public double ITEM_TIME = 3.3;
        public MarketQueue(int max)
        {
            maxSize = max;
            queue_arr = new int[maxSize];
            head = 0; tail = -1; elementCount = 0;
        }
        public void Enque(int item)
        {
            if (tail == maxSize - 1 && head == 0 && IsEmpty())
            {
                Console.WriteLine("There was an attempt to enque an item to a queue with no space left: enque failed");
                return;
            }
            if (tail == maxSize - 1)
                tail = -1;

            queue_arr[++tail] = item;
            elementCount++;
        }

        public void Enque(params int[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Enque(items[i]);
            }
        }

        public int Deque()
        {
            int temp = queue_arr[head++];
            if (temp == 0)
            {
                Console.WriteLine("There was an attempt to deque an item that doesn't exist: returning to spot");
                head--; return 0;
            }
            if (head == maxSize)
                head = 0;
            elementCount--;
            sum += temp;
            return temp;
        }
        public bool IsEmpty()
        {
            return (elementCount == 0);
        }

        public double FindTime(int customerIndex) 
        {
            for (int i = 0; i < customerIndex; i++)
            {
                Deque();
            }

            return sum*ITEM_TIME;
        }


        public double Clear() 
        {
            int length = elementCount;
            for (int i = 0; i < length; i++)
            {
                Deque();
            }
            double temp = sum;;
            sum = 0; head = 0; tail = -1; elementCount = 0;
            return temp;
        }
    }

}
