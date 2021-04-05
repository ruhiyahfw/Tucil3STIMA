using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static int JmlSimpul(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(@path);
            //baca jumlah simpul
            string baris = lines[0];
            return Convert.ToInt32(baris);
        }
        static void Main(string[] args)
        {
            string path = "C:/Users/farad/source/repos/BuramSTIMA3/BuramSTIMA3/text1.txt";
            int N = JmlSimpul(path);
            Console.WriteLine(N);
            graf grafmap = new graf(N);
            grafmap.CreateGraf(path);
            grafmap.showGraf();
        }
    }
}
