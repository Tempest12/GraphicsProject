using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicsFinalProject
{
    public class Colour4f
    {
        public float red;
        public float green;
        public float blue;
        public float alpha;

        public Colour4f(float red, float green, float blue, float alpha)
        {
            this.red = red;
            this.blue = blue;
            this.green = green;
            this.alpha = alpha;
        }

        public Colour4f(float red, float green, float blue) : this(red, green, blue, 1.0f)
        {            
        }
    }
}
