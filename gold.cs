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
    class gold : Form
    {
        int loc_x, loc_y;
        int value, sizex,sizey;
        Boolean gizli_mi;
        static int i = 0;
       public static List<string> degerler = new List<string>();
        static List<string> g_degerler = new List<string>();
        private PictureBox altin = new PictureBox();

        public gold(int x, int y, Boolean gizli_mi,  int sizex, int sizey, int value)
        {
            this.loc_x = x;
            this.loc_y = y;
            this.gizli_mi = gizli_mi;
            this.sizex = sizex;
            this.sizey = sizey;
            this.value = value;
            
        }
        
        public Panel gold_loc(Panel board)
        {
            
            altin.BackgroundImage = Image.FromFile("altın.png");
            altin.BackgroundImageLayout = ImageLayout.Stretch;
            altin.BackColor = Color.FromArgb(128, 57, 30);
            altin.Location = new Point(loc_x, loc_y);
            altin.Size = new Size(sizex,sizey);
            
            if(gizli_mi == true)
            {
                altin.Visible = gizli_mi;
                
                
            }
            else
            {
                altin.Visible = true;
                altin.BackColor = Color.Blue;
                i++;
                Console.WriteLine(altin.Location + ": " + i);
            }
            
            board.Controls.Add(altin);
            altin.BringToFront();
            return board;

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // gold
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "gold";
            this.Load += new System.EventHandler(this.gold_Load);
            this.ResumeLayout(false);

        }

        private void gold_Load(object sender, EventArgs e)
        {

        }

        public List<string> gold_value(int uretx1, int urety)
        {
            if(gizli_mi == true)
            {
                string a = uretx1.ToString() + "," + urety.ToString() + "," + value.ToString();
                degerler.Add(a);
            }
            
            return degerler;

        }
        public List<string> s_gold_value(int uretx1, int urety)
        {
            if (gizli_mi == false)
            {
                string a = uretx1.ToString() + "," + urety.ToString() + "," + value.ToString();
                g_degerler.Add(a);
            }
            return g_degerler;
        }
        public void goruntule()
        {
            for(int i=0;i<degerler.Count;i++)
            {
                Console.Write(degerler[i]);
            }
            Console.WriteLine();
        }
       
       



    }
}
