using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Allocation
{
    public partial class Holes_settings : Form
    {
        int num_holes;
        List<TextBox> holes_size = new List<TextBox>();
        List<TextBox> holes_starting = new List<TextBox>();
        List<Label> holes_lbl = new List<Label>();
        Panel panel1 = new Panel();
        public Holes_settings(int holes,List<TextBox> holes_size,List<TextBox> holes_starting,List<Label>holes_lbl)
        {
            InitializeComponent();
            this.num_holes = holes;
            this.holes_size = holes_size;
            this.holes_starting = holes_starting;
            this.holes_lbl = holes_lbl;
            this.panel1.AutoScroll = true;
        }

        private void Holes_settings_Load(object sender, EventArgs e)
        {
            Show_Holes_Data_Fields(num_holes);
        }
     
        private void Show_Holes_Data_Fields(int holes_num)
        {
            
           
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            for (int i = 0; i < holes_num ; i++)
                {
                this.panel1.Controls.Add(holes_lbl[i]);
                this.panel1.Controls.Add(holes_size[i]);
                this.panel1.Controls.Add(holes_starting[i]);


            }

            this.groupBox1.Controls.Add(panel1);
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
