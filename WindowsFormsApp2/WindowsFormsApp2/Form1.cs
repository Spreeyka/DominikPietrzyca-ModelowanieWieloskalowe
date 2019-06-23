using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int width;
        int nwidth;
        int height;
        int nheight;
        string pattern;
        static bool[,] logicTable;
        static bool[,] logicTable2;
        static bool[,] logicTable3;
        static bool[,] logicTable4;
        int licznik = 1;
        static int iteration = 0;
        bool[] neighbours;
        double startingX;
        double startingY;
        bool isWorking = true;
        Pen p;
        Brush brush;
        Graphics l;
        Thread thread1;
        float x;
        float y;
        float squareLength;


        public Form1()
        { 
            p = new Pen(Color.Black, 1);
            brush = new SolidBrush(Color.Black);
            
            neighbours = new bool[8];          
            InitializeComponent();
            
        }
        //musze zrobic zeby nie resetowal sie stan za kazdym razem, bo nie moge teraz naklikac co chce zmienic. Edit reset button
        private void button1_Click(object sender, EventArgs e)
        {
            isWorking = true;
            thread1 = new Thread(new ThreadStart(start));
            thread1.Start();
        }
        public void start()
        {

            //pictureBox1.Refresh();
            if (textBox1.Text == "") width = 10; else width = Convert.ToInt32(textBox1.Text);
            if (textBox2.Text == "") height = 10; else height = Convert.ToInt32(textBox2.Text);
            if (comboBox1.Text == "") pattern = "oscylator"; else pattern = comboBox1.Text;

            if (licznik == 1 || nwidth != width || nheight != height)
            {

                logicTable = new bool[width, height];
                for (int i = 0; i < width; ++i)
                    for (int j = 0; j < height; j++)
                    {
                        {
                            logicTable[i, j] = new bool();
                        }
                    }
                logicTable2 = new bool[width, height];
                for (int i = 0; i < width; ++i)
                    for (int j = 0; j < height; j++)
                    {
                        {
                            logicTable2[i, j] = new bool();
                        }
                    }
                logicTable3 = new bool[width, height];
                for (int i = 0; i < width; ++i)
                    for (int j = 0; j < height; j++)
                    {
                        {
                            logicTable3[i, j] = new bool();
                        }
                    }
                logicTable4 = new bool[width, height];
                for (int i = 0; i < width; ++i)
                    for (int j = 0; j < height; j++)
                    {
                        {
                            logicTable4[i, j] = new bool();
                        }
                    }
                setInitialPattern();
                licznik = 0;
            }
            nwidth = width;
            nheight = height;

            while (isWorking)
            {


                Clear(logicTable4);
                if (iteration % 2 == 0)
                {
                    
                    drawTable(logicTable);
                    setNeightbours(logicTable, logicTable2);

                    for (int i = 0; i < width; ++i)
                        for (int j = 0; j < height; j++)
                        {
                            {
                                logicTable3[i, j] = logicTable[i, j];
                            }
                        }
                    for (int i = 0; i < width; ++i)
                        for (int j = 0; j < height; j++)
                        {
                            {
                                logicTable[i, j] = false; 
                            }
                        }
                    
                }
                if (iteration % 2 == 1)
                {
                    
                    drawTable(logicTable2); 
                    setNeightbours(logicTable2, logicTable); // setNeightbours(logicTable3, logicTable2) a pozniej to <--

                    for (int i = 0; i < width; ++i)
                        for (int j = 0; j < height; j++)
                        {
                            {
                                logicTable3[i, j] = logicTable2[i, j];
                            }
                        }
                    for (int i = 0; i < width; ++i)          // z tego 1231
                        for (int j = 0; j < height; j++)     // dlaczego pozniej znika?
                        {
                            {
                                logicTable2[i, j] = false;
                            }
                        }
                    
                }
                
                ++iteration;
                
                Thread.Sleep(200);
                
                


            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            l = pictureBox1.CreateGraphics();
            MouseEventArgs e2 = (MouseEventArgs)e;
            int ff = (int)Math.Floor(((e2.X - startingX) / squareLength));
            int gg = (int)Math.Floor(((e2.Y - startingY) / squareLength));

            logicTable3[ff, gg] = true;
            if (iteration % 2 == 1)
            {
                
                setNeightbours(logicTable3, logicTable2);
                //setNeightbours(logicTable3, logicTable);
            }

            if (iteration % 2 == 0)
            {
                
                setNeightbours(logicTable3, logicTable);
                //setNeightbours(logicTable3, logicTable);

            }

            l.FillRectangle(brush, (float)startingX + (ff * squareLength), gg * squareLength, squareLength, squareLength);

            
        }
        public void drawTable(bool[,] logicTable)
        {
            x = 0;
            y = 0;
            startingY = y;
            l = pictureBox1.CreateGraphics();
            if (width > height)
            {
                squareLength = pictureBox1.Height / width;
                x = pictureBox1.Width / 4;
                startingX = x;
            }
            else
            {

                squareLength = pictureBox1.Height / height;
                x = pictureBox1.Width / 4;
                startingX = x;

            }

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (logicTable[j, i] == false)
                    {
                        l.DrawRectangle(p, x, y, squareLength, squareLength);
                    }
                    if (logicTable[j, i] == true)
                    {
                        l.FillRectangle(brush, x, y, squareLength, squareLength);
                    }
                    x += squareLength;
                }
                if (width > height)
                {
                    x = pictureBox1.Width / 4;
                }
                else
                {
                    x = pictureBox1.Width / 4;
                }

                y += squareLength;
            }
           
        }
        public void Clear(bool[,] logicTable)
        {
            x = 0;
            y = 0;
            startingY = y;
            l = pictureBox1.CreateGraphics();
            if (width > height)
            {
                squareLength = pictureBox1.Height / width;
                x = pictureBox1.Width / 4;
                startingX = x;
            }
            else
            {

                squareLength = pictureBox1.Height / height;
                x = pictureBox1.Width / 4;
                startingX = x;

            }

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (logicTable[j, i] == false)
                    {
                        l.FillRectangle(Brushes.White, x, y, squareLength, squareLength);
                    }
                    if (logicTable[j, i] == true)
                    {
                        l.FillRectangle(Brushes.White, x, y, squareLength, squareLength);
                    }
                    x += squareLength;
                }
                if (width > height)
                {
                    x = pictureBox1.Width / 4;
                }
                else
                {
                    x = pictureBox1.Width / 4;
                }

                y += squareLength;
            }

        }

        public void setInitialPattern()
        {
            if(pattern == "niezmienne")
            {
                logicTable[width / 2 - 1, height / 2] = true;
                logicTable[width / 2, height / 2 + 1] = true;
                logicTable[width / 2 + 1, height / 2 + 1] = true;
                logicTable[width / 2 + 2, height / 2] = true;
                logicTable[width / 2, height / 2 - 1] = true;
                logicTable[width / 2 + 1, height / 2 - 1] = true;
            }

            if (pattern == "glider")
            {
                logicTable[width / 2 - 1, height / 2] = true;
                logicTable[width / 2, height / 2 - 1] = true;
                logicTable[width / 2, height / 2] = true;
                logicTable[width / 2 + 1, height / 2 - 1] = true;
                logicTable[width / 2 + 1, height / 2 + 1] = true;
                
            }

            if (pattern == "oscylator")
            {
                logicTable[width / 2 - 1, height / 2 ] = true;
                logicTable[width / 2 - 0, height / 2 ] = true;
                logicTable[width / 2 + 1, height / 2 ] = true;
            }

            if (pattern == "losowe")
            {
                Random rand = new Random();
                
                for (int i = 0; i < width; ++i)
                    for (int j = 0; j < height; j++)
                    {
                        {
                            if (rand.NextDouble() >= 0.7) logicTable[i, j] = true;                        
                        }
                    }
            }
            if (pattern == "ręczne")
            {
             
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            isWorking = false;
            thread1.Join();
            button1.Visible = true;

            
        }

        public void setNeightbours(bool[,] logicTable, bool[,] logicTable2)
        {

            
            
            for (int k = 0; k < height; ++k)
            {
                for (int j = 0; j < width; ++j)
                {
                    if( j - 1 == -1 && 0 < k && k < height - 1 )
                    {
                        neighbours[0] = logicTable[width-1, k - 1];
                        neighbours[1] = logicTable[width-1, k];
                        neighbours[2] = logicTable[width-1, k + 1];

                        neighbours[3] = logicTable[j , k - 1];
                        neighbours[4] = logicTable[j , k + 1];

                        neighbours[5] = logicTable[j + 1, k - 1];
                        neighbours[6] = logicTable[j + 1, k];
                        neighbours[7] = logicTable[j + 1, k + 1];
                    }
                    if (k - 1 == -1 && 0 < j && j < width - 1)
                    {
                        neighbours[0] = logicTable[j-1, height-1];
                        neighbours[1] = logicTable[j-1, k];
                        neighbours[2] = logicTable[j-1, k + 1];

                        neighbours[3] = logicTable[j, height - 1];
                        neighbours[4] = logicTable[j, k + 1];

                        neighbours[5] = logicTable[j + 1, height - 1];
                        neighbours[6] = logicTable[j + 1, k];
                        neighbours[7] = logicTable[j + 1, k + 1];
                    }
                    if (j + 1 == width && 0 < k && k < height - 1)
                    {
                        neighbours[0] = logicTable[j - 1, k-1];
                        neighbours[1] = logicTable[j - 1, k];
                        neighbours[2] = logicTable[j - 1, k + 1];

                        neighbours[3] = logicTable[j, k - 1];
                        neighbours[4] = logicTable[j, k + 1];

                        neighbours[5] = logicTable[0 , k - 1];
                        neighbours[6] = logicTable[0 , k];
                        neighbours[7] = logicTable[0 , k + 1];
                    }
                    if (k + 1 == height && 0 < j && j < width - 1)
                    {
                        neighbours[0] = logicTable[j - 1, k - 1];
                        neighbours[1] = logicTable[j - 1, k];
                        neighbours[2] = logicTable[j - 1, 0];

                        neighbours[3] = logicTable[j, k - 1];
                        neighbours[4] = logicTable[j, 0];

                        neighbours[5] = logicTable[j + 1, k - 1];
                        neighbours[6] = logicTable[j + 1, k];
                        neighbours[7] = logicTable[j + 1, 0];
                    }
                    if(j == 0 && k == 0)
                    {
                        neighbours[0] = logicTable[width-1, height-1];
                        neighbours[1] = logicTable[width-1, 0];
                        neighbours[2] = logicTable[width - 1, 1];

                        neighbours[3] = logicTable[0, height-1];
                        neighbours[4] = logicTable[0, 1];

                        neighbours[5] = logicTable[1, height - 1];
                        neighbours[6] = logicTable[1, 0];
                        neighbours[7] = logicTable[1, 1];
                    }
                    if (j == width - 1 && k == 0)
                    {
                        neighbours[0] = logicTable[width - 2, height - 1];
                        neighbours[1] = logicTable[width - 2, 0];
                        neighbours[2] = logicTable[width - 2, 1];

                        neighbours[3] = logicTable[width - 1, height - 1];
                        neighbours[4] = logicTable[width - 1, 1];

                        neighbours[5] = logicTable[0, height - 1];
                        neighbours[6] = logicTable[0, 0];
                        neighbours[7] = logicTable[0, 1];
                    }
                    if (j == width - 1  && k == height - 1)
                    {
                        neighbours[0] = logicTable[width - 2, height - 2];
                        neighbours[1] = logicTable[width - 2, height - 1];
                        neighbours[2] = logicTable[width - 2, 0];

                        neighbours[3] = logicTable[width - 1, height - 2];
                        neighbours[4] = logicTable[width - 1, 0];

                        neighbours[5] = logicTable[0, height - 2];
                        neighbours[6] = logicTable[0, height - 1];
                        neighbours[7] = logicTable[0, 0];
                    }
                    if (j == 0 && k == height - 1)
                    {
                        neighbours[0] = logicTable[width - 1, height - 2];
                        neighbours[1] = logicTable[width - 1, height - 1];
                        neighbours[2] = logicTable[width - 1, 0];

                        neighbours[3] = logicTable[0, height - 2];
                        neighbours[4] = logicTable[0, 0];

                        neighbours[5] = logicTable[1, height - 2];
                        neighbours[6] = logicTable[1, height - 1];
                        neighbours[7] = logicTable[1, 0];
                    }
                    if(j > 0 && k > 0 && j < width-1 && k < height-1)
                    {
                        neighbours[0] = logicTable[j - 1 , k-1];
                        neighbours[1] = logicTable[j - 1, k];
                        neighbours[2] = logicTable[j - 1, k+1];

                        neighbours[3] = logicTable[j, k-1];
                        neighbours[4] = logicTable[j, k+1];

                        neighbours[5] = logicTable[j + 1, k - 1];
                        neighbours[6] = logicTable[j + 1, k];
                        neighbours[7] = logicTable[j + 1, k + 1];
                    }

                    if (logicTable[j, k] == false && neighbours.Where(c => c).Count()==3)
                    {
                        logicTable2[j, k] = true;
                    }
                    if (logicTable[j, k] == true && neighbours.Where(c => c).Count() > 3)
                    {
                        logicTable2[j, k] = false;
                    }
                    if (logicTable[j, k] == true && neighbours.Where(c => c).Count() < 2)
                    {
                        logicTable2[j, k] = false;
                    }
                    if (logicTable[j, k] == true && neighbours.Where(c => c).Count() == 2 || neighbours.Where(c => c).Count() == 3)
                    {
                        logicTable2[j, k] = true;
                    }

                }               
            }
            
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            isWorking = false;
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; j++)
                {
                    {
                        logicTable[i, j] = false;
                    }
                }
            iteration = 0;
            licznik = 1;
            pictureBox1.Refresh();
            button1.Visible = true;

        }
    }
}
