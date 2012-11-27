﻿using System;
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

        public Corner selectedCorner;

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

                if (normals.Count > 0)
                {
                    hasNormals = true;
                }

                buildCornerInformation();
            }

            selectedCorner = triangles[0];
        }


        public void draw()
        {
            //Draw Selected here
            GL.Begin(BeginMode.Lines);
            {
                //Draw One Line here
                GL.Color4( Config.convertSettingToFloat("colours", "cornerSelectedColour_red"), Config.convertSettingToFloat("colours", "cornerSelectedColour_green"), Config.convertSettingToFloat("colours", "cornerSelectedColour_blue"), Config.convertSettingToFloat("colours", "cornerSelectedColour_alpha"));
                GL.Vertex3(selectedCorner.vertex.X, selectedCorner.vertex.Y, selectedCorner.vertex.Z);
                GL.Vertex3(selectedCorner.next.vertex.X, selectedCorner.next.vertex.Y, selectedCorner.next.vertex.Z);

                //Draw the other line
                GL.Color4(Config.convertSettingToFloat("colours", "cornerSelectedColour_red"), Config.convertSettingToFloat("colours", "cornerSelectedColour_green"), Config.convertSettingToFloat("colours", "cornerSelectedColour_blue"), Config.convertSettingToFloat("colours", "cornerSelectedColour_alpha"));
                GL.Vertex3(selectedCorner.vertex.X, selectedCorner.vertex.Y, selectedCorner.vertex.Z);
                GL.Vertex3(selectedCorner.prev.vertex.X, selectedCorner.prev.vertex.Y, selectedCorner.prev.vertex.Z);
            }
            GL.End();


            GL.Begin(BeginMode.Triangles);
            {
                if (hasNormals && Config.convertSettingToBool("model", "lighting"))
                {
                    GL.Enable(EnableCap.Lighting);
                    GL.Enable(EnableCap.Light0);
                    GL.Light(LightName.Light0, LightParameter.Position, new OpenTK.Vector4(Config.convertSettingToFloat("lights", "one_x"), Config.convertSettingToFloat("lights", "one_y"), Config.convertSettingToFloat("lights", "one_z"), Config.convertSettingToFloat("lights", "one_w")));
                    GL.Light(LightName.Light0, LightParameter.Ambient, new OpenTK.Vector4(Config.convertSettingToFloat("colours", "light_one_ambient_red"), Config.convertSettingToFloat("colours", "light_one_ambient_green"), Config.convertSettingToFloat("colours", "light_one_ambient_blue"), Config.convertSettingToFloat("colours", "light_one_ambient_alpha")));
                    GL.Light(LightName.Light0, LightParameter.Diffuse, new OpenTK.Vector4(Config.convertSettingToFloat("colours", "light_one_diffuse_red"), Config.convertSettingToFloat("colours", "light_one_diffuse_green"), Config.convertSettingToFloat("colours", "light_one_diffuse_blue"), Config.convertSettingToFloat("colours", "light_one_diffuse_alpha")));
                    GL.Light(LightName.Light0, LightParameter.Specular, new OpenTK.Vector4(Config.convertSettingToFloat("colours", "light_one_specular_red"), Config.convertSettingToFloat("colours", "light_one_specular_green"), Config.convertSettingToFloat("colours", "light_one_specular_blue"), Config.convertSettingToFloat("colours", "light_one_specular_alpha"))); 

                    for (int index = 0; index < triangles.Count; index += 3)
                    {
                        if (triangles[index].textured)
                        {
                            GL.Normal3(normals[triangles[index + 0].vertexIndex].X, normals[triangles[index + 0].vertexIndex].Y, normals[triangles[index + 0].vertexIndex].Z);
                            GL.TexCoord2(triangles[index + 0].textureCoordinates.X, triangles[index + 0].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 0].vertex.X, triangles[index + 0].vertex.Y, triangles[index + 0].vertex.Z);

                            GL.Normal3(normals[triangles[index + 1].vertexIndex].X, normals[triangles[index + 1].vertexIndex].Y, normals[triangles[index + 1].vertexIndex].Z);
                            GL.TexCoord2(triangles[index + 1].textureCoordinates.X, triangles[index + 1].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 1].vertex.X, triangles[index + 1].vertex.Y, triangles[index + 1].vertex.Z);

                            GL.Normal3(normals[triangles[index + 2].vertexIndex].X, normals[triangles[index + 2].vertexIndex].Y, normals[triangles[index + 2].vertexIndex].Z);
                            GL.TexCoord2(triangles[index + 2].textureCoordinates.X, triangles[index + 2].textureCoordinates.Y);
                            GL.Vertex3(triangles[index + 2].vertex.X, triangles[index + 2].vertex.Y, triangles[index + 2].vertex.Z);
                        }
                        else
                        {
                            GL.Normal3(normals[triangles[index + 0].vertexIndex].X, normals[triangles[index + 0].vertexIndex].Y, normals[triangles[index + 0].vertexIndex].Z);
                            GL.Color4(triangles[index + 0].colour.red, triangles[index + 0].colour.green, triangles[index + 0].colour.blue, triangles[index + 0].colour.alpha);
                            GL.Vertex3(triangles[index + 0].vertex.X, triangles[index + 0].vertex.Y, triangles[index + 0].vertex.Z);

                            GL.Normal3(normals[triangles[index + 1].vertexIndex].X, normals[triangles[index + 1].vertexIndex].Y, normals[triangles[index + 1].vertexIndex].Z);
                            GL.Color4(triangles[index + 1].colour.red, triangles[index + 1].colour.green, triangles[index + 1].colour.blue, triangles[index + 1].colour.alpha);
                            GL.Vertex3(triangles[index + 1].vertex.X, triangles[index + 1].vertex.Y, triangles[index + 1].vertex.Z);

                            GL.Normal3(normals[triangles[index + 2].vertexIndex].X, normals[triangles[index + 2].vertexIndex].Y, normals[triangles[index + 2].vertexIndex].Z);
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
            //Iterate through Corners and flip the winding order.
            
            for (int index = 0; index < triangles.Count; index += 3)
            {
                Corner temp = triangles[index + 0];
                triangles[index + 0] = triangles[index + 2];
                triangles[ index + 2] = temp;
            }
        }
        
        private void buildCornerInformation()
        {
            //Build Corner Table info like next, previous, opposite, left and right.

            //Build Corner Table next and previous
            for (int index = 0; index < triangles.Count; index += 3)
            {

                triangles[index + 0].next = triangles[index + 1];
                triangles[index + 0].prev = triangles[index + 2];

                triangles[index + 1].next = triangles[index + 2];
                triangles[index + 1].prev = triangles[index + 0];

                triangles[index + 2].next = triangles[index + 0];
                triangles[index + 2].prev = triangles[index + 1];
            }

            //Build Corner Table Opposite
            for (int index = 0; index < triangles.Count; index += 1)
            {
                
                for (int indexToo = 0; indexToo < triangles.Count; indexToo += 1)
                {
                    if (triangles[ index].next.vertexIndex == triangles[ indexToo].prev.vertexIndex)
                    {
                        if (triangles[ index].prev.vertexIndex == triangles[ indexToo].next.vertexIndex)
                        {
                            triangles[ index].opposite = triangles[ indexToo];
                        }
                    }
                }
            }


            //Build Corner Table Opposite
            for (int index = 0; index < triangles.Count; index += 1)
            {
                        triangles[index].right = triangles[index].prev.opposite;
                        triangles[index].left = triangles[index].next.opposite ;
            }

        }
    }
}