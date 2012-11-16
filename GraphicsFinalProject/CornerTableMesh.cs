using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicsFinalProject
{
    public class CornerTableMesh
    {
        public List<OpenTK.Vector3> vertices;
        public List<Colour4f> colours;

        public List<Corner> triangles;

        public CornerTableMesh(String fileName)
        {
            this.vertices = new List<OpenTK.Vector3>();
            this.colours = new List<Colour4f>();

            this.triangles = new List<Corner>();
        }
    }
}
