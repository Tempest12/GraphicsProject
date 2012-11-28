using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace GraphicsFinalProject
{
    public class PolygonFileParser
    {
        public static void parsePLYFile(String fileName, CornerTableMesh container)
        {
            int lineNumber = 0;
            StreamReader reader = null;
            string temp = null;

            if (!fileName.EndsWith(".ply"))
            {
                MainMethod.reportError("PolyGonFileParser.parsePLYFile : Filename: \"" + fileName + "\" does not appear to be a polygon file");
            } 

            try
            {
                reader = new StreamReader(fileName);
            }
            catch (FileNotFoundException fnfe)
            {
               MainMethod.reportError("PolygonFilePasrer.parsePLYFile : Error could not find file. Exception message:" + fnfe.Message);
               return;
            }

            PLYFileHeader header = new PLYFileHeader();
            lineNumber = parseHeader(reader, header);

            if (lineNumber < 0)
            {
                return;
            }

            List<Colour4f> colourList = new List<Colour4f>();
            OpenTK.Vector3 tempPosition = new OpenTK.Vector3(0.0f, 0.0f, 0.0f);
            OpenTK.Vector3 tempNormal = new OpenTK.Vector3(0.0f, 0.0f, 0.0f);
            Colour4f tempColour = new Colour4f(0.0f, 0.0f, 0.0f, 0.0f);
            int vertexIndex = 0;
            int faceIndex = 0;
            int triangleIndex = 0;

            while (reader.Peek() > -1)
            {
                temp = getNewLine(reader, ref lineNumber);
                //First Parse Vertices
                if (vertexIndex < header.vertexCount)
                {
                    if (String.IsNullOrWhiteSpace(temp))
                    {
                        continue;
                    }

                    vertexIndex++;

                    tempPosition.X = 0.0f;
                    tempPosition.Y = 0.0f;
                    tempPosition.Z = 0.0f;

                    tempColour.red = Config.convertSettingToFloat("colours", "corner_default_red");
                    tempColour.green = Config.convertSettingToFloat("colours", "corner_default_green");
                    tempColour.blue = Config.convertSettingToFloat("colours", "corner_default_blue");
                    tempColour.alpha = Config.convertSettingToFloat("colours", "corner_default_alpha");

                    string[] pieces = temp.Split(' ');

                    if (header.xIndex >= 0)
                    {
                        tempPosition.X = float.Parse(pieces[header.xIndex]);   
                    }
                    if (header.yIndex >= 0)
                    {
                        tempPosition.Y = float.Parse(pieces[header.yIndex]);
                    }
                    if (header.zIndex >= 0)
                    {
                        tempPosition.Z = float.Parse(pieces[header.zIndex]);
                    }
                    if(header.normalXIndex >= 0)
                    {
                        tempNormal.X = float.Parse(pieces[header.normalXIndex]);
                    }
                    if(header.normalYIndex >= 0)
                    {
                        tempNormal.Y = float.Parse(pieces[header.normalYIndex]);
                    }
                    if(header.normalZIndex >= 0)
                    {
                        tempNormal.Z = float.Parse(pieces[header.normalZIndex]);
                    }
                    if (header.redIndex >= 0)
                    {
                        tempColour.red = float.Parse(pieces[header.redIndex]);
                    }
                    if (header.greenIndex >= 0)
                    {
                        tempColour.green = float.Parse(pieces[header.greenIndex]);
                    }
                    if (header.blueIndex >= 0)
                    {
                        tempColour.blue = float.Parse(pieces[header.blueIndex]);
                    }
                    if (header.alphaIndex >= 0)
                    {
                        tempColour.alpha = float.Parse(pieces[header.alphaIndex]);
                    }

                    if (header.normalXIndex != -1 && header.normalYIndex != -1 && header.normalZIndex != -1)
                    {
                        container.vertices.Add(new Vertex3f(tempPosition, tempNormal));
                    }
                    else
                    {
                        container.vertices.Add(new Vertex3f(tempPosition));
                    }
                    colourList.Add(new Colour4f(tempColour));

                    if (vertexIndex == header.vertexCount)
                    {
                        header.vertexIndexConversion = new int[header.vertexCount];
                        for (int index = 0; index < header.vertexCount; index++)
                        {
                            header.vertexIndexConversion[index] = index;
                        }

                        header.vertexIndexConversion = weldVertices(ref container.vertices);
                    }
                }
                else if (faceIndex < header.faceCount)
                {
                    if (String.IsNullOrWhiteSpace(temp))
                    {
                        continue;
                    }

                    String[] pieces = temp.Split(' ');

                    if (pieces.Length < 4)
                    {
                        MainMethod.reportError("Not enough information on line: " + lineNumber + " to make a face.");
                        continue;
                    }

                    faceIndex++;

                    if (pieces[0] == "3")
                    {
                        Corner tempCornerOne = new Corner(header.vertexIndexConversion[int.Parse(pieces[1])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[1])]], colourList[int.Parse(pieces[1])], triangleIndex);
                        Corner tempCornerTwo = new Corner(header.vertexIndexConversion[int.Parse(pieces[2])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[2])]], colourList[int.Parse(pieces[2])], triangleIndex);
                        Corner tempCornerThree = new Corner(header.vertexIndexConversion[int.Parse(pieces[3])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[3])]], colourList[int.Parse(pieces[3])], triangleIndex);

                        container.triangles.Add(tempCornerOne);
                        container.triangles.Add(tempCornerTwo);
                        container.triangles.Add(tempCornerThree);
                        triangleIndex++;
                    }
                    else if (pieces[0] == "4")
                    {
                        Corner tempCornerOne = new Corner(header.vertexIndexConversion[int.Parse(pieces[1])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[1])]], colourList[int.Parse(pieces[1])], triangleIndex);
                        Corner tempCornerTwo = new Corner(header.vertexIndexConversion[int.Parse(pieces[2])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[2])]], colourList[int.Parse(pieces[2])], triangleIndex);
                        Corner tempCornerThree = new Corner(header.vertexIndexConversion[int.Parse(pieces[3])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[3])]], colourList[int.Parse(pieces[3])], triangleIndex);
                        triangleIndex++;

                        Corner tempCornerFour = new Corner(header.vertexIndexConversion[int.Parse(pieces[1])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[1])]], colourList[int.Parse(pieces[1])], triangleIndex);
                        Corner tempCornerFive = new Corner(header.vertexIndexConversion[int.Parse(pieces[3])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[3])]], colourList[int.Parse(pieces[3])], triangleIndex);
                        Corner tempCornerSix = new Corner(header.vertexIndexConversion[int.Parse(pieces[4])], container.vertices[header.vertexIndexConversion[int.Parse(pieces[4])]], colourList[int.Parse(pieces[4])], triangleIndex);
                        //Corner tempCornerFour = new Corner(int.Parse(pieces[1]), container.vertices[int.Parse(pieces[1])], colourList[int.Parse(pieces[1])], triangleIndex);
                        //Corner tempCornerFive = new Corner(int.Parse(pieces[3]), container.vertices[int.Parse(pieces[3])], colourList[int.Parse(pieces[3])], triangleIndex);
                        //Corner tempCornerSix = new Corner(int.Parse(pieces[4]), container.vertices[int.Parse(pieces[4])], colourList[int.Parse(pieces[4])], triangleIndex);
                        triangleIndex++;

                        container.triangles.Add(tempCornerOne);
                        container.triangles.Add(tempCornerTwo);
                        container.triangles.Add(tempCornerThree);
                        
                        container.triangles.Add(tempCornerFour);
                        container.triangles.Add(tempCornerFive);
                        container.triangles.Add(tempCornerSix);
                    }
                    else
                    {
                        MainMethod.reportError("Error program does not support meshes that are not quads or triangles");
                    }
                }
                else
                {
                    //We dont' care about lines

                }
            }

            reader.Close();
        }

        private static string getNewLine(StreamReader reader, ref int lineNumber)
        {
            string temp = reader.ReadLine();
            temp = temp.Trim();
            lineNumber++;
            return temp;
        }

        private static int parseHeader(StreamReader reader, PLYFileHeader header)
        {
            int lineNumber = 0;

            string temp = getNewLine(reader, ref lineNumber);

            if (!temp.Equals("ply"))
            {
                MainMethod.reportError("Warning file does match ply specifiction.");
                return -1;
            }

            while (!temp.Equals("end_header") && reader.Peek() > -1)
            {
                temp = getNewLine(reader, ref lineNumber);

                if (String.IsNullOrWhiteSpace(temp))
                {
                    continue;
                }

                string[] pieces = temp.Split(' ');

                switch (pieces[0])
                {
                    case "element":
                        if (pieces.Length == 3)
                        {
                            switch (pieces[1])
                            {
                                case "vertex":
                                    header.vertexCount = Convert.ToInt32(pieces[2]);
                                    break;

                                case "face":
                                    header.faceCount = Convert.ToInt32(pieces[2]);
                                    break;
                            }
                        }
                        break;

                    case "property":

                        if(pieces.Length != 3)
                        {
                            if (pieces.Length == 5 || pieces[1] == "list")
                            {

                            }
                            else
                            {
                                //Bad Line
                                MainMethod.reportError("Error, PLY property line " + lineNumber + " isn't complete.");
                                return -1;
                            }
                        }

                        switch (pieces[2])
                        {
                            case "x":
                                header.xIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "y":
                                header.yIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "z":
                                header.zIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "nx":
                                header.normalXIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "ny":
                                header.normalYIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "nz":
                                header.normalZIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "red":
                                header.redIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "green":
                                header.greenIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "blue":
                                header.blueIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;

                            case "alpha":
                                header.alphaIndex = header.vertexPropertyCount;
                                header.vertexPropertyCount++;
                                break;
                        }
                        break;
                }          
            }

            if (reader.Peek() <= -1)
            {
                MainMethod.reportError("Warning ply file header does not end.");
                return -1;
            }

            return lineNumber;
        }

        private static int[] weldVertices(ref List<Vertex3f> vertices)
        {
            int[] converter = new int[vertices.Count];
            List<Vertex3f> weldedList = new List<Vertex3f>();

            for (int index = 0; index < vertices.Count; index++)
            {
                if (weldedList.Contains(vertices[index]))
                {
                    Log.writeDebug("Two Vertices that are equal have the following coords: \n" + vertices[index] + "\n" + vertices[findFirstIndex(weldedList, vertices[index])]);
                    converter[index] = findFirstIndex(weldedList, vertices[index]);
                    
                }
                else
                {
                    converter[index] = weldedList.Count;
                    weldedList.Add(new Vertex3f(vertices[index]));
                }
            }

            vertices = weldedList;
            Console.WriteLine(weldedList.Count);

            for (int index = 0; index < vertices.Count; index++)
            {
                Log.writeDebug(vertices[index].ToString() + "\t");
                Log.writeDebug(weldedList[converter[index]] + "\n");
            }

            return converter;
        }

        private static int findFirstIndex(List<Vertex3f> list, Vertex3f vertex)
        {
            for (int index = 0; index < list.Count;)
            {
                if (list[index] == vertex)
                {
                    //Log.writeDebug("Two Vertices that are equal have the following coords: \n" + list[index] + "\n" + vertex);
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}
