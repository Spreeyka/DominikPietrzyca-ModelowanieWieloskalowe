using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekrystalizacja   
{
    class Element
    {
        public double dislocationDensity { get; set; }
        public bool isRecristalized { get; set; } = false;
        public bool wasRecristalized { get; set; } = false;
        public Brush brush { get; set; }
        public Brush brushChanged { get; set; }
        public float startingX { get; set; }
        public float startingY { get; set; }
        public float squareLength { get; set; }
        public List<Element> neighbours;
        public bool isColored { get; set; } = false;
        public bool isChecked { get; set; } = false;
        public int energy { get; set; } = 0;
            
        public Element(Brush brush, float startingX, float startingY, float squareLength)
        {
            this.brush = brush;
            this.startingX = startingX;
            this.startingY = startingY;
            this.squareLength = squareLength;
            
        }
        public void SetEnergy()
        {
            foreach (Element el in neighbours.Where(n=>n.brush != brush))
            {
                ++energy;
            }
        }
    }   
}
