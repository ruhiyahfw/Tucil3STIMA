using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace tucil3_0404
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static int JmlSimpul(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(@path);
            //baca jumlah simpul
            string baris = lines[0];
            return  Convert.ToInt32(baris);
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    

    public class coordinate
    {
        private double longitude;
        private double latitude;

        public coordinate()
        {
            this.longitude = 0;
            this.latitude = 0;
        }

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
                coordinate c = new coordinate(lo, la);
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

        //public void addEdge(int u, int v)
        //{
        //    this._adj[u].AddLast(v);
        //    this._adj[v].AddLast(u);
        //}

        public List<KeyValuePair<string, coordinate>> getAllSimpul()
        {
            return this.simpul;
        }

        public coordinate getCoordinate(string s)
        {
            coordinate result = new coordinate();
            foreach(var a in this.simpul)
            {
                if(Equals(a.Key, s))
                {
                    result = a.Value;
                    return result;
                }
            }
            return null;
        }

        public string getNamaCoordinate(coordinate c)
        {
            string result = "";
            foreach (var a in this.simpul)
            {
                if (Equals(a.Value, c))
                {
                    result = a.Key;
                    return result;
                }
            }
            return null;
        }


    }

    public class astarsearch
    {
        public KeyValuePair<int, coordinate> getMinfromQueue(List<KeyValuePair<int, coordinate>> queue)
        {
            KeyValuePair<int, coordinate> result = new KeyValuePair<int, coordinate>(99999999, new coordinate());
            foreach(var a in queue)
            {
                if(a.Key < result.Key)
                {
                    result = a;
                }
            }
            return result;
        }

        public double HeuristicDistance(coordinate a, coordinate b)
        {
            double result = Math.Sqrt(Math.Pow((a.getLat() - b.getLat()), 2) + Math.Pow((a.getLong() - b.getLong()), 2));
            return result;
        }

        public void astar(string asal, string tujuan, graf g)
        {
            //cari koordinat dari simpul asal
            coordinate coorAsal = g.getCoordinate(asal);
            //cari koordinat dari
            coordinate coorTujuan = g.getCoordinate(tujuan);

            //untuk menyimpan hasil
            List<KeyValuePair<string, coordinate>> hasil = new List<KeyValuePair<string, coordinate>>();

            //untuk menyimpan nilai heuristik dan coordinat simpul
            List<KeyValuePair<int, coordinate>> queue = new List<KeyValuePair<int, coordinate>>();
            HashSet<coordinate> visited = new HashSet<coordinate>();
            //HashMap<coordinate, coordinate> parent = new HashMap<coordinate, coordinate>();

            int JarakNow = 0; //g(n)
            //dicari semua simpul yang bertetangga dengan simpul asal
            //lalu dicari nilai heuristik untuk tiap simpulnya
            //masukin ke dalam queue
            //dari queue dicari nilai heuristik yang paling kecil
            //pilih simpul itu lalu keluarkan dari queue



        }
    }
}
