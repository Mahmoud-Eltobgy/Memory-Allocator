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
    public partial class Process_config : Form
    {
        int num_proc;
        List<TextBox> process_size = new List<TextBox>();
        List<ComboBox> alocating_Algorithm = new List<ComboBox>();
        List<Label> process_lbl = new List<Label>();
        Panel panel1 = new Panel();

        public Process_config(int process,List<Label>p_lbl,List<TextBox>p_size,List<ComboBox>P_algo)
        {
            
            InitializeComponent();
       
            this.num_proc = process;
            this.process_lbl = p_lbl;
            this.process_size = p_size;
            this.alocating_Algorithm = P_algo;
            this.panel1.AutoScroll = true;
            Show_Process_Data_Fields(num_proc);
        }

  
        private void Process_config_Load(object sender, EventArgs e)
        {
            
        }
        private void Show_Process_Data_Fields(int process_num)
        {


            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            for (int i = 0; i < process_num; i++)
            {
                this.panel1.Controls.Add(process_lbl[i]);
                this.panel1.Controls.Add(process_size[i]);
                this.panel1.Controls.Add(alocating_Algorithm[i]);


            }

            this.groupBox1.Controls.Add(panel1);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

    }
}
