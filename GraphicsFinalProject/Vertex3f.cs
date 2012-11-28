using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicsFinalProject
{
    public class Vertex3f
    {
        public float x;
        public float y;
        public float z;

        public bool visited;

        public OpenTK.Vector3 normal;

        public Vertex3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;

            this.visited = false;
        }

        public Vertex3f(float x, float y, float z, float normalX, float normalY, float normalZ) : this(x, y, z)
        {
            this.normal = new OpenTK.Vector3(normalX, normalY, normalZ);
        }

        public Vertex3f(OpenTK.Vector3 that) : this(that.X, that.Y, that.Z)
        {
        }

        public Vertex3f(OpenTK.Vector3 position, OpenTK.Vector3 normal) : this(position.X, position.Y, position.Z, normal.X, normal.Y, normal.Z)
        {
        }

        public Vertex3f(Vertex3f that) : this(that.x, that.y, that.z)
        {
            if (that.normal != null)
            {
                this.normal = new OpenTK.Vector3(that.normal.X, that.normal.Y, that.normal.Z);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (obj is Vertex3f)
            {
                return this == (Vertex3f)obj;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(Vertex3f one, Vertex3f two)
        {
            return (one.x == two.x && one.y == two.y && one.z == two.z);
        }

        public static bool operator !=(Vertex3f one, Vertex3f two)
        {
            return !(one == two);
        }
    }
}
