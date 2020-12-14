using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    class tahta : Form1
    {
        public tahta(int blockx, int blocky, int gold_percent, int gold_value, int secret_gold)
        {
            this.blockx = blockx;
            this.blocky = blocky;
            this.gold_percent = gold_percent;
            this.gold_value = gold_value;
            this.secret_gold = secret_gold;
            form_size(game_area);
            game_size_set();
            board_back.Size = new Size(game_width * 45 / 100, game_width * 45 / 100);
        }
        private int board_width; // board_back'in genişliği
        private int board_height; // board_back'imn yüksekliği
        private int blockx; // karelerin x eksenince sayısı
        private int blocky; // karelerin y eksenince sayısı
        private int gold_percent;
        private int gold_value;
        private int game_width;
        private int game_height;
        private int secret_gold;
        public int block_sizex , block_sizey;
        public List<string> degerler = new List<string>();
        public List<string> g_degerler = new List<string>();
        List<int> arrayx = new List<int>();
       public List<List<int>> matrisx = new List<List<int>>();
        public List<List<int>> matrisy = new List<List<int>>();
        List<string> kontrol = new List<string>();
        List<string> g_kontrol = new List<string>();
        public Panel board_back = new Panel();
        List<gold> altınlar = new List<gold>();
        public Label a_bilgi = new Label();
        public Label b_bilgi = new Label();
        public Label c_bilgi = new Label();
        public Label d_bilgi = new Label();
        public Button close_game_area = new Button();


        Form game_area = new Form();
        
        public Form form_size(Form game_area) // oyun alanının özelliklerini kaydeder
        {
            game_area.WindowState = FormWindowState.Maximized;
            game_area.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            game_area.FormBorderStyle = FormBorderStyle.None;
            game_area.BackColor = Color.FromArgb(224, 153, 57);
            Panel A = new Panel();
            A.BackgroundImage = Image.FromFile("kare.png");
            A.BackColor = Color.Transparent;
            A.BackgroundImageLayout = ImageLayout.Stretch;
            A.Size = new Size(game_area.Width * 20 / 100, game_area.Height * 30 / 100);
            A.Location = new Point(game_area.Width * 3 / 100, game_area.Height * 10 / 100);
            Label a_name = new Label();
            a_name.Text = "A:";
            a_name.BackColor = Color.Transparent;
            a_name.Font = new Font("boycott", 36, FontStyle.Bold);
            a_name.Location = new Point(A.Width * 40 / 100, A.Height * 5 / 100);
            a_name.Size = new Size(A.Width * 30/ 100, A.Height * 30/ 100);
            Label a_cuzdan = new Label();
            a_cuzdan.Text = "Cüzdan:";
            a_cuzdan.BackColor = Color.Transparent;
            a_cuzdan.Font = new Font("boycott", 20, FontStyle.Bold);
            a_cuzdan.Location = new Point(A.Width * 5 / 100, A.Height * 50 / 100);
            a_cuzdan.Size = new Size(A.Width * 40 / 100, A.Height * 15 / 100);
            a_bilgi.Text = "200";
            a_bilgi.BackColor = Color.Transparent;
            a_bilgi.Font = new Font("boycott", 30, FontStyle.Italic);
            a_bilgi.Location = new Point(A.Width * 40 / 100, A.Height *45 / 100);
            a_bilgi.Size = new Size(A.Width * 60 / 100, A.Height * 40 / 100);
            A.Controls.Add(a_cuzdan);
            A.Controls.Add(a_bilgi);
            A.Controls.Add(a_name);
            game_area.Controls.Add(A);

            Panel D = new Panel();
            D.BackgroundImage = Image.FromFile("kare.png");
            D.BackColor = Color.Transparent;
            D.BackgroundImageLayout = ImageLayout.Stretch;
            D.Size = new Size(game_area.Width * 20 / 100, game_area.Height * 30 / 100);
            D.Location = new Point(game_area.Width * 3 / 100, game_area.Height * 50 / 100);
            Label d_name = new Label();
            Label d_cuzdan = new Label();
            d_cuzdan.Text = "Cüzdan:";
            d_cuzdan.BackColor = Color.Transparent;
            d_cuzdan.Font = new Font("boycott", 20, FontStyle.Bold);
            d_cuzdan.Location = new Point(D.Width * 5 / 100, D.Height * 50 / 100);
            d_cuzdan.Size = new Size(D.Width * 40 / 100, D.Height * 15 / 100);
            d_bilgi.Text = "200";
            d_bilgi.BackColor = Color.Transparent;
            d_bilgi.Font = new Font("boycott", 30, FontStyle.Italic);
            d_bilgi.Location = new Point(A.Width * 40 / 100, A.Height * 45 / 100);
            d_bilgi.Size = new Size(A.Width * 60 / 100, A.Height * 40 / 100);
            D.Controls.Add(d_cuzdan);
            D.Controls.Add(d_bilgi);
            d_name.Text = "d";
            d_name.BackColor = Color.Transparent;
            d_name.Font = new Font("boycott", 36, FontStyle.Bold);
            d_name.Location = new Point(D.Width * 40 / 100, A.Height * 5 / 100);
            d_name.Size = new Size(D.Width * 30 / 100, A.Height * 40 / 100);
            D.Controls.Add(d_name);
            game_area.Controls.Add(D);

            ///--------------------------------,

            Panel b = new Panel();
            b.BackgroundImage = Image.FromFile("kare.png");
            b.BackColor = Color.Transparent;
            b.BackgroundImageLayout = ImageLayout.Stretch;
            b.Size = new Size(game_area.Width * 20 / 100, game_area.Height * 30 / 100);
            b.Location = new Point(game_area.Width * 75 / 100, game_area.Height * 10 / 100);
            Label b_cuzdan = new Label();
            b_cuzdan.Text = "Cüzdan:";
            b_cuzdan.BackColor = Color.Transparent;
            b_cuzdan.Font = new Font("boycott", 20, FontStyle.Bold);
            b_cuzdan.Location = new Point(D.Width * 5 / 100, D.Height * 50 / 100);
            b_cuzdan.Size = new Size(D.Width * 40 / 100, D.Height * 15 / 100);
            b_bilgi.Text = "200";
            b_bilgi.BackColor = Color.Transparent;
            b_bilgi.Font = new Font("boycott", 30, FontStyle.Italic);
            b_bilgi.Location = new Point(A.Width * 40 / 100, A.Height * 45 / 100);
            b_bilgi.Size = new Size(A.Width * 60 / 100, A.Height * 40 / 100);
            b.Controls.Add(b_cuzdan);
            b.Controls.Add(b_bilgi);
            Label b_name = new Label();
            b_name.Text = "b";
            b_name.BackColor = Color.Transparent;
            b_name.Font = new Font("boycott", 36, FontStyle.Bold);
            b_name.Location = new Point(b.Width * 40 / 100, b.Height * 5 / 100);
            b_name.Size = new Size(b.Width * 30 / 100, b.Height * 40 / 100);
            b.Controls.Add(b_name);
            game_area.Controls.Add(b);

            ///---------------------------------------

            Panel c = new Panel();
            c.BackgroundImage = Image.FromFile("kare.png");
            c.BackColor = Color.Transparent;
            c.BackgroundImageLayout = ImageLayout.Stretch;
            c.Size = new Size(game_area.Width * 20 / 100, game_area.Height * 30 / 100);
            c.Location = new Point(game_area.Width * 75 / 100, game_area.Height * 50 / 100);
            Label c_cuzdan = new Label();
            c_cuzdan.Text = "Cüzdan:";
            c_cuzdan.BackColor = Color.Transparent;
            c_cuzdan.Font = new Font("boycott", 20, FontStyle.Bold);
            c_cuzdan.Location = new Point(D.Width * 5 / 100, D.Height * 50 / 100);
            c_cuzdan.Size = new Size(D.Width * 40 / 100, D.Height * 15 / 100);
            c_bilgi.Text = "200";
            c_bilgi.BackColor = Color.Transparent;
            c_bilgi.Font = new Font("boycott", 30, FontStyle.Italic);
            c_bilgi.Location = new Point(A.Width * 40 / 100, A.Height * 45 / 100);
            c_bilgi.Size = new Size(A.Width * 60 / 100, A.Height * 40 / 100);
            c.Controls.Add(c_cuzdan);
            c.Controls.Add(c_bilgi);
            Label c_name = new Label();
            c_name.Text = "c";
            c_name.BackColor = Color.Transparent;
            c_name.Font = new Font("boycott", 36, FontStyle.Bold);
            c_name.Location = new Point(c.Width * 40 / 100, c.Height * 5 / 100);
            c_name.Size = new Size(c.Width * 30 / 100, c.Height * 40 / 100);
            c.Controls.Add(c_name);

            close_game_area.BackgroundImage = Image.FromFile("close.png");
            close_game_area.Location = new Point(game_area.Width * 94 / 100, game_area.Height * 1 / 100);
            close_game_area.Size = new Size(game_area.Width * 5 / 100, game_area.Height * 8 / 100);
            close_game_area.BackgroundImageLayout = ImageLayout.Stretch;
            close_game_area.ForeColor = Color.Black;
            close_game_area.BackColor = Color.Transparent;
            close_game_area.FlatStyle = FlatStyle.Flat;
            close_game_area.FlatAppearance.BorderSize = 0;
            close_game_area.FlatAppearance.MouseDownBackColor = Color.Transparent;
            close_game_area.FlatAppearance.MouseOverBackColor = Color.Transparent;

            game_area.Controls.Add(close_game_area);
            close_game_area.BringToFront();
            game_area.Controls.Add(c);


            return game_area;
        }
        public void game_size_set() // oyun formunun boyutunu kaydeder
        {
            this.game_width = game_area.Width;
            this.game_height = game_area.Height;
        }
        public void board_size_set() // tahta arka planının boyutunu kaydeder
        {
            this.board_width = board_back.Width;
            this.board_height = board_back.Height;
        }
        public Form make_board()
        {         
            board_back.Location = new Point(game_width*50/100 - (board_back.Width/2) , game_height * 50 / 100 -( board_back.Height/2) );
            board_back.BackColor = Color.FromArgb(185, 116, 85);
            
            game_area.Controls.Add(board_back);
            board_size_set();
            return game_area;
        }
        public void block()
        {
            int uzunluk = 0;
            int aralıkx = 0;
            int aralıky = 0;
            int uzunluky = 0;
            int blok_uzun = 0;
            int x =0 , y=0;
           for(int i=0;i<blocky;i++)
            {
                
                aralıkx = 0;
                arrayx.Clear();
                List<int> arrayy = new List<int>();
             
                for (int j=0; j< blockx;j++)
                {
                    Panel block = new Panel(); 
                    block.Size = new Size(board_back.Width / blockx, board_back.Height / blocky);
                    block.BackColor = Color.FromArgb(128, 57, 30);
                    block.Location = new Point(x+aralıkx,y+ aralıky);
                    board_back.Controls.Add(block);
                    arrayy.Add(block.Location.Y);
                    arrayx.Add(block.Location.X);
                    aralıkx += block.Size.Width + 1;
                    uzunluk = aralıkx;  
                    blok_uzun = block.Size.Height ;
                    block_sizex = block.Size.Width;
                    block_sizey = block.Size.Height;
                   // block.Visible = false;
                    //block.SendToBack();
                    
                }
                matrisx.Add(arrayx);
                matrisy.Add(arrayy);
                aralıky += blok_uzun+1;
                uzunluky = aralıky;

            }


            Console.WriteLine(block_sizex + ","  + block_sizey);
            board_back.Size = new Size(uzunluk, uzunluky);
           


        }
        public void altin()
        {

             
            gold_percent = (blockx * blocky) * gold_percent / 100;
            
            secret_gold = gold_percent * secret_gold / 100;

            Console.WriteLine("gizliiii:" + secret_gold);
            Console.WriteLine(gold_percent);
            string compl = "";
            int uretx, uretx1, urety, urety1;
            Random rast = new Random();
            for (int i=0;i<gold_percent;i++)
            {
                a:
                uretx = rast.Next(0, blocky); 
                uretx1 = rast.Next(0, blockx); 
                urety = rast.Next(0, blocky);
                urety1 = rast.Next(0, blockx);
                compl =  uretx1.ToString() + urety.ToString();
           
               
                if(kontrol.Contains(compl) || (urety == 0 && uretx1 ==0) || (urety == blocky-1 && uretx1 == 0) || (urety == blocky-1 && uretx1 == blockx-1) || (urety == 0 && uretx1 == blockx-1))
                {
                    goto a;
                }
                kontrol.Add(compl);
                gold_value = rast.Next(1, 5) * 5;
                gold altın = new gold(matrisx[uretx][uretx1], matrisy[urety][urety1], true, block_sizex, block_sizey, gold_value );
                this.degerler = altın.gold_value(uretx1, urety);
                board_back = altın.gold_loc(board_back);
                
             


            }

            for (int i = 0; i < secret_gold; i++)
            {
                a:
                uretx = rast.Next(0, blocky); 
                uretx1 = rast.Next(0, blockx); 
                urety = rast.Next(0, blocky);
                urety1 = rast.Next(0, blockx);
                compl = uretx1.ToString()  + urety.ToString();
                if (g_kontrol.Contains(compl) || kontrol.Contains(compl) 
                    || (urety == 0 && uretx1 == 0) || (urety == blocky-1 && uretx1 == 0) || (urety == blocky-1 && uretx1 == blockx-1) 
                    || (urety == 0 && uretx1 == blockx-1))
                {
                    goto a;
                }
                g_kontrol.Add(compl);
                // Console.WriteLine(kontrol[i]);

                gold_value = rast.Next(1, 5)*5;

                gold altın = new gold(matrisx[uretx][uretx1], matrisy[urety][urety1], false, block_sizex, block_sizey, gold_value);
                this.g_degerler = altın.s_gold_value(uretx1, urety);
                board_back = altın.gold_loc(board_back);
               


            }


        }





    }
}
