using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sobel_Kenar_Cikarma
{
    public partial class Form1 : Form
    {
        string str = "saklanacak mesaj";
        int padding = 0;
        public Form1()
        {
            InitializeComponent();
        }

       

        private Bitmap sobelEdgeDetection(Bitmap image)
        {
            Bitmap gri = griYap(image);
            Bitmap buffer = new Bitmap(gri.Width, gri.Height);
            Color renk;

            int str_i = 0;
            int x, y, gradyan;
            int[,] GX = new int[3, 3];
            int[,] GY = new int[3, 3];

            GX[0, 0] = -1; GX[0, 1] = 0; GX[0, 2] = 1;
            GX[1, 0] = -2; GX[1, 1] = 0; GX[1, 2] = 2;
            GX[2, 0] = -1; GX[2, 1] = 0; GX[2, 2] = 1;


            GY[0, 0] = -1; GY[0, 1] = -2; GY[0, 2] = -1;
            GY[1, 0] = 0; GY[1, 1] = 0; GY[1, 2] = 0;
            GY[2, 0] = 1; GY[2, 1] = 2; GY[2, 2] = 1;


            for (int i = 0; i < gri.Height - 1; i++)
            {
                for (int j = 0; j < gri.Width - 1; j++)
                {
                    if (i == 0 || i == gri.Height - 1 || j == 0 || j == Width - 1)
                    {

                        renk = Color.FromArgb(255, 255, 255);
                        buffer.SetPixel(j, i, renk);
                        x = 0;
                        y = 0;
                    }
                    else
                    {
                        x = gri.GetPixel(j - 1, i - 1).R * GX[0, 0] +
                            gri.GetPixel(j, i - 1).R * GX[0, 1] +
                            gri.GetPixel(j + 1, i - 1).R * GX[0, 2] +
                            gri.GetPixel(j - 1, i).R * GX[1, 0] +
                            gri.GetPixel(j, i).R * GX[1, 1] +
                            gri.GetPixel(j + 1, i).R * GX[1, 2] +
                            gri.GetPixel(j - 1, i + 1).R * GX[2, 0] +
                            gri.GetPixel(j, i + 1).R * GX[2, 1] +
                            gri.GetPixel(j + 1, i + 1).R * GX[2, 2];

                        y = gri.GetPixel(j - 1, i - 1).R * GY[0, 0] +
                            gri.GetPixel(j, i - 1).R * GY[0, 1] +
                            gri.GetPixel(j + 1, i - 1).R * GY[0, 2] +
                            gri.GetPixel(j - 1, i).R * GY[1, 0] +
                            gri.GetPixel(j, i).R * GY[1, 1] +
                            gri.GetPixel(j + 1, i).R * GY[1, 2] +
                            gri.GetPixel(j - 1, i + 1).R * GY[2, 0] +
                            gri.GetPixel(j, i + 1).R * GY[2, 1] +
                            gri.GetPixel(j + 1, i + 1).R * GY[2, 2];

                        gradyan = (int)(Math.Abs(x) + Math.Abs(y));
                        if (gradyan < 0)
                        {
                            gradyan = 0;
                        }
                        if (gradyan > 255)
                        {
                            gradyan = 255;
                        }
                        renk = Color.FromArgb(gradyan, gradyan, gradyan);
                        buffer.SetPixel(j, i, renk);

                        for (int k = 0; k < padding; k++)
                        {
                            Color c = Color.FromArgb(Convert.ToInt32(str[str_i].ToString()), Convert.ToInt32(str[str_i].ToString()), Convert.ToInt32(str[str_i].ToString()));
                            buffer.SetPixel(i, buffer.Width + padding, c);
                            str_i++;
                            if (str_i == str.Length) str_i = 0;
                        }
                        
                    }
                }
            }

            return buffer;
        }
        private Bitmap griYap(Bitmap image)
        {

            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    int deger = (image.GetPixel(j, i).R + image.GetPixel(j, i).G + image.GetPixel(j, i).B) / 3;
                    
                    Color renk;
                    renk = Color.FromArgb(deger, deger, deger);
                    image.SetPixel(j, i, renk);
                }
            }

            return image;
        }
        private void button2_Click(object sender, EventArgs e)
        {

            Bitmap image = new Bitmap(pictureBox1.Image);
            padding = (image.Width * 3 % 4); //eğer width 3 se padding 1 olur.
            Bitmap sobel =sobelEdgeDetection(image);
            pictureBox2.Image = sobel;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Filter = ".JPEG Dosyaları|*.jpg|Bütün Dosyalar (*.*)|*.*";
            file.InitialDirectory = ".";
            file.Title = "Bir Resim Dosyası Seçiniz";
            if (file.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = file.FileName;
            }
        }
       


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
