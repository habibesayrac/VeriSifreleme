﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace VeriSifreleme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestProject;Integrated Security=True");

        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select*from TBLVERILER", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("ID");
            dt2.Columns.Add("AD");
            dt2.Columns.Add("SOYAD");
            dt2.Columns.Add("MAIL");
            dt2.Columns.Add("SIFRE");
            dt2.Columns.Add("HESAPNO");
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                DataRow r = dt2.NewRow();
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    try
                    {
                        string cozum = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        byte[] cozumdızı = Convert.FromBase64String(cozum);
                        string cozumverisi = ASCIIEncoding.ASCII.GetString(cozumdızı);
                        r[j] = cozumverisi;
                    }
                    catch (Exception)
                    {
                        r[j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }
                dt2.Rows.Add(r);
            }
            dataGridView2.DataSource = dt2;

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            string ad = TxtAd.Text;
            byte[] addizi = ASCIIEncoding.ASCII.GetBytes(ad);
            string adsifre = Convert.ToBase64String(addizi);

            string soyad = TxtSoyad.Text;
            byte[] soyaddizi = ASCIIEncoding.ASCII.GetBytes(soyad);
            string soyadsifre = Convert.ToBase64String(soyaddizi);

            string mail = TxtMail.Text;
            byte[] maildizi = ASCIIEncoding.ASCII.GetBytes(mail);
            string mailsifre = Convert.ToBase64String(maildizi);

            string sifre = TxtSifre.Text;
            byte[] sifredizi = ASCIIEncoding.ASCII.GetBytes(sifre);
            string sifresifre = Convert.ToBase64String(sifredizi);

            string hesapno = TxtHesapNo.Text;
            byte[] hesapnodizi = ASCIIEncoding.ASCII.GetBytes(hesapno);
            string hesapnosifre = Convert.ToBase64String(hesapnodizi);

            connection.Open();
            SqlCommand komut = new SqlCommand("insert into TBLVERILER (AD,SOYAD,MAIL,SIFRE,HESAPNO) values (@p1,@p2,@p3,@p4,@p5)", connection);
            komut.Parameters.AddWithValue("@p1", adsifre);
            komut.Parameters.AddWithValue("@p2", soyadsifre);
            komut.Parameters.AddWithValue("@p3", mailsifre);
            komut.Parameters.AddWithValue("@p4", sifresifre);
            komut.Parameters.AddWithValue("@p5", hesapnosifre);
            komut.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Veriler eklendi");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();


        }

    }
}