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
    class player_c
    {
        public int cuzdan;
        int sizex, sizey;
        int blockx;
        int blocky;
        int kx;
        int ky;
        int hamle_sayısı;
        int ind = 0;
        int g_ind = 0;
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
        List<int> gx = new List<int>();
        List<int> gy = new List<int>();
        List<int> gg_value = new List<int>();
        List<List<int>> matrisx = new List<List<int>>();
        List<List<int>> matrisy = new List<List<int>>();
        private List<int> uzaklık = new List<int>();
        Panel playerb = new Panel();
        public player_c(int cuzdan, List<List<int>> matrisx, List<List<int>> matrisy, int sizex, int sizey, int hamle_sayıs, int hamle, int hedef, int blockx, int blocky)
        {
            this.kx = matrisx[0][blockx-1];
            this.ky = matrisy[blocky-1][0];
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
            playerb.BackgroundImage = Image.FromFile("c.png");
            playerb.BackgroundImageLayout = ImageLayout.Stretch;
            board.Controls.Add(playerb);
            playerb.BringToFront();
            return board;
        }
        private void parcala(List<string> degerler)
        {

            /*for (int i = 0; i < degerler.Count; i++)
            {
                Console.WriteLine(degerler[i] + "  " + i);
            }*/
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
            if(degerler.Count>0)
            {
                try
                {
                    parcala(degerler);
                    /* for (int i = 0; i < x.Count; i++)
                      {
                          Console.WriteLine("---->" + x[i] + "," + y[i] + ":" + g_value[i]);
                      }*/
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
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("Hata!: " + e);
                }

            }
            return degerler;
           
         //   Console.WriteLine("en az maliyetli altın: " + x[ind] + "," + y[ind]);
        }
        public List<string> oyna(List<string> degerler, Panel board)
        {
           // Console.WriteLine("toplam:" + toplam + " " + "altın değeri: " + g_value[ind] + " hedef: " + x[ind] + "," + y[ind]);
           

            if (toplam <= hamle_sayısı && x.Count>0 && y.Count>0)
            {
                Console.WriteLine("C'nin toplam:" + toplam + " " + "altın değeri: " + g_value[ind] + " C'nin hedefi: " + x[ind] + "," + y[ind]);
                kx = matrisx[0][x[ind]];
                ky = matrisy[y[ind]][0];
                playerb.Location = new Point(kx, ky);
                ozet += (kx / (sizex + 1)).ToString() + "," + (ky / (sizey + 1)).ToString() + "\n\t  |\n\t  v\n\t";
                toplam_adım++;
                cuzdan -= hamle;
                harcanan_altın += hamle;
                cuzdan += g_value[ind] - hedef;
                toplanan_altın += g_value[ind];
                harcanan_altın += hedef;
           //     Console.WriteLine("hedef için -" + hedef + " uygulandı");
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
               Console.WriteLine("1-)  C nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);

            }

            else if (x.Count > 0 || y.Count > 0)
            {
                Console.WriteLine("C'nin toplamı:" + toplam + " " + "altın değeri: " + g_value[ind] + " C'nin  hedefi: " + x[ind] + "," + y[ind]);
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

                    Console.WriteLine("2-)  C nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);
                    if (Math.Abs(x[ind] - (kx / (sizex + 1))) == hamle_sayısı && y[ind] == (ky / (sizey + 1)))
                    {
                        cuzdan += g_value[ind] - hedef;
                        toplanan_altın += g_value[ind];
                        harcanan_altın += hedef;
         //               Console.WriteLine("hedef için -" + hedef + " uygulandı");
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

                       Console.WriteLine("3-)  C nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);

                        if (y[ind] - (ky / (sizey + 1)) == 0)
                        {
                            cuzdan += g_value[ind] - hedef;
                            toplanan_altın += g_value[ind];
                            harcanan_altın += hedef;
            //                Console.WriteLine("hedef için -" + hedef + " uygulandı");
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
                        toplanan_altın += g_value[ind];
                        harcanan_altın += hedef;
           //             Console.WriteLine("hedef için -" + hedef + " uygulandı");
                       Console.WriteLine("4-)  C nin koor:" + kx / (sizex + 1) + " , " + ky / (sizey + 1) + " Cüzdan:" + cuzdan);
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
        private void g_parcala(List<string> g_degerler)
        {
           
            
            gx.Clear();
            gy.Clear();
            gg_value.Clear();
            for (int i = 0; i < g_degerler.Count; i++)
            {
                string[] metin2 = g_degerler[i].Split(',');

                gx.Add(Int32.Parse(metin2[0]));

                gy.Add(Int32.Parse(metin2[1]));

                gg_value.Add(Int32.Parse(metin2[2]));


            }

        }
        public List<string> g_bul(List<string> g_degerler, Panel board, List<string> degerler)
        {
           
            g_parcala(g_degerler);
            for (int i = 0; i < g_degerler.Count; i++)
            {


            //    Console.WriteLine(g_degerler[i]);
            }
            int temp = 0;
            int toplam = int.MaxValue;
            for (int i = 0; i < gx.Count; i++)
            {
                toplam = Math.Abs((gy[i] - ky / (sizey + 1))) + Math.Abs((gx[i] - kx / (sizex + 1)));
                g_ind = i;
                uzaklık.Add(toplam);
            }

            for(int i=0;i<gx.Count;i++)
            {
                for(int j=0;j<i;j++)
                {
                    if(uzaklık[i]<uzaklık[j])
                    {
                        temp = uzaklık[i];
                        uzaklık[i] = uzaklık[j];
                        uzaklık[j] = temp;

                        temp = gx[i];
                        gx[i] = gx[j];
                        gx[j] = temp;


                        temp = gy[i];
                        gy[i] = gy[j];
                        gy[j] = temp;

                        temp = gg_value[i];
                        gg_value[i] = gg_value[j];
                        gg_value[j] = temp;
                    }
                }
            }
            foreach (Control item in board.Controls.OfType<Control>())
            {

               
                    item.Visible =true;
              

                }


                for (int i=2;i<gx.Count;i++)
            {
                foreach (Control item in board.Controls.OfType<Control>())
                {
                    
                    string p = "System.Windows.Forms.PictureBox, SizeMode: Normal";
                    if (item.Location.X == gx[i]*(sizex+1) && item.Location.Y == gy[i]*(sizey+1) && item.ToString().Equals(p))
                        {
                        item.Visible = false;
                   //     Console.WriteLine(item.Location.X/70 + ", " + item.Location.Y/70);
                        
                    }
                   
                    
                }
            }
            
            for (int i = 0; i < gx.Count; i++)
            {

                g_degerler[i] = gx[i].ToString() + "," + gy[i].ToString() + "," + gg_value[i].ToString();
               
            }
            for (int i = 0; i < g_degerler.Count; i++)
            {


        //        Console.WriteLine("sıralandır: " + g_degerler[i] + " i: " + i);
            }
            if (g_degerler.Count < 2)
            {
                degerler = ekle(degerler, g_degerler);
                g_degerler.Clear();
            }
            else
            {
                degerler = ekle(degerler, g_degerler);
                for (int i = 0; i < degerler.Count; i++)
                {


          //          Console.WriteLine("degerler: " + degerler[i] + " i: " + i);
                }
                g_degerler.RemoveAt(0);
                g_degerler.RemoveAt(0);
                

            }
            for (int i = 0; i < g_degerler.Count; i++)
            {

            //    Console.WriteLine("çıktı: " + g_degerler[i]);
            }
            return g_degerler;


        }
        private List<string> ekle (List<string> degerler, List<string> g_degerler)
        {
            
         
             if(g_degerler.Count<=1 && g_degerler.Count>0)
            {
              //  Console.WriteLine("g_degerler[0]" + g_degerler[0]);
                degerler.Add(g_degerler[0]);

            }
             else if(g_degerler.Count > 0)
            {
                degerler.Add(g_degerler[0]);
                degerler.Add(g_degerler[1]);
            }
            
            return degerler;
        }



    }
}
