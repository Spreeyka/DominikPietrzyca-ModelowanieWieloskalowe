using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RozrostZiarna
{

    public partial class Form1 : Form
    {
        string neighbourhoodType;
        double numberOfIteration;
        string condition;
        double radius;
        bool isWorking = true;
        List<Brush> colorList;
        int[] colorCounter;
        int iteration;
        int width;
        int nwidth;
        int height;
        int nheight;
        float startingX;
        float startingY;
        string pattern;
        bool needToReload;
        int max;
        int maxIndex;
        Graphics l;
        List<Element> elements;
        float squareLength;
        Thread thread;

        public Form1()
        {

            iteration = 0;
            needToReload = true;
            elements = new List<Element>();
            colorList = new List<Brush>();
            colorList.Add(new SolidBrush(Color.White));
            colorList.Add(new SolidBrush(Color.Red));
            colorList.Add(new SolidBrush(Color.Black)); //dlaczego radius jest jeden za duzo za drugą iteracja?
            colorList.Add(new SolidBrush(Color.Green));
            colorList.Add(new SolidBrush(Color.Blue));
            colorList.Add(new SolidBrush(Color.Beige));
            colorList.Add(new SolidBrush(Color.Violet));
            colorList.Add(new SolidBrush(Color.Tomato));
            colorCounter = new int[colorList.Count];
            for (int i = 0; i < colorList.Count; ++i)
            {
                colorCounter[i] = new int();
            }

            InitializeComponent();
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            isWorking = true;
            thread = new Thread(new ThreadStart(Start));
            thread.Start();
            
        }

        public void Start()
        {
            if (needToReload == true)
            {
                SetInitials();
                SetStartingPoint();
                InitializeElements();
                setInitialPattern();
                numberOfIteration = CalculateRadius();
                if (condition == "Periodyczne" && neighbourhoodType == "Moore")
                {
                    SetPeriodicMooreNeighbours();
                }
                if (condition == "Absorbujące" && neighbourhoodType == "Moore")
                {
                    SetAbsorbingMooreNeighbours();
                }
                if (condition == "Periodyczne" && neighbourhoodType == "Neumann")
                {
                    SetPeriodicNeumannNeighbours();
                }
                if (condition == "Absorbujące" && neighbourhoodType == "Neumann")
                {
                    SetAbsorbingNeumannNeighbours();
                }
                if (condition == "Periodyczne" && neighbourhoodType == "Pentagonalne")
                {
                    SetPeriodicPentNeighbours();
                }
                if (condition == "Absorbujące" && neighbourhoodType == "Pentagonalne")
                {
                    SetAbsorbingPentNeighbours();
                }
                if (condition == "Periodyczne" && neighbourhoodType == "Heksagonalne")
                {
                    SetPeriodicHexNeighbours();
                }
                if (condition == "Absorbujące" && neighbourhoodType == "Heksagonalne")
                {
                    SetAbsorbingHexNeighbours();
                }
                Draw();
                needToReload = false;
            }

            while (isWorking)
            {
                if (iteration != 0)
                {
                    for (int i = 0; i < numberOfIteration; i++)
                    {

                        Grow2();
                    }
                    Draw();
                }
                iteration++;
                Thread.Sleep(200); //dorobic button stop
            }
        }

        private void SetInitials()
        {
            if (widthTextBox.Text == "") width = 40; else width = Convert.ToInt32(widthTextBox.Text);
            if (heightTextBox.Text == "") height = 40; else height = Convert.ToInt32(widthTextBox.Text);
            if (patternComboBox.Text == "") pattern = "pojedyncze"; else pattern = patternComboBox.Text;
            if (conditionComboBox.Text == "") condition = "Periodyczne"; else condition = conditionComboBox.Text;
            if (neighbourhoodBox.Text == "") neighbourhoodType = "Moore"; else neighbourhoodType = neighbourhoodBox.Text;

        }

        private void SetStartingPoint()
        {
            if (width > height)
            {
                squareLength = pictureBox1.Height / width;
                startingX = pictureBox1.Width / 4;
            }
            else
            {
                squareLength = pictureBox1.Height / height;
                startingX = pictureBox1.Width / 4;
            }
        }

        private void InitializeElements()
        {
            float initialX = startingX;
            for (int i = 0; i < width * height; ++i)
            {
                if (i % width == 0 && i != 0)
                {
                    startingX = initialX;
                    startingY += squareLength;
                }
                elements.Add(new Element(colorList[0], startingX, startingY, squareLength));
                startingX += squareLength;

            }
            for (int i = 0; i < width * height; i++)
            {
                elements.ElementAt(i).neighbours = new List<Element>();
            }
        }

        public void setInitialPattern()
        {
            if (pattern == "pojedyncze") //works
            {
                int interval = height / 8;
                elements.ElementAt(elements.Count - (int)(width / 1.3) - 7* interval * width).brush = colorList[6];
                elements.ElementAt(elements.Count - (int)(width / 1.3 - 1) - 5 * interval * width).brush = colorList[1];
                elements.ElementAt(elements.Count - (int)(width / 1.3 + 1) - 3 * interval * width).brush = colorList[2];
                elements.ElementAt(elements.Count - (int)(width / 1.3 + 2) - 2 * interval * width).brush = colorList[4];
                elements.ElementAt(elements.Count - (int)(width / 1.3 + 1) -  2* interval * width).brush = colorList[5];
                elements.ElementAt(elements.Count - (int)(width / 1.3 - 2) - interval*width).brush = colorList[6];
                elements.ElementAt(elements.Count - (int)(width/1.3)).brush = colorList[7];
                elements.ElementAt(elements.Count - (width / 4 - 2) - 7 * interval * width).brush = colorList[1];
                elements.ElementAt(elements.Count - (width / 4 - 3) - 5 * interval * width).brush = colorList[3];
                elements.ElementAt(elements.Count - (width / 4 - 2) - 3 * interval * width).brush = colorList[5];
                elements.ElementAt(elements.Count - (width / 4 - 1) - 2 * interval * width).brush = colorList[2];
                elements.ElementAt(elements.Count - (width / 4 - 5) - interval * width).brush = colorList[3];
                elements.ElementAt(elements.Count - (width / 4 + 2)).brush = colorList[4];


            }
            if (pattern == "losowe")
            {
                Random rand = new Random();
                elements.ElementAt(rand.Next(width * height - 1)).brush = colorList[1];
                elements.ElementAt(rand.Next(width * height - 1)).brush = colorList[2]; //reuse rand!!
                elements.ElementAt(rand.Next(width * height - 1)).brush = colorList[3];
                elements.ElementAt(rand.Next(width * height - 1)).brush = colorList[4];

            }
            if (pattern == "test")
            {

                elements.ElementAt(elements.Count()/2 + width/2).brush = colorList[1];
                elements.ElementAt(elements.Count() / 2 + width / 2 + 5).brush = colorList[7];
                elements.ElementAt(width/2 * 10).brush = colorList[2];
                elements.ElementAt(width/2).brush = colorList[1];

            }
        }

        private void Draw()
        {
            l = pictureBox1.CreateGraphics();
            foreach (var item in elements)
            {
                l.FillRectangle(item.brush, item.startingX, item.startingY, item.squareLength, item.squareLength);
            }
        }
        private void SetPeriodicMooreNeighbours()
        {
            elements.ElementAt(0).neighbours.Add(elements.Last()); //lewy rog
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1));
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1 + width));

            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width));
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));

            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width + 1));
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));

            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 2)); //prawy rog
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));

            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 1));
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - width));
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(0));
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width));

            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width - 1)); //dolny lewy
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width - 1));

            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));

            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(1));

            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 2));

            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 1));

            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2 * width));
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width));
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(0));

            for (int i = width; i < height * width - width; i = i + width)
            {
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 2 * width - 1)); //lewy bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 2 * width + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
            }

            for (int i = 1; i < width - 1; i = i + 1)
            {
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) - 1 + i));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) - 1));  //dolny bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1)));

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) + 1));
            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width)); //cala reszta

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
            }
        }

        private void SetAbsorbingMooreNeighbours()
        {
            //lewy rog



            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));


            elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));


            //prawy rog
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));


            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));



            //dolny lewy


            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));


            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));


            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));


            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));



            for (int i = width; i < height * width - width; i = i + width)
            {


                //lewy bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

            }

            for (int i = 1; i < width - 1; i = i + 1)
            {

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));  //dolny bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
            }
        }



        public void Grow2()
        {
            foreach (var whiteElement in elements.Where(n => n.brush == colorList[0]))    //wszystkie białe elementy
            {

                if (whiteElement.neighbours.Any(m => m.brush != colorList[0])) //jesli bialy element ma jakiegokolwiek sasiada innego niz bialy
                {
                    whiteElement.isColored = true;
                    foreach (var neigh in whiteElement.neighbours.Where(b => b.brush != colorList[0]))
                    {
                        ++colorCounter[colorList.FindIndex(z => z == neigh.brush)];
                    }
                    max = colorCounter.Max();
                    maxIndex = Array.IndexOf(colorCounter, max);
                    Brush winnerColor = colorList.ElementAt(maxIndex);
                    whiteElement.brushChanged = winnerColor;
                    Array.Clear(colorCounter, 0, colorCounter.Count());

                }
            }
            foreach (var elementsToColor in elements.Where(x => x.isColored == true))
            {
                elementsToColor.brush = elementsToColor.brushChanged;
                elementsToColor.isColored = false;
            }
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            isWorking = false;
            needToReload = true;
            startingY = 0;
            iteration = 0;
            elements.Clear();
            pictureBox1.Refresh();
        }

        private void InsertElementClick(object sender, EventArgs e)
        {
            l = pictureBox1.CreateGraphics();
            MouseEventArgs e2 = (MouseEventArgs)e;
            Element clickedElement = elements.SingleOrDefault(n => (n.startingX < e2.X && n.startingX + n.squareLength >= e2.X) &&
            (n.startingY < e2.Y && n.startingY + n.squareLength >= e2.Y));

            switch (chooseColorComboBox.Text)
            {
                case "":
                    clickedElement.brush = colorList[1];
                    l.FillRectangle(colorList[1], clickedElement.startingX, clickedElement.startingY, clickedElement.squareLength, clickedElement.squareLength);
                    break;
                case "Czarny":
                    clickedElement.brush = colorList[2];
                    l.FillRectangle(colorList[2], clickedElement.startingX, clickedElement.startingY, clickedElement.squareLength, clickedElement.squareLength);
                    break;
                case "Zielony":
                    clickedElement.brush = colorList[3];
                    l.FillRectangle(colorList[3], clickedElement.startingX, clickedElement.startingY, clickedElement.squareLength, clickedElement.squareLength);
                    break;
                case "Niebieski":
                    clickedElement.brush = colorList[4];
                    l.FillRectangle(colorList[4], clickedElement.startingX, clickedElement.startingY, clickedElement.squareLength, clickedElement.squareLength);
                    break;
                case "Beżowy":
                    clickedElement.brush = colorList[5];
                    l.FillRectangle(colorList[5], clickedElement.startingX, clickedElement.startingY, clickedElement.squareLength, clickedElement.squareLength);
                    break;
                case "Pomidorowy":
                    clickedElement.brush = colorList[7];
                    l.FillRectangle(colorList[7], clickedElement.startingX, clickedElement.startingY, clickedElement.squareLength, clickedElement.squareLength);
                    break;
                case "Fioletowy":
                    clickedElement.brush = colorList[6];
                    l.FillRectangle(colorList[6], clickedElement.startingX, clickedElement.startingY, clickedElement.squareLength, clickedElement.squareLength);
                    break;
            }
        }
        public double CalculateRadius()
        {
            if (radiusBox.Text == "") radius = squareLength; else radius = Convert.ToDouble(radiusBox.Text);
            return Math.Floor((radius) / squareLength);
        }

        private void SetPeriodicNeumannNeighbours()
        {
            //lewy rog
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1));


            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width));
            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));


            elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));


            //prawy rog
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));


            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 1));
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));


            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(0));


            //dolny lewy
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));


            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));
            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));


            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));


            //dolny prawy
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));


            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 1));


            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width));


            for (int i = width; i < height * width - width; i = i + width)
            {

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));
                //lewy bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                //prawy bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));

            }

            for (int i = 1; i < width - 1; i = i + 1)
            {

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                //górny bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                //dolny bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1)));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {
                    //cala reszta
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));


                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));


                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

                }
            }
        }

        private void SetAbsorbingNeumannNeighbours()
        {    //lewy rog

            elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));


            elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));


            //prawy rog
            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));



            elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));



            //dolny lewy


            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));


            elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));


            //dolny prawy
            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));


            elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));



            for (int i = width; i < height * width - width; i = i + width)
            {
                //lewy bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {
                //prawy bok
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

            }

            for (int i = 1; i < width - 1; i = i + 1)
            {

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                //górny bok


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));


                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                //dolny bok

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {
                    //cala reszta
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));


                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));


                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));

                }
            }
        }

        private void SetPeriodicPentNeighbours()
        {
            Random rand = new Random();
            int random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                System.Diagnostics.Debug.WriteLine("dupa0");
                elements.ElementAt(0).neighbours.Add(elements.Last()); //lewy rog
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1 + width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));


            }

            if (random == 1)
            {
                //lewy rog
                System.Diagnostics.Debug.WriteLine("dupa1");
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width + 1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));
            }
            if (random == 2)
            {
                //lewy rog
                System.Diagnostics.Debug.WriteLine("dupa2");
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1 + width));


                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));


                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));
            }
            if (random == 3)
            {
                System.Diagnostics.Debug.WriteLine("dupa3");
                elements.ElementAt(0).neighbours.Add(elements.Last()); //lewy rog
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1));


                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width + 1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
            }
            random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 2)); //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

            }
            if (random == 1)
            {
                //prawy rog

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(0));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width));
            }
            if (random == 2)
            {
                //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));


                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(0));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width));
            }
            if (random == 3)
            {
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 2)); //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 1));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(0));
            }
            random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width - 1)); //dolny lewy
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width - 1));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));

            }
            if (random == 1)
            {
                //dolny lewy
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(1));
            }
            if (random == 2)
            {
                //dolny lewy
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width - 1));


                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));


                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(1));
            }
            if (random == 3)
            {
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width - 1)); //dolny lewy
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }
            random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 2));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 1));

            }
            if (random == 1)
            {

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 1));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2 * width));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(0));
            }
            if (random == 2)
            {

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 2));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 1));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(0));
            }
            if (random == 3)
            {
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2 * width));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width));
            }

            for (int i = width; i < height * width - width; i = i + width)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 2 * width - 1)); //lewy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 2)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 2 * width - 1)); //lewy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 3)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 2 * width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 2)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 3)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 2 * width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                }
            }

            for (int i = 1; i < width - 1; i = i + 1)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) - 1 + i));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 2)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 3)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) - 1 + i));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) - 1));  //dolny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1)));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1)));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) + 1));
                }
                if (random == 2)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) - 1));  //dolny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1)));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) + 1));
                }
                if (random == 3)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {

                    int random2 = rand.Next(0, 4);


                    if (random2 == 0)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width)); //cala reszta

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    }
                    if (random2 == 1)
                    {

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                    }
                    if (random2 == 2)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width)); //cala reszta

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                    }
                    if (random2 == 3)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    }
                }

            }



        }
        private void SetAbsorbingPentNeighbours()
        {
            Random rand = new Random();
            int random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));
            }

            if (random == 1)
            {
                //lewy rog

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));
            }
            if (random == 2)
            {
                //lewy rog

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));


                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));
            }
            if (random == 3)
            {
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
            }
            random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                 //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

            }
            if (random == 1)
            {
                //prawy rog

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

            }
            if (random == 2)
            {
                //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

            }
            if (random == 3)
            {
                 //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
            }
            random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                //dolny lewy

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));

            }
            if (random == 1)
            {
                //dolny lewy
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }
            if (random == 2)
            {
                //dolny lewy

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }
            if (random == 3)
            {
                 //dolny lewy

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }
            random = rand.Next(0, 4); // od 0 to 3 losowanie

            if (random == 0)
            {
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));

            }
            if (random == 1)
            {

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));

            }
            if (random == 2)
            {

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));

            }
            if (random == 3)
            {
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));

            }

            for (int i = width; i < height * width - width; i = i + width)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 2)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 3)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 2)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 3)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                }
            }

            for (int i = 1; i < width - 1; i = i + 1)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 2)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
                if (random == 3)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));


                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {
                random = rand.Next(0, 4);

                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 2)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));


                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 3)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {

                    int random2 = rand.Next(0, 4);


                    if (random2 == 0)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width)); //cala reszta

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    }
                    if (random2 == 1)
                    {

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                    }
                    if (random2 == 2)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width)); //cala reszta

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                    }
                    if (random2 == 3)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    }
                }
            }

        }
        private void SetPeriodicHexNeighbours()
        {
            Random random3 = new Random();
            int random = random3.Next(0, 2);
            if (random == 0)
            {

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1 + width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width + 1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
            }
            if (random == 1)
            {
                elements.ElementAt(0).neighbours.Add(elements.Last()); //lewy rog
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width - 1));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));
            }

            random = random3.Next(0, 2);

            if (random == 0)
            {
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(0));
            }
            if (random == 1)
            {
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 2)); //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(0));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width));
            }

            random = random3.Next(0, 2);

            if (random == 0)
            {
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width - 1));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }
            if (random == 1)
            {
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width - 1)); //dolny lewy
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(1));
            }

            random = random3.Next(0, 2);

            if (random == 0)
            {
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width - 1));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(0));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1));
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }
            if (random == 1)
            {
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width - 1));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width));
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(0));
            }
            for (int i = width; i < height * width - width; i = i + width)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 2 * width - 1)); //lewy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }   
                if (random == 1)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 2 * width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                }
                if (random == 1)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }

            for (int i = 1; i < width - 1; i = i + 1)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 1)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) - 1 + i));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(width * (height - 1) + i));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) - 1));  //dolny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1)));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 1)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1)));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width * (height - 1) + 1));
                }
            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {
                    random = random3.Next(0, 2);
                    if (random == 0)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width)); //cala reszta

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    }
                    if (random == 1)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                    }
                }
            }
        }
        private void SetAbsorbingHexNeighbours()
        {   
            Random random3 = new Random();
            int random = random3.Next(0, 2);
            if (random == 0)
            {
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
            }
            if (random == 1)
            {

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width));

                elements.ElementAt(0).neighbours.Add(elements.ElementAt(1));
                elements.ElementAt(0).neighbours.Add(elements.ElementAt(width + 1));
            }

            random = random3.Next(0, 2);

            if (random == 0)
            {
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 2));

 //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

            }
            if (random == 1)
            {
                 //prawy rog
                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width - 2));

                elements.ElementAt(width - 1).neighbours.Add(elements.ElementAt(width * 2 - 1));

            }

            random = random3.Next(0, 2);

            if (random == 0)
            {

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width + 1)); //dolny lewy
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }
            if (random == 1)
            {

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - width + 1));
            }

            random = random3.Next(0, 2);

            if (random == 0)
            {
                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 1));

                elements.ElementAt(width * height - width).neighbours.Add(elements.ElementAt(width * height - 2 * width));

            }
            if (random == 1)
            {
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 2)); //dolny prawy
                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - 2));

                elements.ElementAt(width * height - 1).neighbours.Add(elements.ElementAt(width * height - width - 1));
            }
            for (int i = width; i < height * width - width; i = i + width)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                     //lewy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 1)
                {

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
            }

            for (int i = 2 * width - 1; i < height * width - 1; i = i + width)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //prawy bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
                if (random == 1)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                }
            }

            for (int i = 1; i < width - 1; i = i + 1)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width - 1));  //górny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 1)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                }
            }

            for (int i = width * height - width + 1; i < width * height - 1; i = i + 1)
            {
                random = random3.Next(0, 2);
                if (random == 0)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                    //dolny bok

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
                if (random == 1)
                {
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));

                    elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                }
            }
            for (int j = 1; j < height - 1; j++)
            {
                for (int i = j * width + 1; i < (j + 1) * width - 1; i++)
                {
                    random = random3.Next(0, 2);
                    if (random == 0)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 + width)); //cala reszta

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                    }
                    if (random == 1)
                    {
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1 - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - 1));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i - width));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width));

                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + 1));
                        elements.ElementAt(i).neighbours.Add(elements.ElementAt(i + width + 1));
                    }
                }
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            isWorking = false;
            thread.Join();
        }
    }
}
