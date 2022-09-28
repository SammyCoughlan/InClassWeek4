using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.FreeGlut;

namespace InClassWeek4
{
    internal class Program
    {
        private static int width = 1200, height = 720;
        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("GAP - GLUT OpenGL Demo");
            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutLeaveMainLoop();

            Console.ReadKey();
        }

        private static void OnRenderFrame()
        {
            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Glut.glutSwapBuffers();
        }
    }
}
