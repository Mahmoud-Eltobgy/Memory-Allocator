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
    public partial class AddProcess : Form
    {
        MemoryLayout memFormHandler;
        int new_ps;
        public AddProcess(ref MemoryLayout memHandler,int new_ps)
        {
            InitializeComponent();
            this.new_ps = new_ps;
            this.memFormHandler = memHandler;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            process p_new = new process();
            Label lbl = new Label();
            lbl.Text= "New P" + new_ps.ToString();
            p_new.size =Convert.ToInt16( textBox1.Text);
            p_new.method = comboBox1.Text;
            p_new.process_lbl = lbl;
            p_new.name = p_new.process_lbl.Text;
          //  p_new.waiting = lbl;
            //memFormHandler.inrement_y += 15;
            memFormHandler.procs.Add(p_new);
            this.Close();
        }
    }
}
