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
    public partial class MemoryLayout : Form
    {
        List<hole> holes = new List<hole>();
       public List<process> procs = new List<process>();
        Stack<process> clicked_process = new Stack<process>();
        Form1 firstFormRef;
        public int inrement_y = 0;
        int mem_size;
        int new_ps = 0;
      //  Panel panel1 = new Panel();
        public MemoryLayout(ref Form1 form1Handler,int mem_size,List<hole> holes,List<process>procs)
        {
 
            InitializeComponent();
            this.holes = holes;
            this.procs = procs;
            this.mem_size = mem_size;
            this.firstFormRef = form1Handler;
            panel1.AutoScroll = true;
            label1.Text = mem_size.ToString();
            process p = new process();
            Show_processData(p ,false);



            // panel1.CausesValidation = false;
            label1.Font = new Font("Tahoma", 5, FontStyle.Regular);
            draw_holes();
           // SendLabelsToBack();
            draw_process();

        }
        public MemoryLayout()
        { }
        public Color GetRandomColor()
        {
            Random randonGen = new Random();
            Color randomColor =
                Color.FromArgb(
                (byte)randonGen.Next(255),
                (byte)randonGen.Next(255),
                (byte)randonGen.Next(255),
                (byte)randonGen.Next(255));
            return randomColor;
        }
        Label start = new Label();
        Label end = new Label();
        private void draw_holes()
        {
           holes.Sort();
            
            for (int i = 0; i <holes.Count;i++)
            {
                if (holes[i].size == 0)
                    continue;
                    int scale_y = Scale(holes[i].start);
                int scale_width = Scale(holes[i].size);
                int scale_end = scale_width;
                //  draw_rec(scale_y, scale_width, Color.White);

                Point pt = new Point(45, scale_y);
                Point lbl_pt = new Point(0, scale_y);



                holes[i].hole_lbl.BackColor = Color.White;
                holes[i].hole_lbl.ForeColor = Color.Black;
                holes[i].hole_lbl.Location = pt;
                holes[i].hole_lbl.Height = scale_width;
                holes[i].hole_lbl.Width = 155;
                holes[i].hole_lbl.TextAlign = ContentAlignment.MiddleCenter;

              //  holes[i].start = scale_y;
             // holes[i].size = scale_width;

                panel1.Controls.Add(holes[i].hole_lbl);
              
                    start = creat_lable(holes[i].start, lbl_pt.X, lbl_pt.Y);
                    end = creat_lable(holes[i].end, lbl_pt.X, lbl_pt.Y + (scale_end - 8));
                    end.TextAlign = ContentAlignment.TopRight;
                    start.BackColor = Color.DimGray;
                    end.BackColor = Color.DimGray;
                    start.ForeColor = Color.White;
                    end.ForeColor = Color.White;

                    start.Font = new Font("Tahoma", 5, FontStyle.Regular);
                    end.Font = new Font("Tahoma", 5, FontStyle.Regular);

                    panel1.Controls.Add(start);
                    panel1.Controls.Add(end);
                    start.BringToFront();
                    end.BringToFront();
                    start.Invalidate();
                    end.Invalidate();
                
            }
            
        }
        int Scale(int num)
        {
            return ((num * 400) / mem_size);
        }
        int waiting_procs = 0;
        Stack<Label> waiting = new Stack<Label>();

        private void draw_process()
        {
            Label start = new Label();
            Label end = new Label();
            
            for (int i = 0; i < procs.Count; i++)
            {
                process current = procs[i];
                bool IsAlocated = false;
                int scale_width = Scale(current.size);
                if (current.IsAllocated && current.rearranged == false)
                    continue;

                else if (current.IsAllocated && current.rearranged == true)
                {
                    panel1.Controls.Remove(current.process_lbl);
                    panel1.Controls.Remove(current.start_lbl);
                    panel1.Controls.Remove(current.end_lbl);
               
                    
                    Point pt = new Point(45, Scale(current.start));
                    Point lbl_p = new Point(20, Scale(current.start));
                    start = creat_lable(current.start, lbl_p.X, lbl_p.Y);
                    end = creat_lable(current.end, lbl_p.X, lbl_p.Y + (Scale(current.size) - 8));

                    start.BackColor = Color.DimGray;
                    end.BackColor = Color.DimGray;
                    start.ForeColor = Color.GreenYellow;
                    end.ForeColor = Color.GreenYellow;
                    start.Name = current.name;
                    end.Name = current.name;
                    start.Font = new Font("Tahoma", 5, FontStyle.Regular);
                    end.Font = new Font("Tahoma", 5, FontStyle.Regular);


                    if (current.size <= 10)
                        current.process_lbl.BackColor = Color.Blue;
                    else
                        current.process_lbl.BackColor = Color.Green;

                    current.process_lbl.ForeColor = Color.Black;
                    current.process_lbl.Location = pt;
                    current.process_lbl.Height = scale_width;
                    current.process_lbl.Width = 155;
                    current.process_lbl.TextAlign = ContentAlignment.TopCenter;
                    current.start_lbl = start;
                    current.end_lbl = end;
                    panel1.Controls.Add(current.process_lbl);
                    panel1.Controls.Add(start);
                    panel1.Controls.Add(end);
                    start.BringToFront();
                    end.BringToFront();
                    start.Invalidate();
                    end.Invalidate();
                    current.process_lbl.Name = current.name;
                    current.process_lbl.BringToFront();
                    current.process_lbl.Invalidate();
                  
                    //current.process_lbl.Click += new System.EventHandler(this.label_Click);
                   // current.process_lbl.Leave += new System.EventHandler(this.label_leave);
                    current.rearranged = false;
                    continue;

                }


                if (current.method=="First Fit")
                    holes.Sort();
                else if(current.method == "Best Fit")
                    holes = new List<hole>(holes.OrderBy(x => x.size).ToList());
                else //worst fit method
                    holes = new List<hole>(holes.OrderByDescending(x => x.size).ToList());

                    for (int j=0;j<holes.Count;j++)
                    {
                        hole current_hole = holes[j];
                        if (current_hole.size == 0)
                            continue;
                        if (current_hole.size>=current.size)
                        {
                            if(current.waiting!=null)
                            {
                                panel2.Controls.Remove(current.waiting);
                                //current.waiting = null;
                                waiting_procs--;
                                inrement_y -= 15;

                            }
                            IsAlocated = true;
                            current.start = current_hole.end - current.size;
                            current.end = current_hole.end;
                            current.IsAllocated = IsAlocated;
                            Point pt = new Point(45,Scale( current.start));
                            Point lbl_p = new Point(20, Scale(current.start));
                            start = creat_lable(current.start,lbl_p.X,lbl_p.Y);
                            end = creat_lable(current.end,lbl_p.X,lbl_p.Y+ (Scale(current.size) - 8));
                            
                            start.BackColor = Color.DimGray;
                            end.BackColor = Color.DimGray;
                            start.ForeColor = Color.GreenYellow;
                            end.ForeColor = Color.GreenYellow;
                            start.Name = current.name;
                            end.Name = current.name;
                            start.Font = new Font("Tahoma", 5, FontStyle.Regular);
                            end.Font = new Font("Tahoma", 5, FontStyle.Regular);

                 
                            if(current.size<=10)
                                current.process_lbl.BackColor = Color.Blue;
                            else
                            current.process_lbl.BackColor = Color.Green;

                            current.process_lbl.ForeColor = Color.Black;
                            current.process_lbl.Location = pt;
                            current.process_lbl.Height = scale_width;
                            current.process_lbl.Width = 155;
                            current.process_lbl.TextAlign = ContentAlignment.TopCenter;
                            current.start_lbl = start;
                            current.end_lbl = end;

                            current_hole.size -= current.size;  //Scale(scale_width);

                               
                            current_hole.end -= current.size;

                            panel1.Controls.Add(current.process_lbl);
                            panel1.Controls.Add(start);
                            panel1.Controls.Add(end);
                            start.BringToFront();
                            end.BringToFront();
                            start.Invalidate();
                            end.Invalidate();
                            current.allocated_hole_name = current_hole.hole_lbl.Text;
                            current.process_lbl.BringToFront();
                            current.process_lbl.Invalidate();
                            current.process_lbl.Name = current.name;
                            current.process_lbl.Click +=  new System.EventHandler(this.label_Click);
                            current.process_lbl.Leave+= new System.EventHandler(this.label_leave);
                   



                            break;
                        }
                    }
        
                
               
                if (!current.IsAllocated)
                {

                    Label waiting_lbl = new Label();
                    waiting_lbl = creat_lable(current.name + " with size = " + current.size + "->" + current.method, 6, 27 + inrement_y);
                    waiting_lbl.Name = current.name;
                    if(current.waiting==null)
                        current.waiting = waiting_lbl;
                    if (!panel2.Controls.Contains(current.waiting))
                    {

                        panel2.Controls.Add(current.waiting);
                        waiting_procs++;
                        inrement_y += 15;
                    }

                }

            }


        }


       



        private Label creat_lable(int name, int x_lbl, int y_lbl)
        {
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            // lbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lbl.Location = new System.Drawing.Point(x_lbl, y_lbl);
            //lbl.Size = new System.Drawing.Size(60, 13);
            lbl.ForeColor = Color.White;
            lbl.BackColor = Color.Black;
            lbl.Text = name.ToString();
            lbl.AutoSize = true;
            return lbl;
        }
        private Label creat_lable(string name, int x_lbl, int y_lbl)
        {
            System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
            // lbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            lbl.Location = new System.Drawing.Point(x_lbl, y_lbl);
            //lbl.Size = new System.Drawing.Size(60, 13);
           // lbl.ForeColor = Color.White;
            //lbl.BackColor = Color.Black;
            lbl.Text = name;
            lbl.AutoSize = true;
            return lbl;
        }
        private void Show_processData(process p,bool f)
        {
            if (f)
            {
                lbl_start.Show();
                lbl_start_v.Show();
                lbl_name.Show();
                lbl_name_v.Show();
                lbl_size.Show();
                lbl_size_v.Show();
                lbl_end.Show();
                lbl_end_v.Show();


                lbl_start_v.Text = p.start.ToString();

                lbl_name_v.Text = p.name;

                lbl_size_v.Text = p.size.ToString();

                lbl_end_v.Text = p.end.ToString();
                return;
            }
            lbl_start.Hide();
            lbl_start_v.Hide();
            lbl_name.Hide();
            lbl_name_v.Hide();
            lbl_size.Hide();
            lbl_size_v.Hide();
            lbl_end.Hide();
            lbl_end_v.Hide();

        }
        private void label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            process p = new process();
            if (clickedLabel != null)
            {
                
                // If the clicked label is black, the player clicked
                // an icon that's already been revealed --
                // ignore the click
                if (clickedLabel.BackColor == Color.Red )
                {
                    
                    Show_processData(p, false);
                    p = clicked_process.Pop();
                    label16.Text = (Convert.ToInt16(label16.Text) - 1).ToString();
                    if (p.size > 10)
                        clickedLabel.BackColor = Color.Green;
                    else
                        clickedLabel.BackColor = Color.Blue;
                }
                else
                {
                     for (int i=0 ; i < procs.Count ; i++)
                    {
                        if (procs[i].name == clickedLabel.Name)
                        {
                            clicked_process.Push(procs[i]);
                            label16.Text = (Convert.ToInt16(label16.Text) + 1).ToString();
                            Show_processData(procs[i], true);
                            break;
                        }
                    }
                   
                    clickedLabel.BackColor = Color.Red;
                   
                    
                }
            }
        }
        private void label_leave(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel.BackColor == Color.Red)
                return;
            clickedLabel.BackColor = Color.Red;
        }

        private void MemoryLayout_Load(object sender, EventArgs e)
        {

            //  draw_rec(0,50);
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            process p = new process();
            Show_processData(p, false);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            process proc=new process();
           
            Show_processData(proc, false);
            while (clicked_process.Count!=0)
            {

                proc = clicked_process.Pop();
               
                hole h = new hole();
                List<process> same_hole_procs;
                h = holes.Find(x => x.hole_lbl.Text.Contains(proc.allocated_hole_name));
                same_hole_procs = new List<process>(procs.FindAll(x => x.allocated_hole_name.Contains(h.hole_lbl.Text.ToString())).OrderByDescending(n => n.start));

                if (h.end  !=proc.start && (same_hole_procs.Count != clicked_process.Count + 1))//there is another processes allocated under than process 
                {
                    //get all processes allocated in that hole and shift them to the bottom of the hole
                    
                    h.end += proc.size;
                
                    bool flag = false;
                    for (int i = 0; i < same_hole_procs.Count; i++)
                    {
                      
                          
                      if(same_hole_procs[i] == proc)
                        {
                            flag = true;
                            procs.Remove(proc);
                            continue;
                        }
                      if(flag)
                        {
                            same_hole_procs[i].end += proc.size;
                            same_hole_procs[i].start = same_hole_procs[i].end - same_hole_procs[i].size;
                            same_hole_procs[i].rearranged = true;
                            draw_process();
                        }
                      
          

                    }


                    h.size += proc.size;

                }

                else
                {
                    h.size += proc.size;
                    h.end += proc.size;
                    procs.Remove(proc);
                }
                this.panel1.Controls.Remove(proc.process_lbl);
                this.panel1.Controls.Remove(proc.start_lbl);
                this.panel1.Controls.Remove(proc.end_lbl);
                label16.Text = (Convert.ToInt16(label16.Text) - 1).ToString();
            }
            
            if( waiting_procs > 0)
            {
                //  waiting_procs-=clicked_process.Count;

                // draw_holes();
               
                draw_process();
            }
            if (waiting_procs == 0)
                panel2.Controls.Clear();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            firstFormRef.holes_lbl.Clear();
            firstFormRef.holes_size.Clear();
            firstFormRef.holes_starting.Clear();
            firstFormRef.process_lbl.Clear();
            firstFormRef.process_size.Clear();
            firstFormRef.alocating_Algorithm.Clear();

            firstFormRef.Step3.Hide();
            firstFormRef.Step2.Hide();

            firstFormRef.btn_showLayout.Enabled = false;
            firstFormRef.btn_showLayout.Text = "Go Through All Steps";
            firstFormRef.btn_showLayout.BackColor = Color.DimGray;

            this.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            process p = new process();
            Show_processData(p, false);
            new_ps++;
            MemoryLayout mem = new MemoryLayout();
            mem = this;
            AddProcess new_processForm = new AddProcess(ref mem,new_ps);
            new_processForm.ShowDialog();
            draw_process();
        }

        private void MemoryLayout_FormClosed(object sender, FormClosedEventArgs e)
        {
            firstFormRef.Step2.Hide();
            firstFormRef.Step3.Hide();
            firstFormRef.btn_showLayout.Text = "Go Through All Steps";
            firstFormRef.btn_showLayout.Enabled = false;
            firstFormRef.btn_showLayout.BackColor = Color.DimGray;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            process p = new process();
            Show_processData(p, false);
        }

        private void MemoryLayout_Click(object sender, EventArgs e)
        {
            process p = new process();
            Show_processData(p, false); 
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {
            process p = new process();
            Show_processData(p, false);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            process p = new process();
            Show_processData(p, false);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
