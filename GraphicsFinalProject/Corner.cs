using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicsFinalProject
{
    public class Corner
    {
        //Basic Stuff
        public int vertexIndex;
        public bool visited;

        public Colour4f colour;
        public bool textured;
        public OpenTK.Vector2 textureCoordinates;

        public OpenTK.Vector3 vertex;

        //Corner Table Implementation details. Nasty stuff
        public Corner next;
        public Corner prev;

        public Corner opposite;
        public Corner right;
        public Corner left;

        public int triangleNumber;

        public Corner(int vertexIndex, OpenTK.Vector3 vertex, Colour4f colour, int triangleNumber)
        {
            this.vertexIndex = vertexIndex;
            this.triangleNumber = triangleNumber;
            this.visited = false;

            this.colour = new Colour4f(colour);
            this.textured = false;
            this.textureCoordinates = new OpenTK.Vector2(0.0f, 0.0f);

            this.vertex = vertex;
        }
    }
}
