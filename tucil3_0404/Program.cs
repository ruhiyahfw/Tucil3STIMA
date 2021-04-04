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
        private List<KeyValuePair<coordinate,string>> simpul;
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
            this.simpul = new List<KeyValuePair<coordinate, string>>();
            this.adjmat = new double[this.JumlahSimpul, this.JumlahSimpul];
        }

        public void CreateGraf(string path)
        {

            string[] lines = System.IO.File.ReadAllLines(@path);
            //baca koordinat dan nama setiap simpul
            string read1 = "";
            string read2 = "";
            string read3 = "";
            for (int i = 1; i <= this.JumlahSimpul; i++)
            {
                int j = 0;
                // baca longitude
                while (lines[i][j] != ' ')
                {
                    read1 = read1 + lines[i][j];
                    j++;
                }
                // baca latitude
                j++;
                while (lines[i][j] != ' ')
                {
                    read2 = read2 + lines[i][j];
                    j++;
                }
                coordinate c = new coordinate(Convert.ToDouble(read1), Convert.ToDouble(read2));
                // baca nama node
                j++;
                while (j < lines[i].Length)
                {
                    read3 = read3 + lines[i][j];
                    j++;
                }
                this.simpul.Add(c, read3);
                
            }


        }


            
        
    }
}
