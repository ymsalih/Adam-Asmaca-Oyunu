using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdamAsmacaOyunu
{
    public partial class Form1 : Form
    {
        string[] kelimeler = { "Şehir" };
        Random rnd = new Random();
        List<string> secilenKelimeler = new List<string>();
        string kelime = "";

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            oyunOluştur();

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        int Move;
        int Mouse_X;
        int Mouse_Y;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Move == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);

            }
        }
            private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Move = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Move = 0;

        }
        int hataSayac = 0;

        private void oyunOluştur()
        {
            pictureBox1.Load("Resimler/" + hataSayac + ".png");
            label2.Text = kelimeler[rnd.Next(kelimeler.Length)];

            FileStream fs = new FileStream("Kelimeler/" + label2.Text + ".txt", FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                secilenKelimeler.Add(yazi.ToUpper());
                yazi = sw.ReadLine();
            }
            sw.Close();
            fs.Close();
            kelime = secilenKelimeler[rnd.Next(secilenKelimeler.Count)];


            for (int i = 0; i < kelime.Length; i++)
                lblTahmin.Text += "_ ";
        }
        private void oyunBitir()
        {
            foreach (Control btns in this.Controls)
                if (btns is Button)
                    ((Button)btns).Enabled = true;
            lblTahmin.Text = "";
            label4.Text = "11";
            hataSayac = 0;
            oyunOluştur();
        }
        public string ReplaceAt(string str, int index, int length, string replace)
        {
            return str.Remove(index, Math.Min(length, str.Length - index))
                    .Insert(index, replace);
        }

        private void Oyun(object sender, EventArgs e)
        {
            Button seciliBtn = sender as Button;
            seciliBtn.Enabled = false;
            if (kelime.Contains(seciliBtn.Text) == false)
            {
                hataSayac++;
                pictureBox1.Load("Resimler/" + hataSayac + ".png");
                label4.Text = (11 - hataSayac).ToString();
            }
            else
            {
                string text = lblTahmin.Text.Replace(" ", "");

                for (int i = 0; i < kelime.Length; i++)
                {
                    if (kelime[i].ToString() == seciliBtn.Text)
                    {


                        text = ReplaceAt(text, i, 1, seciliBtn.Text);
                    }


                }
                string sonuç = "";
                for (int i = 0; i < text.Length; i++)

                    sonuç += text[i].ToString() + " ";
                lblTahmin.Text = sonuç;

            }
            if (label4.Text == "0")
            {


                DialogResult dialogResult = MessageBox.Show("Maalesef Kaybettiniz Kelime : " + kelime + " Tekrar Oynamak İstiyor Musunuz ?", "@kodzamani.tk", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    oyunBitir();
                else
                    Application.Exit();
            }
            if (lblTahmin.Text.Replace(" ", "") == kelime)
            {


                DialogResult dialogResult = MessageBox.Show("Tebrikler Kazandınız Kelime : " + kelime + " Tekrar Oynamak İstiyor Musunuz ?", "@kodzamani.tk", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                    oyunBitir();
                else
                    Application.Exit();
            }
        }

    }


}

