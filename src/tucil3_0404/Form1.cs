﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace tucil3_0404
{
    public partial class Form1 : Form
    {
        string simpulasal, simpultujuan, mapterpilih, path;
        int N;

        public Form1()
        {
            InitializeComponent();
            daftarMap.Items.Add("Peta jalan sekitar kampus ITB/Dago");
            daftarMap.Items.Add("Peta jalan sekitar Alun-alun Bandung");
            daftarMap.Items.Add("Peta jalan sekitar Buahbatu");
            daftarMap.Items.Add("Peta kawasan sekitar Kota Padang");
            daftarMap.Items.Add("Peta wilayah Romania");
            daftarMap.Items.Add("Peta kawasan sekitar Kota Payakumbuh");
            label3.Visible = false;
            label4.Visible = false;
            search.Visible = false;
            asal.Visible = false;
            tujuan.Visible = false;
            groupBox2.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.Remove(Global.viewer);
            //tampilkan box untuk input simpul asal dan tujuan
            label3.Visible = true;
            label4.Visible = true;
            search.Visible = true;
            asal.Visible = true;
            tujuan.Visible = true;
            groupBox2.Visible = true; //tempat untuk meanmpilkan jarak dari simpil asal ke simpul tujuan

            //baca filename dan path nya
            string filename = Global.PilihMap(mapterpilih);
            string currentDir = Environment.CurrentDirectory.ToString();
            //Console.WriteLine(currentDir);
            DirectoryInfo d = new DirectoryInfo(currentDir);
            string parent = System.IO.Directory.GetParent(currentDir).FullName;
            string parentDir = System.IO.Directory.GetParent(parent).FullName;
            //string dir = System.IO.Directory.GetParent(parentDir).FullName;
            //Console.WriteLine(dir);
            path = Path.GetFullPath(Path.Combine(parentDir, @"test", filename));

            //string path = "C:/Users/farad/source/repos/BuramSTIMA3/BuramSTIMA3/map5.txt";

            // buat graf
            N = Global.JmlSimpul(path);
            Global.g = new graf(N);

            // buat graf MSAGL
            Global.viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Global.graph = new Microsoft.Msagl.Drawing.Graph("graph");
            Global.nodes = new List<string>();

            //baca txt dan tambahkan ke msagl
            Global.g.CreateGraf(path);

            //masukin isi combobox dari asal dan tujuan
            asal.Items.Clear();
            tujuan.Items.Clear();
            foreach (var simpul in Global.g.getAllSimpul())
            {
                asal.Items.Add(simpul.Key);
                tujuan.Items.Add(simpul.Key);
            }

            Global.g.AddMSAGL(Global.graph, Global.nodes);
            foreach (string node in Global.nodes)
            {
                Global.graph.FindNode(node).Attr.Color = Microsoft.Msagl.Drawing.Color.CadetBlue;
                Global.graph.FindNode(node).Attr.FillColor = Microsoft.Msagl.Drawing.Color.CadetBlue;
            }


            Global.viewer.Graph = Global.graph;
            Global.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBox1.Controls.Add(Global.viewer);
        }

        private void search_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.Remove(Global.viewer);
            // inisialissi MSAGL
            foreach (string node in Global.nodes)
            {
                Global.graph.FindNode(node).Attr.Color = Microsoft.Msagl.Drawing.Color.CadetBlue;
                Global.graph.FindNode(node).Attr.FillColor = Microsoft.Msagl.Drawing.Color.CadetBlue;
            }

            // buat graf
            N = Global.JmlSimpul(path);
            Global.g = new graf(N);

            // buat graf MSAGL
            Global.viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            Global.graph = new Microsoft.Msagl.Drawing.Graph("graph");
            Global.nodes = new List<string>();

            //baca txt dan tambahkan ke msagl
            Global.g.CreateGraf(path);

            //panggil fungsi astar
            string msg = "";
            (List<string>, double) hasil = (new List<string>(), 0);
            astarsearch Astar = new astarsearch();
            try
            {
                hasil = Astar.astar(simpulasal, simpultujuan, Global.g);
                Global.g.AddMSAGLHasil(Global.graph, Global.nodes, hasil);
                foreach (string node in Global.nodes)
                {
                    Global.graph.FindNode(node).Attr.Color = Microsoft.Msagl.Drawing.Color.CadetBlue;
                    Global.graph.FindNode(node).Attr.FillColor = Microsoft.Msagl.Drawing.Color.CadetBlue;
                }
                foreach (string nama in hasil.Item1)
                {
                    //var temp = graph.AddEdge()
                    Global.graph.FindNode(nama).Attr.Color = Microsoft.Msagl.Drawing.Color.Coral;
                    Global.graph.FindNode(nama).Attr.FillColor = Microsoft.Msagl.Drawing.Color.Coral;
                }
                msg = "Jarak yang ditempuh: " + Convert.ToString(hasil.Item2);
            }
            catch (Exception)
            {
                msg = "Tidak ditemukan jalan :(";
            }
            
            //tampilkan hasil MSAGL sesuai hasil pencarian
            Global.viewer.Graph = Global.graph;
            Global.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            label5.Text = msg;
            groupBox1.Controls.Add(Global.viewer);
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
            simpultujuan = tujuan.SelectedItem.ToString();
        }
    }
}