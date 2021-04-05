using System;
using System.Collections.Generic;
using System.IO;

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
            string currentDir = Environment.CurrentDirectory.ToString();
            Console.WriteLine(currentDir);
            DirectoryInfo d = new DirectoryInfo(currentDir);
            //string parentDir = d.Parent.Parent.Parent.Parent.Parent.ToString();
            string parent = System.IO.Directory.GetParent(currentDir).FullName;
            string parentDir = System.IO.Directory.GetParent(parent).FullName;
            string dir = System.IO.Directory.GetParent(parentDir).FullName;
            //Console.WriteLine(dir);
            var path = Path.GetFullPath(Path.Combine(dir, @"test", "text1.txt"));
            //string path = "C:/Users/farad/source/repos/BuramSTIMA3/BuramSTIMA3/text1.txt";
            int N = JmlSimpul(path);
            Console.WriteLine(N);
            graf grafmap = new graf(N);
            grafmap.CreateGraf(path);
            grafmap.showGraf();
        }
    }
}
