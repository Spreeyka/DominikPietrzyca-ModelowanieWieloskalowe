using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonteCarlo
{
    public partial class Form1 : Form
    {
        int randomColorIndex;
        List<int> indexes = new List<int>();
        double randomProbability;
        Brush initialColor;
        int initialEnergy;
        Brush testColor;
        int deltaEnergy;
        int randomIndex;
        Element randomElement;
        Random random;
        double kt;
        List<Brush> neighColors;
        string neighbourhoodType;
        double numberOfIteration;
        string condition;
        //double radius;
        List<Brush> colorList;
        int[] colorCounter;
        int iteration;
        //int iteration2 = 0;
        int width;
        //int nwidth;
        int height;
        //int nheight;
        float startingX;
        float startingY;
        //string pattern;
        bool needToReload;
        int max;
        int maxIndex;
        Graphics l;
        List<Element> elements;
        float squareLength;
        public Form1()
        {
            deltaEnergy = 0;
            random = new Random();
            neighColors = new List<Brush>();
            iteration = 0;
            //iteration2 = 0;
            needToReload = true;
            elements = new List<Element>();
            colorList = new List<Brush>();
            colorList.Add(new SolidBrush(Color.White));
            colorList.Add(new SolidBrush(Color.Red));
            colorList.Add(new SolidBrush(Color.Black));
            colorList.Add(new SolidBrush(Color.Green));
            colorList.Add(new SolidBrush(Color.Blue));
            colorList.Add(new SolidBrush(Color.Beige));
            colorList.Add(new SolidBrush(Color.Khaki));
            colorList.Add(new SolidBrush(Color.DarkMagenta));
            colorList.Add(new SolidBrush(Color.AliceBlue));
            colorList.Add(new SolidBrush(Color.DodgerBlue));
            colorList.Add(new SolidBrush(Color.Crimson));
            colorList.Add(new SolidBrush(Color.Aquamarine));
            colorList.Add(new SolidBrush(Color.DarkGreen));
            colorList.Add(new SolidBrush(Color.Chocolate));
            colorList.Add(new SolidBrush(Color.DarkSlateBlue));
            colorList.Add(new SolidBrush(Color.Lime));
            colorList.Add(new SolidBrush(Color.MistyRose));
            colorCounter = new int[colorList.Count];
            for (int i = 0; i < colorList.Count; ++i)
            {
                colorCounter[i] = new int();
            }
            InitializeComponent();
        }
        private void EnergyButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            foreach (var element in elements)
            {
                element.SetEnergy();
            }
            foreach (var element in elements)
            {
                if (element.energy == 0)
                {

                    element.brush = colorList[4]; //niebieski

                }
                else
                {
                    element.brush = colorList[3]; //zielony
                }

                
            }
            Draw();
        } 
        

        private void StartButtonClick(object sender, EventArgs e)
        {          
            if (iteration != 0)
            {
                MessageBox.Show(kt.ToString());
                for (int i = 0; i < numberOfIteration; i++)
                {
                    Iterate();
                    
                }               
            }
            Draw();
        }

        private void SetInitials() 
        {
            if (widthTextBox.Text == "") width = 100; else width = Convert.ToInt32(widthTextBox.Text);
            if (heightTextBox.Text == "") height = 100; else height = Convert.ToInt32(widthTextBox.Text);
            if (conditionComboBox.Text == "") condition = "Periodyczne"; else condition = conditionComboBox.Text;
            if (neighbourhoodBox.Text == "") neighbourhoodType = "Moore"; else neighbourhoodType = neighbourhoodBox.Text;
            if (ktBox.Text == "") kt = 0.6; else kt = Double.Parse(ktBox.Text);
            if (numberOfIterationBox.Text == "") numberOfIteration = 1; else numberOfIteration = Convert.ToInt32(numberOfIterationBox.Text);

        }

        public void Iterate()
        {
            
            for (int i = 0; i < elements.Count; i++) //dodajemy mozliwe indeksy
            {
                indexes.Add(i); //do przeniesienia w gore
            }

            

            for (int i = 0; i < elements.Count; i++)
            {
                randomIndex = random.Next(0, indexes.Count);
                randomElement = elements.ElementAt(indexes.ElementAt(randomIndex));
                randomElement.SetEnergy();

                if (randomElement.energy != 0)
                {
                    initialColor = randomElement.brush;                  
                    initialEnergy = randomElement.energy;

                    foreach (var diffColorElement in randomElement.neighbours)
                    {
                        if(neighColors.Exists(n=>n == diffColorElement.brush)==false)
                        {
                            neighColors.Add(diffColorElement.brush);
                        }
                        
                    }
                    


                    randomColorIndex = random.Next(0, neighColors.Count);
                    testColor = neighColors.ElementAt(randomColorIndex);
                    randomElement.brush = testColor;
                    randomElement.energy = 0;                               
                    randomElement.SetEnergy();
                    deltaEnergy = randomElement.energy - initialEnergy;

                    if (deltaEnergy > 0)
                    {
                        randomProbability = random.NextDouble();
                        if (randomProbability > Math.Exp(-(deltaEnergy / kt)))
                        {

                            randomElement.brush = initialColor;
                            randomElement.energy = initialEnergy;
                        }
                    }
                    neighColors.Clear();
                    
                }
                indexes.Remove(indexes.ElementAt(randomIndex));
                randomElement.energy = 0;
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

        public void setInitialPattern()
        {
            int drawnColorIndex;
            Random randomColor = new Random();
            int dy = (height / 10);
            int dx = (width / 10);
            int startingP = 5 * width + width/(dx*2);
            for (int j = 1; j <= dy; j++)
            {
                for (int i = 1; i <= dx; i++)
                {
                    drawnColorIndex = randomColor.Next(1, colorList.Count());
                    elements.ElementAt(startingP).brush = colorList.ElementAt(drawnColorIndex);
                    startingP += width / dx;
                }
                startingP = 5 * width + width / (dx * 2) + j * 10*width;
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
        
        private void ResetButton_Click(object sender, EventArgs e)
        {
            needToReload = true;
            startingY = 0;
            iteration = 0;
            elements.Clear();
            pictureBox1.Refresh();
        }

        private void GrowButton_Click(object sender, EventArgs e)
        {
            if (needToReload == true)
            {
                SetInitials();
                SetStartingPoint();
                InitializeElements();
                setInitialPattern();
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
                Draw();
                needToReload = false;
            }



            if (iteration != 0)
            {
                for (int i = 0; i < 100; i++)
                {
                    Grow2();
                }
            }          
            iteration = 1;
            Draw();
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

    }
}

