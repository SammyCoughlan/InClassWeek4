using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Tao.FreeGlut;

namespace InClassWeek4
{
    internal class Program
    {
        private static int width = 1200, height = 720;
        private static ShaderProgram shaderProg;
        private static VBO<Vector3> triangle, square;
        private static VBO<int> triangleElement, squareElement;
        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("GAP - GLUT OpenGL Demo");
            Glut.glutIdleFunc(OnRenderFrame);
            SetUpShader();
            Glut.glutLeaveMainLoop();

            Console.ReadKey();
        }

        public static void DrawTriangle()
        {
            Vector3 pt1 = new Vector3(0, 1, 0);
            Vector3 pt2 = new Vector3(-1, -1, 0);
            Vector3 pt3 = new Vector3(1, -1, 0);

            triangle = new VBO<Vector3>(new Vector3[] {pt1, pt2, pt3 });
            triangleElement = new VBO<int>(new int[] { 0, 1, 2 }, BufferTarget.ElementArrayBuffer);
        }
        private static void SetUpShader()
        {
            shaderProg = new ShaderProgram(VertexShader, FragmentShader);
            shaderProg.Use();

            shaderProg["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, .1f, 1000f));
            shaderProg["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.UnitX));
        }
        private static void OnRenderFrame()
        {
            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            shaderProg.Use();
            shaderProg["model_matrix"].SetValue(Matrix4.CreateTranslation(new Vector3(-1.5f, 0, 0)));
            uint vertexPosIndex = (uint)Gl.GetAttribLocation(shaderProg.ProgramID, "vertexPosition");
            Gl.EnableVertexAttribArray(vertexPosIndex);
            Gl.BindBuffer(triangle);
            Gl.VertexAttribPointer(vertexPosIndex, triangle.Size, triangle.PointerType, true, 12, IntPtr.Zero);
            Gl.BindBuffer(triangleElement);
            Gl.DrawElements(BeginMode.Triangles, triangleElement.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Glut.glutSwapBuffers();
        }

        public static string VertexShader = @"
uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;
void main(void)
{
    gl_position = projection_matrix * view_matrix * model_matrix * vec4(vertixPosition, 1);
}
";

        public static string FragmentShader = @"
void main(void)
{
gl_FragColor = vec4(1,1,1,1);
}
";

    }
}
