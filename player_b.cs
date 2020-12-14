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
    class player_b
    {
        public int cuzdan;
        int sizex, sizey;
        int blockx;
        int blocky;
        int kx;
        int ky;
        int hamle_sayısı;
        int ind = 0;
        public int toplam;
        int kontrol = 0;
        public string ozet = "";
       public int hedef, hamle;
        public int toplam_adım = 0, harcanan_altın, toplanan_altın;
        int kontrol2 = 0;
        public int hedef_x, hedef_y;
        List<int> x = new List<int>();
        List<int> y = new List<int>();
        List<int> g_value = new List<int>();
        List<List<int>> matrisx = new List<List<int>>();
        List<List<int>> matrisy = new List<List<int>>();
        Panel playerb = new Panel();
        public player_b(int cuzdan, List<List<int>> matrisx, List<List<int>> matrisy, int sizex, int sizey, int hamle_sayıs, int hamle, int hedef, int blockx, int blocky)
        {
            this.kx = matrisx[0][blockx - 1];
            this.ky = matrisy[0][0];
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
            playerb.BackgroundImage = Image.FromFile("b.png");
            playerb.BackgroundImageLayout = ImageLayout.Stretch;
            board.Controls.Add(playerb);
            playerb.BringToFront();
            return board;
        }

        private void parcala(List<string> degerler)
        {

            for (int i = 0; i < degerler.Count; i++)
            {
             //   Console.WriteLine(degerler[i] + "  " + i);
            }
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
       
        public List<string> bul(List<string> degerler)
        {
            if (degerler.Count > 0)
            {
                try
                {

                    parcala(degerler);
                    for (int i = 0; i < x.Count; i++)
                    {
                  //      Console.WriteLine("---->" + x[i] + "," + y[i] + ":" + g_value[i]);
                    }
                    int maliyet = int.MaxValue;
                    int kac = 0;
                    for (int i = 0; i < x.Count; i++)//Kaç hamlede ulaşabiliyor
                    {
                        this.toplam = Math.Abs((y[i] - ky / (sizey + 1))) + Math.Abs((x[i] - kx / (sizex + 1)));
                //        Console.WriteLine("toplam: " + toplam + ": " + x[i] + "," + y[i]);
                        if (toplam % hamle_sayısı == 0)
                        {
                            kac = toplam / hamle_sayısı;
                      //      Console.WriteLine("kalansız:" + kac);
                            if (maliyet > kac * hamle + hedef)
                            {
                                maliyet = kac * hamle + hedef;
                                ind = i;
                            }
                        }
                        else
                        {
                            kac = (toplam / hamle_sayısı) + 1;
                      //      Console.WriteLine("kalanlo:" + kac);
                            if (maliyet > kac * hamle + hedef)
                            {
                                maliyet = kac * hamle + hedef;
                                ind = i;

                            }
                        }

                    }

              //      Console.WriteLine("en az maliyetli altın: " + x[ind] + "," + y[ind]);
                }
                catch (Exception e)
                {
                 //   Console.WriteLine(e.Message);
                }
            }
            
                return degerler;
            

        }
        public List<string> oyna(List<string> degerler, Panel board)
        {
            
            hedef_x = x[ind];
            hedef_y = y[ind];
            if (toplam <= hamle_sayısı && x.Count > 0 && y.Count > 0)
            {
                Console.WriteLine("B'nin toplamı:" + toplam + " " + "altın değeri: " + g_value[ind] + " B'nin hedefi: " + x[ind] + "," + y[ind]);
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
             //   Console.WriteLine("hedef için -" + hedef + " uygulandı");
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
                Console.WriteLine("1-)  B nın koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);

            }

            else if (x.Count > 0 || y.Count > 0)
            {
                Console.WriteLine("B'nin toplamı:" + toplam + " " + "altın değeri: " + g_value[ind] + " B'nin hedefi: " + x[ind] + "," + y[ind]);
                //Burası tek hamlede ulaşamayacağı alan x'i ve y'yi kontrol etmeliyiz
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
                   Console.WriteLine("2-)  B nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);
                    if (Math.Abs(x[ind] - (kx / (sizex + 1))) == hamle_sayısı && y[ind] == (ky / (sizey + 1)))
                    {
                        cuzdan += g_value[ind] - hedef;
                        harcanan_altın += hedef;
                        toplanan_altın += g_value[ind];
               //         Console.WriteLine("hedef için -" + hedef + " uygulandı");
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
                    //Buradaki Y değerleri önemli
                }

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

                        Console.WriteLine("3-)  B nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);

                        if (y[ind] - (ky / (sizey + 1)) == 0)
                        {
                            cuzdan += g_value[ind] - hedef;
                            harcanan_altın += hedef;
                            toplanan_altın += g_value[ind];
                      //      Console.WriteLine("hedef için -" + hedef + " uygulandı");
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
                  //      Console.WriteLine("hedef için -" + hedef + " uygulandı");
                        Console.WriteLine("4-)  B nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);
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
                }
            }
            return degerler;
        }


    }
}
