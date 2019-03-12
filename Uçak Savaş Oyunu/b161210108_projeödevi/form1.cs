using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace b161210108_projeödevi
{
    public partial class form1 : Form
    {

        public form1()
        {
            InitializeComponent();
        }
        int hareketmesafesi = 40;
        int sayac = 0;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            timer1.Stop();
           
            dusman_olustur();

     
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
          if(e.KeyCode==Keys.Enter)
            {
                timer1.Start();
            }
            
            if(e.KeyCode==Keys.Left)
            {
                if(pb_bizimucak.Location.X-hareketmesafesi+50>0)
                pb_bizimucak.Location = new System.Drawing.Point(pb_bizimucak.Location.X - hareketmesafesi, pb_bizimucak.Location.Y);
            }
            else if(e.KeyCode==Keys.Right)
            {
                if (pb_bizimucak.Location.X + hareketmesafesi-50+pb_bizimucak.Width< panel1.Width)
                    pb_bizimucak.Location = new System.Drawing.Point(pb_bizimucak.Location.X+hareketmesafesi , pb_bizimucak.Location.Y);
            }
            if(e.KeyCode==Keys.Space)
            {
                mermi_olustur();
            }
        }
        private void mermi_olustur()
        {
            PictureBox mermi = new PictureBox();
            mermi.Image = Image.FromFile("bullet.png");
            mermi.Width = 20;
            mermi.Height = 25;
            mermi.SizeMode = PictureBoxSizeMode.Zoom;
            mermi.Location = new Point(pb_bizimucak.Location.X + pb_bizimucak.Height, pb_bizimucak.Location.Y - pb_bizimucak.Width/ 2);
            mermi.BackColor = Color.Transparent;     
            mermi.Tag = "mermi";
            panel1.Controls.Add(mermi);
       }
       
        private void dusman_olustur()
        {
            Random rast = new Random();
            PictureBox pic = new PictureBox();
            pic.Image = Image.FromFile("airplane.png");
            pic.Width = 150;
            pic.Height =45;
            int r = rast.Next(0,panel1.Height+pic.Height+100);
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            
            pic.Location=new Point(r, 0);
            pic.BackColor = Color.Transparent;
            pic.Tag = "dusman";
      
            panel1.Controls.Add(pic);
          
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            for (int i=0;i<panel1.Controls.Count;i++)
            {
                PictureBox pb = (PictureBox)panel1.Controls[i];
                if (panel1.Controls[i].Tag == "mermi")
                {
                    if (pb.Location.X + pb.Width > panel1.Width)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    {
                        pb.Location = new Point(pb.Location.X, pb.Location.Y-5);
                    }
                    for(int j=0;j<panel1.Controls.Count;j++)
                    {
                        if(panel1.Controls[j].Tag=="dusman")
                        {
                            if(pb.Location.X+pb.Width>panel1.Controls[j].Location.X && pb.Location.X+pb.Width<panel1.Controls[j].Location.X+panel1.Controls[j].Width)
                            {
                                if(pb.Location.Y+pb.Height>panel1.Controls[j].Location.Y&&pb.Location.Y+pb.Height<panel1.Controls[j].Location.Y+panel1.Controls[j].Height)
                                {
                                    panel1.Controls.RemoveAt(j);
                                    panel1.Controls.Remove(pb);
                                }
                            }
                        }
                    }
                }
                else if (panel1.Controls[i].Tag == "dusman")
                {
                    if (pb.Location.X < 0)
                    {
                        panel1.Controls.RemoveAt(i);
                    }
                    else
                    { 
                        pb.Location = new Point(pb.Location.X, pb.Location.Y + 5);
                        if(pb.Location.X>panel1.Location.X&&pb.Location.X<panel1.Location.X+panel1.Width)
                            {
                            if (pb.Location.Y >= panel1.Location.Y && pb.Location.Y >  panel1.Height || pb.Location.Y >= panel1.Location.Y && pb.Location.Y > pb.Location.Y + pb.Height)
                            {
                                panel1.Controls.RemoveAt(i);
                                timer1.Stop();
                                MessageBox.Show("OYUN BİTTİ!");
                               panel1.Controls.Clear();
                                panel1.Controls.Add(pb_bizimucak);                               
                                pb.Location = new Point(0, 0);
                            }
                        }
                    }
                }
            }
            if(sayac==15)
            {
                sayac = 0;
                dusman_olustur();
               
            }
       }
    }
}
