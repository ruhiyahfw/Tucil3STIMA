using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace tucil3_0404
{
    public static class Global
    {
        public static string PilihMap(string input)
        {
            string filename = "";
            if(Equals(input, "Peta jalan sekitar kampus ITB/Dago"))
            {
                filename = "map1.txt";
            } else if (Equals(input, "Peta jalan sekitar Alun-alun Bandung"))
            {
                filename = "map2.txt";
            }
            else if (Equals(input, "Peta jalan sekitar Buahbatu"))
            {
                filename = "map3.txt";
            }
            else if (Equals(input, "Peta kawasan sekitar Kota Padang"))
            {
                filename = "map4.txt";
            }
            else if (Equals(input, "Peta wilayah Romania"))
            {
                filename = "map5.txt";
            }
            else if (Equals(input, "Peta kawasan sekitar Kota Payakumbuh"))
            {
                filename = "map6.txt";
            }

            return filename;
        }
        public static int JmlSimpul(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(@path);
            //baca jumlah simpul
            string baris = lines[0];
            return Convert.ToInt32(baris);
        }

        public static Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        public static Microsoft.Msagl.Drawing.Graph graph;
        public static List<string> nodes;
        public static graf g;
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
        private List<KeyValuePair<string, coordinate>> simpul; //asumsinama tempat/jalan pasti berbeda
        private double[,] adjmat;

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
            //membaca adjacency matrix
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

        public void AddMSAGL(Microsoft.Msagl.Drawing.Graph graph, List<string> node)
        {
            for (int i = 0; i < this.JumlahSimpul; i++)
            {
                string a = this.simpul[i].Key;
                node.Add(a);
                for (int j = 0; j < i; j++)
                {
                    string b = this.simpul[j].Key;
                    if (this.adjmat[i,j] != 0)
                    {
                        var Edge = graph.AddEdge(a, b);
                        Edge.Attr.Color = Microsoft.Msagl.Drawing.Color.DarkBlue;
                        Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        Edge.LabelText = Convert.ToString(this.adjmat[i, j]);
                    }
                }
            }
        }

        public bool isPasangan(string a, string b, (List<string>, double) hasil)
        {
            bool found = false;
            int i = 0;
            while (!found && i < hasil.Item1.Count - 1)
            {
                if ((Equals(hasil.Item1[i], a) && Equals(hasil.Item1[i + 1], b)) || (Equals(hasil.Item1[i], b) && Equals(hasil.Item1[i + 1], a)))
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            return found;
        }

        public void AddMSAGLHasil(Microsoft.Msagl.Drawing.Graph graph, List<string> node, (List<string>, double) hasil)
        {
            for (int i = 0; i < this.JumlahSimpul; i++)
            {
                string a = this.simpul[i].Key;
                node.Add(a);
                for (int j = 0; j < i; j++)
                {
                    string b = this.simpul[j].Key;
                    if (this.adjmat[i, j] != 0)
                    {
                        // buat edge dari node
                        var Edge = graph.AddEdge(a, b);
                        Edge.Attr.ArrowheadAtSource = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        Edge.Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
                        Edge.LabelText = Convert.ToString(this.adjmat[i, j]);

                        // buat warna edge yang sesuai
                        if (isPasangan(a, b, hasil) == true)
                        {
                            Edge.Attr.Color = Microsoft.Msagl.Drawing.Color.Coral;
                            
                        }
                        else
                        {
                            Edge.Attr.Color = Microsoft.Msagl.Drawing.Color.DarkBlue;
                        }
                    }
                }
            }
        }

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

        private int getIndeksSiapa(string siapa)
        {
            int i = 0;
            foreach (var a in this.simpul)
            {
                if (a.Key == siapa)
                {
                    return i;
                }
                else
                {
                    i++;
                }
            }
            return -1;
        }

        private string getNamadiSimpulkeIdx(int idx)
        {
            int i = 0;
            foreach (var a in this.simpul)
            {
                if (i == idx)
                {
                    return a.Key;
                }
                else
                {
                    i++;
                }
            }
            return null;
        }
        public List<(string,double)> getTetangga (string awal)
        {
            // mengembalikan daftar tetangga si-awal
            List<(string,double)> hasil = new List<(string,double)>();
            int idx = this.getIndeksSiapa(awal);
            for (int j = 0; j < this.JumlahSimpul; j++)
            {
                if (this.adjmat[idx,j] > 0)
                {
                    (string,  double) a = (getNamadiSimpulkeIdx(j), this.adjmat[idx,  j]);
                    hasil.Add(a);
                }
            }
            return hasil;
        }
    }

    public class astarsearch
    {
        public (List<string>, double, double) getMinfromQueue(List<(List<string>, double, double)> queue)
        {
            (List<string>, double, double) result = (new List<string>(), 999999999, 0);
            foreach(var a in queue)
            {
                if(a.Item2 < result.Item2)
                {
                    result = a;
                }
            }
            return result;
        }

        public double HeuristicDistance(coordinate a, coordinate b) //h(n)
        {
            double result = Math.Sqrt(Math.Pow((a.getLat() - b.getLat()), 2) + Math.Pow((a.getLong() - b.getLong()), 2));
            return result;
        }

        //tetangga itu tetangga dari simpul yang paling ujung dari list string dicari
        //jaraknow itu jarak dari root ke simpul yang paling ujung dari list string dicari (cost dari rute dicari)
        //list string dicari itu rute dari root ke simpul n
        public void addQueue(graf g, List<string> dicari, List<(string, double)> tetangga, List<(List<string>, double, double)> queue, double JarakNow)
        {
            
            foreach (var node in tetangga)
            {
                // cari g(n) 
                double realJarak = JarakNow; 
                realJarak = realJarak + node.Item2;
                coordinate coorDicari = g.getCoordinate(dicari[dicari.Count - 1]);
                //cari h(n)
                double heuristik = HeuristicDistance(coorDicari, g.getCoordinate(node.Item1));
                double functionheuritik = realJarak + heuristik;
                // tambah list untuk queue
                List<string> nama = new List<string>(dicari);
                nama.Add(node.Item1);
                (List<string>, double, double) masukkan = (nama, functionheuritik, realJarak);
                //masukin ke dalam queue
                queue.Add(masukkan);
            }
        }



        public (List<string>,double jarak) astar(string asal, string tujuan, graf g)
        {
            //untuk menyimpan hasil
            List<string> hasil = new List<string>();

            //untuk menyimpan nilai heuristik dan list simpul
            List<(List<string>, double, double)> queue = new List<(List<string>, double, double)>();

            //inisialisasi g(n)
            double JarakNow = 0;

            //dicari semua simpul yang bertetangga dengan simpul asal
            List<(string,double)> tetangga = g.getTetangga(asal);

            //lalu dicari nilai heuristik untuk tiap simpulnya dan ditambahkan ke queue
            List<string> awal = new List<string>();
            awal.Add(asal);
            addQueue(g, awal, tetangga, queue, JarakNow);
            (List<string>, double jarak) keluar = (awal,0);


            bool found = false;
            while (queue.Count != 0 && !found)
            {
                //dari queue dicari nilai heuristik yang paling kecil
                (List<string>, double, double) iter = getMinfromQueue(queue);
                JarakNow = iter.Item3; 

                //pilih simpul itu lalu keluarkan dari queue
                queue.Remove(iter);

                //dapetin simpul yang terakhir dari list string di iter
                string ujung = iter.Item1[iter.Item1.Count - 1];

                //cek apakah simpul ujung merupakan simpul goal
                if(Equals(ujung, tujuan))
                {
                    hasil = iter.Item1;
                    keluar = (hasil, JarakNow);
                    found = true;
                } else
                {
                    //cari tetangganya dari simpul ujung
                    tetangga = g.getTetangga(ujung);

                    //tambah queue
                    addQueue(g, iter.Item1, tetangga, queue, JarakNow);
                }
            }

            //kalo sudah ditemukan jarak dari asal ke tujuan yang paling minimum
            if(!found)
            {
                throw new Exception();
            }

            return keluar;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]



        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
