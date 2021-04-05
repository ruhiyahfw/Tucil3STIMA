using System;
using System.Collections.Generic;

public class coordinate
{
    private double longitude;
    private double latitude;

    public coordinate(double lo, double la)
    {
        this.longitude = lo;
        this.latitude = la;
    }
    public double getLong()
    {
        return this.longitude;
    }
    public double getLat()
    {
        return this.latitude;
    }
    public void setLong(double lo)
    {
        this.longitude = lo;
    }
    public void setLat(double la)
    {
        this.latitude = la;
    }
}

public class graf
{
    private int JumlahSimpul;
    private List<KeyValuePair<string, coordinate>> simpul;
    private double[,] adjmat;

    //public graf()
    //{
    //    this.JumlahSimpul = ;
    //    this.simpul = new List<KeyValuePair<coordinate, string>>();
    //    this.adjmat = new double[,];
    //}

    public graf(int N)
    {
        this.JumlahSimpul = N;
        this.simpul = new List<KeyValuePair<string, coordinate>>();
        this.adjmat = new double[this.JumlahSimpul, this.JumlahSimpul];
    }

    public void CreateGraf(string path)
    {

        string[] lines = System.IO.File.ReadAllLines(@path);
        //baca koordinat dan nama setiap simpul
        int i, j;
        double lo, la;
        for (i = 1; i <= this.JumlahSimpul; i++)
        {
            string read1 = "";
            string read2 = "";
            string read3 = "";
            j = 0;
            // baca longitude
            while (lines[i][j] != ' ')
            {
                read1 = read1 + lines[i][j];
                j++;
            }
            lo = Convert.ToDouble(read1);
            // baca latitude
            j++;
            while (lines[i][j] != ' ')
            {
                read2 = read2 + lines[i][j];
                j++;
            }
            
            la = Convert.ToDouble(read2);
            coordinate c = new coordinate(lo,la);
            // baca nama node
            j++;
            while (j < lines[i].Length)
            {
                read3 = read3 + lines[i][j];
                j++;
            }
            this.simpul.Add(new KeyValuePair<string, coordinate>(read3, c));
        }
        // membaca adjacency matrix
        for (int k = 0; k < this.JumlahSimpul; k++)
        {
            j = 0;
            for (int l = 0; l < this.JumlahSimpul; l++)
            {
                string read1 = "";
                while (j < lines[i].Length && lines[i][j] != ' ')
                {
                    read1 += lines[i][j];
                    j++;
                }
                double a = Convert.ToDouble(read1);

                this.adjmat[k, l] = a;
                j++;
            }
            i++;
        }
    }

    public void showGraf()
    {
        Console.WriteLine("jumlah simpul : " + JumlahSimpul);
        Console.WriteLine("koordinat simpul: ");
        foreach (KeyValuePair<string, coordinate> line in this.simpul)
        {
            Console.Write(line.Key + " : ");
            Console.WriteLine("long: " + line.Value.getLong() + ", lat: " + line.Value.getLat());
        }
        Console.WriteLine("Adjacency matrix:");
        for (int i = 0; i < this.JumlahSimpul; i++)
        {
            for (int j = 0; j < this.JumlahSimpul; j++)
            {
                Console.Write(this.adjmat[i, j] + " ");
            }
            Console.WriteLine("");
        }
    }
}
