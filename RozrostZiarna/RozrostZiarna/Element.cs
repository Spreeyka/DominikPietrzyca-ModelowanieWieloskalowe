using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RozrostZiarna
{

    class Element
    {
        static int id=0;
        public Brush brush { get; set; }
        public Brush brushChanged { get; set; }
        public float startingX { get; set; }
        public float startingY { get; set; }
        public float squareLength { get; set; }
        public List<Element> neighbours;
        public bool isColored { get; set; } = false;

        public Element(Brush brush, float startingX, float startingY, float squareLength)
        {
            ++id;
            this.brush = brush;

            this.startingX = startingX;
            this.startingY = startingY;
            this.squareLength = squareLength;
            //this.neighboursId = neighboursId;
        }
       
    }
}
