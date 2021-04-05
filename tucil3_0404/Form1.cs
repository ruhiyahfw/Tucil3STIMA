using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tucil3_0404
{
    public partial class Form1 : Form
    {
        string simpulasal, simpultujuan, mapterpilih;

        public Form1()
        {
            InitializeComponent();
            daftarMap.Items.Add("Peta jalan sekitar kampus ITB/Dago");
            daftarMap.Items.Add("Peta jalan sekitar Alun-alun Bandung");
            daftarMap.Items.Add("Peta jalan sekitar Buahbatu");
            daftarMap.Items.Add("Peta jalan sekitar kawasan rumah");
            label3.Hide();
            label4.Hide();
            search.Hide();
            asal.Hide();
            tujuan.Hide();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Show();
            label4.Show();
            search.Show();
            asal.Show();
            tujuan.Show();
            //masukin isi combobox dari asal dan tujuan
        }

        private void search_Click(object sender, EventArgs e)
        {
            //panggil fungsi astar
        }

        private void daftarMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            mapterpilih = daftarMap.SelectedItem.ToString();
        }

        private void asal_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpulasal = asal.SelectedItem.ToString();
        }

        private void tujuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            simpultujuan = asal.SelectedItem.ToString();
        }
    }
}
