using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    class player_d
    {

        public int cuzdan;
        int sizex, sizey;
        int blockx;
        int blocky;
        int kx;
        int ky;
        int hamle_sayısı;
        int ind = 0;
        int toplam;
        int kontrol = 0;
        int kontrol2 = 0;
        public int hedef, hamle;
        public int toplam_adım = 0, harcanan_altın, toplanan_altın;
        public string ozet = "";
        public int toplam_a, toplam_b, toplam_c;
        List<int> x = new List<int>();
        List<int> y = new List<int>();
        List<int> g_value = new List<int>();
        List<int> gx = new List<int>();
        List<int> gy = new List<int>();
        List<int> gg_value = new List<int>();
        List<List<int>> matrisx = new List<List<int>>();
        List<List<int>> matrisy = new List<List<int>>();
        private List<int> uzaklık = new List<int>();
        Panel playerb = new Panel();
        public player_d(int cuzdan, List<List<int>> matrisx, List<List<int>> matrisy, int sizex, int sizey, int hamle_sayıs, int hamle, int hedef, int blockx, int blocky)
        {
            this.kx = matrisx[0][0];
            this.ky = matrisy[blocky - 1][0];
            this.matrisx = matrisx;
            this.matrisy = matrisy;
            this.sizex = sizex;
            this.sizey = sizey;
            this.cuzdan = cuzdan;
            this.hamle_sayısı = hamle_sayıs;
            this.hedef = hedef;
            this.hamle = hamle;
            this.blockx = blockx;
            this.blocky = blocky;

        }

        public Panel yerlestir(Panel board)
        {
            playerb.Location = new Point(this.kx, this.ky);
            playerb.Size = new Size(this.sizex, this.sizey);
            playerb.BackColor = Color.Black;
            playerb.BackgroundImage = Image.FromFile("d.png");
            playerb.BackgroundImageLayout = ImageLayout.Stretch;
            board.Controls.Add(playerb);
            playerb.BringToFront();
            return board;
        }
        private void parcala(List<string> degerler)
        {
            x.Clear();
            y.Clear();
            g_value.Clear();
            for (int i = 0; i < degerler.Count; i++)
            {
                string[] metin2 = degerler[i].Split(',');

                x.Add(Int32.Parse(metin2[0]));

                y.Add(Int32.Parse(metin2[1]));

                g_value.Add(Int32.Parse(metin2[2]));
            }

        }
        bool a = true;
        public List<string> bul(List<string> degerler, int toplam_a, int toplam_b, int toplam_c, int hedef_ax, int hedef_ay, int hedef_bx, int hedef_by, int hedef_cx, int hedef_cy)
        {
            parcala(degerler);



            if (degerler.Count > 0)
            {
                try
                {
                    parcala(degerler);
                    int maliyet = int.MaxValue;
                    int kac = 0;
                    for (int i = 0; i < x.Count; i++)//Kaç hamlede ulaşabiliyor
                    {
                        this.toplam = Math.Abs((y[i] - ky / (sizey + 1))) + Math.Abs((x[i] - kx / (sizex + 1)));

                        if (toplam % hamle_sayısı == 0)
                        {
                            kac = toplam / hamle_sayısı;

                            if (maliyet > kac * hamle + hedef)
                            {
                                maliyet = kac * hamle + hedef;
                                ind = i;
                            }
                        }
                        else
                        {
                            kac = (toplam / hamle_sayısı) + 1;

                            if (maliyet > kac * hamle + hedef)
                            {
                                maliyet = kac * hamle + hedef;
                                ind = i;

                            }
                        }
                        if (x[ind] == hedef_ax && y[ind] == hedef_ay && toplam - hamle_sayısı >= toplam_a)
                        {

                            // Console.WriteLine("----------->A' nın hedefi: " + hedef_ax + "," + hedef_ay + "toplam_a: " + toplam_a + "D' toplamı: " + toplam);
                            Console.WriteLine("A ile ortak hedef,hedef değiştirildi !"+"("+hedef_ax+","+hedef_ay+")");
                            x.RemoveAt(ind);
                            y.RemoveAt(ind);
                            g_value.RemoveAt(ind);
                            i = 0;

                        }
                        else if (x[ind] == hedef_bx && y[ind] == hedef_by && toplam - hamle_sayısı >= toplam_b)
                        {
                           // Console.WriteLine("--------->B' nın hedefi: " + hedef_bx + "," + hedef_by + "toplam_a: " + toplam_b + "D' toplamı: " + toplam);
                            Console.WriteLine("B ile ortak hedef,hedef değiştirildi !" + "(" + hedef_bx + "," + hedef_by + ")");
                            x.RemoveAt(ind);
                            y.RemoveAt(ind);
                            g_value.RemoveAt(ind);
                            i = 0;
                        }
                        else if (x[ind] == hedef_cx && y[ind] == hedef_cy && toplam - hamle_sayısı >= toplam_c)
                        {
                            //   Console.WriteLine("------->C' nın hedefi: " + hedef_cx + "," + hedef_cy + "toplam_a: " + toplam_c + "D' toplamı: " + toplam);
                            Console.WriteLine("C ile ortak hedef,hedef değiştirildi !" + "(" + hedef_cx + "," + hedef_cy + ")");
                            x.RemoveAt(ind);
                            y.RemoveAt(ind);
                            g_value.RemoveAt(ind);
                            i = 0;
                        }


                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Hata!: " + e);
                }
            }


            return degerler;
        }
        public List<string> oyna(List<string> degerler, Panel board)
        {
           // Console.WriteLine("D'nin toplamı:" + toplam + " " + "altın değeri: " + g_value[ind] + "D'nin hedefi: " + x[ind] + "," + y[ind]);

            if (toplam <= hamle_sayısı && x.Count > 0 && y.Count > 0)
            {
                Console.WriteLine("D'nin toplamı:" + toplam + " " + "altın değeri: " + g_value[ind] + " D'nin hedefi: " + x[ind] + "," + y[ind]);
                kx = matrisx[0][x[ind]];
                ky = matrisy[y[ind]][0];
                playerb.Location = new Point(kx, ky);
                ozet += (kx / (sizex + 1)).ToString() + "," + (ky / (sizey + 1)).ToString() + "\n\t  |\n\t  v\n\t";
                toplam_adım++;
                cuzdan -= hamle;
                harcanan_altın += hamle;
                cuzdan += g_value[ind] - hedef;
                harcanan_altın += hedef;
                toplanan_altın += g_value[ind];
                foreach (Control item in board.Controls.OfType<Control>())
                {

                    string p = "System.Windows.Forms.PictureBox, SizeMode: Normal";
                    if (item.Location.X == playerb.Location.X && item.Location.Y == playerb.Location.Y && item.ToString().Equals(p))
                    {
                        item.BackColor = Color.Orange;

                    }


                }


                if (degerler.Count > 0)
                {
                    degerler.RemoveAt(ind);
                }
                else
                {
                    return degerler;
                }
                Console.WriteLine("1-)  D nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);

            }//Tek hamlede ulaşabilecekse

            else if (x.Count > 0 || y.Count > 0)
            {
                Console.WriteLine("D'nin toplamı:" + toplam + " " + "altın değeri: " + g_value[ind] + " D'nin hedefi: " + x[ind] + "," + y[ind]);
                if (Math.Abs(x[ind] - (kx / (sizex + 1))) >= hamle_sayısı)
                {
                    if (x[ind] - (kx / (sizex + 1)) > 0)
                    {
                        kx += (hamle_sayısı * (sizex + 1));
                    }
                    else
                    {
                        kx -= (hamle_sayısı * (sizex + 1));
                    }

                    playerb.Location = new Point(kx, ky);
                    ozet += (kx / (sizex + 1)).ToString() + "," + (ky / (sizey + 1)).ToString() + "\n\t  |\n\t  v\n\t";
                    toplam_adım++;
                    cuzdan -= hamle;
                    harcanan_altın += hamle;

                    Console.WriteLine("2-)  D nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);
                  

                    if (x[ind] == (kx / (sizex + 1))   && y[ind] == (ky / (sizey + 1)))
                    {
                        cuzdan += g_value[ind] - hedef;
                        harcanan_altın += hedef;
                        toplanan_altın += g_value[ind];

                        //  Console.WriteLine("hedef için -" + hedef + " uygulandı");
                     //   Console.WriteLine("Bu değerlerin sayısı:" + degerler.Count);
                        if (degerler.Count > 0)
                        {
                          //  Console.WriteLine("Bu ind:" + degerler[ind]);
                            degerler.RemoveAt(ind);
                           // Console.WriteLine("Bu ind:" + degerler[ind]);
                        }
                    }
                    else
                    {
                        return degerler;
                    }
                    foreach (Control item in board.Controls.OfType<Control>())
                    {

                        string p = "System.Windows.Forms.PictureBox, SizeMode: Normal";
                        if (item.Location.X == playerb.Location.X && item.Location.Y == playerb.Location.Y && item.ToString().Equals(p))
                        {
                            item.BackColor = Color.Orange;
                            // Console.WriteLine(item);

                        }


                    }

                    //Buradaki Y değerleri önemli
                }//Tek hamlede ulaşamayacağı X farkları

                else if (Math.Abs(x[ind] - (kx / (sizex + 1))) < hamle_sayısı)
                {
                    kontrol = (hamle_sayısı - Math.Abs(x[ind] - (kx / (sizex + 1))));
                    kx += (x[ind] - (kx / (sizex + 1))) * (sizex + 1); // MATH ABSSS !!!

                    kontrol2 = y[ind] - (ky / (sizey + 1));
                    if (Math.Abs(y[ind] - (ky / (sizey + 1))) >= kontrol)
                    {
                        if (kontrol2 > 0)
                        {
                            ky += kontrol * (sizey + 1);
                        }
                        else
                        {
                            ky -= kontrol * (sizey + 1);
                        }


                        playerb.Location = new Point(kx, ky);
                        ozet += (kx / (sizex + 1)).ToString() + "," + (ky / (sizey + 1)).ToString() + "\n\t  |\n\t  v\n\t";
                        toplam_adım++;
                        cuzdan -= hamle;
                        harcanan_altın += hamle;

                        Console.WriteLine("3-)  D nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);

                        if (y[ind] - (ky / (sizey + 1)) == 0)
                        {
                            cuzdan += g_value[ind] - hedef;
                            harcanan_altın += hedef;
                            toplanan_altın += g_value[ind];
                            foreach (Control item in board.Controls.OfType<Control>())
                            {

                                string p = "System.Windows.Forms.PictureBox, SizeMode: Normal";
                                if (item.Location.X == playerb.Location.X && item.Location.Y == playerb.Location.Y && item.ToString().Equals(p))
                                {
                                    item.BackColor = Color.Orange;
                                    // Console.WriteLine(item);

                                }


                            }
                            //  Console.WriteLine("hedef için -" + hedef + " uygulandı");
                            if (degerler.Count > 0)
                            {
                                degerler.RemoveAt(ind);
                            }
                            else
                            {
                                return degerler;
                            }
                        }
                    }
                    else
                    {
                        ky = y[ind] * (sizey + 1);
                        playerb.Location = new Point(kx, ky);
                        ozet += (kx / (sizex + 1)).ToString() + "," + (ky / (sizey + 1)).ToString() + "\n\t  |\n\t  v\n\t";
                        toplam_adım++;
                        cuzdan -= hamle;
                        harcanan_altın += hamle;
                        cuzdan += g_value[ind] - hedef;
                        harcanan_altın += hedef;
                        toplanan_altın += g_value[ind];
                        //Console.WriteLine("hedef için -" + hedef + " uygulandı");
                        Console.WriteLine("4-) D nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);
                        foreach (Control item in board.Controls.OfType<Control>())
                        {

                            string p = "System.Windows.Forms.PictureBox, SizeMode: Normal";
                            if (item.Location.X == playerb.Location.X && item.Location.Y == playerb.Location.Y && item.ToString().Equals(p))
                            {
                                item.BackColor = Color.Orange;
                                // Console.WriteLine(item);

                            }


                        }
                        if (degerler.Count > 0)
                        {
                            degerler.RemoveAt(ind);
                        }
                        else
                        {
                            return degerler;
                        }
                    }

                }//Tek hamlede ulaşabileceği X farkları
            }
           
            return degerler;
        }









    }
}
