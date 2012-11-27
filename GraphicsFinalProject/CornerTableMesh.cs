using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//3rd Party Libs
using OpenTK.Graphics.OpenGL;

namespace GraphicsFinalProject
{
    public class CornerTableMesh
    {
        public List<OpenTK.Vector3> vertices;
        public List<OpenTK.Vector3> normals;

        public List<Corner> triangles;

        public bool hasNormals = false;

        public CornerTableMesh(String fileName)
        {
            this.vertices = new List<OpenTK.Vector3>();
            this.normals = new List<OpenTK.Vector3>();

            this.triangles = new List<Corner>();

            if (fileName == null)
            {
                MainMethod.reportError("File name passed to Corner Table is null");
            }
            else
            {
                PolygonFileParser.parsePLYFile(fileName, this);
                buildCornerInformation();
            }
        }

        public void draw()
        {
            GL.Begin(BeginMode.Triangles);
            {
                if (hasNormals && Config.convertSettingToBool("model", "lighting"))
                {
                    for (int index = 0; index < triangles.Count; index += 3)
                    {
                        if (triangles[index].textured)
                        {
                            GL.Normal3(normals[index + 0].X, normals[index + 0].Y, normals[index + 0].Z);
                            GL.TexCoord2(triangles[index + 0].textureCoordinates.X, triangles[index + 0].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 0].vertex.X, triangles[index + 0].vertex.Y, triangles[index + 0].vertex.Z);

                            GL.Normal3(normals[index + 1].X, normals[index + 1].Y, normals[index + 1].Z);
                            GL.TexCoord2(triangles[index + 1].textureCoordinates.X, triangles[index + 1].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 1].vertex.X, triangles[index + 1].vertex.Y, triangles[index + 1].vertex.Z);

                            GL.Normal3(normals[index + 2].X, normals[index + 2].Y, normals[index + 2].Z);
                            GL.TexCoord2(triangles[index + 2].textureCoordinates.X, triangles[index + 2].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 2].vertex.X, triangles[index + 2].vertex.Y, triangles[index + 2].vertex.Z);
                        }
                        else
                        {
                            GL.Normal3(normals[index + 0].X, normals[index + 0].Y, normals[index + 0].Z);
                            GL.Color4(triangles[index + 0].colour.red, triangles[index + 0].colour.green, triangles[index + 0].colour.blue, triangles[index + 0].colour.alpha);
                            GL.Vertex3(triangles[index + 0].vertex.X, triangles[index + 0].vertex.Y, triangles[index + 0].vertex.Z);

                            GL.Normal3(normals[index + 1].X, normals[index + 1].Y, normals[index + 1].Z);
                            GL.Color4(triangles[index + 1].colour.red, triangles[index + 1].colour.green, triangles[index + 1].colour.blue, triangles[index + 1].colour.alpha);
                            GL.Vertex3(triangles[index + 1].vertex.X, triangles[index + 1].vertex.Y, triangles[index + 1].vertex.Z);

                            GL.Normal3(normals[index + 2].X, normals[index + 2].Y, normals[index + 2].Z);
                            GL.Color4(triangles[index + 2].colour.red, triangles[index + 2].colour.green, triangles[index + 2].colour.blue, triangles[index + 2].colour.alpha);
                            GL.Vertex3(triangles[index + 2].vertex.X, triangles[index + 2].vertex.Y, triangles[index + 2].vertex.Z);
                        }
                    }
                }
                else
                {
                    for (int index = 0; index < triangles.Count; index += 3)
                    {
                        if (triangles[index].textured)
                        {
                            GL.TexCoord2(triangles[index + 0].textureCoordinates.X, triangles[index + 0].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 0].vertex.X, triangles[index + 0].vertex.Y, triangles[index + 0].vertex.Z);

                            GL.TexCoord2(triangles[index + 1].textureCoordinates.X, triangles[index + 1].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 1].vertex.X, triangles[index + 1].vertex.Y, triangles[index + 1].vertex.Z);

                            GL.TexCoord2(triangles[index + 2].textureCoordinates.X, triangles[index + 2].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 2].vertex.X, triangles[index + 2].vertex.Y, triangles[index + 2].vertex.Z);
                        }
                        else
                        {
                            GL.Color4(triangles[index + 0].colour.red, triangles[index + 0].colour.green, triangles[index + 0].colour.blue, triangles[index + 0].colour.alpha);
                            GL.Vertex3(triangles[index + 0].vertex.X, triangles[index + 0].vertex.Y, triangles[index + 0].vertex.Z);

                            GL.Color4(triangles[index + 1].colour.red, triangles[index + 1].colour.green, triangles[index + 1].colour.blue, triangles[index + 1].colour.alpha);
                            GL.Vertex3(triangles[index + 1].vertex.X, triangles[index + 1].vertex.Y, triangles[index + 1].vertex.Z);

                            GL.Color4(triangles[index + 2].colour.red, triangles[index + 2].colour.green, triangles[index + 2].colour.blue, triangles[index + 2].colour.alpha);
                            GL.Vertex3(triangles[index + 2].vertex.X, triangles[index + 2].vertex.Y, triangles[index + 2].vertex.Z);
                        }
                    }
                }
            }
            GL.End();
        }

        public void flipWindingOrder()
        {
            //Iterate throuh Corners and flip the winding order.
        }

        private void buildCornerInformation()
        {
            //Build Corner Table info like next, previous, opposite, left and right.
        }
    }
}