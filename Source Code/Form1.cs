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
   
    public partial class Form1 : Form
    {
        const int lbl_x = 6;
        const int lbl_y = 22;
        const int txt1_x = 70;
        const int txt1_y = 19;
        const int txt2_x = 150;
        const int txt2_y = 19;
        const int increment = 30;
        int mem_size;
        int process_number;
        int holes_number;
        
       public List<TextBox> holes_size = new List<TextBox>();
        public List<TextBox> holes_starting = new List<TextBox>();
       public List<Label> holes_lbl = new List<Label>();
        public List<ComboBox> alocating_Algorithm = new List<ComboBox>();
        public List<Label> process_lbl = new List<Label>();
        public List<TextBox> process_size = new List<TextBox>();
        List<process> processes = new List<process>();
        List<hole> holes = new List<hole>();

        public Form1()
        {
            InitializeComponent();
           
            Step2.Hide();
            Step3.Hide();
            btn_showLayout.Text = "Go Through All Steps";
            btn_showLayout.Enabled = false;
           
        }

        private void update_process_list()
        {
            processes.Clear();
          
            for (int i = 0; i < process_number; i++)
            {
                process proc = new process();
                proc.name = process_lbl[i].Text;
                proc.size = Convert.ToInt16(process_size[i].Text);
                proc.method = alocating_Algorithm[i].Text;
                proc.process_lbl = process_lbl[i];
                processes.Add(proc);

                    }
        }
        private void update_holes_list()
        {
            holes.Clear();
            for (int i = 0; i < holes_number; i++)
            {
                hole hol = new hole();
                hol.start = Convert.ToInt16(holes_starting[i].Text);
                hol.size = Convert.ToInt16(holes_size[i].Text);
                hol.end = hol.start+hol.size;
                hol.hole_lbl = holes_lbl[i];
                holes.Add(hol);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             holes_number = (int)numericUpDown1.Value;
            Step3.Show();
            btn_showLayout.Text = "Show holes!?";
            btn_showLayout.Enabled = true;
            Create_Holes_Data_Fields(holes_number);
            Holes_settings holes_window = new Holes_settings(holes_number,holes_size,holes_starting,holes_lbl);
            holes_window.ShowDialog();
           
        }

        private Label creat_lable(string name, int x_lbl, int y_lbl)
        {
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            // lbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lbl.Location = new System.Drawing.Point(x_lbl, y_lbl);
            lbl.Size = new System.Drawing.Size(60, 13);
            lbl.Text = name;
            lbl.AutoSize = false;
            return lbl;
        }
        private TextBox creat_textBox(string text, int x, int y)
        {


            System.Windows.Forms.TextBox txt = new System.Windows.Forms.TextBox();
            // txt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            txt.Size = new System.Drawing.Size(76, 20);
            txt.Location = new System.Drawing.Point(x, y);
        //    txt.Click += new System.EventHandler(this.textBox_Click);
            txt.Text = text; 
            
            return txt;
        }
        private ComboBox creat_ComboBox(string text, int x, int y)
        {


            System.Windows.Forms.ComboBox txt = new System.Windows.Forms.ComboBox();
            // txt.Anchor = System.Windows.Forms.AnchorStyles.Top;
            txt.Size = new System.Drawing.Size(100, 20);
            txt.Location = new System.Drawing.Point(x, y);
            txt.Items.Add("First Fit");
            txt.Items.Add("Best Fit");
            txt.Items.Add("Worst Fit");
            
            txt.Text = text;
            //    txt.Click += new System.EventHandler(this.textBox_Click);
            return txt;
        }

        private void Create_Holes_Data_Fields(int holes_num)
        {
            TextBox hole_size = new TextBox();
            TextBox hole_start = new TextBox();
            Label hole_lbl = new Label();
            if(holes_size.Count>=0||holes_starting.Count>=0||holes_lbl.Count>=0)
            {
                holes_lbl.Clear();
                holes_size.Clear();
                holes_starting.Clear();

            }
            int pad = 0;
            for (int i = 1; i < holes_num + 1; i++)
            {
                string hoel_name = "Hole " + ((i).ToString());
                hole_lbl =creat_lable(hoel_name, lbl_x, lbl_y + pad);
                hole_size = creat_textBox("Size", txt1_x, txt1_y + pad);
                hole_start = creat_textBox("Start From", txt2_x, txt2_y + pad);
                holes_size.Add(hole_size);
                holes_starting.Add(hole_start);
                holes_lbl.Add(hole_lbl);
                pad += increment;


            }



        }

        private void Create_Process_Data_Fields(int procs_num)
        {
            TextBox process_size_txt = new TextBox();
            ComboBox algorthm = new ComboBox();
            Label process_lable = new Label();
            if (process_size.Count >= 0 || alocating_Algorithm.Count >= 0 || process_lbl.Count >= 0)
            {
                process_size.Clear();
                alocating_Algorithm.Clear();
                process_lbl.Clear();

            }
            int pad = 0;
            for (int i = 1; i < procs_num + 1; i++)
            {
                string process_name = "P " + ((i).ToString());
                process_lable = creat_lable(process_name, lbl_x, lbl_y + pad);
                process_size_txt = creat_textBox("Size", txt1_x, txt1_y + pad);
                algorthm = creat_ComboBox("Choose Method", txt2_x, txt2_y + pad);
                process_size.Add(process_size_txt);
                alocating_Algorithm.Add(algorthm);
                process_lbl.Add(process_lable);
                pad += increment;


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
             process_number = (int)numericUpDown2.Value;
            btn_showLayout.BackColor = Color.Green;
            btn_showLayout.Text = "Good to Go !";
            btn_showLayout.Enabled = true;
            Create_Process_Data_Fields(process_number);
            Process_config process_window = new Process_config(process_number, process_lbl, process_size, alocating_Algorithm);
            process_window.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_mem_size_Click(object sender, EventArgs e)
        {
            mem_size = (int)numericUpDown3.Value;
            btn_showLayout.Text = "Not Ready Yet !";
            Step2.Show();

        }

        private void btn_showLayout_Click(object sender, EventArgs e)
        {
            update_holes_list();
            update_process_list();
            Form1 newform = new Form1();
            newform = this;
            MemoryLayout mem_layout = new MemoryLayout(ref newform,mem_size,holes,processes);
            mem_layout.ShowDialog();

        }
    }
}
public class process 
{

    public int size;
    public string method;
    public int start;
    public int end;
    public string name;
    public Label process_lbl;
    public Label start_lbl;
    public Label end_lbl;
    public Label waiting;
    public bool IsAllocated=false;
    public string allocated_hole_name="";
    public bool rearranged = false;
  
}
public class hole : IComparable<hole>
{

    public int size;
    public int start;
    public int end;
    public Label hole_lbl;
    public int CompareTo(hole other)
    {
        return Convert.ToInt16(Convert.ToInt16( this.start).CompareTo(Convert.ToInt16(other.start)));
    }
}