//Windows Auto Imports
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//My Imports

//3rd Party Imports
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GraphicsFinalProject
{
    public class RenderWindow : GameWindow
    {
        //Mesh Stuff
        private List<CornerTableMesh> models;

        //Camrea Stuff
        public Camera camera;
        
        //Mouse Stuff
        bool leftClickDown;

        int oldMouseX = 0;
        int oldMouseY = 0;

        public RenderWindow() : base(Config.convertSettingToInt("window", "width"), Config.convertSettingToInt("window", "height"), OpenTK.Graphics.GraphicsMode.Default, Config.getValue("window", "title"))
        {
            this.VSync = VSyncMode.On;

            //GL.ClearDepth(1.0);
            
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);
            GL.ClearColor(Config.convertSettingToFloat("colours", "bg_red"), Config.convertSettingToFloat("colours", "bg_green"), Config.convertSettingToFloat("colours", "bg_red"), Config.convertSettingToFloat("colours", "bg_alpha"));

            GL.LightModel(LightModelParameter.LightModelAmbient, Config.convertSettingToFloat("lights", "ambient_light_level"));
            //GL.ShadeModel(ShadingModel.Smooth);
            GL.Enable(EnableCap.ColorMaterial);
            //GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            GL.Viewport(0, 0, Width, Height);

            camera = new Camera();

            this.Keyboard.KeyDown += handleKeyboardDown;
            this.Keyboard.KeyUp += handleKeyboardUp;

            this.Mouse.ButtonDown += handleMouseButtonDown;
            this.Mouse.ButtonUp += handleMouseButtonUp;
            this.Mouse.Move += handleMouseMove;

            this.models = new List<CornerTableMesh>();
        }

        public void handleMouseButtonUp(object sender, MouseButtonEventArgs mbea)
        {
            if (mbea.Button == MouseButton.Left)
            {
                leftClickDown = false;
            }
        }

        public void handleMouseButtonDown(object sender, MouseButtonEventArgs mbea)
        {
            if (mbea.Button == MouseButton.Left)
            {
                leftClickDown = true;

                oldMouseX = Mouse.X;
                oldMouseY = Mouse.Y;
            }
        }

        public void handleMouseMove(object sender, MouseMoveEventArgs mmea)
        {
            if (leftClickDown)
            {
                camera.rotate(Mouse.X - oldMouseX, Mouse.Y - oldMouseY, 0.0f);
            }
            else
            {
                return;
            }

            oldMouseX = Mouse.X;
            oldMouseY = Mouse.Y;
        }

        public void handleKeyboardDown(object sender, KeyboardKeyEventArgs kkea)
        {
            switch(kkea.Key)
            {
                case Key.Escape:
                    
                    Core.uninit();
                    break;

                case Key.A:
                case Key.Left:

                    break;

                case Key.W:
                case Key.Up:

                    break;

                case Key.S:
                case Key.Down:

                    break;

                case Key.D:
                case Key.Right:

                    break;


                // Select Next Corner
                case Key.N:

                    models[0].selectedCorner = models[0].selectedCorner.next;

                    break;

                // Select Previous Corner
                case Key.P:

                    models[0].selectedCorner = models[0].selectedCorner.prev;

                    break;

                // Select Opposite Corner
                case Key.O:

                    models[0].selectedCorner = models[0].selectedCorner.opposite;

                    break;

                // Select Right Corner
                case Key.R:

                    models[0].selectedCorner = models[0].selectedCorner.right;

                    break;

                // Select Left Corner
                case Key.L:

                    models[0].selectedCorner = models[0].selectedCorner.left;

                    break;

            }
        }

        public void handleKeyboardUp(object sender, KeyboardKeyEventArgs kkea)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
        
        }

        public void update()
        {

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            update();

            //GL.LoadIdentity();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 matrixStack = Matrix4.LookAt(camera.position, camera.focusPoint, camera.upDirection);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref matrixStack);

            camera.applyRotation();

            GL.Begin(BeginMode.Triangles);
                GL.Color3(1.0f, 0.0f, 0.0f);
                GL.Vertex3(-3.0f, 0.0f, 0.0f);

                GL.Color3(0.0f, 1.0f, 0.0f);
                GL.Vertex3(0.0f, 3.0f, 0.0f);

                GL.Color3(0.0f, 0.0f, 1.0f);
                GL.Vertex3(3.0f, 0.0f, 0.0f);
            GL.End();

            for (int index = 0; index < models.Count; index++)
            {
                models[index].draw();
            }
 
            this.SwapBuffers();
        }

        public int getMouseX()
        {
            return this.Mouse.X;
        }

        public int getMouseY()
        {
            return this.Mouse.Y;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        public void addModel(CornerTableMesh model)
        {
            this.models.Add(model);
        }
    }
}
