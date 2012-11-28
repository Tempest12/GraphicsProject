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

        public int normalXIndex;
        public int normalYIndex;
        public int normalZIndex;

        public int redIndex;
        public int greenIndex;
        public int blueIndex;
        public int alphaIndex;

        public int[] vertexIndexConversion;

        public PLYFileHeader()
        {
            this.xIndex = -1;
            this.yIndex = -1;
            this.zIndex = -1;

            this.normalXIndex = -1;
            this.normalYIndex = -1;
            this.normalZIndex = -1;

            this.redIndex = -1;
            this.greenIndex = -1;
            this.blueIndex = -1;
            this.alphaIndex = -1;
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
