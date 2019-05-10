using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    
    public partial class Form1 : Form
    {
        int width;
        int nwidth;
        int height;
        int nheight;
        int method;
        int counter;
        static int iteration = 0;
        bool[,] logicTable;
        int licznik = 1;
        string condition;
        Pen p = new Pen(Color.Black, 1);
        Brush w = new SolidBrush(Color.White);
        Brush brush = new SolidBrush(Color.Black);
        
        public Form1()
        {
            
            InitializeComponent();
            
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            condition = comboBox1.Text;
            if (textBox1.Text == "") width = 16;  else width = Convert.ToInt32(textBox1.Text);
            if (textBox2.Text == "") height = 31; else height = Convert.ToInt32(textBox2.Text);
            if (textBox3.Text == "") method = 250; else method = Convert.ToInt32(textBox3.Text);
            if (textBox4.Text == "\r\n") counter = 1;  else counter = Convert.ToInt32(textBox4.Text);
            if (condition == "") condition = "periodyczne";
            


            string binaryString = Convert.ToString(method, 2).PadLeft(8,'0');
            

            if (licznik==1 || nwidth != width || nheight != height)
            {
                logicTable = new bool[width, height];
                for (int i = 0; i < width; ++i)
                    for (int j = 0; j < height; j++)
                    {
                        {
                            logicTable[i, j] = new bool();
                        }
                    }
                logicTable[0, height / 2] = true;
                licznik = 0;
            }
            nwidth = width;
            nheight = height;

            for (int i = 0; i < counter; i++)
            {
                drawTable(logicTable, panel1);

                if (iteration < width - 1)
                {
                    if (condition == "periodyczne")
                    {
                        setPeriodicNeightbours(binaryString, iteration);
                    }
                    else
                    {
                        selectNeightbours(binaryString, iteration);
                    }
                }
                ++iteration;
            }
                
            if (iteration == width-1)
            {
                button1.Visible = false;
            }

            
        }

        public void drawTable(bool[,] logicTable, Panel panel)
        {
            float x = 0;
            float y = 0;
            float squareLength;
            Graphics l = panel.CreateGraphics();
            if (width > height)
            {
                squareLength = panel.Height / width;
                x = panel.Width / 4;
            }
            else
            {

                squareLength = panel.Height / height;
                x = panel.Width / 4;

            }
                   
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    if (logicTable[i, j] == false)
                    {
                        l.FillRectangle(w, x, y, squareLength, squareLength);
                    }
                    if (logicTable[i, j] == true)
                    {
                        l.FillRectangle(brush, x, y, squareLength, squareLength);
                    }
                    x += squareLength;
                }
                if (width > height)
                {
                    x = panel.Width / 4;
                }
                else
                {
                    x = panel.Width / 4;
                }
                
                y += squareLength;
            }
        }
        public void selectNeightbours(string method, int i) //i to numer iteracji/rzędu
        {
            for (int j = 0; j < height; ++j)
                if (j-1!=-1 && j+1 !=height)
                {
                    {
                        if (method.ElementAt(0) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == true && logicTable[i, j + 1] == true)
                        {
                            logicTable[i + 1, j] = true;
                        }
                        if (method.ElementAt(1) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == true && logicTable[i, j + 1] == false)
                        {
                            logicTable[i + 1, j] = true;
                        }
                        if (method.ElementAt(2) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == false && logicTable[i, j + 1] == true)
                        {
                            logicTable[i + 1, j] = true;
                        }
                        if (method.ElementAt(3) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == false && logicTable[i, j + 1] == false)
                        {
                            logicTable[i + 1, j] = true;
                        }
                        if (method.ElementAt(4) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == true && logicTable[i, j + 1] == true)
                        {
                            logicTable[i + 1, j] = true;
                        }
                        if (method.ElementAt(5) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == true && logicTable[i, j + 1] == false)
                        {
                            logicTable[i + 1, j] = true;
                        }
                        if (method.ElementAt(6) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == false && logicTable[i, j + 1] == true)
                        {
                            logicTable[i + 1, j] = true;
                        }
                        if (method.ElementAt(7) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == false && logicTable[i, j + 1] == false)
                        {
                            logicTable[i + 1, j] = true;
                        }
                    }
                }
            
        }

        //w tym przypadku lepsze byloby uzycie listy a nie typu wartosciowego

        public void setPeriodicNeightbours (string method, int i)
        {
            for (int j = 0; j < height; ++j)

            {
                if (j - 1 != -1  && j + 1 != height)
                {
                    if (method.ElementAt(0) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == true && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(1) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == true && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(2) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == false && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(3) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == false && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(4) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == true && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(5) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == true && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(6) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == false && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(7) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == false && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                }

                if (j - 1 == -1)
                {
                    if (method.ElementAt(0) == '1' && logicTable[i, height-1] == true && logicTable[i, j] == true && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(1) == '1' && logicTable[i, height - 1] == true && logicTable[i, j] == true && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(2) == '1' && logicTable[i, height - 1] == true && logicTable[i, j] == false && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(3) == '1' && logicTable[i, height - 1] == true && logicTable[i, j] == false && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(4) == '1' && logicTable[i, height - 1] == false && logicTable[i, j] == true && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(5) == '1' && logicTable[i, height - 1] == false && logicTable[i, j] == true && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(6) == '1' && logicTable[i, height - 1] == false && logicTable[i, j] == false && logicTable[i, j + 1] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(7) == '1' && logicTable[i, height - 1] == false && logicTable[i, j] == false && logicTable[i, j + 1] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                }
                if (j + 1 == height)
                {
                    if (method.ElementAt(0) == '1' && logicTable[i, j-1] == true && logicTable[i, j] == true && logicTable[i, 0] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(1) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == true && logicTable[i, 0] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(2) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == false && logicTable[i, 0] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(3) == '1' && logicTable[i, j - 1] == true && logicTable[i, j] == false && logicTable[i, 0] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(4) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == true && logicTable[i, 0] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(5) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == true && logicTable[i, 0] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(6) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == false && logicTable[i, 0] == true)
                    {
                        logicTable[i + 1, j] = true;
                    }
                    if (method.ElementAt(7) == '1' && logicTable[i, j - 1] == false && logicTable[i, j] == false && logicTable[i, 0] == false)
                    {
                        logicTable[i + 1, j] = true;
                    }
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {        
                logicTable = new bool[width, height];
                for (int i = 0; i < width; ++i)
                    for (int j = 0; j < height; j++)
                    {
                        {
                            logicTable[i, j] = new bool();
                        }
                    }
                logicTable[0, height / 2] = true;
                iteration = 0;
                panel1.Refresh();
                button1.Visible = true;
        }      
    }  
}
