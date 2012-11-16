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
            }

            PLYFileHeader header = new PLYFileHeader();
            lineNumber = parseHeader(reader, header);

            if (lineNumber < 0)
            {
                return;
            }

            OpenTK.Vector3 tempPosition = new OpenTK.Vector3(0.0f, 0.0f, 0.0f);
            Colour4f tempColour4f = new Colour4f(0.0f, 0.0f, 0.0f, 0.0f);

            while (reader.Peek() > -1)
            {
                temp = getNewLine(reader, ref lineNumber);
                //First Parse Vertices
                if (header.vertexCount > 0)
                {
                    if (String.IsNullOrWhiteSpace(temp))
                    {
                        continue;
                    }

                    tempPosition.X = 0.0f;
                    tempPosition.Y = 0.0f;
                    tempPosition.Z = 0.0f;

                    tempColour4f.red = Config.convertSettingToFloat("colours", "corner_default_red");
                    tempColour4f.green = Config.convertSettingToFloat("colours", "corner_default_green");
                    tempColour4f.blue = Config.convertSettingToFloat("colours", "corner_default_blue");
                    tempColour4f.alpha = Config.convertSettingToFloat("colours", "corner_default_alpha");

                    string[] pieces = temp.Split(' ');

                    if (header.xIndex >= 0)
                    {
                        //tempPosition.X = float.p  (pieces[header.xIndex]);   
                    }
                    if (header.yIndex >= 0)
                    {

                    }
                    if (header.zIndex >= 0)
                    {

                    }
                    if (header.redIndex >= 0)
                    {

                    }
                    if (header.greenIndex >= 0)
                    {

                    }
                    if (header.blueIndex >= 0)
                    {

                    }
                    if (header.alphaIndex >= 0)
                    {

                    }
                }
                else if (header.faceCount > 0)
                {

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

            if (reader.Peek() > -1)
            {
                MainMethod.reportError("Warning ply file header does not end.");
                return -1;
            }

            return lineNumber;
        }
    }
}
