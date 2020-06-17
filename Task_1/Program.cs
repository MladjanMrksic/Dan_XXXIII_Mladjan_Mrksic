using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;


namespace Task_1
{
    class Program
    {
        string matrixPath = ".../.../FileByThread_1.txt";
        string randomOddNumbersPath = ".../.../FileByThread_22.txt";
        static void Main(string[] args)
        {
            Program prog = new Program();
            List<Thread> threadList = new List<Thread>();
            Thread t;
            for (int i = 1; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    if (i == 2)
                    {
                        t = new Thread(prog.RandomNumbersToFile);
                        t.Name = string.Format("Thread_" + i + i);
                        Console.WriteLine(t.Name + " created.");
                        threadList.Add(t);
                    }
                    else
                    {
                        t = new Thread(prog.AddingRandomNumbersFromFile);
                        t.Name = string.Format("Thread_" + i + i);
                        Console.WriteLine(t.Name + " created.");
                        threadList.Add(t);
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        t = new Thread(prog.MatrixToFile);                        
                        t.Name = string.Format("Thread_" + i);
                        Console.WriteLine(t.Name + " created.");
                        threadList.Add(t);
                    }
                    else
                    {
                        t = new Thread(prog.ReadMatrixFromFile);                        
                        t.Name = string.Format("Thread_" + i);
                        Console.WriteLine(t.Name + " created.");
                        threadList.Add(t);
                    }
                }
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            threadList[0].Start();
            threadList[1].Start();
            threadList[0].Join();
            threadList[1].Join();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
            Console.WriteLine("Thread_1 and Thread_22 run time was " + elapsedTime);
            threadList[2].Start();
            threadList[3].Start();
            Console.ReadLine();
        }
        public void MatrixToFile()
        {
            StreamWriter sw = new StreamWriter(matrixPath);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                sb.Clear();
                for (int j = 0; j < 100; j++)
                {                    
                    if (i==j)
                    {
                        sb.Append(1);
                    }
                    else
                    {
                        sb.Append(0);
                    }                   
                }
                sw.WriteLine(sb);                
            }
            sw.Close();
        }
        public void RandomNumbersToFile()
        {
            StreamWriter sw2 = new StreamWriter(randomOddNumbersPath);
            Random rnd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int temp = rnd.Next(0, 10000);
                if (temp % 2 == 1)
                {
                    sw2.WriteLine(temp);
                }
                else --i;
            }
            sw2.Close();
        }
        public void ReadMatrixFromFile()
        {
            
            StreamReader sr = new StreamReader(matrixPath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            sr.Close();
        }         
        public void AddingRandomNumbersFromFile()
        {           
            StreamReader sr2 = new StreamReader(randomOddNumbersPath);
            string line;
            int temp = 0;
            while ((line = sr2.ReadLine()) != null)
            {
                temp += Convert.ToInt32(line);
            }
            Console.WriteLine(temp);
            sr2.Close();
        }
    }
}
