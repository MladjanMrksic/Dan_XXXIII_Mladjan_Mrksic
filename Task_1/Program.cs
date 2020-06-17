using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();
            Thread t1 = new Thread(prog.MatrixToFile);
            Thread t2 = new Thread(prog.RandomNumbersToFile);
            Thread t3 = new Thread(prog.ReadMatrixFromFile);
            Thread t4 = new Thread(prog.AddingRandomNumbersFromFile);
            List<Thread> threadList = new List<Thread>() { t1, t2, t3, t4 };
            for (int i = 1; i < 5; i++)
            {
                if (i % 2 == 0)
                {
                    threadList[i - 1].Name = string.Format("Thread_" + i + i);
                    Console.WriteLine(threadList[i - 1].Name + " created.");
                }
                else
                {
                    threadList[i - 1].Name = string.Format("Thread_" + i);
                    Console.WriteLine(threadList[i - 1].Name + " created.");
                }
            }
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            t1.Start();
            t2.Start();
            t2.Join();
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Thread_1 and Thread_22 run time was " + elapsedTime); 
            t3.Start();            
            t4.Start();

            Console.ReadLine();

        }
        public void MatrixToFile()
        {
            StreamWriter sw = new StreamWriter(".../.../FileByThread_1.txt");
            int[,] matrix = new int[100, 100];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                sb.Clear();
                for (int j = 0; j < 100; j++)
                {                    
                    if (i==j)
                    {
                        matrix[i, j] = 1;
                        sb.Append(1);
                    }
                    else
                    {
                        matrix[i, j] = 0;
                        sb.Append(0);
                    }                   
                }
                sw.WriteLine(sb);                
            }
            sw.Close();
        }

        public void RandomNumbersToFile()
        {
            StreamWriter sw2 = new StreamWriter(".../.../FileByThread_22.txt");
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
            
            StreamReader sr = new StreamReader(".../.../FileByThread_1.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            sr.Close();
        }
         
        public void AddingRandomNumbersFromFile()
        {
           
            StreamReader sr2 = new StreamReader(".../.../FileByThread_22.txt");
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
