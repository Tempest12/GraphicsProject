//Windows Auto Imports 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//My Imports
using System.Windows.Forms;

//3rd Party Imports
//using OpenTK;

//using GLU = OpenTK.Graphics.Glu;
using OpenTK.Graphics.OpenGL;
using OpenTK.Math;
using OpenTK.Input;

namespace GraphicsFinalProject
{
    /**
     * The Core of the application.
     */
    public class Core
    {
        public static RenderWindow window;
 
        public static Random numberGenerator;

        /**
         * Initializes the Core of the program by initliazling everything. Failures here cause program termination
         */
        public static void init()
        {
            Config.init();

            Log.init(Config.convertSettingToInt("log", "default_level"));

            initGL();
        }

        /**
         * Initializes anything we need to Render
         */
        private static void initGL()
        {
            window = new RenderWindow();

            numberGenerator = new Random();
        }

        public static void restart()
        {
            
        }

        public static void startGameLoop()
        {
            //Test
            CornerTableMesh model = new CornerTableMesh(Config.getValue("model", "model_filename"));
            window.addModel(model);

            window.Run(60);
        }

        /**
         * Uninitializes my OpenGL Stuff
         */
        private static void uninitGL()
        {

        }

        /**
         * Uninitialies the Core of the program
         */
        public static void uninit()
        {
            Config.uninit();

            Log.uninit();

            uninitGL();

            Console.WriteLine("Exiting");

            Environment.Exit(0);
        }

        public static float degreeToRadian(float degree)
        {
            return degree * (180.0f / (float)Math.PI);
        }

        public static float radianToDegree(float radian)
        {
            return radian * ((float)Math.PI / 180.0f);
        }
    }
}
