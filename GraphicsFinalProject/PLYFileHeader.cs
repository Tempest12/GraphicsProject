using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphicsFinalProject
{
    public class PLYFileHeader
    {
        public int vertexPropertyCount;
        public int facePropertyCount;

        public VERTEXCOLOURFORMAT vertexColourFormat;

        public int vertexCount;
        public int faceCount;

        public int xIndex;
        public int yIndex;
        public int zIndex;

        public int redIndex;
        public int greenIndex;
        public int blueIndex;
        public int alphaIndex;

        public PLYFileHeader()
        {
            xIndex = -1;
            yIndex = -1;
            zIndex = -1;

            redIndex = -1;
            greenIndex = -1;
            blueIndex = -1;
            alphaIndex = -1;
        }
    }



    public enum VERTEXCOLOURFORMAT
    {
        CHAR,
        UCHAR,
        SHORT,
        USHORT,
        INT,
        UINT,
        FLOAT,
        DOUBLE,
    }
}
