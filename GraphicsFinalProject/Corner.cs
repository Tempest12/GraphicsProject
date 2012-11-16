using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicsFinalProject
{
    public class Corner
    {
        //Basic Stuff
        public int index;
        public bool visited;

        public OpenTK.Vector3 vertice;

        //Corner Table Implementation details. Nasty stuff
        public Corner next;
        public Corner prev;

        public Corner opposite;
        public Corner right;
        public Corner left;

        public int triangleNumber;
    }
}
